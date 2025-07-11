using System;
using System.Diagnostics.CodeAnalysis;
using Game.Generated;
using Game.LocationSystem;
using UnityEngine;
using Zenject;
using UTIRLib.Diagnostics;

#nullable enable
namespace Game.Gameplay.BuildingSystem
{
    public class BuildableLocationMap : LocationMap, IBuildableLocationMap
    {
        protected LocationLayerFactory locationLayerFactory = null!;

        public IBuildableLocationLayer BuildLayer { get; protected set; } = null!;
        public IBuildableLocationLayer BuildableTechnical { get; protected set; } = null!;

        [Inject]
        [SuppressMessage("", "IDE0051")]
        private void Construct(LocationLayerFactory locationLayerFactory) =>
            this.locationLayerFactory = locationLayerFactory;

        protected override void OnAwake()
        {
            base.OnAwake();
            BuildLayer = locationLayerFactory.Create<BuildableLocationLayer>(MapPrefabs.BuildableLayer, this).
                ThrowIfNull("Failed to create location map layer.");
            BuildableTechnical = locationLayerFactory.Create<BuildableLocationLayer>(MapPrefabs.BuildableTechLayer, this).
                ThrowIfNull("Failed to create location map layer.");
        }

        /// <exception cref="ArgumentNullException"></exception>
        public void PlaceItem(IPlaceableItem placeableItem, Vector2Int position)
        {
            if (placeableItem.IsNull()) {
                throw new ArgumentNullException(nameof(placeableItem));
            }
            if (BuildLayer.HasTile(position)) {
                Debug.LogWarning("Error while placing item. Cell already contains item.");
                return;
            }

            BuildLayer.SetTile(position, placeableItem.TileProvider.Tile);
        }
        public void PlaceItem(IPlaceableItem placeableItem, Vector3 position) =>
            PlaceItem(placeableItem, FirstLayer.WorldToCell(position));

        public IPlaceableItem? GetPlacedItem(Vector2Int position) => BuildLayer.GetPlacedItem(position);
        public IPlaceableItem? GetPlacedItem(Vector3 position) => BuildLayer.GetPlacedItem(position);

        public bool TryGetPlacedItem(Vector2Int position, [NotNullWhen(true)] out IPlaceableItem? placeableItem) =>
            BuildLayer.TryGetPlacedItem(position, out placeableItem);
        public bool TryGetPlacedItem(Vector3 position, [NotNullWhen(true)] out IPlaceableItem? placeableItem) =>
            BuildLayer.TryGetPlacedItem(position, out placeableItem);

        public bool HasPlacedItem(Vector2Int position) => BuildLayer.HasPlacedItem(position);
        public bool HasPlacedItem(Vector3 position) => BuildLayer.HasPlacedItem(position);
    }
}
