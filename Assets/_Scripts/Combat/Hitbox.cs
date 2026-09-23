using UnityEngine;

namespace A3C.Combat
{
    public class Hitbox : MonoBehaviour
    {
        public bool head;
        public HealthSystem health;
        void Awake() { if (health == null) health = GetComponentInParent<HealthSystem>(); }
    }
}
