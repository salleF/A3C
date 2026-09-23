using UnityEngine;

namespace A3C.Combat
{
    public enum FireMode
    {
        SemiAuto = 0,
        FullAuto = 1,
        Burst = 2,
        ShotgunPellets = 3,
        Melee = 4
    }

    public enum WeaponCategory
    {
        Unspecified = 0,
        Pistol = 1,
        Melee = 2,
        SMG = 3,
        Shotgun = 4,
        Rifle = 5,
        Sniper = 6,
        MachineGun = 7,
        Special = 8
    }

    public enum WeaponMechanic
    {
        Standard = 0,
        AlternatingDual = 1,
        IndependentDual = 2,
        Punishment = 3,
        SprintBonus = 4,
        Sweep = 5,
        BarrierBreaker = 6,
        Choke = 7,
        SpinUp = 8
    }

    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "A3C/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("Identidade da Arma")]
        public string weaponName = "Rifle Arcano";
        public FireMode fireMode = FireMode.FullAuto;

        [Header("Catalogo de Design")]
        public string weaponId;
        public WeaponCategory category = WeaponCategory.Unspecified;
        public WeaponMechanic mechanic = WeaponMechanic.Standard;
        [Min(0)] public int priceCR;
        [TextArea(2, 5)] public string gameplaySummary;
        [TextArea(2, 5)] public string implementationNotes;
        public string designSheetPath;

        [Header("Dano")]
        public float bodyDamage = 40f;
        public float headshotDamage = 160f; // Teto competitivo de rifles

        [Header("Cadencia & Disparo")]
        public float fireRateRPM = 600f; // Tiros por minuto
        public int pelletsPerShot = 1;   // >1 para Shotguns (ex: TriShot = 3)
        public int burstCount = 3;

        [Header("Dispersao (Recoil & Bloom)")]
        public float baseSpread = 0.005f;           // Precisao do primeiro tiro parado
        public float spreadIncreasePerShot = 0.01f; // Abertura ao segurar dedo
        public float maxSpread = 0.08f;             // Dispersao maxima do spray
        public float spreadRecoverySpeed = 0.15f;   // Velocidade de recuperacao da mira
        public float movementSpreadMultiplier = 2.5f; // Penalidade ao correr/pular

        [Header("Municao & Recarga")]
        public int magazineSize = 25;
        public int maxReserveAmmo = 100;
        public float reloadTime = 1.8f;

        [Header("Projetil & Visual")]
        public GameObject projectilePrefab;
        public float projectileSpeed = 120f;
        public Color muzzleFlashColor = new Color(0.2f, 0.8f, 1f);

        [Header("Parametros de Design (requerem suporte no controlador)")]
        [Min(0)] public int magazinePerHand;
        [Min(1)] public int maxSimultaneousShots = 1;
        public bool silenced;
        public bool visibleTracer = true;
        [Min(0f)] public float effectiveRange = 50f;
        [Min(0f)] public float burstInterval = 0.12f;
        [Min(0f)] public float chokeChargeTime;
        [Range(0f, 1f)] public float chargedSpreadMultiplier = 1f;
        [Min(0f)] public float spinUpTime;
        [Min(0f)] public float equippedMoveMultiplier = 1f;
        [Min(0f)] public float sprintMultiplier = 1f;
        [Min(0f)] public float meleeRange;
        [Range(0f, 180f)] public float meleeArcDegrees;
        [Min(1f)] public float barrierDamageMultiplier = 1f;
        public WeaponData specialWeapon;
        [Min(0)] public int shotsToUnlockSpecial;

        public bool SupportsArcaneCartridges =>
            category == WeaponCategory.SMG || category == WeaponCategory.Shotgun ||
            category == WeaponCategory.Rifle || category == WeaponCategory.Sniper ||
            category == WeaponCategory.MachineGun;

        public float TimeBetweenShots => 60f / fireRateRPM;
    }
}
