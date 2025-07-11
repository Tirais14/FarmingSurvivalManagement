#nullable enable
using System;
using System.Collections.Generic;
using Game.Core.DatabaseSystem;
using UnityEngine;
using UTIRLib.Diagnostics;
using UTIRLib.Patterns.Factory;
using UTIRLib.TickerX;
using UTIRLib.UExtensions;
using Zenject;

namespace Game.Core
{
    public class GameObjectFactory : PrefabFactory<GameObject>,
        IFactory<string, Transform, GameObject>,
        IFactory<Enum, GameObject>, IFactory<Enum, Transform, GameObject>,
        IFactory<string, Component, GameObject>,
        IFactory<Enum, Component, GameObject>
    {
        public GameObjectFactory(AssetDatabaseRegistry assetDatabaseRegistry,
                                 KeyValuePair<string, GameObject>[] prefabs,
                                 DiContainer? diContainer = null,
                                 TickerRegistry? tickerRegistry = null) : base(
                                     assetDatabaseRegistry.GameObjects,
                                     prefabs,
                                     diContainer,
                                     tickerRegistry)
        {
        }
        public GameObjectFactory(AssetDatabaseRegistry assetDatabaseRegistry,
                                 AssetDatabaseKey[] assetDatabaseKeys,
                                 DiContainer? diContainer = null,
                                 TickerRegistry? tickerRegistry = null) : base(assetDatabaseRegistry.GameObjects,
                                     assetDatabaseKeys,
                                     diContainer,
                                     tickerRegistry)
        {
        }
        public GameObjectFactory(AssetDatabaseRegistry assetDatabaseRegistry,
                                 string databaseName,
                                 string[] assetNames,
                                 DiContainer? diContainer = null,
                                 TickerRegistry? tickerRegistry = null) : base(
                                     assetDatabaseRegistry.GameObjects,
                                     databaseName,
                                     assetNames,
                                     diContainer,
                                     tickerRegistry)
        {
        }
        public GameObjectFactory(AssetDatabaseRegistry assetDatabaseRegistry,
                                 string databaseName,
                                 Type assetNameEnumType,
                                 DiContainer? diContainer = null,
                                 TickerRegistry? tickerRegistry = null) : base(
                                     assetDatabaseRegistry.GameObjects,
                                     databaseName,
                                     assetNameEnumType,
                                     diContainer,
                                     tickerRegistry)
        {
        }

        public GameObject Create(Enum prefabName) => Create(prefabName.ToString());
        public T? Create<T>(string prefabName) => Create(prefabName).
            IfNotNullQ((created) => created.GetAssignedObject<T>());
        public T? Create<T>(Enum prefabName) where T : Component => Create<T>(prefabName.ToString());
        /// <exception cref="ArgumentNullException"></exception>
        public GameObject Create(string prefabName, Transform parent)
        {
            if (prefabName is null) {
                throw new ArgumentNullException(nameof(prefabName));
            }
            if (parent == null) {
                throw new ArgumentNullException(nameof(parent));
            }

            return CreateInternal(prefabName, parent);
        }
        public GameObject Create(string prefabName, Component parent) => Create(prefabName.ToString(), parent.transform);
        public GameObject Create(Enum prefabName, Transform parent) => Create(prefabName.ToString(), parent);
        public GameObject Create(Enum prefabName, Component parent) => Create(prefabName.ToString(), parent.transform);
        public T? Create<T>(string prefabName, Transform parent) => Create(prefabName, parent).GetComponent<T>();
        public T? Create<T>(string prefabName, Component parent) => Create<T>(prefabName, parent.transform);
        public T? Create<T>(Enum prefabName, Transform parent) => Create<T>(prefabName.ToString(), parent);
        public T? Create<T>(Enum prefabName, Component parent) => Create<T>(prefabName.ToString(), parent.transform);

        protected override bool IsDiContainerInjectable(string prefabName, GameObject prefab)
        {
            if (diContainer is null) {
                return false;
            }

            if (cachedInjectables.Contains(prefabName)) {
                return true;
            }
            else if (TickableHelper.IsTickable(prefab)) {
                cachedInjectables.Add(prefabName);
                return true;
            }

            return false;
        }

        protected override bool IsAlternativeTickable(string prefabName, GameObject prefab)
        {
            if (tickerRegistry is null) {
                return false;
            }

            if (cachedTickables.Contains(prefabName)) {
                return true;
            }
            else if (TickableHelper.IsTickable(prefab)) {
                cachedTickables.Add(prefabName);
                return true;
            }

            return false;
        }

        protected override void InjectByDiContainer(GameObject instantiated) =>
            diContainer!.InjectGameObject(instantiated);

        protected override void RegisterAlternativeTickable(GameObject instantiated) =>
            tickerRegistry!.Register(instantiated);
    }
}
