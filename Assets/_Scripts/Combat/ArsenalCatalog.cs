using System;
using UnityEngine;

namespace A3C.Combat
{
    [CreateAssetMenu(fileName = "ArsenalCatalog", menuName = "A3C/Arsenal Catalog")]
    public class ArsenalCatalog : ScriptableObject
    {
        public const int MaxCartridgeSlots = 3;
        public WeaponData defenderStarter;
        public WeaponData attackerStarter;
        public WeaponData[] weapons = Array.Empty<WeaponData>();
        public ArcaneCartridgeData[] cartridges = Array.Empty<ArcaneCartridgeData>();

        public ArcaneCartridgeData FindCartridge(WeaponData weapon, ArcaneElement element)
        {
            if (weapon == null || !weapon.SupportsArcaneCartridges) return null;
            foreach (var cartridge in cartridges)
            {
                if (cartridge != null && cartridge.element == element && cartridge.IsCompatibleWith(weapon))
                    return cartridge;
            }
            return null;
        }
    }
}
