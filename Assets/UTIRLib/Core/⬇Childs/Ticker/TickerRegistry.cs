using System;
using System.Collections.Generic;
using UnityEngine;
using UTIRLib.Diagnostics;
using UTIRLib.UExtensions;
using UTIRLib.Utils;

#nullable enable

namespace UTIRLib.TickerX
{
    public sealed class TickerRegistry
    {
        private readonly Dictionary<Type, ITicker> registeredTickers = new();
        private readonly Type defaultTickerType;

        /// <exception cref="ArgumentNullException"></exception>
        public TickerRegistry(Type defaultTickerType, bool registerTickables = false)
        {
            this.defaultTickerType = defaultTickerType ?? throw new ArgumentNullException(nameof(defaultTickerType));
            RegisterTickers();
            if (registerTickables)
            {
                RegisterAllGameObjectComponents();
            }
        }

        /// <exception cref="ArgumentNullException"></exception>
        public void Register(ITickableBase tickableBase, Type? tickerType = null)
        {
            if (tickableBase.IsNull())
            {
                throw new ArgumentNullException(nameof(tickableBase));
            }

            tickerType ??= TickableHelper.GetCustomTickableAttribute(tickableBase)?.TickerType;
            tickerType ??= defaultTickerType;
            if (!registeredTickers.TryGetValue(tickerType, out ITicker ticker))
            {
                Debug.LogWarning($"Tickable: {tickableBase.GetTypeName()} wasn't registered in ticker: {tickerType.Name}.");
                return;
            }

            ticker.Register(tickableBase);
        }

        public void Register<T>(T obj, Type? tickerType = null) where T : ITickableBase => Register(obj, tickerType);

        public void Register<T, TTicker>(T obj)
            where T : ITickableBase
            where TTicker : ITicker => Register(obj, typeof(TTicker));

        public void Register(GameObject gameObject, Type? tickerType = null)
        {
            ITickableBase[] tickables = gameObject.GetAssignedObjects<ITickableBase>();
            for (int i = 0; i < tickables.Length; i++)
            {
                Register(tickables[i], tickerType);
            }
        }

        public void Register<TTicker>(GameObject gameObject)
            where TTicker : ITicker => Register(gameObject, typeof(TTicker));

        private void RegisterAllGameObjectComponents()
        {
            UnregisterAllTickables();
            ITickableBase[] tickableComponents = GetAllTickableComponents();
            int allGameObjectComponentsCount = tickableComponents.Length;
            for (int i = 0; i < allGameObjectComponentsCount; i++)
            {
                Register(tickableComponents[i],
                    TickableHelper.GetCustomTickableAttribute(tickableComponents[i].GetType())?.TickerType);
            }
        }

        private void UnregisterAllTickables()
        {
            foreach (var ticker in registeredTickers.Values)
            {
                ticker.UnregisterAll();
            }
        }

        private void RegisterTickers()
        {
            ITicker[] tickers = GetAllTickers();
            for (int i = 0; i < tickers.Length; i++)
            {
                registeredTickers.Add(tickers[i].GetType(), tickers[i]);
            }
        }

        private static ITickableBase[] GetAllTickableComponents() =>
            UnityObjectHelper.FindObjectsByType<ITickableBase>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        /// <exception cref="ObjectNotFoundException"></exception>
        private static ITicker[] GetAllTickers()
        {
            ITicker[] tickers = UnityObjectHelper.FindObjectsByType<ITicker>(FindObjectsInactive.Include,
                FindObjectsSortMode.None);

            if (tickers == null || tickers.Length == 0)
            {
                throw new WrongCollectionException("Not found tickers on scene.");
            }

            return tickers;
        }
    }
}