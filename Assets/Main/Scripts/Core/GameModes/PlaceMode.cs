#nullable enable
namespace Core.GameModes
{
    public class PlaceMode : StateBase, IGameMode
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

        public override void Enter() { }

        public void Execute() { }

        public override void Exit() { }
    }
}
