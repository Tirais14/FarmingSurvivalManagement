using Game.Core.InputSystem;
using Game.Enums.Generated;
using Game.Gameplay.BuildingSystem;
using Game.InventorySystem;
using Game.PlayerSystem;
using Game.StoreSystem;
using UnityEngine;
using UTIRLib.InputSystem;
using Zenject;

#nullable enable
namespace Game.Core
{
    public sealed class PlayerInputBinder
    {
        private readonly PlayerEntity playerEntity;
        private readonly IPlayerInputHandler playerInputHandler;
        private readonly IUIInputHandler uiInputHandler;
        private readonly IBuildingMode buildingMode;
        private readonly InventoryUI inventoryUI;
        private readonly StoreUI storeUI;

        [Inject]
        public PlayerInputBinder(PlayerEntity playerEntity, IPlayerInputHandler playerInputHandler,
            IUIInputHandler uiInputHandler, IBuildingMode buildingMode, InventoryUI inventoryUI, StoreUI storeUI)
        {
            this.playerEntity = playerEntity;
            this.playerInputHandler = playerInputHandler;
            this.uiInputHandler = uiInputHandler;
            this.buildingMode = buildingMode;
            this.inventoryUI = inventoryUI;
            this.storeUI = storeUI;
        }

        public void BindInputs()
        {
            BindBuildingMode();
            BindInventoryUI();
            BindStoreUI();

            Debug.Log("Player inputs binded.");
        }

        public void UnbindInputs()
        {
            UnbindBuildingMode();
            UnbindInventoryUI();
            UnbindStoreUI();
        }

        private void SwitchBuildingMode()
        {
            if (buildingMode.IsEnabled) {
                buildingMode.Disable();
            }
            else {
                buildingMode.Enable();
            }
        }

        private void SwitchInventoryUI()
        {
            if (inventoryUI.IsOpened) {
                inventoryUI.Close();
            }
            else {
                inventoryUI.Open();
            }
        }

        private void SwitchStoreUI()
        {
            if (storeUI.IsOpened) {
                storeUI.Close();
            }
            else {
                storeUI.Open();
            }
        }

        private void BindBuildingMode() => playerInputHandler.
            BindAction(PlayerInputAction.BuildingMode, SwitchBuildingMode, InputActionEventType.OnStarted);

        private void BindInventoryUI() => playerInputHandler.
            BindAction(PlayerInputAction.Inventory, SwitchInventoryUI, InputActionEventType.OnStarted);

        private void BindStoreUI() => playerInputHandler.
            BindAction(PlayerInputAction.Store, SwitchStoreUI, InputActionEventType.OnStarted);

        private void UnbindBuildingMode() => playerInputHandler.
            UnbindAction(PlayerInputAction.BuildingMode, SwitchBuildingMode, InputActionEventType.OnStarted);

        private void UnbindInventoryUI() => playerInputHandler.
            UnbindAction(PlayerInputAction.Inventory, SwitchInventoryUI, InputActionEventType.OnStarted);

        private void UnbindStoreUI() => playerInputHandler.
            UnbindAction(PlayerInputAction.Store, SwitchStoreUI, InputActionEventType.OnStarted);
    }
}
