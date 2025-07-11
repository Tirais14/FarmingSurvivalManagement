using UnityEditor;
using UnityEditor.SceneManagement;

#nullable enable
namespace Game.UnityEditorCustom
{
    [InitializeOnLoad]
    public static class PlayModeSceneLoader
    {
        static PlayModeSceneLoader()
        {
            EditorApplication.playModeStateChanged += LoadDefaultSceneOnPlayMode;
        }

        private static void LoadDefaultSceneOnPlayMode(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode) {
                // ��������� ������� �����, ���� �����
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

                // ��������� ������ ����� �� Build Settings
                if (EditorBuildSettings.scenes.Length > 0) {
                    string scenePath = EditorBuildSettings.scenes[0].path;
                    EditorSceneManager.OpenScene(scenePath);
                }
            }
        }
    }
}
