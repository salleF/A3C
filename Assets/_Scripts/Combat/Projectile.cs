using UnityEngine;

namespace A3C.Combat
{
    public class Projectile : MonoBehaviour
    {
        [Header("Configuracao")]
        public float lifeTime = 5f;
        public LayerMask hitMask = ~0;

        private float bodyDamage = 40f;
        private float headDamage = 160f;
        private float speed = 120f;
        private HealthSystem shooterHealth;
        private Collider[] shooterColliders;
        private Vector3 lastPosition;
        private bool isInitialized = false;

        public void Initialize(float bodyDmg, float headDmg, float projSpeed, HealthSystem shooter)
        {
            bodyDamage = bodyDmg;
            headDamage = headDmg;
            speed = projSpeed;
            shooterHealth = shooter;
            if (shooter != null)
            {
                shooterColliders = shooter.GetComponentsInChildren<Collider>();
            }

            lastPosition = transform.position;
            isInitialized = true;

            Destroy(gameObject, lifeTime);
        }

        void Update()
        {
            if (!isInitialized) return;

            float moveStep = speed * Time.deltaTime;
            Vector3 nextPosition = transform.position + transform.forward * moveStep;

            Vector3 direction = nextPosition - lastPosition;
            float distance = direction.magnitude;

            if (distance > 0f)
            {
                RaycastHit[] hits = Physics.RaycastAll(lastPosition, direction.normalized, distance, hitMask);
                
                // Ordena por distancia para pegar o impacto mais proximo primeiro
                System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

                foreach (var hit in hits)
                {
                    // Ignora colisor da propria bala
                    if (hit.collider.gameObject == gameObject) continue;

                    // Ignora colisores do pr?prio jogador que disparou
                    if (IsShooterCollider(hit.collider)) continue;

                    // Ignora triggers que nao sejam alvos
                    if (hit.collider.isTrigger && !hit.collider.GetComponentInParent<TargetDummy>()) continue;

                    OnHit(hit);
                    return;
                }
            }

            lastPosition = transform.position;
            transform.position = nextPosition;
        }

        private bool IsShooterCollider(Collider col)
        {
            if (shooterHealth != null)
            {
                if (col.transform.IsChildOf(shooterHealth.transform)) return true;
                if (shooterColliders != null)
                {
                    for (int i = 0; i < shooterColliders.Length; i++)
                    {
                        if (shooterColliders[i] == col) return true;
                    }
                }
            }
            return false;
        }

        private void OnHit(RaycastHit hit)
        {
            // Acertou TargetDummy de treino
            var targetDummy = hit.collider.GetComponentInParent<TargetDummy>();
            if (targetDummy != null)
            {
                bool isHead = hit.collider.CompareTag("Head");
                float dmg = isHead ? headDamage : bodyDamage;
                targetDummy.TakeHit(dmg, isHead, shooterHealth);
            }
            else
            {
                // Acertou inimigo com HealthSystem
                var health = hit.collider.GetComponentInParent<HealthSystem>();
                if (health != null)
                {
                    bool isHead = hit.collider.CompareTag("Head");
                    float dmg = isHead ? headDamage : bodyDamage;
                    health.TakeDamage(dmg);
                }
            }

            Destroy(gameObject);
        }
    }
}
