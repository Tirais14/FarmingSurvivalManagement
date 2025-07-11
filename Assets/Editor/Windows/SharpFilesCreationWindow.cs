using System.IO;
using System.Linq;
using Game.Core;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;
using UnityEngine.UIElements;
using UTIRLib.Editor;
using UTIRLib.Extensions;
using UTIRLib.FileSystem.Editor;
using UTIRLib.FileSystem.ScriptUtils;
using UTIRLib.Utils;
using UTIRLib.FileSystem;
using UTIRLib;

#nullable enable
namespace Game.UnityEditorCustom
{
    public sealed class SharpFilesCreationWindow : TirLibEditorWindow
    {
        private readonly static string[] excludeDirectories = new string[]{
            "UTIRLib",
        };
        private Toggle directoryPathsToggle = null!;
        private Toggle includeEnumsPathsToggle = null!;

        private VisualElement separator = null!;
        private Toggle assetNamesToggle = null!;
        private Toggle includeEnumsNamesToggle = null!;

        private VisualElement empty = null!;
        private Button updateInfoButton = null!;

        [MenuItem("Window/Custom/Create Auxilary Files")]
        public static void ShowWindow() => GetWindow<SharpFilesCreationWindow>("Auxilary Files Creation");

        protected override void ConstructUIElements()
        {
            base.ConstructUIElements();

            directoryPathsToggle = new Toggle("Directory Paths");
            includeEnumsPathsToggle = new Toggle("Include Enum");

            separator = GetSeparatorElement();
            assetNamesToggle = new Toggle("Asset Names");
            includeEnumsNamesToggle = new Toggle("Include Enum");

            empty = GetEmptyElement(5);

            updateInfoButton = new Button(OnUpdateInfoClick) {
                text = "Update Info"
            };
        }

        private void OnUpdateInfoClick()
        {
            UpdateDirectoryPaths();
            UpdateAssetNames();

            if (directoryPathsToggle.value || includeEnumsPathsToggle.value
                || assetNamesToggle.value || includeEnumsNamesToggle.value) {
                AssetDatabase.Refresh();
            }
        }

        private void UpdateDirectoryPaths()
        {
            string saveDirectory = Path.Combine(AppDirectory.MAIN_PATH, "Scripts/Generated/Directories").ToFilePath();

            if (saveDirectory.IsNullOrEmpty()) {
                Abort("Not selected directory.");
                return;
            }
            if (!Directory.Exists(saveDirectory)) {
                Debug.LogException(new DirectoryNotFoundException(saveDirectory));
            }

            string[] directories = Directory.GetDirectories(
                AppDirectory.MAIN_PATH,
                "*",
                SearchOption.TopDirectoryOnly).
                    Select(x => FSPathHelper.SetStyle(x, PathStyle.Universal)).
                    Where(directory => excludeDirectories.All(exclude => !directory.Contains(exclude))).
                    ToArray();

            DirectoryInfoFilesCreator creator = new(directories,
                                                        excludeDirectories,
                                                        excludeByFullName: false,
                                                        isEnum: false,
                                                        nameof(Game),
                                                        "Generated");

            if (directoryPathsToggle.value) {
                ScriptFile[] files = creator.GetFiles();
                foreach (var file in files) {
                    file.SetPath(saveDirectory);

                    file.TrySave();
                }
            }

            if (includeEnumsPathsToggle.value) {
                creator.IsEnum = true;

                ScriptFile[] files = creator.GetFiles();
                foreach (var file in files) {
                    file.SetPath(saveDirectory);

                    file.TrySave();
                }
            }
        }

        private void UpdateAssetNames()
        {
            string saveDirectory = Path.Combine(AppDirectory.MAIN_PATH, "Scripts/Generated/AssetNames");

            if (saveDirectory.IsNullOrEmpty()) {
                Abort("Not selected directory.");
                return;
            }
            if (!Directory.Exists(saveDirectory)) {
                Debug.LogException(new DirectoryNotFoundException(saveDirectory));
            }

            AssetNamesCreator creator = new(AddressableAssetSettingsDefaultObject.Settings,
                                            Helper.ArrayFrom(nameof(Game), "Generated"));

            if (assetNamesToggle.value) {
                ScriptFile[] files = creator.GetFiles();
                foreach (var file in files) {
                    file.SetPath(saveDirectory);

                    file.Save();
                }
            }

            if (includeEnumsNamesToggle.value) {
                creator.IsEnum = true;

                ScriptFile[] files = creator.GetFiles();
                foreach (var file in files) {
                    file.SetPath(saveDirectory);

                    file.TrySave();
                }
            }
        }
    }
}
