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
                "Configuracao consumida pelo WeaponController. Use a cena A3C_Training para testar. " +
                "Numeros de balanceamento continuam sujeitos a playtest.", MessageType.Info);
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
                "Configuracao consumida pelo ArcaneSystem. Q/E/C arma o proximo disparo. " +
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
