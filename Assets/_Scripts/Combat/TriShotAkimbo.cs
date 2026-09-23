using UnityEngine;

namespace A3C.Combat
{
    [RequireComponent(typeof(WeaponController))]
    public class TriShotAkimbo : MonoBehaviour
    {
        public int ammoLeft = 2, ammoRight = 2, reserveAmmo = 32;
        public float headshotDamage = 24f, bodyDamage = 12f, maxRange = 22f, pelletSpread = 0.07f;
        public Camera playerCamera;
        public LayerMask hitMask = ~0;
        private WeaponController controller;
        void Awake() => controller = GetComponent<WeaponController>();
        void LateUpdate()
        {
            if (controller.State == null) return;
            ammoLeft = controller.State.Left;
            ammoRight = controller.State.Right;
            reserveAmmo = controller.State.Reserve;
        }
        public void ShootSingle() => controller.TryFire();
        public void ShootDual() => controller.TryFire(true);
        public void Reload() => controller.Reload();
    }
}
