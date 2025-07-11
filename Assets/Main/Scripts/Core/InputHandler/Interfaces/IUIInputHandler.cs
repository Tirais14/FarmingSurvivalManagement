using UnityEngine;
using UTIRLib.InputSystem;

#nullable enable
namespace Game.Core.InputSystem
{
    public interface IUIInputHandler : IInputHandler<bool>, IInputHandler<Vector2>, IPointerHandler
    {
    }
}
