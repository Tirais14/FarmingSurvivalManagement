#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib.Attributes;
using UTIRLib.Disposables;
using UTIRLib.InputSystem;
using UTIRLib.Utils;

namespace Core.InputSystem
{
    public sealed class PlayerInputHandler : IDisposableContainer
    {
        private readonly DisposableCollection disposables = new();

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

            MoveInput = InputActionFactory.Create<Vector2>(actionMap,
                "Move")
                .AddTo(this);

            PrimaryActionInput = InputActionFactory.Create<bool>(actionMap,
                "PrimaryAction")
                .AddTo(this);

            SecondaryActionInput = InputActionFactory.Create<bool>(actionMap,
                "SecondaryAction")
                .AddTo(this);

            SwitchPlaceModeInput = InputActionFactory.Create<bool>(actionMap,
                "SwitchPlaceMode")
                .AddTo(this);

            SwitchPauseModeInput = InputActionFactory.Create<bool>(actionMap,
                "SwitchPauseMode")
                .AddTo(this);

            disposables.TrimExcess();

            MemberValidator.ValidateInstance(this);
        }

        public void Dispose()
        {
            disposables.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
