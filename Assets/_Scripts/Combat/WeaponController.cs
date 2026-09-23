using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace A3C.Combat
{
    public class WeaponController : MonoBehaviour
    {
        public WeaponData currentWeapon;
        public Transform firePoint;
        public Camera playerCamera;
        public int currentAmmo;
        public int currentReserve;
        public bool isReloading;
        public bool acceptPlayerInput = true;
        public bool interactionBlocked;
        public WeaponState State { get; private set; }
        public float Charge { get; private set; }
        public bool Scoped { get; private set; }
        public event Action<WeaponData> OnShot;
        private readonly Dictionary<WeaponData, WeaponState> inventory = new Dictionary<WeaponData, WeaponState>();
        private HealthSystem health;
        private CharacterController movement;
        private ArcaneSystem arcane;
        private float nextShot;
        private float spread;
        private float normalFov = 70f;
        private Coroutine action;

        void Awake()
        {
            health = GetComponentInParent<HealthSystem>();
            movement = GetComponentInParent<CharacterController>();
            arcane = GetComponent<ArcaneSystem>();
            if (playerCamera == null) playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera != null) normalFov = playerCamera.fieldOfView;
            if (currentWeapon != null) EquipWeapon(currentWeapon);
        }

        public void EquipWeapon(WeaponData weapon)
        {
            if (weapon == null || weapon.mechanic == WeaponMechanic.Punishment) return;
            CancelAction();
            if (!inventory.TryGetValue(weapon, out var state))
            {
                state = new WeaponState(weapon);
                inventory.Add(weapon, state);
            }
            State = state;
            spread = weapon.baseSpread;
            SyncState();
        }

        public void ResetForRound()
        {
            CancelAction();
            foreach (var state in inventory.Values) state.Reset();
            nextShot = 0f;
            SyncState();
        }

        public void CancelAction()
        {
            if (action != null) StopCoroutine(action);
            action = null;
            isReloading = false;
            Charge = 0f;
            Scoped = false;
            if (playerCamera != null) playerCamera.fieldOfView = normalFov;
            if (arcane == null) arcane = GetComponent<ArcaneSystem>();
            if (arcane != null) arcane.Cancel();
        }

        void OnDisable() { CancelAction(); }
        void Update()
        {
            if (State == null) return;
            if (health != null && !health.IsAlive) { CancelAction(); return; }
            if (Time.time > nextShot) spread = Mathf.MoveTowards(spread, currentWeapon.baseSpread, currentWeapon.spreadRecoverySpeed * Time.deltaTime);
            if (!acceptPlayerInput) return;
            if (Cursor.lockState != CursorLockMode.Locked) { CancelAction(); return; }
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame) Reload();
            var mouse = Mouse.current;
            if (mouse == null || isReloading) return;
            Scoped = currentWeapon.category == WeaponCategory.Sniper && mouse.rightButton.isPressed;
            if (playerCamera != null) playerCamera.fieldOfView = Scoped ? 30f : normalFov;
            if (currentWeapon.mechanic == WeaponMechanic.Choke)
            {
                if (mouse.leftButton.isPressed) Charge = Mathf.Min(currentWeapon.chokeChargeTime, Charge + Time.deltaTime);
                if (mouse.leftButton.wasReleasedThisFrame) { TryFire(false, Charge); Charge = 0f; }
                return;
            }
            if (currentWeapon.mechanic == WeaponMechanic.SpinUp)
            {
                AdvanceSpinUp(mouse.leftButton.isPressed, Time.deltaTime);
                if (mouse.leftButton.isPressed) TryFire();
                return;
            }
            bool primary = currentWeapon.fireMode == FireMode.FullAuto ? mouse.leftButton.isPressed : mouse.leftButton.wasPressedThisFrame;
            if (primary) TryFire();
            else if (mouse.rightButton.wasPressedThisFrame && currentWeapon.magazinePerHand > 0) TryFire(true);
        }

        public bool TryFire(bool secondary = false, float chokeSeconds = 0f)
        {
            if (State == null || playerCamera == null || interactionBlocked || isReloading || action != null || Time.time < nextShot ||
                (health != null && !health.IsAlive)) return false;
            var weapon = State.ActiveWeapon;
            if (weapon.mechanic == WeaponMechanic.SpinUp && Charge < weapon.spinUpTime) return false;
            int rounds = State.Consume(secondary);
            if (rounds == 0) return false;
            var cast = arcane != null ? arcane.ConsumeForShot(weapon, playerCamera.transform.position, playerCamera.transform.forward) : null;
            nextShot = Time.time + weapon.TimeBetweenShots;
            if (weapon.fireMode == FireMode.Melee) Melee(weapon);
            else FireProjectiles(weapon, rounds, chokeSeconds, cast);
            SyncState();
            if (weapon.fireMode == FireMode.Burst && weapon.burstCount > 1)
            {
                nextShot += weapon.burstInterval * (weapon.burstCount - 1);
                action = StartCoroutine(Burst(weapon, cast));
            }
            return true;
        }

        public void AdvanceSpinUp(bool held, float seconds)
        {
            if (currentWeapon == null || currentWeapon.mechanic != WeaponMechanic.SpinUp) return;
            Charge = held ? Mathf.Min(currentWeapon.spinUpTime, Charge + Mathf.Max(0, seconds)) : 0f;
        }

        IEnumerator Burst(WeaponData weapon, ArcaneCast cast)
        {
            for (int i = 1; i < weapon.burstCount; i++)
            {
                yield return new WaitForSeconds(weapon.burstInterval);
                if ((health != null && !health.IsAlive) || State.Data != weapon || State.Consume(false) == 0) break;
                FireProjectiles(weapon, 1, 0f, cast);
                SyncState();
            }
            action = null;
        }

        void FireProjectiles(WeaponData weapon, int rounds, float chokeSeconds, ArcaneCast cast)
        {
            float shotSpread = spread;
            if (rounds > 1) shotSpread *= 1.35f;
            if (movement != null && (movement.velocity.sqrMagnitude > 0.1f || !movement.isGrounded)) shotSpread *= weapon.movementSpreadMultiplier;
            var status = health != null ? health.GetComponent<CombatStatus>() : null;
            if (status != null) shotSpread *= status.SpreadMultiplier;
            if (weapon.mechanic == WeaponMechanic.Choke)
                shotSpread *= Mathf.Lerp(1f, weapon.chargedSpreadMultiplier, Mathf.Clamp01(chokeSeconds / Mathf.Max(0.01f, weapon.chokeChargeTime)));
            for (int i = 0; i < Mathf.Max(1, weapon.pelletsPerShot) * rounds; i++)
            {
                Vector2 offset = UnityEngine.Random.insideUnitCircle * shotSpread;
                Transform aim = playerCamera.transform;
                Vector3 direction = (aim.forward + aim.right * offset.x + aim.up * offset.y).normalized;
                GameObject obj = weapon.projectilePrefab != null ? Instantiate(weapon.projectilePrefab) :
                    RuntimeVisuals.Primitive("Arcane projectile", PrimitiveType.Sphere, aim.position, Vector3.one * 0.08f, weapon.muzzleFlashColor, false);
                obj.transform.SetPositionAndRotation(aim.position, Quaternion.LookRotation(direction));
                obj.transform.SetParent(ArcaneWorld.Root);
                foreach (var col in obj.GetComponentsInChildren<Collider>()) col.enabled = false;
                foreach (var renderer in obj.GetComponentsInChildren<Renderer>()) renderer.enabled = weapon.visibleTracer;
                var projectile = obj.GetComponent<Projectile>() ?? obj.AddComponent<Projectile>();
                projectile.Initialize(weapon.bodyDamage, weapon.headshotDamage, weapon.projectileSpeed, health, weapon.effectiveRange, cast, weapon.barrierDamageMultiplier);
            }
            spread = Mathf.Min(weapon.maxSpread, spread + weapon.spreadIncreasePerShot);
            OnShot?.Invoke(weapon);
        }

        void Melee(WeaponData weapon)
        {
            Vector3 origin = playerCamera.transform.position;
            var targets = Physics.OverlapSphere(origin, weapon.meleeRange, ~0, QueryTriggerInteraction.Collide);
            Array.Sort(targets, (a, b) => (a.ClosestPoint(origin) - origin).sqrMagnitude.CompareTo((b.ClosestPoint(origin) - origin).sqrMagnitude));
            var damaged = new HashSet<UnityEngine.Object>();
            foreach (var target in targets)
            {
                var targetHealth = target.GetComponentInParent<HealthSystem>();
                var barrier = target.GetComponentInParent<ArcaneBarrier>();
                UnityEngine.Object identity = targetHealth != null ? (UnityEngine.Object)targetHealth : barrier;
                if (identity == null || targetHealth == health || damaged.Contains(identity)) continue;
                Vector3 delta = target.ClosestPoint(origin) - origin;
                if (Vector3.Angle(playerCamera.transform.forward, delta) > weapon.meleeArcDegrees * 0.5f) continue;
                if (!CombatPhysics.FirstHit(origin, delta.normalized, delta.magnitude + 0.1f, health, out var hit)) continue;
                if (hit.collider.GetComponentInParent<HealthSystem>() != targetHealth || hit.collider.GetComponentInParent<ArcaneBarrier>() != barrier) continue;
                if (!CombatPhysics.Damage(hit, weapon.bodyDamage, weapon.headshotDamage, health, weapon.barrierDamageMultiplier)) continue;
                damaged.Add(identity);
                if (weapon.mechanic != WeaponMechanic.Sweep) break;
            }
            OnShot?.Invoke(weapon);
        }

        public void Reload()
        {
            if (State == null || isReloading || (health != null && !health.IsAlive)) return;
            if (!State.CanReload) return;
            CancelAction();
            State.BeginReload();
            isReloading = true;
            action = StartCoroutine(ReloadRoutine(State));
        }

        IEnumerator ReloadRoutine(WeaponState state)
        {
            yield return new WaitForSeconds(state.Data.reloadTime);
            if (State == state && (health == null || health.IsAlive)) state.FinishReload();
            isReloading = false;
            action = null;
            SyncState();
        }

        void SyncState()
        {
            if (State == null) return;
            currentWeapon = State.ActiveWeapon;
            currentAmmo = State.PunishmentReady ? 1 : State.Ammo;
            currentReserve = State.Reserve;
        }
        public float GetCurrentSpread() => spread;
    }
}
