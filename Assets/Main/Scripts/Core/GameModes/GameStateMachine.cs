using Core.GameModes;
using System;
using UTIRLib;
using Zenject;

#nullable enable
namespace Core
{
    public sealed class GameStateMachine : MonoXInitable, IStateMachine<IState>
    {
        private StateMachine stateMachine = null!;
        private IGameMode gameMode = null!;
        private PlayerInputHandler playerInputHandler;

        private PauseMode pauseMode = null!;
        private IdleMode idleMode = null!;
        private PlaceMode placeMode = null!;

        public GameMode GameMode => GetGameMode();

        [Inject]
        private void Construct(PlayerInputHandler playerInputHandler)
        {
            this.playerInputHandler = playerInputHandler;
        }

        protected override void OnInit()
        {
            stateMachine = new StateMachine(idleMode);
        }

        protected override void OnStart()
        {
            base.OnStart();

            BindInputs();
        }

        private void BindInputs()
        {
            playerInputHandler.OnSwitchBuildMode += SwitchPlaceMode;
        }

        private void UnbindInputs()
        {
            playerInputHandler.OnSwitchBuildMode -= SwitchPlaceMode;
        }

        private GameMode GetGameMode()
        {
            return gameMode switch
            {
                PauseMode => GameMode.Pause,
                IdleMode => GameMode.Idle,
                PlaceMode => GameMode.Place,
                _ => throw new InvalidOperationException(),
            };
        }

        private void ResetGameMode() => gameMode = idleMode;

        private void SwitchPlaceMode(bool value)
        {
            if (!value)
                return;

            switch (GameMode)
            {
                case GameMode.None:
                    break;
                case GameMode.Pause:
                    break;
                case GameMode.Idle:
                    gameMode = placeMode;
                    break;
                case GameMode.Place:
                    ResetGameMode();
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            gameMode.Execute();
        }

        private void OnDisable() => UnbindInputs();
    }
}
