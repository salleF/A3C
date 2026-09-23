using UnityEngine;
using System.Collections;

namespace A3C.Combat
{
    public class TargetDummy : MonoBehaviour
    {
        [Header("Configuracoes do Boneco")]
        public float maxHealth = 200f;
        public float currentHealth = 200f;
        public GameObject headObject;
        public GameObject bodyObject;

        private bool isDead = false;
        private Material headMat;
        private Material bodyMat;

        void Awake()
        {
            if (headObject != null)
            {
                var hitbox = headObject.GetComponent<Hitbox>() ?? headObject.AddComponent<Hitbox>();
                hitbox.head = true;
                headMat = headObject.GetComponent<Renderer>()?.material;
            }
            if (bodyObject != null)
            {
                bodyMat = bodyObject.GetComponent<Renderer>()?.material;
            }
        }

        public void TakeHit(float damage, bool isHeadshot, HealthSystem attacker)
        {
            if (isDead) return;

            currentHealth -= damage;
            StartCoroutine(FlashDamage(isHeadshot));

            if (currentHealth <= 0f)
            {
                isDead = true;
                if (attacker != null)
                {
                    attacker.RegisterKill();
                }
                StartCoroutine(RespawnRoutine());
            }
        }

        private IEnumerator FlashDamage(bool isHeadshot)
        {
            Color originalHead = headMat != null ? headMat.color : Color.red;
            Color originalBody = bodyMat != null ? bodyMat.color : Color.white;

            if (headMat != null) headMat.color = Color.yellow;
            if (bodyMat != null) bodyMat.color = Color.yellow;

            yield return new WaitForSeconds(0.08f);

            if (headMat != null) headMat.color = originalHead;
            if (bodyMat != null) bodyMat.color = originalBody;
        }

        private IEnumerator RespawnRoutine()
        {
            var renderers = GetComponentsInChildren<Renderer>();
            var colliders = GetComponentsInChildren<Collider>();
            foreach (var renderer in renderers) renderer.enabled = false;
            foreach (var collider in colliders) collider.enabled = false;
            yield return new WaitForSeconds(2.5f);
            currentHealth = maxHealth;
            isDead = false;
            foreach (var renderer in renderers) renderer.enabled = true;
            foreach (var collider in colliders) collider.enabled = true;
        }
    }
}
