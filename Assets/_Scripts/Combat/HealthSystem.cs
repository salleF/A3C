using System;
using System.Collections.Generic;
using UnityEngine;

namespace A3C.Combat
{
    public enum CombatTeam { Neutral, Defenders, Attackers }

    public class HealthSystem : MonoBehaviour
    {
        public static readonly HashSet<HealthSystem> Actors = new HashSet<HealthSystem>();
        public float maxHealth = 100f;
        public float currentHealth = 100f;
        public VestType currentVest = VestType.Pesado;
        public float currentShield = 100f;
        public float maxShield = 100f;
        public float timeToRegenShield = 6f;
        public float shieldRegenSpeed = 15f;
        public CombatTeam team;
        public int kills;
        private float sinceDamage;
        public bool IsAlive => currentHealth > 0f;
        public event Action<float, float> OnHealthChanged;
        public event Action<float, float> OnShieldChanged;
        public event Action OnDeath;

        void OnEnable() => Actors.Add(this);
        void OnDisable() => Actors.Remove(this);
        void Update() => TickRegeneration(Time.deltaTime);

        public void TickRegeneration(float seconds)
        {
            if (!IsAlive || seconds <= 0f) return;
            float previous = sinceDamage;
            sinceDamage += seconds;
            if (currentVest != VestType.Energetico || sinceDamage <= timeToRegenShield) return;
            float activeTime = sinceDamage - Mathf.Max(previous, timeToRegenShield);
            currentShield = Mathf.Min(maxShield, currentShield + shieldRegenSpeed * activeTime);
            OnShieldChanged?.Invoke(currentShield, maxShield);
        }

        public void ResetForRound()
        {
            maxHealth = Mathf.Clamp(maxHealth, 1f, 100f);
            currentHealth = maxHealth;
            kills = 0;
            sinceDamage = 0;
            EquipVest(currentVest);
            GetComponent<CombatStatus>()?.Clear();
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public void EquipVest(VestType vest)
        {
            currentVest = vest;
            maxShield = vest == VestType.None ? 0f :
                vest == VestType.Leve || vest == VestType.Energetico ? 50f : 100f;
            currentShield = vest == VestType.Construtivo ? 0f : maxShield;
            sinceDamage = 0f;
            OnShieldChanged?.Invoke(currentShield, maxShield);
        }

        public bool IsEnemy(HealthSystem other) => other != null && other != this &&
            (team == CombatTeam.Neutral || other.team == CombatTeam.Neutral || team != other.team);

        public void TakeDamage(float damage) => ApplyDamage(damage, null);

        public bool ApplyDamage(float damage, HealthSystem attacker)
        {
            if (!IsAlive || damage <= 0f || float.IsNaN(damage) || float.IsInfinity(damage)) return false;
            if (attacker != null && !IsEnemy(attacker)) return false;
            sinceDamage = 0f;
            float absorbed = Mathf.Min(currentShield, damage);
            currentShield -= absorbed;
            currentHealth = Mathf.Max(0f, currentHealth - (damage - absorbed));
            OnShieldChanged?.Invoke(currentShield, maxShield);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            if (!IsAlive)
            {
                attacker?.RegisterKill();
                GetComponent<CombatStatus>()?.Clear();
                OnDeath?.Invoke();
            }
            return true;
        }

        public void RegisterKill()
        {
            if (!IsAlive) return;
            kills++;
            if (currentVest != VestType.Construtivo) return;
            currentShield = Mathf.Min(100f, currentShield + 20f);
            OnShieldChanged?.Invoke(currentShield, maxShield);
        }

        public void Heal(float amount)
        {
            if (!IsAlive || amount <= 0f || float.IsNaN(amount) || float.IsInfinity(amount)) return;
            currentHealth = Mathf.Min(Mathf.Min(100f, maxHealth), currentHealth + amount);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }
}
