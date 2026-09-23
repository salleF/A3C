using A3C.Combat;
using UnityEditor;
using UnityEngine;

namespace A3C.Editor
{
    [CustomEditor(typeof(WeaponData))]
    public class WeaponDataInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "Asset de dados. Os parametros de design nao implementam as mecanicas. " +
                "Consulte Implementation Notes e a ficha antes de equipar no prototipo.", MessageType.Info);
            DrawDefaultInspector();
            var weapon = (WeaponData)target;
            EditorGUILayout.LabelField("Aceita S.A.A.", weapon.SupportsArcaneCartridges ? "Sim" : "Nao");
        }
    }

    [CustomEditor(typeof(ArcaneCartridgeData))]
    public class ArcaneCartridgeDataInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "Especificacao configuravel. O prototipo ainda nao executa cartuchos ou efeitos. " +
                "Custos e duracoes sao propostas para playtest.", MessageType.Info);
            DrawDefaultInspector();
        }
    }

    public static class ArsenalCatalogMenu
    {
        [MenuItem("A3C/Arsenal/Selecionar catalogo")]
        public static void SelectCatalog()
        {
            var catalog = AssetDatabase.LoadAssetAtPath<ArsenalCatalog>("Assets/Data/Arsenal/ArsenalCatalog.asset");
            if (catalog == null)
            {
                Debug.LogError("Catalogo nao encontrado em Assets/Data/Arsenal/ArsenalCatalog.asset.");
                return;
            }
            Selection.activeObject = catalog;
            EditorGUIUtility.PingObject(catalog);
        }
    }
}
