using Core.GameModes;
using System;
using UTIRLib;
using UTIRLib.Diagnostics;
using UTIRLib.UI;
using Zenject;

#nullable enable
namespace Core
{
    public class Player : MonoX, IPlayer
    {
        private IGameMode gameMode = new IdleMode();
        private PlayerInputHandler inputHandler = null!;

        public Type GameModeType => gameMode.GetType();
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
            inputHandler.OnSwitchBuildMode += SwitchPlaceMode;
        }

        //TODO: Incapsulate transitions
        private void SwitchPlaceMode(bool buttonValue)
        {
            if (!buttonValue)
                return;

            gameMode.Exit();

            switch (gameMode)
            {
                case PlaceMode:
                    gameMode = new IdleMode();
                    GameDebug.Log(nameof(IdleMode));
                    break;
                case IdleMode:
                    gameMode = new PlaceMode(location, this, inputHandler);
                    GameDebug.Log(nameof(PlaceMode));
                    break;
                default:
                    GameDebug.Log($"{gameMode.GetProccessedTypeName()} cannot be switched to {nameof(PlaceMode)}.");
                    break;
            }

            gameMode.Enter();
        }

        private void Update() => gameMode.Execute();
    }
}
