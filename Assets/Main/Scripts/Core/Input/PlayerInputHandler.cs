#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib;
using UTIRLib.Init;
using UTIRLib.InputSystem;

namespace Core
{
    public class PlayerInputHandler : MonoX, IPointerHandler, IInitable
    {
        private InputAction pointer = null!;

        [SerializeField]
        private InputActionAsset inputs = null!;

        public Vector2 PointerPosition => pointer.ReadValue<Vector2>();
        public Vector2 WorldPointerPosition {
            get => Camera.main.ScreenToWorldPoint(pointer.ReadValue<Vector2>());
        }

        public InputAction PrimaryAction { get; private set; } = null!;
        public InputAction SecondaryAction { get; private set; } = null!;
        public InputAction SwitchBuildMode { get; private set; } = null!;
        public InputAction SwitchPauseMode { get; private set; } = null!;

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

        public void Init()
        {

        }

        private void SetActions()
        {
            InputActionMap playerActions = inputs.FindActionMap("Player");

            pointer = inputs.FindActionMap("UI").FindAction("Point");

            PrimaryAction = playerActions.FindAction("PrimaryAction");
            SecondaryAction = playerActions.FindAction("SecondaryAction");
            SwitchBuildMode = playerActions.FindAction("SwitchPlaceMode");
            SwitchPauseMode = playerActions.FindAction("SwitchPauseMode");
        }

        private void RegisterInputs()
        {
            PrimaryAction.performed += (context) =>
            {
                OnPrimaryAction?.Invoke(context.ReadValueAsButton());
            };

            SecondaryAction.performed += (context) =>
            {
                OnSecondaryAction?.Invoke(context.ReadValueAsButton());
            };

            SwitchBuildMode.performed += (context) =>
            {
                OnSwitchBuildMode?.Invoke(context.ReadValueAsButton());
            };

            SwitchPauseMode.performed += (context) =>
            {
                OnSwitchPauseMode?.Invoke(context.ReadValueAsButton());
            };
        }
    }
}
