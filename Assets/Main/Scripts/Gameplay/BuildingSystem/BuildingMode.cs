using System;
using System.Diagnostics.CodeAnalysis;
using Game.Core;
using UnityEngine;
using UTIRLib;
using UTIRLib.Diagnostics;
using UTIRLib.InputSystem;
using Zenject;

#nullable enable
namespace Game.Gameplay.BuildingSystem
{
    public sealed class BuildingMode : MonoX, IBuildingMode
    {
        private IBuildableLocationMap locationMap = null!;
        private IPointerHandler pointerHandler = null!;
        private ILocationCell? lastPlacedItemCell;

        public event Action<IBuildingMode>? OnStateChanged;

        public IPlaceableItem? PlaceableItem { get; set; }
        public bool IsEnabled { get; private set; }

        [Inject]
        [SuppressMessage("", "IDE0051")]
        private void Construct(IBuildableLocationMap locationMap, IPointerHandler pointerHandler)
        {
            this.locationMap = locationMap;
            this.pointerHandler = pointerHandler;
        }

        public void PlaceItem()
        {
            if (PlaceableItem.IsNull()) {
                Debug.LogWarning("Try to place null item.");
                return;
            }

            locationMap.PlaceItem(PlaceableItem, pointerHandler.PointerPosition);
        }

        public void Enable()
        {
            if (locationMap.BuildableTechnical.IsNull()) {
                Debug.LogError("Building mode layer doesn't exist.", this);
                return;
            }

            IsEnabled = true;

            OnStateChanged?.Invoke(this);
        }

        public void Disable()
        {
            IsEnabled = false;
            PlaceableItem = null;
            ClearLastPlacedItemCell();

            OnStateChanged?.Invoke(this);
        }

        private void VisualizePlaceableItem()
        {
            if (PlaceableItem.IsNull()) {
                Debug.LogError("Placeable tile cannot be null.");
                Disable();
                return;
            }

            ClearLastPlacedItemCell();
            if (locationMap.BuildableTechnical.TryGetCell(pointerHandler.WorldPointerPosition,
                out ILocationCell? locationCell)) {
                locationCell.SetTile(PlaceableItem.TileProvider.Tile);
                lastPlacedItemCell = locationCell;
            }
        }

        private void ClearLastPlacedItemCell() => lastPlacedItemCell?.Clear();

        private void Update()
        {
            if (IsEnabled) {
                VisualizePlaceableItem();
            }
        }
    }
}
