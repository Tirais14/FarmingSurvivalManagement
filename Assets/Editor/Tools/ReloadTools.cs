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

            // 1. ������ ��� ������������ ������ (Domain Reload)
            if (GUILayout.Button("Reload Domain")) {
                ReloadScriptDomain();
            }

            // 2. ������ ��� ������������ ������� �����
            if (GUILayout.Button("Reload Current Scene")) {
                ReloadCurrentScene();
            }

            // 3. ������ ��� ������������ ������ � �����
            if (GUILayout.Button("Reload Domain & Scene")) {
                ReloadScriptDomain();
                ReloadCurrentScene();
            }
        }

        // ����� ��� �������������� ������������ Script Domain
        private static void ReloadScriptDomain()
        {
            EditorUtility.RequestScriptReload();
            Debug.Log("Script Domain Reloaded!");
        }

        // ����� ��� ������������ ������� �����
        private static void ReloadCurrentScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            EditorSceneManager.OpenScene(currentScene.path);
            Debug.Log("Scene Reloaded: " + currentScene.name);
        }
    }
}
