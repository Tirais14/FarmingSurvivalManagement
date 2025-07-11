using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable S2325 // Methods and properties that don't access instance data should be static
namespace Game.UnityEditorCustom
{
    public class ReloadTools : EditorWindow
    {
        [MenuItem("Tools/Reload Tools")]
        public static void ShowWindow()
        {
            GetWindow<ReloadTools>("Reload Tools");
        }

        private void OnGUI()
        {
            GUILayout.Label("Domain & Scene Reload", EditorStyles.boldLabel);

            // 1. Кнопка для перезагрузки домена (Domain Reload)
            if (GUILayout.Button("Reload Domain")) {
                ReloadScriptDomain();
            }

            // 2. Кнопка для перезагрузки текущей сцены
            if (GUILayout.Button("Reload Current Scene")) {
                ReloadCurrentScene();
            }

            // 3. Кнопка для перезагрузки домена И сцены
            if (GUILayout.Button("Reload Domain & Scene")) {
                ReloadScriptDomain();
                ReloadCurrentScene();
            }
        }

        // Метод для принудительной перезагрузки Script Domain
        private static void ReloadScriptDomain()
        {
            EditorUtility.RequestScriptReload();
            Debug.Log("Script Domain Reloaded!");
        }

        // Метод для перезагрузки текущей сцены
        private static void ReloadCurrentScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            EditorSceneManager.OpenScene(currentScene.path);
            Debug.Log("Scene Reloaded: " + currentScene.name);
        }
    }
}
