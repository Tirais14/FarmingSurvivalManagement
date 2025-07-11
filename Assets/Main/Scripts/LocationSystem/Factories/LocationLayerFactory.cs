using Game.Core;
using Game.Core.DatabaseSystem;
using Game.Generated;
using Zenject;

#nullable enable
namespace Game.LocationSystem
{
    public class LocationLayerFactory : GameObjectFactory
    {
        public LocationLayerFactory(AssetDatabaseRegistry assetDatabaseRegistry, DiContainer diContainer)
            : base(assetDatabaseRegistry, AssetDatabaseNames.MAP, typeof(MapPrefabs), diContainer)
        {
        }
    }
}
