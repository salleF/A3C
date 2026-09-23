using System;
using System.Collections;
using System.IO;
using System.Linq;
using A3C.Player;
using UnityEngine;

namespace A3C.Combat
{
    public class RuntimeVerification : MonoBehaviour
    {
        private int assertions;
        private string errorLog;
        private void Check(bool condition, string message)
        {
            assertions++;
            if (!condition) throw new InvalidOperationException(message);
        }
        IEnumerator Start()
        {
            if (!Environment.GetCommandLineArgs().Contains("-a3c-verify")) yield break;
            Application.logMessageReceived += CaptureError;
            yield return null;
            var run = Run();
            while (true)
            {
                object step;
                try
                {
                    if (!run.MoveNext()) break;
                    step = run.Current;
                }
                catch (Exception error) { Finish(false, error.ToString()); yield break; }
                yield return step;
            }
            Finish(string.IsNullOrEmpty(errorLog), errorLog);
        }
        void CaptureError(string message, string stack, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert) errorLog += message + "\n" + stack;
        }
        IEnumerator Run()
        {
            var session = GetComponent<TrainingSession>();
            Check(session != null && session.player != null && session.dragon != null, "Cena inicializada");
            session.SetMenu(false); session.enabled = false;
            session.player.GetComponent<PlayerMovement>().enabled = false;
            session.weapons.playerCamera.GetComponent<MouseLook>().enabled = false;
            session.weapons.acceptPlayerInput = false; session.arcane.acceptPlayerInput = false;
            session.player.GetComponent<CharacterController>().enabled = false;
            session.player.transform.SetPositionAndRotation(new Vector3(0, 0, 1000), Quaternion.identity);
            session.weapons.playerCamera.transform.localRotation = Quaternion.identity;
            session.player.team = CombatTeam.Defenders;
            WeaponData Find(string id) => session.catalog.weapons.Single(w => w.weaponId == id);
            var victim = new GameObject("Verification victim");
            victim.transform.position = new Vector3(0, 0, 1010);
            var health = victim.AddComponent<HealthSystem>(); health.team = CombatTeam.Attackers;
            var head = RuntimeVisuals.Primitive("Test head", PrimitiveType.Cube, new Vector3(0, 1.65f, 0), Vector3.one, Color.red);
            head.transform.SetParent(victim.transform, false); head.AddComponent<Hitbox>().head = true;
            Physics.SyncTransforms();
            session.weapons.EquipWeapon(Find("runic_vandal"));
            Check(session.weapons.TryFire(), "Vandal dispara");
            Check(health.currentHealth == 100, "Projetil nao e hitscan instantaneo");
            yield return new WaitForSeconds(0.25f);
            Check(health.currentHealth == 40 && health.currentShield == 0, "Projetil atravessa frames e aplica HS 160 uma vez");
            health.ResetForRound(); head.GetComponent<Hitbox>().head = false;
            session.weapons.EquipWeapon(Find("heavy_awp")); session.weapons.ResetForRound();
            Check(session.weapons.TryFire(), "AWP dispara");
            yield return new WaitForSeconds(0.25f);
            Check(health.currentHealth == 10, "AWP corpo real 190");
            head.GetComponent<Hitbox>().head = true; health.ResetForRound();
            session.weapons.ResetForRound(); session.weapons.TryFire();
            yield return new WaitForSeconds(0.25f);
            Check(!health.IsAlive, "AWP HS elimina");
            health.ResetForRound();
            var wall = RuntimeVisuals.Primitive("Verification cover", PrimitiveType.Cube, new Vector3(0, 1.5f, 1005), new Vector3(4, 3, 0.5f), Color.gray);
            Physics.SyncTransforms(); session.weapons.ResetForRound(); session.weapons.TryFire();
            yield return new WaitForSeconds(0.25f);
            Check(health.currentHealth == 100 && health.currentShield == 100, "Cobertura bloqueia projetil");
            wall.GetComponent<Collider>().enabled = false; Destroy(wall);
            session.weapons.EquipWeapon(Find("arcane_famas")); session.weapons.ResetForRound();
            int shots = 0; Action<WeaponData> count = _ => shots++; session.weapons.OnShot += count;
            session.weapons.TryFire(); yield return new WaitForSeconds(0.4f);
            Check(shots == 3 && session.weapons.currentAmmo == Find("arcane_famas").magazineSize - 3, "Rajada tres tiros consome tres balas");
            session.weapons.OnShot -= count;
            session.weapons.Reload(); session.weapons.EquipWeapon(Find("classic"));
            yield return new WaitForSeconds(Find("arcane_famas").reloadTime + 0.1f);
            session.weapons.EquipWeapon(Find("arcane_famas"));
            Check(session.weapons.currentAmmo == Find("arcane_famas").magazineSize - 3, "Troca cancela recarga sem repor municao");
            session.weapons.EquipWeapon(Find("trishot")); session.weapons.ResetForRound(); health.ResetForRound();
            int countBefore = FindObjectsByType<Projectile>().Length;
            session.weapons.TryFire(true);
            Check(FindObjectsByType<Projectile>().Length == countBefore + 6, "TriShot gera seis bagos no duplo");
            yield return new WaitForSeconds(0.3f);
            Check(health.IsAlive, "TriShot nao elimina 200 HP num duplo");
            session.weapons.EquipWeapon(Find("arcane_minigun")); session.weapons.ResetForRound();
            Check(!session.weapons.TryFire(), "Minigun nao dispara fria");
            session.weapons.AdvanceSpinUp(true, 0.1f);
            Check(!session.weapons.TryFire(), "Aquecimento parcial nao dispara");
            session.weapons.AdvanceSpinUp(true, Find("arcane_minigun").spinUpTime);
            Check(session.weapons.TryFire(), "Minigun aquecida dispara");
            session.weapons.AdvanceSpinUp(false, 0);
            Check(session.weapons.Charge == 0, "Soltar gatilho zera aquecimento");
            yield return new WaitForSeconds(0.3f);
            var floor = RuntimeVisuals.Primitive("Verification floor", PrimitiveType.Cube, new Vector3(0, -0.25f, 1010), new Vector3(30, 0.5f, 30), Color.gray);
            health.ResetForRound();
            foreach (var cartridge in session.catalog.cartridges)
            {
                ArcaneWorld.Clear();
                var compatible = session.catalog.weapons.First(w => w.category == cartridge.category);
                session.weapons.EquipWeapon(compatible); session.weapons.ResetForRound();
                Check(session.arcane.Equip(0, cartridge), "Equip " + cartridge.cartridgeId);
                Check(session.arcane.Arm(0), "Arm " + cartridge.cartridgeId);
                var cast = session.arcane.ConsumeForShot(compatible, session.player.transform.position + Vector3.up, Vector3.forward);
                Check(cast != null && session.arcane.slots[0].charges == 2, "Uma carga " + cartridge.cartridgeId);
                Physics.SyncTransforms();
                cast.Impact(new Vector3(0, 0, 1007), Vector3.up);
                int effects = ArcaneWorld.Root.childCount;
                cast.Impact(new Vector3(0, 0, 1007), Vector3.up);
                Check(cast.ImpactConsumed && effects == ArcaneWorld.Root.childCount, "Um impacto " + cartridge.cartridgeId);
                Check(!session.arcane.Arm(0), "Cooldown " + cartridge.cartridgeId);
                yield return null;
            }
            ArcaneWorld.Clear();
            var barrierEffect = new ArcaneEffectDefinition { kind = ArcaneEffectKind.Barrier, target = ArcaneTarget.World, barrierHealth = 100, barrierWidth = 3, barrierHeight = 2, barrierDepth = 0.4f, durationSeconds = 5 };
            Physics.SyncTransforms();
            var barrier = ArcaneBarrier.Create(barrierEffect, Color.cyan, new Vector3(5, 0, 1007), Vector3.forward, session.player);
            Check(barrier != null, "Barreira criada em piso livre");
            barrier.TakeDamage(100); Check(!barrier.GetComponent<Collider>().enabled, "Barreira quebrada libera colisao");
            session.player.currentHealth = 50; session.player.currentShield = 0;
            ArcaneWorld.Apply(new ArcaneEffectDefinition { kind = ArcaneEffectKind.Heal, target = ArcaneTarget.Allies, anchor = ArcaneAnchor.Caster, amount = 20, radiusMetres = 3 }, Color.green, session.player.transform.position + Vector3.up, Vector3.forward, session.player);
            Check(session.player.currentHealth == 70 && session.player.currentShield == 0, "Cura executada em runtime");
            ArcaneWorld.Apply(new ArcaneEffectDefinition { kind = ArcaneEffectKind.Smoke, target = ArcaneTarget.World, durationSeconds = 0.1f, radiusMetres = 2 }, Color.gray, new Vector3(0, 1, 1005), Vector3.forward, session.player);
            Check(ArcaneWorld.SmokeBlocks(new Vector3(0, 1, 1000), new Vector3(0, 1, 1010)), "Fumaca bloqueia visao");
            yield return new WaitForSeconds(0.2f);
            Check(!ArcaneWorld.SmokeBlocks(new Vector3(0, 1, 1000), new Vector3(0, 1, 1010)), "Fumaca expira");
            Destroy(victim); Destroy(floor); ArcaneWorld.Clear();
        }
        [Serializable] private class Result { public bool passed; public int assertions; public string unity; public string error; }
        void Finish(bool passed, string error)
        {
            Application.logMessageReceived -= CaptureError;
            string[] args = Environment.GetCommandLineArgs();
            int index = Array.IndexOf(args, "-a3c-report");
            string path = index >= 0 && index + 1 < args.Length ? args[index + 1] : Path.Combine(Application.persistentDataPath, "runtime-verification.json");
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path)));
            File.WriteAllText(path, JsonUtility.ToJson(new Result { passed = passed, assertions = assertions, unity = Application.unityVersion, error = error }, true));
            Debug.Log(passed ? "A3C_RUNTIME_TESTS_PASS " + assertions : "A3C_RUNTIME_TESTS_FAIL " + error);
            Application.Quit(passed ? 0 : 1);
        }
    }
}
