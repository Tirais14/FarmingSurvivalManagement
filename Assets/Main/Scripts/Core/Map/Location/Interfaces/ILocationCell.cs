#nullable enable
using UnityEngine.Tilemaps;

namespace Core.Map
{
    public interface ILocationCell
    {
        TileBase Tile { get; }
    }
}
