using UnityEngine;
using System;

namespace A3C.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        [Header("Vida Base (Maximo 100 HP)")]
        public float maxHealth = 100f;
        public float currentHealth = 100f;

        [Header("Colete e Escudo")]
        public VestType currentVest = VestType.Pesado;
        public float currentShield = 100f;
        public float maxShield = 100f;

        [Header("Regeneracao do Colete Energetico")]
        public float timeToRegenShield = 6.0f;
        public float shieldRegenSpeed = 15f;
        private float timeSinceLastDamage = 0f;

        public event Action<float, float> OnHealthChanged;
        public event Action<float, float> OnShieldChanged;
        public event Action OnDeath;

        void Start()
        {
            EquipVest(currentVest);
            currentHealth = maxHealth;
        }

        void Update()
        {
            if (currentVest == VestType.Energetico && currentShield < 50f)
            {
                timeSinceLastDamage += Time.deltaTime;
                if (timeSinceLastDamage >= timeToRegenShield)
                {
                    currentShield = Mathf.Min(50f, currentShield + shieldRegenSpeed * Time.deltaTime);
                    OnShieldChanged?.Invoke(currentShield, maxShield);
                }
            }
        }

        public void EquipVest(VestType vest)
        {
            currentVest = vest;
            switch (vest)
            {
                case VestType.None:
                    maxShield = 0f;
                    currentShield = 0f;
                    break;
                case VestType.Leve:
                    maxShield = 50f;
                    currentShield = 50f;
                    break;
                case VestType.Energetico:
                    maxShield = 50f;
                    currentShield = 50f;
                    break;
                case VestType.Pesado:
                    maxShield = 100f;
                    currentShield = 100f;
                    break;
                case VestType.Construtivo:
                    maxShield = 200f;
                    currentShield = 0f;
                    break;
            }
            OnShieldChanged?.Invoke(currentShield, maxShield);
        }

        public void TakeDamage(float damage)
        {
            timeSinceLastDamage = 0f;

            if (currentShield > 0f)
            {
                if (damage <= currentShield)
                {
                    currentShield -= damage;
                    damage = 0f;
                }
                else
                {
                    damage -= currentShield;
                    currentShield = 0f;
                }
                OnShieldChanged?.Invoke(currentShield, maxShield);
            }

            if (damage > 0f)
            {
                currentHealth = Mathf.Max(0f, currentHealth - damage);
                OnHealthChanged?.Invoke(currentHealth, maxHealth);

                if (currentHealth <= 0f)
                {
                    Die();
                }
            }
        }

        public void RegisterKill()
        {
            if (currentVest == VestType.Construtivo)
            {
                currentShield += 20f;
                OnShieldChanged?.Invoke(currentShield, maxShield);
            }
        }

        public void Heal(float amount)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        private void Die()
        {
            OnDeath?.Invoke();
        }
    }
}
