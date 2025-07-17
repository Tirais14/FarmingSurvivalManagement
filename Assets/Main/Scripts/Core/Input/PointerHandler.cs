using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib;
using UTIRLib.InputSystem;

#nullable enable
namespace Core.InputSystem
{
    [InitFirst]
    public class PointerHandler : IPointerHandler
    {
        private readonly InputActionAsset inputActionAsset = null!;

        public IInputAction<Vector2> PointerInput { get; private set; }
        public Vector2 PointerPosition => PointerInput.Value;
        public Vector2 WorldPointerPosition {
            get => Camera.main.ScreenToWorldPoint(PointerPosition);
        }

        public PointerHandler(InputActionAsset inputActionAsset)
        {
            this.inputActionAsset = inputActionAsset;

            PointerInput = InputActionFactory.Create<Vector2>(inputActionAsset,
                                                              "UI",
                                                              "Point");
        }
    }
}
