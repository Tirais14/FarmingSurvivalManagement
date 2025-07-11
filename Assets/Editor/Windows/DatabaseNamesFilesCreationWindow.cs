using Game.Core;
using UnityEditor;
using UTIRLib;
using UTIRLib.FileSystem.ScriptUtils;
using UTIRLib.FileSystem;

#nullable enable
namespace Game.Editor
{
    public class DatabaseNamesFilesCreationWindow : UTIRLib.Json.Editor.DatabaseNamesFilesCreationWindow
    {
        protected override NamespaceEntry NamespaceData => new(nameof(Game), AppConstantNames.GENERATED);

        [MenuItem("Window/Custom/Create Database Names Files")]
        protected static void ShowWindow() => GetWindow<DatabaseNamesFilesCreationWindow>("Create Database Names Files");

        protected override FSPath SelectConstFileDirectoryPath()
        {
            return new FSPath(AppDirectory.MAIN_PATH, "Scripts", AppConstantNames.GENERATED);
        }

        protected override FSPath SelectEnumFileDirectoryPath()
        {
            return new FSPath(AppDirectory.MAIN_PATH, "Scripts", AppConstantNames.GENERATED);
        }
    }
}
