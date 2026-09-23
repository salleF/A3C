using UnityEngine;
using UnityEngine.InputSystem;
using A3C.Combat;

namespace A3C.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Velocidade e Movimentacao")]
        public float walkSpeed = 6.0f;
        public float sprintSpeed = 9.0f;
        public float jumpHeight = 1.6f;
        public float gravity = -20f;

        [Header("Checagem de Solo")]
        public Transform groundCheck;
        public float groundDistance = 0.3f;
        public LayerMask groundMask;

        private CharacterController controller;
        private Vector3 velocity;
        private bool isGrounded;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            if (!controller.enabled) return;
            var health = GetComponent<HealthSystem>();
            if ((health != null && !health.IsAlive) || Cursor.lockState != CursorLockMode.Locked) return;
            if (groundCheck != null)
            {
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            }
            else
            {
                isGrounded = controller.isGrounded;
            }

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = 0f;
            float z = 0f;
            bool sprintPressed = false;
            bool jumpPressed = false;

            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed) z += 1f;
                if (Keyboard.current.sKey.isPressed) z -= 1f;
                if (Keyboard.current.aKey.isPressed) x -= 1f;
                if (Keyboard.current.dKey.isPressed) x += 1f;

                sprintPressed = Keyboard.current.leftShiftKey.isPressed;
                jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
            }

            Vector3 move = transform.right * x + transform.forward * z;
            move.Normalize();

            float currentSpeed = sprintPressed ? sprintSpeed : walkSpeed;
            var weapon = GetComponent<WeaponController>()?.currentWeapon;
            if (weapon != null) currentSpeed *= weapon.equippedMoveMultiplier * (sprintPressed ? weapon.sprintMultiplier : 1f);
            var status = GetComponent<CombatStatus>();
            if (status != null) currentSpeed *= status.MovementMultiplier;
            controller.Move(move * currentSpeed * Time.deltaTime);

            if (jumpPressed && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
