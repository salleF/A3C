using UnityEngine;

namespace A3C.Combat
{
    public enum FireMode
    {
        SemiAuto,
        FullAuto,
        Burst,
        ShotgunPellets
    }

    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "A3C/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("Identidade da Arma")]
        public string weaponName = "Rifle Arcano";
        public FireMode fireMode = FireMode.FullAuto;

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

        public float TimeBetweenShots => 60f / fireRateRPM;
    }
}
