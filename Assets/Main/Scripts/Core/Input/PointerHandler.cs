using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib;
using UTIRLib.InputSystem;

#nullable enable
namespace Core
{
    [InitFirst]
    public class PointerHandler : MonoXInitable, IPointerHandler
    {
        private InputAction pointer = null!;

        [SerializeField]
        private InputActionAsset inputs = null!;

        public Vector2 PointerPosition => pointer.ReadValue<Vector2>();
        public Vector2 WorldPointerPosition {
            get => Camera.main.ScreenToWorldPoint(pointer.ReadValue<Vector2>());
        }

        protected override void OnInit()
        {
            pointer = inputs.FindActionMap("UI").FindAction("Point");
        }
    }
}
