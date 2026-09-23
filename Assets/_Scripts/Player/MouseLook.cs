using UnityEngine;
using UnityEngine.InputSystem;

namespace A3C.Player
{
    public class MouseLook : MonoBehaviour
    {
        [Header("Sensibilidade da Mira")]
        public float mouseSensitivity = 0.15f;
        public Transform playerBody;
        public bool captureOnStart = true;
        public bool manageCursor = true;

        private float xRotation = 0f;

        void Start()
        {
            if (!captureOnStart) return;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            // Toggle cursor lock com tecla ESC
            if (manageCursor && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            if (Cursor.lockState != CursorLockMode.Locked) return;

            float mouseX = 0f;
            float mouseY = 0f;

            if (Mouse.current != null)
            {
                Vector2 delta = Mouse.current.delta.ReadValue();
                mouseX = delta.x * mouseSensitivity;
                mouseY = delta.y * mouseSensitivity;
            }

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -89f, 89f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            if (playerBody != null)
            {
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }
    }
}
