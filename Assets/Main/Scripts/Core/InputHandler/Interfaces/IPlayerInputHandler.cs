using UnityEngine;
using UTIRLib.InputSystem;

#nullable enable
namespace Game.Core.InputSystem
{
    public interface IPlayerInputHandler : IInputHandler<bool>, IInputHandler<Vector2>
    {
        Vector2 MoveInputValue { get; }
        bool IsMoving { get; }
        bool AttackInputValue { get; }
    }
}
