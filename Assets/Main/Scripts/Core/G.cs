#nullable enable
namespace Core
{
    public static class G
    {
#if UNITY_EDITOR
        public static class Editor
        {
            public const int GAME_ASSET_MENU_ORDER = 100;
        }
#endif
    }
}
