#nullable enable
using Core.Map;
using UnityEngine;
using UTIRLib;

namespace Core.GameModes
{
    public class PlaceMode : State, IGameMode
    {
        private readonly ILocationLayer location;
        private readonly IPlayer player;
        private readonly PlayerInputHandler playerInputHandler;

        public PlaceMode(ILocationLayer location,
                         IPlayer player,
                         PlayerInputHandler playerInputHandler)
        {
            this.location = location;
            this.player = player;
            this.playerInputHandler = playerInputHandler;
        }

        public override void Enter() => RegisterActions();

        public override void Execute() { }

        public override void Exit() => UnregisterActions();

        public void Dispose() => Exit();

        private void Place(bool value)
        {
            if (!value)
                return;

            if (!player.HasHoldItem)
            {
                GameDebug.Log("Nothing to place.", this);
                return;
            }

            if (player.Is<ILocationCell>(out var cell))
            {
                Vector2 pointerPosition = playerInputHandler.PointerPosition;
                location.SetCell(pointerPosition.FloorToInt().ToVector3(), cell);
            }
            else
                GameDebug.Log($"{player.HoldItem!.ItemName} cannot be placed.");
        }

        private void RegisterActions()
        {
            playerInputHandler.OnPrimaryAction += Place;
        }

        private void UnregisterActions()
        {
            playerInputHandler.OnPrimaryAction -= Place;
        }
    }
}
