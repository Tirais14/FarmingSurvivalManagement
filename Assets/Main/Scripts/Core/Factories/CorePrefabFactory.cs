#nullable enable
using Game.Core.DatabaseSystem;
using Game.Enums.Generated;
using Game.Generated;
using Zenject;

namespace Game.Core
{
    public sealed class CorePrefabFactory : GameObjectFactory
    {
        [Inject]
        public CorePrefabFactory(AssetDatabaseRegistry assetDatabaseRegistry, DiContainer locationDiContainer) :
            base(assetDatabaseRegistry, AssetDatabaseNames.CORE, typeof(CorePrefabs), locationDiContainer)
        {
        }
    }
}