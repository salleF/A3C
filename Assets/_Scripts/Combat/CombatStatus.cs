using System.Collections.Generic;
using UnityEngine;

namespace A3C.Combat
{
    public class CombatStatus : MonoBehaviour
    {
        private struct Entry
        {
            public int source;
            public ArcaneEffectKind kind;
            public float amount;
            public float until;
        }
        private readonly List<Entry> entries = new List<Entry>();
        public float MovementMultiplier => (1f + Strongest(ArcaneEffectKind.SpeedBoost)) * (1f - Strongest(ArcaneEffectKind.Slow));
        public float SpreadMultiplier => 1f + Strongest(ArcaneEffectKind.Suppression);
        public float Blindness => Strongest(ArcaneEffectKind.Blind);

        public void Apply(int source, ArcaneEffectKind kind, float amount, float seconds)
        {
            Remove(source, kind);
            entries.Add(new Entry { source = source, kind = kind, amount = Mathf.Clamp01(amount), until = Time.time + seconds });
        }
        public void Remove(int source, ArcaneEffectKind kind) => entries.RemoveAll(e => e.source == source && e.kind == kind);
        public void Clear() => entries.Clear();
        public float Strongest(ArcaneEffectKind kind)
        {
            entries.RemoveAll(e => e.until <= Time.time);
            float value = 0f;
            foreach (var e in entries) if (e.kind == kind) value = Mathf.Max(value, e.amount);
            return value;
        }
    }
}
