using System.IO;

namespace Game.Core
{
    public static class OuterDatabaseDirectory
    {
        public readonly static string assetConfigsDirectoryPath;

        public static string MainDirectoryPath => AppDirectory.outerDatabaseDirectoryPath;

        static OuterDatabaseDirectory()
        {
            assetConfigsDirectoryPath = Path.Combine(MainDirectoryPath, AppConstantNames.CONFIGS);
        }
    }
}
