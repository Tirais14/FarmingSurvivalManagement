using System;
using UnityEngine;
using UTIRLib.InputSystem;

#nullable enable
namespace Game.Core
{
    public abstract class InputHandlerMain : InputHandler, IInputHandler<bool>, IInputHandler<Vector2>
    {
        public void BindAction(string inputActionName, Action<bool> action,
           InputActionEventType eventType = InputActionEventType.OnPerformed) => BindActionInternal(inputActionName, action, eventType);
        public void BindAction(string inputActionName, Action<Vector2> action,
          InputActionEventType eventType = InputActionEventType.OnPerformed) => BindActionInternal(inputActionName, action, eventType);

        public void UnbindAction(string inputActionName, Action<bool> action, InputActionEventType eventType) =>
            UnbindActionInternal(inputActionName, action, eventType);
        public void UnbindAction(string inputActionName, Action<Vector2> action, InputActionEventType eventType) =>
            UnbindActionInternal(inputActionName, action, eventType);

        public void GetValue(string inputActionName, out bool value) => value = GetInputAction(inputActionName).ReadValue<bool>();
        public void GetValue(string inputActionName, out Vector2 value) => value = GetInputAction(inputActionName).ReadValue<Vector2>();

        bool IInputHandler<bool>.GetValue(string inputActionName) => GetInputAction(inputActionName).ReadValue<bool>();
        Vector2 IInputHandler<Vector2>.GetValue(string inputActionName) => GetInputAction(inputActionName).ReadValue<Vector2>();
    }
}
