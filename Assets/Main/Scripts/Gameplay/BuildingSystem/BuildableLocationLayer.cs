using System.Diagnostics.CodeAnalysis;
using Game.Core.DatabaseSystem;
using Game.Generated;
using Game.LocationSystem;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

#nullable enable
namespace Game.Gameplay.BuildingSystem
{
    public class BuildableLocationLayer : LocationLayer, IBuildableLocationLayer
    {
        protected AssetDatabaseRegistry assetDatabaseRegistry = null!;

        [Inject]
        [SuppressMessage("", "IDE0051")]
        private void Construct(AssetDatabaseRegistry assetDatabaseRegistry) =>
            this.assetDatabaseRegistry = assetDatabaseRegistry;

        public IPlaceableItem? GetPlacedItem(Vector2Int position) => GetPlacedItemInternal(position);
        public IPlaceableItem? GetPlacedItem(Vector3 position) => GetPlacedItemInternal(WorldToCell(position));

        public bool TryGetPlacedItem(Vector2Int position, [NotNullWhen(true)] out IPlaceableItem? placeableItem)
        {
            placeableItem = GetPlacedItem(position);

            return placeableItem is not null;
        }
        public bool TryGetPlacedItem(Vector3 position, [NotNullWhen(true)] out IPlaceableItem? placeableItem)
        {
            placeableItem = GetPlacedItem(position);

            return placeableItem is not null;
        }

        public bool HasPlacedItem(Vector2Int position) => TryGetPlacedItem(position, out _);
        public bool HasPlacedItem(Vector3 position) => TryGetPlacedItem(position, out _);

        private IPlaceableItem? GetPlacedItemInternal(Vector2Int position)
        {
            if (TryGetTile(position, out TileBase? tile)) {
                return assetDatabaseRegistry.ScriptableObjects[AssetDatabaseNames.TILES].FindAssetT<IPlaceableItem>(tile.name);
            }

            return null;
        }
    }
}
