using System.Diagnostics.CodeAnalysis;
using Game.LocationSystem;
using UnityEngine;

#nullable enable
namespace Game.Gameplay.BuildingSystem
{
    public interface IBuildableLocationMap : ILocationMap
    {
        IBuildableLocationLayer BuildLayer { get; }
        IBuildableLocationLayer BuildableTechnical { get; }

        void PlaceItem(IPlaceableItem placeableItem, Vector2Int position);
        void PlaceItem(IPlaceableItem placeableItem, Vector3 position);

        IPlaceableItem? GetPlacedItem(Vector2Int position);
        IPlaceableItem? GetPlacedItem(Vector3 position);

        bool TryGetPlacedItem(Vector2Int position, [NotNullWhen(true)] out IPlaceableItem? placeableItem);
        bool TryGetPlacedItem(Vector3 position, [NotNullWhen(true)] out IPlaceableItem? placeableItem);

        bool HasPlacedItem(Vector2Int position);
        bool HasPlacedItem(Vector3 position);
    }
}
