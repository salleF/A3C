using UnityEngine;

namespace A3C.Combat
{
    public class ArcaneZone : MonoBehaviour
    {
        private ArcaneEffectDefinition effect;
        private HealthSystem owner;
        private int source;
        private float expires;
        public bool IsSmoke => effect != null && effect.kind == ArcaneEffectKind.Smoke;
        public float Radius => effect != null ? effect.radiusMetres : 0f;
        public void Initialize(ArcaneEffectDefinition definition, Color color, Vector3 point, HealthSystem caster, int id)
        {
            effect = definition;
            owner = caster;
            source = id;
            expires = Time.time + effect.durationSeconds;
            transform.position = point;
            transform.SetParent(ArcaneWorld.Root);
            ArcaneWorld.Zones.Add(this);
            var visual = RuntimeVisuals.Primitive(IsSmoke ? "Smoke" : "Runic field", IsSmoke ? PrimitiveType.Sphere : PrimitiveType.Cylinder,
                point, IsSmoke ? Vector3.one * Radius * 2f : new Vector3(Radius * 2f, 0.025f, Radius * 2f),
                IsSmoke ? Color.Lerp(color, Color.gray, 0.75f) : color * 0.75f, false);
            visual.transform.SetParent(transform, true);
        }
        void Update()
        {
            if (effect == null || Time.time >= expires || owner == null) { Destroy(gameObject); return; }
            if (IsSmoke) return;
            foreach (var actor in HealthSystem.Actors)
            {
                if (actor == null) continue;
                bool inside = ArcaneWorld.Matches(effect.target, owner, actor) &&
                    Vector3.Distance(transform.position, actor.transform.position + Vector3.up) <= Radius &&
                    CombatPhysics.Visible(transform.position + Vector3.up * 0.15f, actor);
                if (inside) ArcaneWorld.Status(actor).Apply(source, effect.kind, effect.amount, 0.2f);
                else actor.GetComponent<CombatStatus>()?.Remove(source, effect.kind);
            }
        }
        void OnDestroy()
        {
            ArcaneWorld.Zones.Remove(this);
            if (effect == null) return;
            foreach (var actor in HealthSystem.Actors)
                if (actor != null) actor.GetComponent<CombatStatus>()?.Remove(source, effect.kind);
        }
    }
}
