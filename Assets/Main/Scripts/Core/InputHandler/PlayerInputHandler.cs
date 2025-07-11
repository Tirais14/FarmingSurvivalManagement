using Game.Enums.Generated;
using UnityEngine;
using UTIRLib.InputSystem;

#nullable enable
namespace Game.Core.InputSystem
{
    public sealed class PlayerInputHandler : InputHandlerMain, IPlayerInputHandler
    {
        public Vector2 MoveInputValue { get; private set; }
        public bool IsMoving { get; private set; }
        public bool AttackInputValue { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            SetHandlerActionTypes();
            AddHandlerActionsByMap("Player");
            BindInputs();
        }

        private void OnMoveInput(Vector2 value) => MoveInputValue = value;

        private void OnAttackInput(bool value) => AttackInputValue = value;

        private void BindInputs()
        {
            BindAction(PlayerInputAction.Move.ToString(), OnMoveInput, InputActionEventType.OnPerformed);

            BindAction(PlayerInputAction.Attack.ToString(), OnAttackInput, InputActionEventType.OnStarted);
            BindAction(PlayerInputAction.Attack.ToString(), OnAttackInput, InputActionEventType.OnCancelled);
        }

        private void OnDestroy() => UnregisterInputActions();
    }
}