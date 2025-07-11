using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Core;
using UnityEngine;

#nullable enable
namespace Game.LocationSystem
{
    public interface ILocationMap : IReadOnlyList<ILocationLayer>
    {
        BoundsInt Bounds { get; }
        Grid GridComponent { get; }
        ILocationLayer FirstLayer { get; }

        ILocationLayer GetLayer(int index);

        ILocationLayer? FindLayer(string layerName);

        bool TryFindLayer(string layerName, [NotNullWhen(true)] out ILocationLayer? locationLayer);

        ILocationCell? FindCell(Vector2Int position);
        ILocationCell? FindCell(Vector3 position);

        bool TryFindCell(Vector2Int positon, [NotNullWhen(true)] out ILocationCell? cell);
        bool TryFindCell(Vector3 positon, [NotNullWhen(true)] out ILocationCell? cell);

        ILocationCell GetCell(int layerIndex, Vector2Int position);
        ILocationCell GetCell(int layerIndex, Vector3 position);
        T? GetCell<T>(int layerIndex, Vector2Int position) where T : ILocationCell;
        T? GetCell<T>(int layerIndex, Vector3 position) where T : ILocationCell;

        bool TryGetCell<T>(int layerIndex, Vector2Int position, [NotNullWhen(true)] out T? locationCell)
            where T : ILocationCell;
        bool TryGetCell<T>(int layerIndex, Vector3 position, [NotNullWhen(true)] out T? locationCell)
            where T : ILocationCell;

        Vector2Int WorldToCell(Vector3 position);

        Vector3 CellToWorld(Vector2Int position);
    }
}
