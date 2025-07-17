using System;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

#nullable enable
namespace Core.InputSystem
{
    public class InputActionX<T> : IInputAction<T>
        where T : struct
    {
        private readonly InputAction input;
        private bool disposedValue;
        private T value;

        public T Value {
            get
            {
                if (disposedValue)
                    throw new Exception(input.name + " is disposed, value cannot be readed.");

                return value;
            }
        }

        public event Action<CallbackContext>? OnPerformed;
        public event Action<T>? OnPerformedValue;

        public InputActionX(InputAction inputAction)
        {
            input = inputAction;

            input.performed += SetValue;
            input.performed += OnPerformedEvent;
            input.performed += PerformedValueEvent;
        }

        public InputAction AsUnityInputAction() => input;

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual T ReadValue(CallbackContext context)
        {
            return context.ReadValue<T>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    input.performed -= SetValue;
                    input.performed -= OnPerformedEvent;
                    input.performed -= PerformedValueEvent;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                OnPerformed = null;
                OnPerformedValue = null;

                disposedValue = true;
            }
        }

        private void SetValue(CallbackContext context)
        {
            value = ReadValue(context);
        }

        private void OnPerformedEvent(CallbackContext context)
        {
            OnPerformed?.Invoke(context);
        }

        private void PerformedValueEvent(CallbackContext context)
        {
            OnPerformedValue?.Invoke(ReadValue(context));
        }

        public static explicit operator InputAction(InputActionX<T> inputActionX)
        {
            return inputActionX.input;
        }
    }
}
