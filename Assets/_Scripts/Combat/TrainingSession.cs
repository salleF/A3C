using System;
using System.Collections.Generic;
using A3C.Objective;
using A3C.Player;
using A3C.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace A3C.Combat
{
    [DefaultExecutionOrder(-100)]
    public class TrainingSession : MonoBehaviour
    {
        public ArsenalCatalog catalog;
        public HealthSystem player;
        public WeaponController weapons;
        public ArcaneSystem arcane;
        public SleepingDragon dragon;
        public bool MenuOpen { get; private set; }
        public bool FreePractice { get; private set; } = true;
        public int Credits { get; private set; } = 15000;
        public string Message { get; private set; } = "Escolha seu equipamento e entre na simulacao.";
        private readonly List<TrainingActor> targets = new List<TrainingActor>();
        private Vector2 weaponScroll, cartridgeScroll;
        private int selectedSlot;
        private GameObject viewModel;
        private WeaponData shownWeapon;
        private float lastShot;
        private AudioSource audioSource;
        private AudioClip shotSound;

        void Start()
        {
            if (catalog == null) { Debug.LogError("A3C: catalogo ausente na cena."); enabled = false; return; }
            BuildArena();
            BuildPlayer();
            BuildTargets();
            var objective = new GameObject("Sleeping Dragon");
            dragon = objective.AddComponent<SleepingDragon>();
            var body = RuntimeVisuals.Primitive("Dragon core", PrimitiveType.Sphere, new Vector3(0, 0.6f, 0), new Vector3(1.1f, 0.7f, 1.6f), new Color(0.45f, 0.22f, 0.65f), false);
            body.transform.SetParent(objective.transform, false);
            for (int side = -1; side <= 1; side += 2)
            {
                var wing = RuntimeVisuals.Primitive("Dragon wing", PrimitiveType.Cube, new Vector3(side * 0.65f, 0.65f, 0), new Vector3(0.9f, 0.1f, 0.9f), new Color(0.6f, 0.4f, 0.8f), false);
                wing.transform.SetParent(objective.transform, false);
                wing.transform.localRotation = Quaternion.Euler(0, 0, side * 25);
            }
            ArcaneWorld.ProtectedPositions.Clear();
            ArcaneWorld.ProtectedPositions.AddRange(dragon.plantSites);
            ArcaneWorld.ProtectedPositions.Add(new Vector3(0, 0, -18));
            var hud = gameObject.AddComponent<TacticalHUD>();
            hud.playerHealth = player; hud.weaponController = weapons; hud.dragon = dragon; hud.session = this;
            ResetSimulation(CombatTeam.Defenders, false);
            SetMenu(true);
        }

        void BuildArena()
        {
            Color stone = new Color(0.18f, 0.2f, 0.27f);
            RuntimeVisuals.Primitive("Projection floor", PrimitiveType.Cube, new Vector3(0, -0.25f, 5), new Vector3(60, 0.5f, 70), stone);
            for (int i = -3; i <= 3; i++)
            {
                RuntimeVisuals.Primitive("Rune grid", PrimitiveType.Cube, new Vector3(i * 8, 0.015f, 5), new Vector3(0.035f, 0.025f, 70), new Color(0.22f, 0.38f, 0.44f), false);
                RuntimeVisuals.Primitive("Rune grid", PrimitiveType.Cube, new Vector3(0, 0.015f, i * 8 + 5), new Vector3(60, 0.025f, 0.035f), new Color(0.22f, 0.38f, 0.44f), false);
            }
            foreach (int side in new[] { -1, 1 })
            {
                RuntimeVisuals.Primitive("Outer wall", PrimitiveType.Cube, new Vector3(side * 30, 3, 5), new Vector3(1, 6, 70), stone * 0.8f);
                RuntimeVisuals.Primitive("Outer wall", PrimitiveType.Cube, new Vector3(0, 3, 5 + side * 35), new Vector3(60, 6, 1), stone * 0.8f);
                for (int i = 0; i < 4; i++)
                {
                    RuntimeVisuals.Primitive("Crushle broken tower", PrimitiveType.Cube, new Vector3(side * 23, 3 + i * 0.5f, i * 13 - 17), new Vector3(2.5f, 6 + i, 2.5f), stone * 1.5f);
                    var shard = RuntimeVisuals.Primitive("Floating ruin", PrimitiveType.Cube, new Vector3(side * 20, 10 + i * 2, i * 12 - 10), new Vector3(3, 2, 3), stone * 1.8f, false);
                    shard.transform.rotation = Quaternion.Euler(i * 12, 30, 20);
                }
                RuntimeVisuals.Primitive("Cover", PrimitiveType.Cube, new Vector3(side * 8, 0.65f, 5), new Vector3(3, 1.3f, 2), stone * 1.4f);
                RuntimeVisuals.Primitive("Convergence site", PrimitiveType.Cylinder, new Vector3(side * 12, 0.03f, 22), new Vector3(8, 0.03f, 8), side < 0 ? new Color(0.12f, 0.55f, 0.65f) : new Color(0.5f, 0.25f, 0.65f), false);
            }
            for (int i = 0; i < 16; i++)
            {
                float angle = i * Mathf.PI * 2f / 16f;
                var rune = RuntimeVisuals.Primitive("Dimensional portal", PrimitiveType.Cube, new Vector3(Mathf.Cos(angle) * 5, 9 + Mathf.Sin(angle) * 5, 34), new Vector3(1.6f, 0.5f, 0.6f), new Color(0.55f, 0.15f, 0.9f), false);
                rune.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg + 90);
            }
        }

        void BuildPlayer()
        {
            var go = new GameObject("A3C Cadet");
            go.SetActive(false);
            player = go.AddComponent<HealthSystem>();
            player.team = CombatTeam.Defenders;
            var controller = go.AddComponent<CharacterController>();
            controller.height = 1.45f; controller.center = Vector3.up * 0.725f; controller.radius = 0.3f;
            go.AddComponent<CombatStatus>();
            go.AddComponent<PlayerMovement>();
            var eyes = new GameObject("Player Camera");
            eyes.transform.SetParent(go.transform, false); eyes.transform.localPosition = Vector3.up * 1.65f;
            var camera = eyes.AddComponent<Camera>();
            camera.tag = "MainCamera"; camera.fieldOfView = 70; camera.nearClipPlane = 0.05f;
            camera.clearFlags = CameraClearFlags.SolidColor; camera.backgroundColor = new Color(0.035f, 0.025f, 0.08f);
            eyes.AddComponent<AudioListener>();
            var look = eyes.AddComponent<MouseLook>(); look.playerBody = go.transform; look.captureOnStart = false; look.manageCursor = false;
            weapons = go.AddComponent<WeaponController>(); weapons.playerCamera = camera;
            arcane = go.AddComponent<ArcaneSystem>(); arcane.weaponController = weapons;
            go.SetActive(true);
            weapons.EquipWeapon(catalog.defenderStarter);
            weapons.OnShot += Fired;
            audioSource = go.AddComponent<AudioSource>(); audioSource.playOnAwake = false;
            shotSound = AudioClip.Create("Runic discharge", 4800, 1, 48000, false);
            var samples = new float[4800];
            for (int i = 0; i < samples.Length; i++) samples[i] = Mathf.Sin(i * 0.17f) * Mathf.Exp(-i / 650f) * 0.35f;
            shotSound.SetData(samples, 0);
        }

        void BuildTargets()
        {
            for (int i = 0; i < 6; i++)
            {
                var go = new GameObject(i == 5 ? "Wounded ally" : "Training opponent " + (i + 1));
                go.SetActive(false); go.transform.position = new Vector3(i == 5 ? -3 : (i - 2) * 5, 0, i == 5 ? -12 : 12 + (i % 2) * 5);
                var health = go.AddComponent<HealthSystem>(); health.team = i == 5 ? CombatTeam.Defenders : CombatTeam.Attackers;
                health.currentVest = i == 5 ? VestType.None : VestType.Pesado;
                health.ResetForRound(); if (i == 5) health.currentHealth = 40;
                Color color = i == 5 ? new Color(0.1f, 0.75f, 0.6f) : new Color(0.75f, 0.24f, 0.27f);
                var body = RuntimeVisuals.Primitive("Body", PrimitiveType.Capsule, new Vector3(0, 0.75f, 0), new Vector3(0.65f, 0.7f, 0.65f), color);
                body.transform.SetParent(go.transform, false);
                var head = RuntimeVisuals.Primitive("Head", PrimitiveType.Sphere, new Vector3(0, 1.65f, 0), Vector3.one * 0.42f, color * 1.3f);
                head.transform.SetParent(go.transform, false); head.AddComponent<Hitbox>().head = true;
                var eyes = new GameObject("Target aim"); eyes.transform.SetParent(go.transform, false); eyes.transform.localPosition = Vector3.up * 1.65f;
                var camera = eyes.AddComponent<Camera>(); camera.enabled = false;
                eyes.transform.localRotation = Quaternion.Euler(0, 180, 0);
                var gun = go.AddComponent<WeaponController>(); gun.playerCamera = camera; gun.acceptPlayerInput = false;
                var actor = go.AddComponent<TrainingActor>(); actor.target = player; actor.restoredHealth = i == 5 ? 40f : 100f;
                go.SetActive(true);
                gun.EquipWeapon(catalog.defenderStarter);
                targets.Add(actor);
            }
        }

        void Update()
        {
            if (player == null) return;
            var keyboard = Keyboard.current;
            if (keyboard != null && (keyboard.escapeKey.wasPressedThisFrame || keyboard.bKey.wasPressedThisFrame)) SetMenu(!MenuOpen);
            if (keyboard != null && keyboard.f5Key.wasPressedThisFrame) ResetSimulation(player.team, dragon.isPlanted);
            bool canInteract = dragon != null && dragon.CanInteract(player);
            arcane.interactionOwnsE = canInteract;
            weapons.interactionBlocked = !MenuOpen && canInteract && keyboard != null && keyboard.eKey.isPressed;
            if (!MenuOpen) dragon.Interact(player, weapons.interactionBlocked, Time.deltaTime);
            if (player.transform.position.y < -5f) player.ApplyDamage(1000, null);
            if (!player.IsAlive || dragon.Winner != CombatTeam.Neutral) SetMenu(true);
            if (shownWeapon != weapons.currentWeapon) BuildViewModel();
            if (viewModel != null) viewModel.transform.localPosition = new Vector3(0.28f, -0.24f, 0.5f - Mathf.Max(0, 0.08f - (Time.time - lastShot)));
        }

        void Fired(WeaponData weapon)
        {
            lastShot = Time.time;
            if (audioSource != null) { audioSource.pitch = weapon.silenced ? 1.6f : 1f; audioSource.PlayOneShot(shotSound, weapon.silenced ? 0.16f : 0.6f); }
        }
        void BuildViewModel()
        {
            shownWeapon = weapons.currentWeapon;
            if (viewModel != null) Destroy(viewModel);
            viewModel = new GameObject("Training weapon silhouette");
            viewModel.transform.SetParent(weapons.playerCamera.transform, false);
            bool melee = shownWeapon.category == WeaponCategory.Melee;
            var wood = RuntimeVisuals.Primitive("Runic wood", PrimitiveType.Cube, Vector3.zero, melee ? new Vector3(0.08f, 0.7f, 0.08f) : new Vector3(0.13f, 0.14f, 0.35f), new Color(0.25f, 0.13f, 0.08f), false);
            wood.transform.SetParent(viewModel.transform, false);
            var brass = RuntimeVisuals.Primitive("Brass and rune", PrimitiveType.Cube, melee ? Vector3.up * 0.35f : Vector3.forward * 0.28f,
                melee ? new Vector3(0.1f, 0.4f, 0.05f) : new Vector3(0.075f, 0.075f, 0.38f), shownWeapon.muzzleFlashColor, false);
            brass.transform.SetParent(viewModel.transform, false);
        }

        public void SetMenu(bool open)
        {
            MenuOpen = open;
            Time.timeScale = open ? 0f : 1f;
            Cursor.lockState = open ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = open;
            if (open) weapons?.CancelAction();
        }
        public void ResetSimulation(CombatTeam team, bool planted)
        {
            ArcaneWorld.Clear();
            player.team = team;
            player.ResetForRound(); weapons.ResetForRound(); arcane.ResetForRound();
            var controller = player.GetComponent<CharacterController>(); controller.enabled = false;
            player.transform.position = planted ? new Vector3(-12, 0.1f, 14) : new Vector3(0, 0.1f, -18);
            controller.enabled = true;
            foreach (var actor in targets)
            {
                actor.GetComponent<HealthSystem>().team = actor.restoredHealth < 100 ? team : team == CombatTeam.Defenders ? CombatTeam.Attackers : CombatTeam.Defenders;
                actor.ResetTarget();
            }
            dragon.ResetForRound(planted);
            Credits = 15000;
            Message = planted ? "Desperte o dragao: segure E perto dele por 4 segundos." : "Ataque: leve o dragao ao ponto A ou B e segure E. Defesa: use o exercicio de despertar.";
        }

        bool Spend(int cost)
        {
            if (FreePractice) return true;
            if (cost > Credits) { Message = "Creditos insuficientes."; return false; }
            Credits -= cost; return true;
        }

        void OnGUI()
        {
            if (!MenuOpen || catalog == null || player == null) return;
            float scale = Mathf.Min(Screen.width / 1100f, Screen.height / 720f);
            Matrix4x4 previous = GUI.matrix; GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * scale);
            GUILayout.BeginArea(new Rect(24, 24, 1052, 672), GUI.skin.box);
            GUILayout.Label("A3C | SALA DE PROJECAO DE CRUSHLE", new GUIStyle(GUI.skin.label) { fontSize = 23, fontStyle = FontStyle.Bold });
            GUILayout.Label("Treino local de armas e S.A.A. | B / Esc: equipamento | F5: reiniciar exercicio");
            GUILayout.Label(!player.IsAlive ? "Voce foi eliminado. Reinicie um exercicio." : dragon.Winner != CombatTeam.Neutral ? "Rodada encerrada: " + dragon.Winner : Message);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Treino de ataque")) { ResetSimulation(CombatTeam.Attackers, false); weapons.EquipWeapon(catalog.attackerStarter); }
            if (GUILayout.Button("Treino de defesa")) { ResetSimulation(CombatTeam.Defenders, true); weapons.EquipWeapon(catalog.defenderStarter); }
            if (GUILayout.Button("Repor municao e cargas")) { weapons.ResetForRound(); arcane.ResetForRound(); }
            FreePractice = GUILayout.Toggle(FreePractice, "Equipamento livre");
            GUILayout.Label("CR " + Credits);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(410));
            GUILayout.Label("ARSENAL | Punishment e desbloqueada pela Lust & Greed");
            weaponScroll = GUILayout.BeginScrollView(weaponScroll, GUILayout.Height(360));
            foreach (var weapon in catalog.weapons)
            {
                if (weapon.mechanic == WeaponMechanic.Punishment) continue;
                if (GUILayout.Button(weapon.weaponName + " | " + weapon.priceCR + " CR"))
                {
                    if (Spend(weapon.priceCR)) { weapons.EquipWeapon(weapon); Message = weapon.gameplaySummary; }
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Label("CARTUCHOS | Arme Q/E/C, depois dispare. E prioriza o objetivo.");
            selectedSlot = GUILayout.Toolbar(selectedSlot, new[] { "Q", "E", "C" });
            cartridgeScroll = GUILayout.BeginScrollView(cartridgeScroll, GUILayout.Height(255));
            foreach (var cartridge in catalog.cartridges)
            {
                if (!cartridge.IsCompatibleWith(weapons.currentWeapon)) continue;
                if (GUILayout.Button(cartridge.cartridgeName + " | " + cartridge.priceCR + " CR"))
                    if (Spend(cartridge.priceCR)) { arcane.Equip(selectedSlot, cartridge); Message = cartridge.gameplaySummary; }
            }
            if (!weapons.currentWeapon.SupportsArcaneCartridges) GUILayout.Label("Pistolas e armas brancas nao recebem S.A.A.");
            GUILayout.EndScrollView();
            GUILayout.Label("COLETES");
            GUILayout.BeginHorizontal();
            var vests = new[] { VestType.Leve, VestType.Energetico, VestType.Pesado, VestType.Construtivo };
            var prices = new[] { 400, 750, 1000, 650 };
            for (int i = 0; i < vests.Length; i++) if (GUILayout.Button(vests[i] + " " + prices[i])) if (Spend(prices[i])) player.EquipVest(vests[i]);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Alvos: mover / parar")) foreach (var target in targets) if (target.restoredHealth == 100) target.moving = !target.moving;
            if (GUILayout.Button("Alvos: atirar / parar")) foreach (var target in targets) if (target.restoredHealth == 100) target.firing = !target.firing;
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Label("WASD: mover | Shift: correr | Espaco: saltar | LMB: disparar | RMB: mira / segunda mao | R: recarregar");
            GUILayout.Label("Alvo verde com 40 HP para testar cura. Vermelhos usam 100 HP + 100 de escudo e reaparecem apos 3 s.");
            GUI.enabled = player.IsAlive && dragon.Winner == CombatTeam.Neutral;
            if (GUILayout.Button("ENTRAR NA SIMULACAO", GUILayout.Height(38))) SetMenu(false);
            GUI.enabled = true;
            GUILayout.EndArea(); GUI.matrix = previous;
        }
        void OnDestroy()
        {
            Time.timeScale = 1f;
            ArcaneWorld.Clear();
            if (shotSound != null) Destroy(shotSound);
        }
    }
}
