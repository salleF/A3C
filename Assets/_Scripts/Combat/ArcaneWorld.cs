using System.Collections.Generic;
using UnityEngine;

namespace A3C.Combat
{
    public static class ArcaneWorld
    {
        public struct Ping { public Vector3 position; public float until; public CombatTeam team; }
        public static readonly List<Ping> Pings = new List<Ping>();
        public static readonly List<Vector3> ProtectedPositions = new List<Vector3>();
        public static readonly HashSet<ArcaneZone> Zones = new HashSet<ArcaneZone>();
        public static int Generation { get; private set; }
        private static int sourceId;
        private static Transform root;
        public static Transform Root
        {
            get
            {
                if (root == null) root = new GameObject("Arcane Effects").transform;
                return root;
            }
        }
        public static void Clear()
        {
            Generation++;
            if (root != null)
            {
                if (Application.isPlaying) Object.Destroy(root.gameObject);
                else Object.DestroyImmediate(root.gameObject);
            }
            root = null;
            Pings.Clear();
            Zones.Clear();
            foreach (var actor in HealthSystem.Actors) if (actor != null) actor.GetComponent<CombatStatus>()?.Clear();
        }
        public static bool Matches(ArcaneTarget target, HealthSystem owner, HealthSystem actor)
        {
            if (actor == null || !actor.IsAlive || owner == null) return false;
            if (target == ArcaneTarget.Self) return actor == owner;
            if (target == ArcaneTarget.Allies) return actor == owner || (!owner.IsEnemy(actor));
            return target == ArcaneTarget.Enemies && owner.IsEnemy(actor);
        }
        public static CombatStatus Status(HealthSystem actor)
        {
            var status = actor.GetComponent<CombatStatus>();
            return status != null ? status : actor.gameObject.AddComponent<CombatStatus>();
        }
        public static void Apply(ArcaneEffectDefinition effect, Color color, Vector3 point, Vector3 forward, HealthSystem owner)
        {
            if (owner == null) return;
            int source = ++sourceId;
            if (effect.kind == ArcaneEffectKind.Barrier)
            {
                ArcaneBarrier.Create(effect, color, point, forward, owner);
                return;
            }
            bool area = effect.kind == ArcaneEffectKind.Smoke || effect.persistentArea;
            if (area)
            {
                var zone = new GameObject(effect.kind.ToString()).AddComponent<ArcaneZone>();
                zone.Initialize(effect, color, point, owner, source);
                return;
            }
            foreach (var actor in HealthSystem.Actors)
            {
                if (!Matches(effect.target, owner, actor)) continue;
                if (effect.target != ArcaneTarget.Self && Vector3.Distance(point, actor.transform.position + Vector3.up) > effect.radiusMetres) continue;
                if (actor != owner && !CombatPhysics.Visible(point, actor, effect.anchor == ArcaneAnchor.Caster ? owner : null)) continue;
                if (effect.kind == ArcaneEffectKind.Heal) actor.Heal(effect.amount);
                else if (effect.kind == ArcaneEffectKind.Reveal)
                    Pings.Add(new Ping { position = actor.transform.position + Vector3.up * 2.3f, until = Time.time + effect.durationSeconds, team = owner.team });
                else if (effect.kind == ArcaneEffectKind.Blind)
                {
                    Vector3 direction = (point - actor.transform.position - Vector3.up).normalized;
                    var aim = actor.GetComponent<WeaponController>()?.playerCamera;
                    Vector3 look = aim != null ? aim.transform.forward : actor.transform.forward;
                    if (Vector3.Dot(look, direction) > 0.25f)
                        Status(actor).Apply(source, effect.kind, effect.amount, effect.durationSeconds);
                }
                else Status(actor).Apply(source, effect.kind, effect.amount, effect.durationSeconds);
            }
            var flash = RuntimeVisuals.Primitive("Arcane pulse", PrimitiveType.Sphere, point, Vector3.one * 0.25f, color, false);
            flash.transform.SetParent(Root);
            Object.Destroy(flash, 0.25f);
        }
        public static bool InSmoke(Vector3 point)
        {
            foreach (var zone in Zones)
                if (zone != null && zone.IsSmoke && Vector3.Distance(point, zone.transform.position) < zone.Radius) return true;
            return false;
        }
        public static bool SmokeBlocks(Vector3 from, Vector3 to)
        {
            Vector3 line = to - from;
            float lengthSquared = line.sqrMagnitude;
            if (lengthSquared < 0.001f) return InSmoke(from);
            foreach (var zone in Zones)
            {
                if (zone == null || !zone.IsSmoke) continue;
                float t = Mathf.Clamp01(Vector3.Dot(zone.transform.position - from, line) / lengthSquared);
                if (Vector3.Distance(from + t * line, zone.transform.position) < zone.Radius) return true;
            }
            return false;
        }
    }
}
