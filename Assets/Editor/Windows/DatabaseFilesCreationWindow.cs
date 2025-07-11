using Game.Generated;
using UnityEditor;
using UTIRLib;
using UTIRLib.FileSystem;

#nullable enable
namespace Game.Editor
{
    public class DatabaseFilesCreationWindow : UTIRLib.FileSystem.Json.Editor.DatabaseFilesCreationWindow
    {
        [MenuItem("Window/Custom/Create Database Files")]
        protected static void ShowWindow() => GetWindow<DatabaseFilesCreationWindow>("Database Files Creation");

        protected override FSPath SelectSaveDirectory() => ResourcesDirectory.Databases.ToFilePath();
    }
}
