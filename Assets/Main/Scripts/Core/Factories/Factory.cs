using System.Collections.Generic;
using UTIRLib.Patterns.Factory;
using UTIRLib.TickerX;
using Zenject;

#nullable enable
namespace Game.Core
{
    public abstract class Factory<TKey, TValue> : IFactory<TKey, TValue>
    {
        protected readonly DiContainer? diContainer;
        protected readonly TickerRegistry? tickerRegistry;
        protected readonly HashSet<TKey> cachedInjectables = new();
        protected readonly HashSet<TKey> cachedTickables = new();

        protected Factory(DiContainer? diContainer = null, TickerRegistry? tickerRegistry = null)
        {
            this.diContainer = diContainer;
            this.tickerRegistry = tickerRegistry;
        }

        public abstract TValue Create(TKey arg);

        protected void AddToInjectables(TKey key) => cachedInjectables.Add(key);
    }
}
