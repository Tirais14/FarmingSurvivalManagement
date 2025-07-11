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
                // Сохраняем текущую сцену, если нужно
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

                // Загружаем первую сцену из Build Settings
                if (EditorBuildSettings.scenes.Length > 0) {
                    string scenePath = EditorBuildSettings.scenes[0].path;
                    EditorSceneManager.OpenScene(scenePath);
                }
            }
        }
    }
}
