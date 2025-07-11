using System;
using UTIRLib.Attributes.Metadata;

namespace Game.Generated
{
    [Flags]
    public enum AssetDatabaseName
    {
        [MetaString("Tiles")]
        Tiles = 1,

        [MetaString("UI")]
        UI = 2,

        [MetaString("Core")]
        Core = 4,

        [MetaString("Farming")]
        Farming = 8,

        [MetaString("Map")]
        Map = 16,

        [MetaString("Items")]
        Items = 32,

        [MetaString("Default")]
        Default = 64,

        [MetaString("Scenes")]
        Scenes = 128,
    }
}
