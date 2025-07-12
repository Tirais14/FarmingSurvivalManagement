using UnityEngine.EventSystems;
using UTIRLib.InputSystem;
using UTIRLib.UI;
using Zenject;

#nullable enable
namespace UI
{
    public class InventoryUI : UserInterface
    {
        [Inject]
        private void Construct(IPointerHandler pointerHandler, EventSystem eventSystem)
        {
            PointerHandler = pointerHandler;
            EventSystem = eventSystem;
        }
    }
}
