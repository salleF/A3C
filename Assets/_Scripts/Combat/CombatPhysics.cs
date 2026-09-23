using System;
using UnityEngine;

namespace A3C.Combat
{
    public static class CombatPhysics
    {
        public static bool FirstHit(Vector3 origin, Vector3 direction, float distance, HealthSystem shooter, out RaycastHit result, int mask = ~0)
        {
            var hits = Physics.RaycastAll(origin, direction, distance, mask, QueryTriggerInteraction.Collide);
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
            foreach (var hit in hits)
            {
                var health = hit.collider.GetComponentInParent<HealthSystem>();
                if (shooter != null && health == shooter) continue;
                if (hit.collider.isTrigger && hit.collider.GetComponent<Hitbox>() == null && hit.collider.GetComponentInParent<TargetDummy>() == null) continue;
                if (health != null && !health.IsAlive) continue;
                result = hit;
                return true;
            }
            result = default;
            return false;
        }
        public static bool Visible(Vector3 origin, HealthSystem target, HealthSystem ignore = null)
        {
            Vector3 destination = target.transform.position + Vector3.up;
            Vector3 delta = destination - origin;
            return !FirstHit(origin, delta.normalized, delta.magnitude, ignore, out var hit) ||
                hit.collider.GetComponentInParent<HealthSystem>() == target;
        }
        public static bool Damage(RaycastHit hit, float body, float head, HealthSystem shooter, float barrierMultiplier = 1f)
        {
            var barrier = hit.collider.GetComponentInParent<ArcaneBarrier>();
            if (barrier != null) { barrier.TakeDamage(body * barrierMultiplier); return true; }
            var hitbox = hit.collider.GetComponent<Hitbox>();
            bool isHead = hitbox != null ? hitbox.head : hit.collider.tag == "Head";
            var health = hit.collider.GetComponentInParent<HealthSystem>();
            if (health != null) return health.ApplyDamage(isHead ? head : body, shooter);
            var dummy = hit.collider.GetComponentInParent<TargetDummy>();
            if (dummy == null) return false;
            dummy.TakeHit(isHead ? head : body, isHead, shooter);
            return true;
        }
    }
}
