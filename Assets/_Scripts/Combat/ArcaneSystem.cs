using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace A3C.Combat
{
    public class ArcaneSystem : MonoBehaviour
    {
        [Serializable]
        public class Slot
        {
            public ArcaneCartridgeData data;
            public int charges;
            [NonSerialized] public float readyAt;
        }
        public Slot[] slots = { new Slot(), new Slot(), new Slot() };
        public int armedSlot = -1;
        public bool acceptPlayerInput = true;
        public bool interactionOwnsE;
        public WeaponController weaponController;
        private HealthSystem owner;

        void Awake()
        {
            owner = GetComponentInParent<HealthSystem>();
            if (weaponController == null) weaponController = GetComponent<WeaponController>();
        }
        void Update()
        {
            if (owner == null || !owner.IsAlive) { Cancel(); return; }
            if (!acceptPlayerInput || Cursor.lockState != CursorLockMode.Locked || Keyboard.current == null) return;
            if (Keyboard.current.qKey.wasPressedThisFrame) Arm(0);
            if (Keyboard.current.eKey.wasPressedThisFrame && !interactionOwnsE) Arm(1);
            if (Keyboard.current.cKey.wasPressedThisFrame) Arm(2);
            if (interactionOwnsE && armedSlot == 1) Cancel();
        }
        public bool Equip(int slot, ArcaneCartridgeData data)
        {
            if (slot < 0 || slot >= 3 || data == null || weaponController == null ||
                !data.IsCompatibleWith(weaponController.currentWeapon)) return false;
            slots[slot] = new Slot { data = data, charges = data.chargesPerPurchase };
            Cancel();
            return true;
        }
        public bool Arm(int slot)
        {
            if (slot < 0 || slot >= 3 || owner == null || !owner.IsAlive || weaponController == null || weaponController.isReloading) return false;
            var value = slots[slot];
            if (value.data == null || value.charges <= 0 || Time.time < value.readyAt ||
                !value.data.IsCompatibleWith(weaponController.currentWeapon) || (slot == 1 && interactionOwnsE)) return false;
            armedSlot = armedSlot == slot ? -1 : slot;
            return true;
        }
        public void Cancel() => armedSlot = -1;
        public void ResetForRound()
        {
            Cancel();
            foreach (var slot in slots)
            {
                slot.charges = slot.data != null ? slot.data.chargesPerPurchase : 0;
                slot.readyAt = 0;
            }
        }
        public ArcaneCast ConsumeForShot(WeaponData weapon, Vector3 origin, Vector3 forward)
        {
            if (armedSlot < 0 || owner == null || !owner.IsAlive) return null;
            var slot = slots[armedSlot];
            Cancel();
            if (slot.data == null || slot.charges <= 0 || Time.time < slot.readyAt || !slot.data.IsCompatibleWith(weapon)) return null;
            slot.charges--;
            slot.readyAt = Time.time + slot.data.cooldownSeconds;
            var cast = new ArcaneCast(slot.data, owner, origin, forward);
            cast.ApplyCaster();
            return cast;
        }
    }

    public sealed class ArcaneCast
    {
        public ArcaneCartridgeData Data { get; }
        public HealthSystem Owner { get; }
        public Vector3 Origin { get; }
        public Vector3 Forward { get; }
        public bool ImpactConsumed { get; private set; }
        private readonly int generation;
        public ArcaneCast(ArcaneCartridgeData data, HealthSystem owner, Vector3 origin, Vector3 forward)
        {
            Data = data;
            Owner = owner;
            Origin = origin;
            Forward = forward;
            generation = ArcaneWorld.Generation;
        }
        public void ApplyCaster()
        {
            foreach (var effect in Data.effects)
                if (effect.anchor == ArcaneAnchor.Caster) ArcaneWorld.Apply(effect, Data.effectColor, Origin, Forward, Owner);
        }
        public void Impact(Vector3 point, Vector3 normal)
        {
            if (ImpactConsumed || generation != ArcaneWorld.Generation) return;
            ImpactConsumed = true;
            if (Owner == null || Vector3.Distance(Origin, point) > Data.castRangeMetres) return;
            foreach (var effect in Data.effects)
                if (effect.anchor == ArcaneAnchor.Impact && effect.kind != ArcaneEffectKind.Barrier)
                    ArcaneWorld.Apply(effect, Data.effectColor, point + normal * 0.12f, Forward, Owner);
            foreach (var effect in Data.effects)
                if (effect.anchor == ArcaneAnchor.Impact && effect.kind == ArcaneEffectKind.Barrier)
                    ArcaneWorld.Apply(effect, Data.effectColor, point + normal * 0.6f, Forward, Owner);
        }
    }
}
