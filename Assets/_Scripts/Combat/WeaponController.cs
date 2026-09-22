using UnityEngine;
using UnityEngine.InputSystem;
using A3C.Player;

namespace A3C.Combat
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Arma Equipada")]
        public WeaponData currentWeapon;
        public Transform firePoint;
        public Camera playerCamera;

        [Header("Estado de Municao")]
        public int currentAmmo;
        public int currentReserve;
        public bool isReloading = false;

        [Header("Debug da Dispersao Atual")]
        [SerializeField] private float currentSpread = 0f;

        private float nextTimeToFire = 0f;
        private HealthSystem playerHealth;
        private CharacterController characterController;

        void Awake()
        {
            playerHealth = GetComponentInParent<HealthSystem>();
            characterController = GetComponentInParent<CharacterController>();
            if (playerCamera == null) playerCamera = Camera.main;

            if (currentWeapon != null)
            {
                EquipWeapon(currentWeapon);
            }
        }

        public void EquipWeapon(WeaponData newWeapon)
        {
            currentWeapon = newWeapon;
            currentAmmo = currentWeapon.magazineSize;
            currentReserve = currentWeapon.maxReserveAmmo;
            currentSpread = currentWeapon.baseSpread;
            isReloading = false;
        }

        void Update()
        {
            if (currentWeapon == null || Cursor.lockState != CursorLockMode.Locked) return;

            HandleSpreadRecovery();

            // Input de Recarga
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                Reload();
            }

            // Input de Disparo (LMB)
            if (Mouse.current != null)
            {
                bool wantsToFire = false;
                if (currentWeapon.fireMode == FireMode.FullAuto)
                {
                    wantsToFire = Mouse.current.leftButton.isPressed;
                }
                else
                {
                    wantsToFire = Mouse.current.leftButton.wasPressedThisFrame;
                }

                if (wantsToFire && Time.time >= nextTimeToFire)
                {
                    TryFire();
                }
            }
        }

        private void TryFire()
        {
            if (isReloading) return;

            if (currentAmmo <= 0)
            {
                Reload();
                return;
            }

            nextTimeToFire = Time.time + currentWeapon.TimeBetweenShots;
            currentAmmo--;

            // Aumenta dispersao por segurar dedo
            currentSpread = Mathf.Min(currentWeapon.maxSpread, currentSpread + currentWeapon.spreadIncreasePerShot);

            // Dispara projeteis (1 para rifles/pistolas, multiplos para shotguns)
            int pellets = Mathf.Max(1, currentWeapon.pelletsPerShot);
            for (int i = 0; i < pellets; i++)
            {
                SpawnProjectile();
            }
        }

        private void SpawnProjectile()
        {
            Vector3 origin = firePoint != null ? firePoint.position : playerCamera.transform.position;
            Vector3 forward = playerCamera.transform.forward;

            // Calcula penalidade de movimento (correndo ou no ar)
            float totalSpread = currentSpread;
            bool isMoving = characterController != null && characterController.velocity.sqrMagnitude > 0.1f;
            bool isAirborne = characterController != null && !characterController.isGrounded;

            if (isAirborne || isMoving)
            {
                totalSpread *= currentWeapon.movementSpreadMultiplier;
            }

            // Dispersao aleatoria c?nica
            Vector2 randomCircle = Random.insideUnitCircle * totalSpread;
            Vector3 spreadDir = forward + playerCamera.transform.right * randomCircle.x + playerCamera.transform.up * randomCircle.y;
            spreadDir.Normalize();

            // Instancia o projetil
            GameObject prefab = currentWeapon.projectilePrefab;
            GameObject projObj;
            if (prefab != null)
            {
                projObj = Instantiate(prefab, origin, Quaternion.LookRotation(spreadDir));
            }
            else
            {
                // Fallback procedural: cria uma esfera basica caso nao tenha prefab assinalado
                projObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                projObj.transform.position = origin;
                projObj.transform.rotation = Quaternion.LookRotation(spreadDir);
                projObj.transform.localScale = Vector3.one * 0.12f;
                var col = projObj.GetComponent<Collider>();
                if (col) col.isTrigger = true;
            }

            var projScript = projObj.GetComponent<Projectile>();
            if (projScript == null) projScript = projObj.AddComponent<Projectile>();

            projScript.Initialize(
                currentWeapon.bodyDamage,
                currentWeapon.headshotDamage,
                currentWeapon.projectileSpeed,
                playerHealth
            );
        }

        private void HandleSpreadRecovery()
        {
            // Recupera a precisao gradualmente se nao estiver atirando
            if (Time.time > nextTimeToFire + 0.05f)
            {
                currentSpread = Mathf.MoveTowards(currentSpread, currentWeapon.baseSpread, currentWeapon.spreadRecoverySpeed * Time.deltaTime);
            }
        }

        public void Reload()
        {
            if (isReloading || currentAmmo >= currentWeapon.magazineSize || currentReserve <= 0) return;

            isReloading = true;
            Invoke(nameof(FinishReload), currentWeapon.reloadTime);
        }

        private void FinishReload()
        {
            int needed = currentWeapon.magazineSize - currentAmmo;
            int toAdd = Mathf.Min(needed, currentReserve);
            currentReserve -= toAdd;
            currentAmmo += toAdd;
            isReloading = false;
        }

        public float GetCurrentSpread() => currentSpread;
    }
}
