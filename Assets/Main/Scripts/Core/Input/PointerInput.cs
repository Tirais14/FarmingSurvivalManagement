using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib.Disposables;
using UTIRLib.InputSystem;

#nullable enable
namespace Core.InputSystem
{
    public class PointerInput : Vector2InputAction, IPointerInput, IDisposableContainer
    {
        public PointerInput(InputAction inputAction) : base(inputAction)
        {
        }

        public Vector2 GetPointerWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(Value);
        }
    }
}
