using System;
using UnityEngine;

namespace A3C.Combat
{
    public enum ArcaneElement { Fire = 0, Water = 1, Earth = 2, Lightning = 3, LightDark = 4 }
    public enum TacticalRole { Advance = 0, Control = 1, Blocking = 2, Observation = 3, Support = 4 }
    public enum ArcaneEffectKind
    {
        Smoke = 0, Slow = 1, Barrier = 2, Reveal = 3,
        SpeedBoost = 4, Heal = 5, Blind = 6, Suppression = 7
    }
    public enum ArcaneTarget { Self = 0, Allies = 1, Enemies = 2, World = 3 }
    public enum ArcaneAnchor { Caster = 0, Impact = 1 }

    [Serializable]
    public class ArcaneEffectDefinition
    {
        public ArcaneEffectKind kind;
        public ArcaneTarget target;
        public ArcaneAnchor anchor;
        [Min(0f)] public float durationSeconds;
        [Min(0f)] public float radiusMetres;
        [Min(0f)] public float amount;
        [Min(0f)] public float barrierHealth;
        [Min(0f)] public float barrierWidth;
        [Min(0f)] public float barrierHeight;
        [TextArea(2, 5)] public string rules;
    }

    [CreateAssetMenu(fileName = "NewCartridge", menuName = "A3C/Arcane Cartridge Data")]
    public class ArcaneCartridgeData : ScriptableObject
    {
        public string cartridgeId;
        public string cartridgeName;
        public WeaponCategory category;
        public ArcaneElement element;
        public TacticalRole primaryRole;
        public TacticalRole secondaryRole;
        [Min(0)] public int priceCR = 200;
        [Min(1)] public int chargesPerPurchase = 3;
        [Min(0f)] public float cooldownSeconds = 1f;
        [Min(0f)] public float castRangeMetres = 30f;
        public Color effectColor = Color.white;
        [TextArea(2, 5)] public string gameplaySummary;
        [TextArea(2, 5)] public string activationRules;
        [TextArea(2, 5)] public string counterplay;
        [TextArea(2, 5)] public string visualDirection;
        [TextArea(2, 5)] public string implementationNotes;
        public string designSheetPath;
        public ArcaneEffectDefinition[] effects = Array.Empty<ArcaneEffectDefinition>();

        public bool IsCompatibleWith(WeaponData weapon) =>
            weapon != null && weapon.SupportsArcaneCartridges && weapon.category == category;
    }
}
