#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib;

namespace Core
{
    [InitFirst]
    public class PlayerInputHandler : MonoXInitable
    {
        [SerializeField]
        private InputActionAsset inputs = null!;

        public InputAction Input_PrimaryAction { get; private set; } = null!;
        public InputAction Input_SecondaryAction { get; private set; } = null!;
        public InputAction Input_SwitchPlaceMode { get; private set; } = null!;
        public InputAction Input_SwitchPauseMode { get; private set; } = null!;

        public bool PrimaryActionValue { get; private set; }
        public bool SecondaryActionValue { get; private set; }
        public bool SwitchPlaceModeValue { get; private set; }
        public bool SwitchPauseModeValue { get; private set; }

        public event Action<bool> OnPrimaryAction = null!;
        public event Action<bool> OnSecondaryAction = null!;
        public event Action<bool> OnSwitchPlaceMode = null!;
        public event Action<bool> OnSwitchPauseMode = null!;

        protected override void OnInit()
        {
            SetActions();

            RegisterInputs();
        }

        private void SetActions()
        {
            InputActionMap playerActions = inputs.FindActionMap("Player");

            Input_PrimaryAction = playerActions.FindAction("PrimaryAction");
            Input_SecondaryAction = playerActions.FindAction("SecondaryAction");
            Input_SwitchPlaceMode = playerActions.FindAction("SwitchPlaceMode");
            Input_SwitchPauseMode = playerActions.FindAction("SwitchPauseMode");
        }

        private void RegisterInputs()
        {
            Input_PrimaryAction.performed += (context) =>
            {
                PrimaryActionValue = context.ReadValueAsButton();
                OnPrimaryAction?.Invoke(PrimaryActionValue);
            };

            Input_SecondaryAction.performed += (context) =>
            {
                SecondaryActionValue = context.ReadValueAsButton();
                OnSecondaryAction?.Invoke(SecondaryActionValue);
            };

            Input_SwitchPlaceMode.performed += (context) =>
            {
                SwitchPauseModeValue = context.ReadValueAsButton();
                OnSwitchPlaceMode?.Invoke(SwitchPauseModeValue);
            };

            Input_SwitchPauseMode.performed += (context) =>
            {
                SwitchPauseModeValue = context.ReadValueAsButton();
                OnSwitchPauseMode?.Invoke(SwitchPauseModeValue);
            };
        }
    }
}
