"""Validate the authored Unity catalog without requiring Unity or third-party packages."""

from collections import Counter
from pathlib import Path
import json
import math
import re
import sys
from urllib.parse import unquote

ROOT = Path(__file__).resolve().parents[1]
DATA = ROOT / "Assets/Data/Arsenal"
DOCS = ROOT / "Assets/Documentation/Arsenal"
EXPECTED_WEAPONS = {
    "classic", "rustic_wand", "ghost", "trishot", "lust_greed", "punishment",
    "runic_sword", "reaper_scythe", "forge_hammer", "runic_p90", "arcane_thompson",
    "dual_mpx", "runic_sawed_off", "lever_action", "arcane_famas", "runic_vandal",
    "arcane_phantom", "arcane_guardian", "light_scout", "heavy_awp", "arcane_minigun",
}
FAMILIES = {3: "smg", 4: "shotgun", 5: "rifle", 6: "sniper", 7: "machinegun"}
ROLES = {3: (0, 1), 4: (1, 2), 5: (2, 3), 6: (3, 4), 7: (1, 2)}
ELEMENTS = ("fire", "water", "earth", "lightning", "light_dark")
ERRORS = []


def check(condition, message):
    if not condition:
        ERRORS.append(message)


def scalar(value):
    if value.startswith('"'):
        return json.loads(value)
    if value == "[]":
        return []
    if value.startswith("{"):
        return {k: scalar(v) for k, v in re.findall(r"(\w+):\s*([^,}]+)", value)}
    if re.fullmatch(r"-?\d+", value):
        return int(value)
    if re.fullmatch(r"-?\d+\.\d+(?:[eE][+-]?\d+)?", value):
        return float(value)
    return value


def read_asset(path):
    """Read the scalar/reference/list subset used by this catalog, not arbitrary YAML."""
    values = {}
    list_key = None
    record = None
    for number, line in enumerate(path.read_text(encoding="utf-8").splitlines(), 1):
        if line.startswith("  - "):
            if list_key is None:
                raise ValueError(f"{path}:{number}: list without a field")
            item = line[4:]
            if item.startswith("{"):
                values[list_key].append(scalar(item))
                record = None
            else:
                key, value = item.split(": ", 1)
                record = {key: scalar(value)}
                values[list_key].append(record)
        elif line.startswith("    ") and record is not None:
            key, value = line.strip().split(": ", 1)
            if key in record:
                raise ValueError(f"{path}:{number}: duplicate field {key}")
            record[key] = scalar(value)
        elif line.startswith("  "):
            key, value = line.strip().split(":", 1)
            if key in values:
                raise ValueError(f"{path}:{number}: duplicate field {key}")
            value = value.strip()
            record = None
            if value:
                values[key] = scalar(value)
                list_key = None
            else:
                values[key] = []
                list_key = key
    return values


def guid(path):
    meta = Path(str(path) + ".meta")
    check(meta.is_file(), f"Missing metadata: {meta.relative_to(ROOT)}")
    if not meta.is_file():
        return None
    match = re.search(r"^guid: ([a-f0-9]{32})$", meta.read_text(encoding="utf-8"), re.M)
    check(match is not None, f"Invalid GUID: {meta.relative_to(ROOT)}")
    return match.group(1) if match else None


def schema(script, class_name):
    source = script.read_text(encoding="utf-8")
    body = re.search(r"public class " + re.escape(class_name) + r"\b[^\{]*\{(.*?)\n    \}", source, re.S)
    if body is None:
        raise ValueError(f"Class {class_name} not found in {script.name}")
    return set(re.findall(r"public\s+[\w<>\[\]]+\s+(\w+)\s*(?:=(?!>)|;)", body.group(1)))


def validate_schema(asset, path, script_name, class_name):
    script = ROOT / "Assets/_Scripts/Combat" / script_name
    check(asset["m_Script"]["guid"] == guid(script), f"{path.name}: incorrect script GUID")
    check(asset["m_Script"]["fileID"] == 11500000, f"{path.name}: incorrect script fileID")
    check(asset["m_EditorClassIdentifier"] == "Assembly-CSharp::A3C.Combat." + class_name,
          f"{path.name}: incorrect serialized class")
    fields = {key for key in asset if not key.startswith("m_")}
    declared = schema(script, class_name)
    check(fields == declared, f"{path.name}: C# schema mismatch: missing {declared-fields}, extra {fields-declared}")
    check("&11400000" in path.read_text(encoding="utf-8"), f"{path.name}: incorrect asset fileID")


