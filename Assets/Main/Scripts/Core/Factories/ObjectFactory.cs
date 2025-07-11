using System.Collections.Generic;
using System.Reflection;
using UTIRLib;
using UTIRLib.TickerX;
using Zenject;

#nullable enable
namespace Game.Core
{
    public abstract class ObjectFactory<TKey, TObject> : Factory<TKey, TObject>
    {
        protected readonly Dictionary<TKey, ConstructorInfo> constructors = new();

        protected ObjectFactory(KeyValuePair<TKey, ConstructorInfo>[] constructors,
                                DiContainer? diContainer = null,
                                TickerRegistry? tickerRegistry = null) : base(diContainer,
                                                                              tickerRegistry)
        {
            this.constructors.AddRange(constructors);
        }
    }
}
