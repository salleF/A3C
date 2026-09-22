using UnityEngine;
using System;

namespace A3C.Objective
{
    public class SleepingDragon : MonoBehaviour
    {
        [Header("Mecanica do Dragao (O Spike)")]
        public bool isPlanted = true;
        public float corruptionPercent = 0f;
        public float corruptionSpeed = 2.0f;
        public float defuseRange = 4.0f;
        public float defuseTimeRequired = 4.0f;

        private float currentDefuseTime = 0f;
        private bool isAwake = false;

        public event Action<float> OnCorruptionUpdated;
        public event Action<float> OnDefuseProgressUpdated;
        public event Action OnDragonAwakened;

        void Update()
        {
            if (isAwake || !isPlanted) return;

            corruptionPercent += corruptionSpeed * Time.deltaTime;
            corruptionPercent = Mathf.Clamp(corruptionPercent, 0f, 100f);
            OnCorruptionUpdated?.Invoke(corruptionPercent);

            if (corruptionPercent >= 100f)
            {
                Debug.Log("Corrupcao atingiu 100%! Vitoria dos Atacantes!");
            }
        }

        public void ProcessDefuse(bool isHoldingKey, float deltaTime)
        {
            if (isAwake || !isPlanted) return;

            if (isHoldingKey)
            {
                currentDefuseTime += deltaTime;
                float progress = Mathf.Clamp01(currentDefuseTime / defuseTimeRequired);
                OnDefuseProgressUpdated?.Invoke(progress);

                if (currentDefuseTime >= defuseTimeRequired)
                {
                    WakeUpDragon();
                }
            }
            else
            {
                currentDefuseTime = 0f;
                OnDefuseProgressUpdated?.Invoke(0f);
            }
        }

        private void WakeUpDragon()
        {
            isAwake = true;
            OnDragonAwakened?.Invoke();
            Debug.Log("DRAGAO ACORDOU! Defensores venceram a rodada de treino da A3C!");
        }
    }
}
