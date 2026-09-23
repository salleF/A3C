using UnityEngine;

namespace A3C.Combat
{
    public class TrainingActor : MonoBehaviour
    {
        public bool moving;
        public bool firing;
        public HealthSystem target;
        public float respawnDelay = 3f;
        public float restoredHealth = 100f;
        private HealthSystem health;
        private WeaponController weapon;
        private Renderer[] visuals;
        private Collider[] hitboxes;
        private Vector3 home;
        private float respawnAt;
        private bool hidden;

        void Start()
        {
            health = GetComponent<HealthSystem>();
            weapon = GetComponent<WeaponController>();
            visuals = GetComponentsInChildren<Renderer>();
            hitboxes = GetComponentsInChildren<Collider>();
            home = transform.position;
        }
        void Update()
        {
            if (!health.IsAlive)
            {
                if (!hidden)
                {
                    SetVisible(false);
                    hidden = true;
                    respawnAt = Time.time + respawnDelay;
                    weapon?.CancelAction();
                }
                if (Time.time >= respawnAt) ResetTarget();
                return;
            }
            if (moving)
            {
                float multiplier = GetComponent<CombatStatus>()?.MovementMultiplier ?? 1f;
                transform.position = Vector3.MoveTowards(transform.position, home + Vector3.right * Mathf.Sin(Time.time * 0.7f) * 2f, 2f * multiplier * Time.deltaTime);
            }
            if (!firing || weapon == null || target == null || !target.IsAlive) return;
            var status = GetComponent<CombatStatus>();
            if (status != null && status.Blindness > 0f) return;
            Vector3 origin = weapon.playerCamera.transform.position;
            if (ArcaneWorld.SmokeBlocks(origin, target.transform.position + Vector3.up) || !CombatPhysics.Visible(origin, target, health)) return;
            weapon.playerCamera.transform.LookAt(target.transform.position + Vector3.up);
            if (weapon.currentAmmo == 0) weapon.Reload(); else weapon.TryFire();
        }
        public void ResetTarget()
        {
            if (health == null) return;
            health.ResetForRound();
            health.currentHealth = restoredHealth;
            weapon?.ResetForRound();
            transform.position = home;
            hidden = false;
            SetVisible(true);
        }
        void SetVisible(bool visible)
        {
            foreach (var renderer in visuals) renderer.enabled = visible;
            foreach (var collider in hitboxes) collider.enabled = visible;
        }
    }
}
