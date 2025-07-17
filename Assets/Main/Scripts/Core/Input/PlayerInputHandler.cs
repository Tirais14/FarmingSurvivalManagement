#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib.Attributes;
using UTIRLib.Utils;

namespace Core.InputSystem
{
    public class PlayerInputHandler : IDisposable
    {
        private readonly InputActionAsset inputActionAsset;
        private bool disposedValue;

        [RequiredMember]
        public IInputAction<Vector2> MoveInput { get; private set; } = null!;

        [RequiredMember]
        public IInputAction<bool> PrimaryActionInput { get; private set; } = null!;

        [RequiredMember]
        public IInputAction<bool> SecondaryActionInput { get; private set; } = null!;

        [RequiredMember]
        public IInputAction<bool> SwitchPlaceModeInput { get; private set; } = null!;

        [RequiredMember]
        public IInputAction<bool> SwitchPauseModeInput { get; private set; } = null!;

        public PlayerInputHandler(InputActionAsset inputActionAsset)
        {
            this.inputActionAsset = inputActionAsset;

            InputActionMap actionMap = this.inputActionAsset.FindActionMap("Player", throwIfNotFound: true);

            MoveInput = InputActionFactory.Create<Vector2>(actionMap, "Move");
            PrimaryActionInput = InputActionFactory.Create<bool>(actionMap, "PrimaryAction");
            SecondaryActionInput = InputActionFactory.Create<bool>(actionMap, "SecondaryAction");
            SwitchPlaceModeInput = InputActionFactory.Create<bool>(actionMap, "SwitchPlaceMode");
            SwitchPauseModeInput = InputActionFactory.Create<bool>(actionMap, "SwitchPauseMode");

            MemberValidator.ValidateInstance(this);
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
