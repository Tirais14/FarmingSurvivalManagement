using Core.InputSystem;
using UnityEngine.InputSystem;

#nullable enable
namespace Core
{
    public class ButtonInputAction : InputActionX<bool>
    {
        public ButtonInputAction(InputAction inputAction) : base(inputAction)
        {
        }

        protected override bool ReadValue(InputAction.CallbackContext context)
        {
            return context.ReadValueAsButton();
        }
    }
}
