using Core.GameModes;
using UTIRLib.Patterns.Factory;
using Zenject;
using System;
using UTIRLib;
using UTIRLib.Diagnostics;

#nullable enable
namespace Core
{
    public class GameModeFactory : IFactory<Type, IGameMode>
    {
        private readonly DiContainer diContainer;

        public GameModeFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public IGameMode Create(Type arg)
        {
            if (arg.IsNull())
                throw new ArgumentNullException(nameof(arg));

            if (arg.Is<PauseMode>())
                return new PauseMode();
            else if (arg.Is<IdleMode>())
                return new IdleMode();
            else if (arg.Is<PlaceMode>())
                return new PlaceMode();

            throw new InvalidOperationException(arg.GetProccessedName());
        }

        public T Create<T>() where T : IGameMode
        {
            return (T)Create(typeof(T));
        }
    }
}
