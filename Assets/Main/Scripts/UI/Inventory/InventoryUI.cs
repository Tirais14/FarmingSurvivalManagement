using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.EventSystems;
using UTIRLib.Injector;
using UTIRLib.InputSystem;
using UTIRLib.UI;
using Zenject;

#nullable enable
namespace Game.InventorySystem
{
    public class InventoryUI<T> : UserInterface, IInventoryUI
        where T : Component, IStorage
    {
        [SerializeField]
        [GetComponentInChildrenIfNull]
        protected T inventory = null!;

        public IStorage Inventory => inventory;

        [Inject]
        [SuppressMessage("", "IDE0051")]
        private void Construct(EventSystem eventSystem, IPointerHandler pointerHandler)
        {
            EventSystem = eventSystem;
            PointerHandler = pointerHandler;
        }

        protected override void OnAwake()
        {
            base.OnAwake();

            Raycaster = new UIRaycaster(PointerHandler, DefaultRaycaster, EventSystem);
        }

        public override void Open()
        {
            base.Open();
            inventory.Open();
        }

        public override void Close()
        {
            base.Close();
            inventory.Close();
        }
    }
    public class InventoryUI : InventoryUI<Inventory>
    {
    }
}
