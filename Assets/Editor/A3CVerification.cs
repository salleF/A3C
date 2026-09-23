using System;
using System.IO;
using System.Linq;
using A3C.Combat;
using A3C.Objective;
using UnityEditor;
using UnityEngine;

namespace A3C.Editor
{
    public static class A3CVerification
    {
        private static int assertions;
        public static void Check(bool condition, string message)
        {
            assertions++;
            if (!condition) throw new InvalidOperationException("A3C verification: " + message);
        }
        [MenuItem("A3C/Validar regras de combate")]
        public static void Run()
        {
            assertions = 0;
            var catalog = AssetDatabase.LoadAssetAtPath<ArsenalCatalog>("Assets/Data/Arsenal/ArsenalCatalog.asset");
            Check(catalog != null && catalog.weapons.Length == 21 && catalog.cartridges.Length == 25, "Cobertura do catalogo");
            WeaponData Find(string id) => catalog.weapons.Single(w => w.weaponId == id);
            foreach (var weapon in catalog.weapons)
            {
                var state = new WeaponState(weapon);
                int initial = state.Ammo + state.Reserve;
                if (weapon.fireMode == FireMode.Melee) { Check(state.Consume(false) == 1 && state.Ammo == 0, "Melee sem municao"); continue; }
                state.Consume(false);
                if (weapon.mechanic != WeaponMechanic.Punishment)
                {
                    state.BeginReload(); state.FinishReload();
                    Check(state.Ammo + state.Reserve == initial - 1, weapon.weaponId + " conserva municao");
                }
                foreach (var cartridge in catalog.cartridges)
                    Check(cartridge.IsCompatibleWith(weapon) == (weapon.SupportsArcaneCartridges && weapon.category == cartridge.category), "Compatibilidade " + weapon.weaponId);
            }
            var dual = new WeaponState(Find("trishot"));
            Check(dual.Consume(true) == 2 && dual.Left == 1 && dual.Right == 1, "TriShot ambas as maos");
            dual.Consume(false);
            Check(dual.Consume(true) == 1 && dual.Ammo == 0, "TriShot fallback uma mao");
            var lust = new WeaponState(Find("lust_greed"));
            for (int i = 0; i < 6; i++) lust.Consume(false);
            Check(lust.Consume(false) == 0 && lust.Right == 6, "Mao vazia nao dispara a outra");
            for (int i = 0; i < 6; i++) lust.Consume(true);
            Check(lust.PunishmentReady && lust.ActiveWeapon == Find("punishment"), "Sequencia de 12 libera Punishment");
            lust.Consume(false);
            Check(!lust.PunishmentReady && lust.Left == 6 && lust.Right == 6 && lust.Reserve == 21, "Retorno especial 6+6 sem custo de reserva");
            lust.Consume(false); lust.BeginReload(); lust.FinishReload();
            Check(lust.Reserve == 20 && lust.Left == 6, "Recarga manual debita reserva");
            lust.Reset();
            for (int i = 0; i < 5; i++) lust.Consume(false);
            lust.BeginReload();
            for (int i = 0; i < 6; i++) lust.Consume(true);
            lust.Consume(false);
            Check(!lust.PunishmentReady, "Recarga interrompida cancela sequencia");
            var root = new GameObject("A3C verification objects");
            try
            {
                HealthSystem Actor(string name, CombatTeam team)
                {
                    var go = new GameObject(name); go.transform.SetParent(root.transform);
                    var hp = go.AddComponent<HealthSystem>(); hp.team = team; hp.ResetForRound(); return hp;
                }
                var attacker = Actor("Attacker", CombatTeam.Attackers);
                var defender = Actor("Defender", CombatTeam.Defenders);
                defender.ApplyDamage(160, attacker);
                Check(defender.currentHealth == 40 && defender.currentShield == 0, "Rifle HS deixa 40 HP");
                defender.ResetForRound(); defender.ApplyDamage(190, attacker);
                Check(defender.currentHealth == 10, "AWP corpo deixa 10 HP");
                defender.Heal(200);
                Check(defender.currentHealth == 100 && defender.currentShield == 0, "Cura nao vira escudo");
                defender.ApplyDamage(350, attacker); defender.ApplyDamage(350, attacker);
                Check(attacker.kills == 1, "Abate contabilizado uma vez");
                defender.Heal(50); Check(!defender.IsAlive, "Cura nao ressuscita");
                attacker.EquipVest(VestType.Construtivo);
                for (int i = 0; i < 8; i++) attacker.RegisterKill();
                Check(attacker.currentShield == 100, "Construtivo limitado a 100");
                attacker.ResetForRound(); Check(attacker.currentShield == 0, "Construtivo zera na rodada");
                defender.ResetForRound(); defender.EquipVest(VestType.Energetico); defender.ApplyDamage(30, attacker);
                defender.TickRegeneration(5.9f); Check(defender.currentShield == 20, "Energetico espera 6 segundos");
                defender.TickRegeneration(1.1f); Check(Mathf.Abs(defender.currentShield - 35) < 0.01f, "Regen conta so tempo ativo");
                defender.TickRegeneration(20); Check(defender.currentShield == 50, "Regen para em 50");
                Check(!defender.ApplyDamage(float.NaN, attacker) && !defender.ApplyDamage(-1, attacker), "Dano invalido rejeitado");
                attacker.team = CombatTeam.Defenders;
                Check(!defender.ApplyDamage(100, attacker), "Sem fogo amigo");
                var status = defender.gameObject.AddComponent<CombatStatus>();
                status.Apply(1, ArcaneEffectKind.Slow, 0.2f, 10); status.Apply(2, ArcaneEffectKind.Slow, 0.4f, 10);
                Check(Mathf.Abs(status.MovementMultiplier - 0.6f) < 0.001f, "Lentidao nao soma");
                status.Remove(2, ArcaneEffectKind.Slow); Check(Mathf.Abs(status.MovementMultiplier - 0.8f) < 0.001f, "Fonte restante preservada");
                var dragon = new GameObject("Dragon").AddComponent<SleepingDragon>(); dragon.transform.SetParent(root.transform);
                dragon.plantSites = new[] { Vector3.zero }; dragon.ResetForRound();
                attacker.team = CombatTeam.Attackers; attacker.transform.position = Vector3.zero;
                dragon.Interact(attacker, true, 2); dragon.Interact(attacker, false, 1);
                Check(!dragon.isPlanted && dragon.Progress == 0, "Interrupcao zera plantio");
                dragon.Interact(attacker, true, 3); Check(dragon.isPlanted, "Plantio completo");
                int results = 0; dragon.OnRoundEnded += _ => results++;
                dragon.Tick(60); dragon.Tick(60);
                Check(dragon.Winner == CombatTeam.Attackers && results == 1, "Corrupcao termina uma vez");
                dragon.ResetForRound(true); defender.transform.position = new Vector3(10, 0, 0);
                dragon.Interact(defender, true, 4); Check(dragon.Winner == CombatTeam.Neutral, "Nao despertar de longe");
                defender.transform.position = Vector3.zero; dragon.Interact(defender, true, 4);
                Check(dragon.Winner == CombatTeam.Defenders, "Despertar em alcance por 4 segundos");
            }
            finally { UnityEngine.Object.DestroyImmediate(root); ArcaneWorld.Clear(); }
            Directory.CreateDirectory("Logs");
            File.WriteAllText("Logs/a3c-editor-verification.json", "{\"passed\":true,\"assertions\":" + assertions + ",\"unity\":\"" + Application.unityVersion + "\"}");
            Debug.Log("A3C_EDITOR_TESTS_PASS " + assertions);
        }
    }
}
