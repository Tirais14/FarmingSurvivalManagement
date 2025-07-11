namespace Game.Core
{
    public static class FileExtensions
    {
        public const string DOT_PREFAB = ".prefab";
        public const string DOT_ASSET = ".asset";
        public const string DOT_INPUT_ACTION_ASSET = ".inputactions";
        public const string DOT_JSON = ".json";
        public static string PrefabWithoutDot => RemoveDot(DOT_PREFAB);
        public static string AssetWithoutDot => RemoveDot(DOT_ASSET);
        public static string InputActionAssetWithoutDot => RemoveDot(DOT_INPUT_ACTION_ASSET);
        public static string JsonWithoutDot => RemoveDot(DOT_JSON);

        private static string RemoveDot(string extension) => extension[1..];
    }
}