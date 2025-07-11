using Game.Enums.Generated;
using UnityEngine;
using UTIRLib.InputSystem;

#nullable enable
namespace Game.Core.InputSystem
{
    public sealed class UIInputHandler : InputHandlerMain, IUIInputHandler
    {
        public Vector2 PointerPosition { get; private set; }
        public Vector2 WorldPointerPosition => Camera.main.ScreenToWorldPoint(PointerPosition);

        protected override void OnAwake()
        {
            base.OnAwake();
            SetHandlerActionTypes();
            AddHandlerActionsByMap("UI");
            BindInputs();
        }

        private void OnPointerMove(Vector2 value) => PointerPosition = value;

        private void BindInputs()
        {
            BindAction(UIInputAction.Point.ToString(), OnPointerMove, InputActionEventType.OnPerformed);
        }

        private void OnDestroy() => UnregisterInputActions();
    }
}