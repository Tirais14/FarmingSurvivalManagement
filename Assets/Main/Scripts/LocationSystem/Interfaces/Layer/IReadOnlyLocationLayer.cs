#nullable enable
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.LocationSystem
{
    public interface IReadOnlyLocationLayer : IEnumerable<IReadOnlyLocationCell>
    {
        string LayerName { get; }
        BoundsInt Bounds { get; }
        ILocationMap LocationMap { get; }
        IReadOnlyLocationCell this[Vector2Int position] { get; }
        IReadOnlyLocationCell this[Vector3 position] { get; }

        TileBase? GetTile(Vector2Int position);
        TileBase? GetTile(Vector3 position);
        T? GetTile<T>(Vector2Int position) where T : TileBase;
        T? GetTile<T>(Vector3 position) where T : TileBase;

        bool TryGetTile(Vector2Int position, [NotNullWhen(true)] out TileBase? tile);
        bool TryGetTile(Vector3 position, [NotNullWhen(true)] out TileBase? tile);
        bool TryGetTile<T>(Vector2Int position, [NotNullWhen(true)] out T? tile) where T : TileBase;
        bool TryGetTile<T>(Vector3 position, [NotNullWhen(true)] out T? tile) where T : TileBase;

        IReadOnlyLocationCell GetCell(Vector2Int position);
        IReadOnlyLocationCell GetCell(Vector3 position);
        T? GetCell<T>(Vector2Int position) where T : IReadOnlyLocationCell;
        T? GetCell<T>(Vector3 position) where T : IReadOnlyLocationCell;

        bool TryGetCell(Vector2Int position, [NotNullWhen(true)] out IReadOnlyLocationCell? locationCell);
        bool TryGetCell(Vector3 position, [NotNullWhen(true)] out IReadOnlyLocationCell? locationCell);
        bool TryGetCell<T>(Vector2Int position, [NotNullWhen(true)] out T? locationCell)
            where T : IReadOnlyLocationCell;
        bool TryGetCell<T>(Vector3 position, [NotNullWhen(true)] out T? locationCell)
            where T : IReadOnlyLocationCell;

        bool HasTile(Vector2Int position);
        bool HasTile(Vector3 position);
        bool HasTile<T>(Vector2Int position) where T : TileBase;
        bool HasTile<T>(Vector3 position) where T : TileBase;

        bool InBounds(Vector2Int position);
        bool InBounds(Vector3 position);

        Vector2Int WorldToCell(Vector3 position);

        Vector3 CellToWorld(Vector2Int position);
    }
}
