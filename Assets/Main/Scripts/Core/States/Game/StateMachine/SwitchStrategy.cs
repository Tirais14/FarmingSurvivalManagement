using Core.GameModes;
using UTIRLib;

namespace Core
{
    public sealed partial class GameStateMachine
    {
        public partial class SwitchStrategy
        {
            private readonly GameStateMachine gameStateMachine;
            private readonly PlayerInputHandler playerInputHandler;

            private readonly PauseMode pauseMode;
            private readonly IdleMode idleMode;
            private readonly PlaceMode placeMode;

            private IGameMode nextGameMode = null!;

            public override IGameMode DefaultState => idleMode;

            public SwitchStrategy(GameStateMachine gameStateMachine,
                                  PlayerInputHandler playerInputHandler,
                                  GameModeFactory gameModeFactory)
            {
                this.gameStateMachine = gameStateMachine;
                this.playerInputHandler = playerInputHandler;

                pauseMode = gameModeFactory.Create<PauseMode>();
                idleMode = gameModeFactory.Create<IdleMode>();
                placeMode = gameModeFactory.Create<PlaceMode>();

                BindInputs();
            }

            public override IGameMode GetNextState() => nextGameMode;

            private void BindInputs()
            {
                playerInputHandler.OnSwitchPlaceMode += SwitchPlaceMode;
            }

            private void UnbindInputs()
            {
                playerInputHandler.OnSwitchPlaceMode -= SwitchPlaceMode;
            }

            private void SwitchPlaceMode(bool value)
            {
                if (!value)
                    return;

                if (gameStateMachine.PlayingStateType.Is<PlaceMode>())
                    nextGameMode = idleMode;
                else
                    nextGameMode = placeMode;
            }

            public void Dispose() => UnbindInputs();
        }
    }
}
