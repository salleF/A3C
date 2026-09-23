using UnityEngine;

namespace A3C.Combat
{
    public sealed class WeaponState
    {
        public WeaponData Data { get; }
        public int Left { get; private set; }
        public int Right { get; private set; }
        public int Reserve { get; private set; }
        public int ShotsInSequence { get; private set; }
        public bool PunishmentReady { get; private set; }
        public int Ammo => Left + Right;
        public WeaponData ActiveWeapon => PunishmentReady && Data.specialWeapon != null ? Data.specialWeapon : Data;
        private bool nextLeft = true;
        private bool eligible = true;

        public WeaponState(WeaponData data) { Data = data; Reset(); }
        public void Reset()
        {
            Left = Data.magazinePerHand > 0 ? Data.magazinePerHand : Data.magazineSize;
            Right = Data.magazinePerHand;
            Reserve = Data.maxReserveAmmo;
            ShotsInSequence = 0;
            PunishmentReady = false;
            eligible = true;
            nextLeft = true;
        }
        public int Consume(bool secondary)
        {
            if (PunishmentReady)
            {
                PunishmentReady = false;
                Left = Right = Data.magazinePerHand;
                ShotsInSequence = 0;
                eligible = true;
                return 1;
            }
            if (Data.fireMode == FireMode.Melee) return 1;
            if (Ammo <= 0) return 0;
            int count = 0;
            if (Data.mechanic == WeaponMechanic.IndependentDual)
            {
                if (secondary && Right > 0) { Right--; count = 1; }
                if (!secondary && Left > 0) { Left--; count = 1; }
            }
            else if (Data.magazinePerHand > 0)
            {
                if (secondary && Left > 0 && Right > 0) { Left--; Right--; count = 2; }
                else
                {
                    bool left = (nextLeft && Left > 0) || Right == 0;
                    if (left) Left--; else Right--;
                    nextLeft = !left;
                    count = 1;
                }
            }
            else { Left--; count = 1; }
            ShotsInSequence += count;
            if (eligible && Data.specialWeapon != null && Data.shotsToUnlockSpecial > 0 &&
                ShotsInSequence == Data.shotsToUnlockSpecial && Ammo == 0) PunishmentReady = true;
            return count;
        }
        public bool CanReload => !PunishmentReady && Data.fireMode != FireMode.Melee && Reserve > 0 && Ammo < Data.magazineSize;
        public bool BeginReload()
        {
            if (!CanReload) return false;
            eligible = false;
            ShotsInSequence = 0;
            return true;
        }
        public void FinishReload()
        {
            if (!CanReload) return;
            if (Data.mechanic == WeaponMechanic.IndependentDual)
            {
                bool left = Left <= Right;
                int amount = Mathf.Min(Reserve, Data.magazinePerHand - (left ? Left : Right));
                if (left) Left += amount; else Right += amount;
                Reserve -= amount;
            }
            else if (Data.magazinePerHand > 0)
            {
                while (Reserve > 0 && Ammo < Data.magazineSize)
                {
                    if (Left <= Right && Left < Data.magazinePerHand) Left++; else Right++;
                    Reserve--;
                }
            }
            else
            {
                int amount = Mathf.Min(Reserve, Data.magazineSize - Left);
                Left += amount;
                Reserve -= amount;
            }
            ShotsInSequence = 0;
            eligible = Ammo == Data.magazineSize;
        }
    }
}
