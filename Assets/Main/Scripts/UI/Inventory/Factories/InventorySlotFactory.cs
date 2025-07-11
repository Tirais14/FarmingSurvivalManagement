using System;
using System.Linq;
using Game.Core;
using Game.Core.DatabaseSystem;
using Game.Generated;
using Zenject;

#nullable enable
namespace Game.InventorySystem
{
    public class InventorySlotFactory : GameObjectFactory
    {
        [Inject]
        public InventorySlotFactory(AssetDatabaseRegistry assetDatabaseRegistry, DiContainer? diContainer = null)
            : base(assetDatabaseRegistry, AssetDatabaseNames.UI,
                  Enum.GetNames(typeof(UIPrefabs)).
                      Where((name) => name.Contains(UIPrefabs.InventorySlot.ToString())).
                      ToArray(), diContainer)
        {
        }
    }
}
