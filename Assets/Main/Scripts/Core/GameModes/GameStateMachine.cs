using Core.GameModes;
using System;
using UTIRLib;
using Zenject;
using UnityEngine;

namespace Core
{
    public sealed class GameStateMachine : MonoX, IStateMachine
    {
        private IGameMode gameMode = new PauseMode();
        private PlayerInputHandler playerInputHandler;

        private PauseMode pauseMode = null!;
        private NormalMode normalMode = null!;
        private PlaceMode placeMode = null!;

        public GameMode GameMode => GetGameMode();

        [Inject]
        private void Construct(PlayerInputHandler playerInputHandler)
        {
            this.playerInputHandler = playerInputHandler;
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        private void BindInputs()
        {
            playerInputHandler.OnSwitchBuildMode += SwitchPlaceMode;
        }

        private GameMode GetGameMode()
        {
            return gameMode switch
            {
                PauseMode => GameMode.Pause,
                NormalMode => GameMode.Normal,
                PlaceMode => GameMode.Place,
                _ => throw new InvalidOperationException(),
            };
        }

        private void SwitchPlaceMode(bool value)
        {
            if (!value)
                return;

            if (GameMode == GameMode.Place)
                gameMode = 
        }

        private void Update()
        {
            gameMode.Execute();
        }
    }
}
