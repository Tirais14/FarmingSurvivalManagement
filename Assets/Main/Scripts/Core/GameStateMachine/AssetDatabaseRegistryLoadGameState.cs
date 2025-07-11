#nullable enable
using Game.Core.DatabaseSystem;
using UnityEngine;
using UTIRLib.Patterns.StateMachine;
using UTIRLib.Patterns.State;

namespace Game.Core
{
    public sealed class AssetDatabaseRegistryLoadGameState : State
    {
        private AssetDatabaseLoader assetDatabaseLoader = new();

        public AssetDatabaseRegistry AssetDatabaseRegistry { get; private set; } = null!;

        public AssetDatabaseRegistryLoadGameState(StateParameters stateParameters) : base(stateParameters)
        {
        }

        public override void Enter()
        {
            if (IsCompleted) {
                Debug.LogError("Trying to load database more than one time.");
                return;
            }

            base.Enter();
            assetDatabaseLoader.OnLoaded += OnLoaded;
            _ = assetDatabaseLoader.LoadDatabases();
        }

        public override void Execute() { }

        public override void Exit() { }

        private void OnLoaded(AssetDatabaseRegistry assetDatabaseRegistry)
        {
            AssetDatabaseRegistry = assetDatabaseRegistry;
            assetDatabaseLoader.OnLoaded -= OnLoaded;
            assetDatabaseLoader = null!;

            LoadAssets();
        }

        private void LoadAssets()
        {
            AssetDatabasePreloader assetDatabasePreloader =
                Object.FindAnyObjectByType<AssetDatabasePreloader>();

            assetDatabasePreloader.AssetDatabaseRegistry = AssetDatabaseRegistry;
            assetDatabasePreloader.OnLoaded += () => IsCompleted = true;

            try {
                _ = assetDatabasePreloader.LoadAssetsAsync();
            }
            catch (System.Exception ex) {
                Debug.LogException(ex);
            }
        }
    }
}
