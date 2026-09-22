using UnityEngine;
using UnityEngine.InputSystem;

namespace A3C.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        public Animator animator;
        public CharacterController characterController;

        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsFiringHash = Animator.StringToHash("IsFiring");

        void Awake()
        {
            if (animator == null) animator = GetComponentInChildren<Animator>();
            if (characterController == null) characterController = GetComponentInParent<CharacterController>();
        }

        void Update()
        {
            if (animator == null) return;

            // Calcula velocidade para Blend Tree ou transicoes (0 = Idle, 1 = Walk, 2 = Run)
            float speed = 0f;
            if (characterController != null)
            {
                Vector3 horizontalVel = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z);
                float currentSpeed = horizontalVel.magnitude;

                if (currentSpeed > 0.1f)
                {
                    bool isSprinting = Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed;
                    speed = isSprinting ? 2f : 1f;
                }
            }
            animator.SetFloat(SpeedHash, speed, 0.1f, Time.deltaTime);

            // Disparo
            bool isFiring = Mouse.current != null && Mouse.current.leftButton.isPressed;
            animator.SetBool(IsFiringHash, isFiring);
        }
    }
}
