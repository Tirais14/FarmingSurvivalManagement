using Core.GameModes;
using UTIRLib.Patterns.Factory;
using Zenject;
using System;

#nullable enable
namespace Core
{
    public class GameModeFactory : IFactory<GameMode, IGameMode>
    {
        private readonly DiContainer diContainer;

        public GameModeFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public IGameMode Create(GameMode arg)
        {
            switch (arg)
            {
                case GameMode.Pause:
                    return new PauseMode();
                case GameMode.Idle:
                    return new IdleMode();
                case GameMode.Place:
                    ILocation location = diContainer.Resolve<ILocation>();
                    IPlayer player = diContainer.Resolve<IPlayer>();
                    PlayerInputHandler inputHandler = diContainer.Resolve<PlayerInputHandler>();

                    return new PlaceMode(location, player, inputHandler);
                default:
                    throw new InvalidOperationException(arg.ToString());
            }
        }
    }
}
