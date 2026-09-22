using UnityEngine;
using UnityEngine.InputSystem;

namespace A3C.Combat
{
    public class TriShotAkimbo : MonoBehaviour
    {
        [Header("Municao (2x2 Akimbo)")]
        public int ammoLeft = 2;
        public int ammoRight = 2;
        public int reserveAmmo = 32;

        [Header("Dano e Balistica")]
        public float headshotDamage = 160f;
        public float bodyDamage = 40f;
        public float maxRange = 60f;
        public float pelletSpread = 0.045f;

        [Header("Referencias")]
        public Camera playerCamera;
        public LayerMask hitMask = ~0;

        private bool nextSideLeft = true;
        private bool isReloading = false;

        void Update()
        {
            if (Cursor.lockState != CursorLockMode.Locked) return;

            // LMB: Disparo Alternado
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                ShootSingle();
            }
            // RMB: Disparo Duplo
            else if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
            {
                ShootDual();
            }

            // R: Recarregar
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                Reload();
            }
        }

        public void ShootSingle()
        {
            if (isReloading) return;
            if (ammoLeft == 0 && ammoRight == 0)
            {
                Reload();
                return;
            }

            bool shootLeft = nextSideLeft;
            if (shootLeft && ammoLeft == 0) shootLeft = false;
            if (!shootLeft && ammoRight == 0) shootLeft = true;

            if (shootLeft)
            {
                ammoLeft--;
                nextSideLeft = ammoRight > 0 ? false : true;
            }
            else
            {
                ammoRight--;
                nextSideLeft = ammoLeft > 0 ? true : false;
            }

            CastPellets(3);
        }

        public void ShootDual()
        {
            if (isReloading) return;
            if (ammoLeft == 0 || ammoRight == 0)
            {
                ShootSingle();
                return;
            }

            ammoLeft--;
            ammoRight--;
            CastPellets(6);
        }

        private void CastPellets(int count)
        {
            if (playerCamera == null) playerCamera = Camera.main;
            Vector3 origin = playerCamera.transform.position;
            Vector3 forward = playerCamera.transform.forward;

            for (int i = 0; i < count; i++)
            {
                Vector3 spread = new Vector3(
                    Random.Range(-pelletSpread, pelletSpread),
                    Random.Range(-pelletSpread, pelletSpread),
                    0f
                );
                Vector3 rayDir = (forward + playerCamera.transform.TransformDirection(spread)).normalized;

                if (Physics.Raycast(origin, rayDir, out RaycastHit hit, maxRange, hitMask))
                {
                    var targetDummy = hit.collider.GetComponentInParent<TargetDummy>();
                    if (targetDummy != null)
                    {
                        bool isHead = hit.collider.CompareTag("Head");
                        float dmg = isHead ? headshotDamage : bodyDamage;
                        targetDummy.TakeHit(dmg, isHead, GetComponentInParent<HealthSystem>());
                    }
                    else
                    {
                        var health = hit.collider.GetComponentInParent<HealthSystem>();
                        if (health != null)
                        {
                            health.TakeDamage(bodyDamage);
                        }
                    }
                }
            }
        }

        public void Reload()
        {
            if (isReloading || (ammoLeft == 2 && ammoRight == 2) || reserveAmmo <= 0) return;

            isReloading = true;
            Invoke(nameof(FinishReload), 0.5f);
        }

        private void FinishReload()
        {
            int needed = 4 - (ammoLeft + ammoRight);
            int toAdd = Mathf.Min(needed, reserveAmmo);
            reserveAmmo -= toAdd;
            ammoLeft = 2;
            ammoRight = 2;
            nextSideLeft = true;
            isReloading = false;
        }
    }
}
