using Core.InputSystem;
using UTIRLib;
using UTIRLib.Diagnostics;
using UTIRLib.UI;
using Zenject;

#pragma warning disable IDE0051
#nullable enable
namespace Core
{
    public class Player : MonoX, IPlayer
    {
        private PlayerInputHandler inputHandler = null!;

        public IItem? HoldItem { get; set; }
        public bool HasHoldItem => HoldItem.IsNotNull();

        [Inject]
        private void Construct(PlayerInputHandler playerInputHandler)
        {
            inputHandler = playerInputHandler;
        }

        protected override void OnStart()
        {
            base.OnStart();

            BindInputs();
        }

        private void BindInputs()
        {
        }

        private void UnbindInputs()
        {

        }

        private void OnDisable() => UnbindInputs();
    }
}
