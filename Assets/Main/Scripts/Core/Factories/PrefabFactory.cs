#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Game.Core.DatabaseSystem;
using UnityEngine;
using UTIRLib;
using UTIRLib.Collections;
using UTIRLib.DB;
using UTIRLib.Diagnostics;
using UTIRLib.Extensions;
using UTIRLib.TickerX;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Core
{
    public abstract class PrefabFactory<TPrefab> : Factory<string, TPrefab>
        where TPrefab : Object
    {
        private readonly MethodInfo setActiveMethod;
        protected readonly Dictionary<string, TPrefab> prefabs = new();
        protected readonly IAssetDatabaseGroup assetDatabaseGroup;

        public event Action<TPrefab>? OnInstantiated;

        protected PrefabFactory(IAssetDatabaseGroup assetDatabaseGroup,
                                KeyValuePair<string, TPrefab>[] prefabs,
                                DiContainer? diContainer = null,
                                TickerRegistry? tickerRegistry = null) : base(diContainer,
                                                                              tickerRegistry)
        {
            setActiveMethod = typeof(TPrefab).GetMethod(
                nameof(GameObject.SetActive),
                BindingFlagsDefault.InstancePublic.ToBindingFlags()).
                    ThrowIfNull($"Wasn't found {nameof(GameObject.SetActive)} method.");

            this.assetDatabaseGroup = assetDatabaseGroup;
            this.prefabs.AddRange(prefabs);
        }
        protected PrefabFactory(IAssetDatabaseGroup assetDatabaseGroup,
                                AssetDatabaseKey[] assetDatabaseKeys,
                                DiContainer? diContainer = null,
                                TickerRegistry? tickerRegistry = null) : this(
                                    assetDatabaseGroup,
                                    AssetDatabaseKeysToAssets(assetDatabaseGroup, assetDatabaseKeys),
                                    diContainer,
                                    tickerRegistry)
        {
        }
        protected PrefabFactory(IAssetDatabaseGroup assetDatabaseGroup,
                                string databaseName,
                                string[] assetNames,
                                DiContainer? diContainer = null,
                                TickerRegistry? tickerRegistry = null) : this(
                                    assetDatabaseGroup,
                                    GetPrefabs(assetDatabaseGroup, databaseName, assetNames),
                                    diContainer,
                                    tickerRegistry)
        {
        }
        protected PrefabFactory(IAssetDatabaseGroup assetDatabaseGroup,
                                string databaseName,
                                Type assetNameEnumType,
                                DiContainer? diContainer = null,
                                TickerRegistry? tickerRegistry = null) : this(
                                    assetDatabaseGroup,
                                    databaseName,
                                    Enum.GetNames(assetNameEnumType).Where(x => x != "None").ToArray(),
                                    diContainer,
                                    tickerRegistry)
        {
        }

        /// <exception cref="ArgumentNullException"></exception>
        public override TPrefab Create(string key) => CreateInternal(key, parent: null);

        protected TPrefab CreateInternal(string prefabName, Transform? parent = null)
        {
            if (prefabName is null) {
                throw new ArgumentNullException(nameof(prefabName));
            }
            if (!prefabs.ContainsKey(prefabName)) {
                throw new Exception($"Cannot find prefab {prefabName}.");
            }

            TPrefab instantiated;

            instantiated = Object.Instantiate(prefabs[prefabName], parent);

            SetActive(instantiated, false);

            OnInstantiated?.Invoke(instantiated);

            if (IsDiContainerInjectable(prefabName, instantiated)) {
                InjectByDiContainer(instantiated);
            }

            if (IsAlternativeTickable(prefabName, instantiated)) {
                RegisterAlternativeTickable(instantiated);
            }

            SetActive(instantiated, true);

            return instantiated;
        }

        protected abstract bool IsDiContainerInjectable(string prefabName, TPrefab prefab);

        protected abstract bool IsAlternativeTickable(string prefabName, TPrefab prefab);

        protected abstract void InjectByDiContainer(TPrefab instantiated);

        protected abstract void RegisterAlternativeTickable(TPrefab instantiated);

        protected void SetActive(TPrefab instantiated, bool state)
        {
            setActiveMethod?.Invoke(instantiated, state);
        }

        private static KeyValuePair<string, TPrefab>[] GetPrefabs(IAssetDatabaseGroup assetDatabaseGroup,
            string databaseName, string[] assetNames)
        {
            AssetDatabaseKey[] assetKeys = AssetNamesToAssetKeys(databaseName, assetNames);

            return AssetDatabaseKeysToAssets(assetDatabaseGroup, assetKeys);
        }

        private static KeyValuePair<string, TPrefab>[] AssetDatabaseKeysToAssets(IAssetDatabaseGroup assetDatabaseGroup,
            AssetDatabaseKey[] assetDatabaseKeys)
        {
            var assets = new KeyValuePair<string, TPrefab>[assetDatabaseKeys.Length];
            string assetKey;
            TPrefab asset;
            for (int i = 0; i < assets.Length; i++) {
                assetKey = assetDatabaseKeys[i].AssetName;

                asset = assetDatabaseGroup.GetDatabase(assetDatabaseKeys[i].DatabaseName)
                                          .GetAsset<TPrefab>(assetKey)
                                          .ThrowIfNull($"Asset with name {assetKey} wasn't found.");

                assets[i] = new KeyValuePair<string, TPrefab>(assetKey, asset);
            }

            return assets;
        }

        private static AssetDatabaseKey[] AssetNamesToAssetKeys(string databaseName, string[] assetNames)
        {
            var assets = new AssetDatabaseKey[assetNames.Length];
            for (int i = 0; i < assetNames.Length; i++) {
                assets[i] = new AssetDatabaseKey(databaseName, assetNames[i]);
            }

            return assets;
        }
    }
}