def table(doc):
    rows = {}
    for line in doc.splitlines():
        if line.startswith("|"):
            cells = [cell.strip() for cell in line.strip("|").split("|")]
            if len(cells) == 2:
                rows[cells[0]] = cells[1]
    return rows


def validate_weapon(w, path):
    id = w["weaponId"]
    check(path.stem == id, f"{path.name}: ID and filename differ")
    validate_schema(w, path, "WeaponData.cs", "WeaponData")
    for key in ("bodyDamage", "headshotDamage", "fireRateRPM", "pelletsPerShot", "maxSimultaneousShots"):
        check(isinstance(w[key], (int, float)) and w[key] > 0 and math.isfinite(w[key]), f"{id}: invalid {key}")
    for key in ("magazineSize", "maxReserveAmmo", "magazinePerHand", "priceCR"):
        check(isinstance(w[key], int) and w[key] >= 0, f"{id}: invalid {key}")
    check(0 <= w["baseSpread"] <= w["maxSpread"], f"{id}: spread range invalid")
    check(0 < w["chargedSpreadMultiplier"] <= 1, f"{id}: invalid choke spread multiplier")
    if w["magazinePerHand"]:
        check(w["magazineSize"] == 2 * w["magazinePerHand"], f"{id}: dual magazine total differs from hands")
    if w["category"] == 2:
        check(w["fireMode"] == 4 and w["magazineSize"] == w["maxReserveAmmo"] == 0,
              f"{id}: melee must not use ammunition")
    if w["category"] == 5:
        check(w["headshotDamage"] <= 160, f"{id}: rifle headshot exceeds 160")
    multiplier = w["pelletsPerShot"] * w["maxSimultaneousShots"]
    check(w["bodyDamage"] * multiplier < 200, f"{id}: unauthorized body hitkill")
    if id not in ("heavy_awp", "punishment"):
        check(w["headshotDamage"] * multiplier < 200, f"{id}: unauthorized simultaneous headshot hitkill")
    sheet = ROOT / w["designSheetPath"]
    check(sheet.is_file(), f"{id}: missing design sheet")
    if sheet.is_file():
        doc = sheet.read_text(encoding="utf-8")
        rows = table(doc)
        for label, field in {"Corpo por bala/bago/golpe": "bodyDamage", "Cabeça por bala/bago/golpe": "headshotDamage",
                             "Cadência (tiros ou golpes/minuto)": "fireRateRPM", "Bagos por carga disparada": "pelletsPerShot",
                             "Cargas simultâneas máximas": "maxSimultaneousShots"}.items():
            check(float(rows.get(label, "nan")) == w[field], f"{id}: sheet differs for {field}")
        check(rows.get("Pente total / reserva") == f"{w['magazineSize']} / {w['maxReserveAmmo']}", f"{id}: ammunition sheet mismatch")
        check(rows.get("Preço proposto") == f"{w['priceCR']} CR", f"{id}: price sheet mismatch")
        check(rows.get("Dano máximo simultâneo por alvo em HS") == str(w["headshotDamage"] * multiplier),
              f"{id}: simultaneous damage sheet mismatch")
        check(w["gameplaySummary"] in doc and w["implementationNotes"] in doc, f"{id}: stale description")


