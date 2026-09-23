using UnityEngine;

namespace A3C.Combat
{
    public class ArcaneBarrier : MonoBehaviour
    {
        public float health;
        public void TakeDamage(float amount)
        {
            if (health <= 0f || amount <= 0f) return;
            health -= amount;
            if (health <= 0f) { GetComponent<Collider>().enabled = false; Destroy(gameObject); }
        }
        public static ArcaneBarrier Create(ArcaneEffectDefinition effect, Color color, Vector3 point, Vector3 forward, HealthSystem owner)
        {
            if (!CombatPhysics.FirstHit(point + Vector3.up * 2f, Vector3.down, 8f, owner, out var floor) || floor.normal.y < 0.5f) return null;
            Vector3 ground = floor.point;
            foreach (var protectedPoint in ArcaneWorld.ProtectedPositions)
                if (Vector3.Distance(ground, protectedPoint) < effect.barrierWidth * 0.5f + 4f) return null;
            Vector3 size = new Vector3(effect.barrierWidth, effect.barrierHeight, Mathf.Max(0.05f, effect.barrierDepth));
            Vector3 center = ground + Vector3.up * (size.y * 0.5f + 0.05f);
            forward.y = 0f;
            Quaternion rotation = forward.sqrMagnitude > 0.01f ? Quaternion.LookRotation(forward) : Quaternion.identity;
            if (Physics.CheckBox(center, size * 0.49f, rotation, ~0, QueryTriggerInteraction.Ignore)) return null;
            var go = RuntimeVisuals.Primitive("Runic barrier", PrimitiveType.Cube, center, size, color);
            go.transform.rotation = rotation;
            go.transform.SetParent(ArcaneWorld.Root);
            var barrier = go.AddComponent<ArcaneBarrier>();
            barrier.health = effect.barrierHealth;
            Destroy(go, effect.durationSeconds);
            return barrier;
        }
    }
}
