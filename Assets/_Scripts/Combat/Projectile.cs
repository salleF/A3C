using UnityEngine;

namespace A3C.Combat
{
    public class Projectile : MonoBehaviour
    {
        public float lifeTime = 5f;
        public LayerMask hitMask = ~0;
        private float bodyDamage, headDamage, speed, remaining, barrierMultiplier;
        private HealthSystem shooter;
        private ArcaneCast cast;
        private bool initialized;

        public void Initialize(float bodyDmg, float headDmg, float projSpeed, HealthSystem owner,
            float range = 100f, ArcaneCast arcaneCast = null, float barrierDamageMultiplier = 1f)
        {
            bodyDamage = bodyDmg;
            headDamage = headDmg;
            speed = Mathf.Max(1f, projSpeed);
            shooter = owner;
            remaining = Mathf.Max(0f, range);
            cast = arcaneCast;
            barrierMultiplier = barrierDamageMultiplier;
            initialized = true;
            foreach (var col in GetComponentsInChildren<Collider>()) col.enabled = false;
            Destroy(gameObject, Mathf.Max(lifeTime, remaining / speed + 0.1f));
        }
        void Update()
        {
            if (!initialized) return;
            float distance = Mathf.Min(remaining, speed * Time.deltaTime);
            if (CombatPhysics.FirstHit(transform.position, transform.forward, distance, shooter, out var hit, hitMask))
            {
                CombatPhysics.Damage(hit, bodyDamage, headDamage, shooter, barrierMultiplier);
                cast?.Impact(hit.point, hit.normal);
                Destroy(gameObject);
                return;
            }
            transform.position += transform.forward * distance;
            remaining -= distance;
            if (remaining <= 0f) Destroy(gameObject);
        }
    }
}