def validate_cartridge(c, path):
    id = c["cartridgeId"]
    validate_schema(c, path, "ArcaneCartridgeData.cs", "ArcaneCartridgeData")
    check(c["category"] in FAMILIES and c["element"] in range(5), f"{id}: invalid family or element")
    check(id == path.stem == FAMILIES[c["category"]] + "_" + ELEMENTS[c["element"]], f"{id}: inconsistent ID")
    check((c["primaryRole"], c["secondaryRole"]) == ROLES[c["category"]], f"{id}: roles conflict with gameplay")
    check(c["chargesPerPurchase"] == 3 and c["priceCR"] > 0, f"{id}: invalid purchase data")
    check(c["cooldownSeconds"] > 0 and c["castRangeMetres"] > 0, f"{id}: invalid timing/range")
    check(len(c["effects"]) > 0, f"{id}: no effects")
    for e in c["effects"]:
        check(set(e) == {"kind", "target", "anchor", "durationSeconds", "radiusMetres", "amount", "barrierHealth",
                         "barrierWidth", "barrierHeight", "rules"}, f"{id}: incomplete effect definition")
        check(e["kind"] in range(8) and e["target"] in range(4) and e["anchor"] in range(2), f"{id}: invalid effect enum")
        check(all(isinstance(e[k], (int, float)) and math.isfinite(e[k]) and e[k] >= 0
                  for k in ("durationSeconds", "radiusMetres", "amount", "barrierHealth", "barrierWidth", "barrierHeight")),
              f"{id}: negative or invalid effect parameter")
        if e["kind"] == 5:
            check(c["category"] == 6 and e["target"] == 1 and 0 < e["amount"] <= 100, f"{id}: healing outside support contract")
        else:
            check(e["durationSeconds"] > 0, f"{id}: lasting effect without lifetime")
        if e["kind"] in (1, 4, 6, 7):
            check(0 < e["amount"] < 1, f"{id}: status amount must be a fraction")
        if e["kind"] == 2:
            check(e["target"] == 3 and min(e["barrierHealth"], e["barrierWidth"], e["barrierHeight"]) > 0,
                  f"{id}: invalid barrier")
        check(e["rules"], f"{id}: missing behavior rules")
    sheet = ROOT / c["designSheetPath"]
    check(sheet.is_file(), f"{id}: missing design sheet")
    if sheet.is_file():
        doc = sheet.read_text(encoding="utf-8")
        check(c["gameplaySummary"] in doc and c["activationRules"] in doc and c["counterplay"] in doc,
              f"{id}: stale design description")
        check(f"**{c['priceCR']} CR**" in doc and f"**{c['chargesPerPurchase']} cargas**" in doc,
              f"{id}: purchase data differs from sheet")
        check(f"**{c['cooldownSeconds']} s**" in doc and f"**{c['castRangeMetres']} m**" in doc,
              f"{id}: range or cooldown differs from sheet")


