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
        public TrainingSession session;
        private GUIStyle label;
        void OnGUI()
        {
            if (playerHealth == null || weaponController == null || (session != null && session.MenuOpen)) return;
            if (label == null) label = new GUIStyle(GUI.skin.label) { fontSize = 16, fontStyle = FontStyle.Bold, wordWrap = true };
            float x = Screen.width * 0.5f, y = Screen.height * 0.5f;
            var camera = weaponController.playerCamera;
            if (camera != null && ArcaneWorld.InSmoke(camera.transform.position))
            {
                GUI.color = new Color(0.22f, 0.23f, 0.28f, 1f);
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
                GUI.color = Color.white;
            }
            float blind = playerHealth.GetComponent<CombatStatus>()?.Blindness ?? 0f;
            if (blind > 0f)
            {
                GUI.color = new Color(1, 1, 1, blind);
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
                GUI.color = Color.white;
            }
            float offset = 5f + weaponController.GetCurrentSpread() * 200f;
            GUI.DrawTexture(new Rect(x - offset - 5, y, 5, 2), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(x + offset, y, 5, 2), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(x, y - offset - 5, 2, 5), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(x, y + offset, 2, 5), Texture2D.whiteTexture);
            GUI.Box(new Rect(20, Screen.height - 95, 295, 75), "");
            GUI.Label(new Rect(30, Screen.height - 90, 285, 70), "VIDA " + Mathf.CeilToInt(playerHealth.currentHealth) + "   ESCUDO " + Mathf.CeilToInt(playerHealth.currentShield) +
                "\n" + playerHealth.currentVest + " | ABATES " + playerHealth.kills, label);
            var weapon = weaponController.currentWeapon;
            if (weapon != null)
            {
                string ammo = weaponController.isReloading ? "RECARREGANDO" : weaponController.currentAmmo + " / " + weaponController.currentReserve;
                if (weaponController.State != null && weapon.magazinePerHand > 0) ammo += "  [" + weaponController.State.Left + " | " + weaponController.State.Right + "]";
                GUI.Box(new Rect(Screen.width - 335, Screen.height - 95, 315, 75), "");
                GUI.Label(new Rect(Screen.width - 325, Screen.height - 90, 300, 70), weapon.weaponName + "\n" + ammo + (weaponController.Charge > 0 ? "\nCARGA " + weaponController.Charge.ToString("0.0") + " s" : ""), label);
            }
            if (dragon != null)
            {
                string objective = dragon.isPlanted ? "PESADELO " + dragon.corruptionPercent.ToString("0") + "%" : "CONVERGENCIA A / B";
                if (dragon.CanInteract(playerHealth)) objective += dragon.isPlanted ? " | SEGURE E: DESPERTAR" : " | SEGURE E: PLANTAR";
                if (dragon.Progress > 0) objective += " " + (dragon.Progress * 100).ToString("0") + "%";
                GUI.Box(new Rect(x - 280, 15, 560, 45), objective);
            }
            var arcane = weaponController.GetComponent<ArcaneSystem>();
            if (arcane != null)
            {
                string[] keys = { "Q", "E", "C" };
                for (int i = 0; i < 3; i++)
                {
                    var slot = arcane.slots[i];
                    bool compatible = slot.data != null && slot.data.IsCompatibleWith(weapon);
                    string info = slot.data == null ? "vazio" : slot.data.cartridgeName + " [" + slot.charges + "]";
                    if (slot.data != null && !compatible) info += " (outra classe)";
                    if (slot.readyAt > Time.time) info += " " + (slot.readyAt - Time.time).ToString("0.0") + "s";
                    GUI.color = arcane.armedSlot == i ? Color.cyan : Color.white;
                    GUI.Box(new Rect(20, 75 + i * 44, 290, 42), keys[i] + " | " + info);
                }
                GUI.color = Color.white;
            }
            if (camera != null && blind < 0.3f)
            {
                ArcaneWorld.Pings.RemoveAll(p => p.until <= Time.time);
                foreach (var ping in ArcaneWorld.Pings)
                {
                    if (ping.team != playerHealth.team) continue;
                    Vector3 screen = camera.WorldToScreenPoint(ping.position);
                    if (screen.z > 0) GUI.Label(new Rect(screen.x - 40, Screen.height - screen.y, 140, 24), "ULTIMA POSICAO", label);
                }
                foreach (var actor in HealthSystem.Actors)
                {
                    if (actor == playerHealth || !actor.IsAlive || Vector3.Distance(actor.transform.position, playerHealth.transform.position) > 50) continue;
                    if (!CombatPhysics.Visible(camera.transform.position, actor, playerHealth) || ArcaneWorld.SmokeBlocks(camera.transform.position, actor.transform.position + Vector3.up)) continue;
                    Vector3 screen = camera.WorldToScreenPoint(actor.transform.position + Vector3.up * 2.1f);
                    if (screen.z > 0) GUI.Label(new Rect(screen.x - 60, Screen.height - screen.y, 190, 25), "HP " + actor.currentHealth.ToString("0") + " | " + actor.currentShield.ToString("0"), label);
                }
            }
            GUI.Label(new Rect(20, 15, 220, 35), "B: equipamento | F5: reiniciar");
        }
    }
}
