using System.Diagnostics.CodeAnalysis;
using Game.LocationSystem;
using UnityEngine;

#nullable enable
namespace Game.Gameplay.BuildingSystem
{
    public interface IBuildableLocationLayer : ILocationLayer
    {
        IPlaceableItem? GetPlacedItem(Vector2Int position);
        IPlaceableItem? GetPlacedItem(Vector3 position);

        bool TryGetPlacedItem(Vector2Int position, [NotNullWhen(true)] out IPlaceableItem? placeableItem);
        bool TryGetPlacedItem(Vector3 position, [NotNullWhen(true)] out IPlaceableItem? placeableItem);

        bool HasPlacedItem(Vector2Int position);
        bool HasPlacedItem(Vector3 position);
    }
}
