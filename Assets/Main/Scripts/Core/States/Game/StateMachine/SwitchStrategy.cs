using UTIRLib;
using UTIRLib.Patterns.States;

#nullable enable
namespace Core
{
    public sealed partial class GameStateMachine
    {
        public partial class SwitchStrategy :
            StateMachine<IGameMode>.SwitchStrategy
        {
            private readonly PlayerInputHandler playerInputHandler;

            private readonly PauseMode pauseMode;
            private readonly PlaceMode placeMode;

            public SwitchStrategy(PlayerInputHandler playerInputHandler,
                                  GameModeFactory gameModeFactory) 
                : base(gameModeFactory.Create<IdleMode>())
            {
                this.playerInputHandler = playerInputHandler;

                pauseMode = gameModeFactory.Create<PauseMode>();
                placeMode = gameModeFactory.Create<PlaceMode>();
            }

            public override IGameMode GetNextState()
            {
                if (playerInputHandler.SwitchPlaceModeValue)
                    SelectedState = SwitchPlaceMode();
                else if (playerInputHandler.SwitchPauseModeValue)
                    SelectedState = SwitchPauseMode();

                return SelectedState;
            }

            private IGameMode SwitchPauseMode()
            {
                if (selectedStateType.Is<PauseMode>())
                    return DefaultState;
                else
                    return pauseMode;
            }

            private IGameMode SwitchPlaceMode()
            {
                if (selectedStateType.Is<PlaceMode>())
                    return DefaultState;
                else
                    return placeMode;
            }
        }
    }
}
