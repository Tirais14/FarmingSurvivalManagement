#nullable enable
using Core.Map;
using UnityEngine;
using UTIRLib;

namespace Core.GameModes
{
    public class PlaceMode : IGameMode
    {
        private readonly ILocation location;
        private readonly IPlayer player;
        private readonly PlayerInputHandler playerInputHandler;

        public PlaceMode(ILocation location,
                         IPlayer player,
                         PlayerInputHandler playerInputHandler)
        {
            this.location = location;
            this.player = player;
            this.playerInputHandler = playerInputHandler;
        }

        public void Enter() => RegisterActions();

        public void Execute() { }

        public void Exit() => UnregisterActions();

        private void Place(bool value)
        {
            if (!value)
                return;

            if (!player.HasHoldItem)
            {
                GameDebug.Log("Nothing to place.", this);
                return;
            }

            if (player.Is<ILocationCell>(out ILocationCell? cell))
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
