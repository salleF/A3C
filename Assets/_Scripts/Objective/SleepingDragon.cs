using System;
using UnityEngine;
using A3C.Combat;

namespace A3C.Objective
{
    public class SleepingDragon : MonoBehaviour
    {
        public bool isPlanted;
        public float corruptionPercent;
        public float corruptionSpeed = 2f;
        public float defuseRange = 4f;
        public float defuseTimeRequired = 4f;
        public float plantTimeRequired = 3f;
        public Vector3[] plantSites = { new Vector3(-12, 0, 22), new Vector3(12, 0, 22) };
        public CombatTeam Winner { get; private set; }
        public float Progress { get; private set; }
        public HealthSystem interactor;
        private float interactionTime;
        public event Action<float> OnCorruptionUpdated;
        public event Action<float> OnDefuseProgressUpdated;
        public event Action OnDragonAwakened;
        public event Action<CombatTeam> OnRoundEnded;

        void Update() => Tick(Time.deltaTime);
        public void Tick(float deltaTime)
        {
            if (!isPlanted || Winner != CombatTeam.Neutral) return;
            corruptionPercent = Mathf.Min(100f, corruptionPercent + corruptionSpeed * Mathf.Max(0f, deltaTime));
            OnCorruptionUpdated?.Invoke(corruptionPercent);
            if (corruptionPercent >= 100f) Finish(CombatTeam.Attackers);
        }
        public void ResetForRound(bool planted = false)
        {
            isPlanted = planted;
            corruptionPercent = 0;
            Winner = CombatTeam.Neutral;
            interactionTime = Progress = 0;
            if (planted) transform.position = plantSites[0] + Vector3.up * 0.6f;
            SetVisual(planted);
        }
        public bool CanInteract(HealthSystem actor)
        {
            if (actor == null || !actor.IsAlive || Winner != CombatTeam.Neutral) return false;
            if (isPlanted) return actor.team == CombatTeam.Defenders &&
                Vector3.Distance(actor.transform.position, transform.position) <= defuseRange &&
                CombatPhysics.Visible(transform.position + Vector3.up * 0.5f, actor);
            if (actor.team != CombatTeam.Attackers) return false;
            foreach (var site in plantSites) if (Vector3.Distance(site, actor.transform.position) <= defuseRange) return true;
            return false;
        }
        public void Interact(HealthSystem actor, bool held, float seconds)
        {
            if (!held || !CanInteract(actor) || (interactor != null && interactor != actor))
            {
                interactionTime = Progress = 0;
                interactor = null;
                OnDefuseProgressUpdated?.Invoke(0f);
                return;
            }
            interactor = actor;
            interactionTime += Mathf.Max(0f, seconds);
            Progress = Mathf.Clamp01(interactionTime / (isPlanted ? defuseTimeRequired : plantTimeRequired));
            OnDefuseProgressUpdated?.Invoke(Progress);
            if (Progress < 1f) return;
            if (isPlanted) { Finish(CombatTeam.Defenders); OnDragonAwakened?.Invoke(); }
            else
            {
                isPlanted = true;
                corruptionPercent = 0;
                transform.position = actor.transform.position + actor.transform.forward + Vector3.up * 0.6f;
                SetVisual(true);
                interactionTime = Progress = 0;
                interactor = null;
            }
        }
        public void ProcessDefuse(bool isHoldingKey, float deltaTime) => Interact(interactor, isHoldingKey, deltaTime);
        void Finish(CombatTeam team)
        {
            if (Winner != CombatTeam.Neutral) return;
            Winner = team;
            OnRoundEnded?.Invoke(team);
        }
        void SetVisual(bool visible)
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>()) renderer.enabled = visible;
        }
    }
}
