#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib.InputSystem;

namespace Core.InputSystem
{
    public class PlayerInputHandler : IDisposable
    {
        private readonly InputActionAsset inputActionAsset;
        private bool disposedValue;

        public IInputAction<Vector2> MoveInput { get; private set; } = null!;
        public IInputAction<bool> PrimaryActionInput { get; private set; } = null!;
        public IInputAction<bool> SecondaryActionInput { get; private set; } = null!;
        public IInputAction<bool> SwitchPlaceModeInput { get; private set; } = null!;
        public IInputAction<bool> SwitchPauseModeInput { get; private set; } = null!;

        public PlayerInputHandler(InputActionAsset inputActionAsset)
        {
            this.inputActionAsset = inputActionAsset;

            InputActionMap actionMap = this.inputActionAsset.FindActionMap("Player");

            InputActionFactory.Create<bool>(actionMap, "PrimaryAction");
            InputActionFactory.Create<bool>(actionMap, "SecondaryAction");
            InputActionFactory.Create<bool>(actionMap, "SwitchPlaceMode");
            InputActionFactory.Create<bool>(actionMap, "SwitchPauseMode");
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    PrimaryActionInput.Dispose();
                    SecondaryActionInput.Dispose();
                    SwitchPlaceModeInput.Dispose();
                    SwitchPauseModeInput.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
    }
}