def main():
    weapons = {p.stem: read_asset(p) for p in (DATA / "Weapons").glob("*.asset")}
    cartridges = [read_asset(p) for p in (DATA / "Cartridges").glob("*.asset")]
    check(set(weapons) == EXPECTED_WEAPONS, f"Weapon coverage differs: {set(weapons) ^ EXPECTED_WEAPONS}")
    pairs = Counter((c["category"], c["element"]) for c in cartridges)
    check(pairs == Counter({(family, element): 1 for family in FAMILIES for element in range(5)}), "Incomplete or duplicated 5 x 5 cartridge matrix")
    for id, w in weapons.items():
        validate_weapon(w, DATA / "Weapons" / (id + ".asset"))
    for c in cartridges:
        validate_cartridge(c, DATA / "Cartridges" / (c["cartridgeId"] + ".asset"))

    a, b = weapons["classic"], weapons["rustic_wand"]
    presentation = {"weaponId", "weaponName", "gameplaySummary", "implementationNotes", "designSheetPath", "muzzleFlashColor"}
    check({k: v for k, v in a.items() if k not in presentation and not k.startswith("m_")} ==
          {k: v for k, v in b.items() if k not in presentation and not k.startswith("m_")}, "Starter weapons are not mechanically symmetric")
    check((weapons["light_scout"]["bodyDamage"], weapons["light_scout"]["headshotDamage"]) == (85, 160), "Scout violates 85/160")
    check(weapons["heavy_awp"]["bodyDamage"] == 190 and weapons["heavy_awp"]["headshotDamage"] >= 350, "AWP violates 190/350+")
    check(weapons["punishment"]["headshotDamage"] == 666 and weapons["punishment"]["magazineSize"] == 1, "Punishment violates 666/one shot")
    check(weapons["lust_greed"]["magazinePerHand"] == 6 and weapons["lust_greed"]["maxReserveAmmo"] == 21 and
          weapons["lust_greed"]["shotsToUnlockSpecial"] == 12, "Lust & Greed violates 6+6/21/12")
    check(weapons["lust_greed"]["specialWeapon"]["guid"] == guid(DATA / "Weapons/punishment.asset"), "Missing Punishment reference")
    check(weapons["trishot"]["magazinePerHand"] == 2 and weapons["trishot"]["pelletsPerShot"] == 3 and
          weapons["trishot"]["maxSimultaneousShots"] == 2, "TriShot violates two per hand/3 or 6 pellets")
    check(not weapons["ghost"]["visibleTracer"] and weapons["ghost"]["silenced"], "Ghost presentation violates gameplay")
    check(weapons["arcane_famas"]["fireMode"] == 2 and weapons["arcane_famas"]["burstCount"] == 3, "Missing FAMAS burst data")
    check(weapons["lever_action"]["chokeChargeTime"] > 0 and weapons["lever_action"]["chargedSpreadMultiplier"] < 1,
          "Missing choke data")

    catalog = read_asset(DATA / "ArsenalCatalog.asset")
    validate_schema(catalog, DATA / "ArsenalCatalog.asset", "ArsenalCatalog.cs", "ArsenalCatalog")
    for key, folder in (("weapons", "Weapons"), ("cartridges", "Cartridges")):
        actual = Counter(r["guid"] for r in catalog[key])
        expected = Counter(guid(p) for p in (DATA / folder).glob("*.asset"))
        check(actual == expected, f"Catalog {key} references missing, extra or duplicate assets")
        check(all(r["fileID"] == 11400000 and r["type"] == 2 for r in catalog[key]), f"Catalog {key} has invalid asset reference type")
    check(catalog["defenderStarter"]["guid"] == guid(DATA / "Weapons/classic.asset") and
          catalog["attackerStarter"]["guid"] == guid(DATA / "Weapons/rustic_wand.asset"), "Incorrect starter references")

    known_guids = {}
    for meta in (ROOT / "Assets").rglob("*.meta"):
        value = guid(Path(str(meta)[:-5]))
        check(value not in known_guids, f"Duplicate GUID: {meta.relative_to(ROOT)} and {known_guids.get(value)}")
        known_guids[value] = meta.relative_to(ROOT)
    for folder in (DATA, ROOT / "Assets/Documentation", ROOT / "Assets/Editor"):
        for path in [folder, *folder.rglob("*")]:
            if path.suffix != ".meta":
                guid(path)
    for path in DATA.rglob("*.asset"):
        for value in re.findall(r"guid: ([a-f0-9]{32})", path.read_text(encoding="utf-8")):
            check(value in known_guids, f"{path.name}: unresolved GUID {value}")

    docs = [*DOCS.rglob("*.md"), ROOT / "Assets/Documentation/A3C_Arsenal_Design.md",
            ROOT / "Assets/Documentation/A3C_Project_Review.md"]
    check(len(list((DOCS / "Weapons").glob("*.md"))) == 21 and len(list((DOCS / "Spells").glob("*.md"))) == 25,
          "Individual sheet coverage differs from catalog")
    for path in docs:
        text = path.read_text(encoding="utf-8")
        check("\u2014" not in text and "\ufffd" not in text, f"{path.name}: forbidden dash or broken encoding")
        for target in re.findall(r"\]\(([^)]+)\)", text):
            if "://" not in target and not target.startswith("#"):
                link = unquote(target.split("#", 1)[0])
                check((path.parent / link).exists(), f"{path.name}: broken link {target}")

    if ERRORS:
        print("FAIL: catalog validation")
        for error in ERRORS:
            print("- " + error)
        return 1
    print(f"PASS: {len(weapons)} weapons, {len(cartridges)} cartridges, 46 individual sheets.")
    print("PASS: gameplay limits, starter symmetry, S.A.A. matrix, C# field schemas, GUIDs, catalog references and documentation links.")
    print("Unity import, compilation and Play Mode are not performed by this check.")
    return 0


if __name__ == "__main__":
    try:
        sys.exit(main())
    except (KeyError, TypeError, ValueError, OSError) as error:
        print(f"FAIL: invalid or missing catalog data: {error}")
        sys.exit(1)
