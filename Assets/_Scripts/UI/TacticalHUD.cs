using UnityEngine;
using A3C.Combat;
using A3C.Objective;

namespace A3C.UI
{
    public class TacticalHUD : MonoBehaviour
    {
        public HealthSystem playerHealth;
        public WeaponController weaponController;
        public SleepingDragon dragon;

        private GUIStyle boldStyle;
        private GUIStyle titleStyle;
        private GUIStyle subStyle;

        void OnGUI()
        {
            InitStyles();

            float centerX = Screen.width / 2f;
            float centerY = Screen.height / 2f;

            // 1. Ret?cula com abertura dinamica por dispersao
            float spreadOffset = 6f;
            if (weaponController != null)
            {
                spreadOffset += weaponController.GetCurrentSpread() * 200f;
            }

            GUI.color = new Color(0.2f, 0.8f, 1f, 0.9f);
            GUI.Box(new Rect(centerX - 1, centerY - 1, 3, 3), GUIContent.none);
            GUI.Box(new Rect(centerX - spreadOffset - 4, centerY - 1, 4, 2), GUIContent.none);
            GUI.Box(new Rect(centerX + spreadOffset, centerY - 1, 4, 2), GUIContent.none);
            GUI.Box(new Rect(centerX - 1, centerY - spreadOffset - 4, 2, 4), GUIContent.none);
            GUI.Box(new Rect(centerX - 1, centerY + spreadOffset, 2, 4), GUIContent.none);
            GUI.color = Color.white;

            // 2. Top Bar: Dragao Adormecido (Spike)
            if (dragon != null)
            {
                GUI.Box(new Rect(centerX - 120, 15, 240, 45), "");
                GUI.Label(new Rect(centerX - 110, 18, 220, 20), "DRAGAO ADORMECIDO", boldStyle);
                string corrText = "Corrupcao: " + Mathf.RoundToInt(dragon.corruptionPercent) + "%";
                GUI.Label(new Rect(centerX - 110, 35, 220, 20), corrText, subStyle);
            }

            // 3. Bottom Left: Vida & Coletes
            if (playerHealth != null)
            {
                GUI.Box(new Rect(25, Screen.height - 95, 220, 75), "");
                string vestInfo = "COLETE: " + playerHealth.currentVest.ToString().ToUpper();
                GUI.Label(new Rect(35, Screen.height - 90, 200, 20), vestInfo, subStyle);

                GUI.color = new Color(0.2f, 0.7f, 1f);
                GUI.Label(new Rect(35, Screen.height - 72, 200, 25), "ESCUDO: " + Mathf.RoundToInt(playerHealth.currentShield), boldStyle);

                GUI.color = new Color(0.3f, 1f, 0.4f);
                GUI.Label(new Rect(35, Screen.height - 50, 200, 25), "VIDA: " + Mathf.RoundToInt(playerHealth.currentHealth), boldStyle);
                GUI.color = Color.white;
            }

            // 4. Bottom Right: Municao da Arma Atual
            if (weaponController != null && weaponController.currentWeapon != null)
            {
                GUI.Box(new Rect(Screen.width - 240, Screen.height - 95, 215, 75), "");
                GUI.Label(new Rect(Screen.width - 230, Screen.height - 90, 200, 20), weaponController.currentWeapon.weaponName, subStyle);
                
                string ammoText = weaponController.isReloading ? "RECARREGANDO..." : (weaponController.currentAmmo + " / " + weaponController.currentReserve);
                GUI.Label(new Rect(Screen.width - 230, Screen.height - 68, 200, 30), ammoText, titleStyle);
                GUI.Label(new Rect(Screen.width - 230, Screen.height - 42, 200, 20), "LMB: Atirar | R: Recarregar", subStyle);
            }
        }

        private void InitStyles()
        {
            if (boldStyle == null)
            {
                boldStyle = new GUIStyle(GUI.skin.label);
                boldStyle.fontSize = 15;
                boldStyle.fontStyle = FontStyle.Bold;

                titleStyle = new GUIStyle(GUI.skin.label);
                titleStyle.fontSize = 20;
                titleStyle.fontStyle = FontStyle.Bold;

                subStyle = new GUIStyle(GUI.skin.label);
                subStyle.fontSize = 11;
                subStyle.normal.textColor = new Color(0.8f, 0.8f, 0.8f);
            }
        }
    }
}
