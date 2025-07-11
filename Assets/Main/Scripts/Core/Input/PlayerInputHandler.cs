#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib;

namespace Core
{
    public class PlayerInputHandler : MonoX
    {
        private InputAction pointer = null!;

        [SerializeField]
        private InputActionAsset inputs = null!;

        public Vector2 PointerPosition => pointer.ReadValue<Vector2>();

        private InputAction primaryAction = null!;
        private InputAction secondaryAction = null!;
        private InputAction switchBuildMode = null!;
        private InputAction switchPauseMode = null!;

        public event Action<bool> OnPrimaryAction = null!;
        public event Action<bool> OnSecondaryAction = null!;
        public event Action<bool> OnSwitchBuildMode = null!;
        public event Action<bool> OnSwitchPauseMode = null!;

        protected override void OnAwake()
        {
            base.OnAwake();

            SetActions();

            RegisterInputs();
        }

        private void SetActions()
        {
            InputActionMap playerActions = inputs.FindActionMap("Player");

            pointer = inputs.FindActionMap("UI").FindAction("Point");

            primaryAction = playerActions.FindAction("PrimaryAction");
            secondaryAction = playerActions.FindAction("SecondaryAction");
            switchBuildMode = playerActions.FindAction("SwitchPlaceMode");
            switchPauseMode = playerActions.FindAction("SwitchPauseMode");
        }

        private void RegisterInputs()
        {
            primaryAction.performed += (context) =>
            {
                OnPrimaryAction?.Invoke(context.ReadValueAsButton());
            };

            secondaryAction.performed += (context) =>
            {
                OnSecondaryAction?.Invoke(context.ReadValueAsButton());
            };

            switchBuildMode.performed += (context) =>
            {
                OnSwitchBuildMode?.Invoke(context.ReadValueAsButton());
            };

            switchPauseMode.performed += (context) =>
            {
                OnSwitchPauseMode?.Invoke(context.ReadValueAsButton());
            };
        }
    }
}
