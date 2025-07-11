using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Game.Generated;
using UnityEngine;
using UnityEngine.UI;
using UTIRLib.Diagnostics;
using UTIRLib.Injector;
using UTIRLib.TwoD;
using UTIRLib.UI;
using UTIRLib.Utils;
using Zenject;

#nullable enable
namespace Game.InventorySystem
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class Inventory : StorageModel
    {
        protected InventorySlotFactory slotFactory = null!;

        [GetComponent]
        protected GridLayoutGroup grid;

        [SerializeField]
        [GetComponentInParentIfNull]
        protected InventoryUI inventoryUI;

        [Inject]
        [SuppressMessage("", "IDE0051")]
        private void Construct(InventorySlotFactory slotFactory) => this.slotFactory = slotFactory;

        protected override void RebuildSlots(int newSlotQuantity)
        {
            base.RebuildSlots(newSlotQuantity);

            //At the end of the frame, to ensure correct positions
            CoroutineTask.Run(CoroutineTask.CallType.OnFrameEnd, ReplaceSlotsByPositionRelative);
        }

        protected override StorageSlotModel CreateSlot()
        {
            return slotFactory.Create<StorageSlotModel>(UIPrefabs.InventorySlot, this).ThrowIfNotFound();
        }

        private void ReplaceSlotsByPositionRelative()
        {
            Relative2DPosition[] slotRelativePositions = GetSlotRelativePositions();

            SaveStoredItems();

            Clear();

            InstantiateSlotsByRelativePosition(slotRelativePositions);

            RestoreItemsFromSaved();
        }

        private void InstantiateSlotsByRelativePosition(Relative2DPosition[] slotRelativePositions)
        {
            StorageSlotModel storageSlot;
            UIPrefabs slotPrefabName;
            foreach (var pos in slotRelativePositions) {
                slotPrefabName = pos switch {
                    Relative2DPosition.Center => UIPrefabs.InventorySlot,
                    Relative2DPosition.Left => UIPrefabs.InventorySlot_Left,
                    Relative2DPosition.Right => UIPrefabs.InventorySlot_Right,
                    Relative2DPosition.Top => UIPrefabs.InventorySlot_Top,
                    Relative2DPosition.Bottom => UIPrefabs.InventorySlot_Bottom,
                    Relative2DPosition.LeftTop => UIPrefabs.InventorySlot_LeftTop,
                    Relative2DPosition.LeftBottom => UIPrefabs.InventorySlot_LeftBottom,
                    Relative2DPosition.RightTop => UIPrefabs.InventorySlot_RightTop,
                    Relative2DPosition.RightBottom => UIPrefabs.InventorySlot_RightBottom,
                    _ => UIPrefabs.None
                };

                storageSlot = slotFactory.Create<StorageSlotModel>(slotPrefabName, this).ThrowIfNotFound();
                slotsProp.Add(storageSlot);
            }
        }

        private Relative2DPosition[] GetSlotRelativePositions()
        {
            GridLayoutGroupElement[] gridElements = GetSlotGridLayoutElemets();
            var slotRelativePositions = new Relative2DPosition[gridElements.Length];
            for (int i = 0; i < gridElements.Length; i++) {
                slotRelativePositions[i] = gridElements[i].RelativePosition;
            }

            return slotRelativePositions;
        }

        private GridLayoutGroupElement[] GetSlotGridLayoutElemets() =>
            slotsProp.Select(x => x.GetComponent<GridLayoutGroupElement>()).ToArray();
    }
}
