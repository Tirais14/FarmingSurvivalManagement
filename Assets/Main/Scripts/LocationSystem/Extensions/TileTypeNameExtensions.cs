using System;
using UnityEngine.Tilemaps;

#nullable enable
namespace Game.LocationSystem
{
    public static class TileTypeNameExtensions
    {
        /// <exception cref="NotSupportedException"></exception>
        public static Type ConvertToSystemType(this TileTypeName tileTypeName) => tileTypeName switch {
            TileTypeName.Tile => typeof(Tile),
            TileTypeName.TileExtended => typeof(TileExtended),
            _ => throw new NotSupportedException(tileTypeName.ToString())
        };
    }
}
