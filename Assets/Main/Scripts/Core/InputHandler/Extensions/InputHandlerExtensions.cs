using System;
using UnityEngine.InputSystem;
using UTIRLib.InputSystem;

#nullable enable
namespace Game.Core.InputSystem
{
    public static class InputHandlerExtensions
    {
        public static void BindAction(this IInputHandler inputHandler, Enum inputActionEnum,
            Action<InputAction.CallbackContext> action, InputActionEventType eventType = InputActionEventType.OnPerformed) =>
            inputHandler.BindAction(inputActionEnum.ToString(), action, eventType);
        public static void BindAction<TValue>(this IInputHandler<TValue> inputHandler, Enum inputActionEnum,
            Action<TValue> action, InputActionEventType eventType = InputActionEventType.OnPerformed)
            where TValue : struct => inputHandler.BindAction(inputActionEnum.ToString(), action, eventType);
        public static void BindAction(this IInputHandler inputHandler, Enum inputActionEnum, Action action,
            InputActionEventType eventType = InputActionEventType.OnPerformed) =>
            inputHandler.BindAction(inputActionEnum.ToString(), action, eventType);

        public static void UnbindAction(this IInputHandler inputHandler, Enum inputActionEnum,
            Action<InputAction.CallbackContext> action, InputActionEventType eventType = InputActionEventType.OnPerformed) =>
            inputHandler.UnbindAction(inputActionEnum.ToString(), action, eventType);
        public static void UnbindAction<TValue>(this IInputHandler<TValue> inputHandler, Enum inputActionEnum,
            Action<TValue> action, InputActionEventType eventType = InputActionEventType.OnPerformed)
            where TValue : struct => inputHandler.UnbindAction(inputActionEnum.ToString(), action, eventType);
        public static void UnbindAction(this IInputHandler inputHandler, Enum inputActionEnum, Action action,
            InputActionEventType eventType = InputActionEventType.OnPerformed) =>
            inputHandler.UnbindAction(inputActionEnum.ToString(), action, eventType);

        public static InputAction GetInputAction(this IInputHandler inputHandler, Enum inputActionEnum) =>
            inputHandler.GetInputAction(inputActionEnum.ToString());

        public static bool IsButtonPressed(this IInputHandler inputHandler, Enum inputActionEnum) =>
            inputHandler.IsButtonPressed(inputActionEnum.ToString());

        public static void GetValue<TValue>(this IInputHandler<TValue> inputHandler, Enum inputActionEnum,
            out TValue value)
            where TValue : struct => inputHandler.GetValue(inputActionEnum.ToString(), out value);
        public static TValue GetValue<TValue>(this IInputHandler<TValue> inputHandler, Enum inputActionEnum)
            where TValue : struct => inputHandler.GetValue(inputActionEnum.ToString());

        public static TValue GetValueT<TValue>(this IInputHandler inputHandler, Enum inputActionEnum)
            where TValue : struct => inputHandler.GetValueT<TValue>(inputActionEnum.ToString());
    }
}
