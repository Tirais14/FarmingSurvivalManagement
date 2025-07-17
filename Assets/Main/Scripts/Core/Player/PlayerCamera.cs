using Core.InputSystem;
using UTIRLib;

#pragma warning disable IDE0051
#nullable enable
namespace Core
{
    public class PlayerCamera : MonoX
    {
        private PlayerInputHandler playerInputHandler = null!;

        private void Construct(PlayerInputHandler playerInputHandler)
        {
            this.playerInputHandler = playerInputHandler;
        }

        protected override void OnStart()
        {
            
        }
    }
}
