using UnityEngine.EventSystems;
using UTIRLib.InputSystem;
using UTIRLib.UI;
using Zenject;

#pragma warning disable IDE0051
#nullable enable
namespace UI
{
    public class InventoryUI : UserInterface
    {
        [Inject]
        private void Construct(IPointerInput pointer, EventSystem eventSystem)
        {
            Pointer = pointer;
            EventSystem = eventSystem;
        }
    }
}
