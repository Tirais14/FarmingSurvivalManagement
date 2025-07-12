#nullable enable
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map
{
    public interface ILocationCell : IDisposable
    {
        Vector3Int Position { get; }
        ILocationLayer Parent { get; }
        TileBase? Tile { get; set; }
        bool HasTile { get; }

        event Action<ILocationCell> OnTileChanged;
    }
}
