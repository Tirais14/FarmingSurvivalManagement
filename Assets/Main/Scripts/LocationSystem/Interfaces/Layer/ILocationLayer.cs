using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

#nullable enable
namespace Game.LocationSystem
{
    public interface ILocationLayer : IReadOnlyLocationLayer, IEnumerable<ILocationCell>
    {
        event Action<ILocationCell> OnCellChanged;

        Tilemap TilemapComponent { get; }
        public TilemapRenderer TilemapRendererComponent { get; }
        new ILocationCell this[Vector2Int position] { get; }
        new ILocationCell this[Vector3 position] { get; }

        void SetTile(Vector2Int position, TileBase? tile);
        void SetTile(Vector3 position, TileBase? tile);

        new ILocationCell GetCell(Vector2Int position);
        new ILocationCell GetCell(Vector3 position);

        bool TryGetCell(Vector2Int position, [NotNullWhen(true)] out ILocationCell? locationCell);
        bool TryGetCell(Vector3 position, [NotNullWhen(true)] out ILocationCell? locationCell);

        void ClearCell(Vector2Int position);
        void ClearCell(Vector3 position);
    }
}
