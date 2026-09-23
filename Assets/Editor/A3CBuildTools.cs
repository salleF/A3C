using System;
using System.IO;
using A3C.Combat;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace A3C.Editor
{
    public static class A3CBuildTools
    {
        public const string TrainingScene = "Assets/Scenes/A3C_Training.unity";
        [MenuItem("A3C/Configurar cena de treino")]
        public static void Configure()
        {
            if (!Application.isBatchMode && !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var session = new GameObject("A3C Training").AddComponent<TrainingSession>();
            session.catalog = AssetDatabase.LoadAssetAtPath<ArsenalCatalog>("Assets/Data/Arsenal/ArsenalCatalog.asset");
            if (session.catalog == null) throw new InvalidOperationException("Catalogo ausente");
            session.gameObject.AddComponent<RuntimeVerification>();
            EditorSceneManager.SaveScene(scene, TrainingScene);
            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(TrainingScene, true),
                new EditorBuildSettingsScene("Assets/Scenes/SampleScene.unity", true)
            };
            PlayerSettings.companyName = "A3C";
            PlayerSettings.productName = "A3C - Crushle Training";
            PlayerSettings.defaultScreenWidth = 1280;
            PlayerSettings.defaultScreenHeight = 720;
            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
            PlayerSettings.runInBackground = true;
            PlayerSettings.SetScriptingBackend(UnityEditor.Build.NamedBuildTarget.Standalone, ScriptingImplementation.Mono2x);
            var settings = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/GraphicsSettings.asset")[0]);
            var shaders = settings.FindProperty("m_AlwaysIncludedShaders");
            var shader = Shader.Find("Universal Render Pipeline/Unlit");
            if (shader == null) throw new InvalidOperationException("Shader URP Unlit ausente");
            bool found = false;
            for (int i = 0; i < shaders.arraySize; i++) if (shaders.GetArrayElementAtIndex(i).objectReferenceValue == shader) found = true;
            if (!found) { shaders.InsertArrayElementAtIndex(shaders.arraySize); shaders.GetArrayElementAtIndex(shaders.arraySize - 1).objectReferenceValue = shader; }
            settings.ApplyModifiedPropertiesWithoutUndo();
            AssetDatabase.SaveAssets();
            Debug.Log("A3C: cena e build Windows configurados.");
        }

        [MenuItem("A3C/Validar e gerar build Windows")]
        public static void VerifyAndBuild()
        {
            try
            {
                Configure();
                A3CVerification.Run();
                Directory.CreateDirectory("Builds/WindowsRelease");
                var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
                {
                    scenes = new[] { TrainingScene },
                    locationPathName = "Builds/WindowsRelease/A3C.exe",
                    target = BuildTarget.StandaloneWindows64,
                    options = BuildOptions.None
                });
                if (report.summary.result != BuildResult.Succeeded) throw new Exception("Build falhou: " + report.summary.result);
                Debug.Log("A3C_BUILD_PASS " + report.summary.totalSize);
                if (Application.isBatchMode) EditorApplication.Exit(0);
            }
            catch (Exception error)
            {
                Debug.LogException(error);
                if (Application.isBatchMode) EditorApplication.Exit(1);
                else throw;
            }
        }
    }
}
