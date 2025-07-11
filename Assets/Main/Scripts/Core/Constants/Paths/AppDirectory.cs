using System.IO;
using UnityEngine;
using UTIRLib;
using UTIRLib.Extensions;

namespace Game.Core
{
    using static G;
    public static class AppDirectory
    {
        public const string MAIN_PATH =
            AppConstantNames.ASSETS + PATH_SEPARATOR + AppConstantNames.MAIN + PATH_SEPARATOR;

        public const string TEXTURES_DIRECTORY = MAIN_PATH + "Textures" + PATH_SEPARATOR;

        public readonly static string rootDirectoryPath =
            rootDirectoryPath = Application.dataPath.Delete(AppConstantNames.ASSETS);
        public readonly static string outerDatabaseDirectoryPath = Path.Combine(rootDirectoryPath, AppConstantNames.DATABASE);
    }
}
