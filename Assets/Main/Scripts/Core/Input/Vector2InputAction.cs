using Core.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

#nullable enable
namespace Core
{
    public class Vector2InputAction : InputActionX<Vector2>
    {
        public Vector2InputAction(InputAction inputAction) : base(inputAction)
        {
        }
    }
}
