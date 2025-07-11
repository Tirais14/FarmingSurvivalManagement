using System;
using System.Diagnostics.CodeAnalysis;
using Game.Core.DatabaseSystem;
using UTIRLib.UI;
using Zenject;

#nullable enable
namespace Game.StoreSystem
{
    public class Store : StorageModel
    {
        protected IItem[] goods = null!;
        protected AssetDatabaseRegistry assetDatabaseRegistry = null!;

        [Inject]
        [SuppressMessage("", "IDE0051")]
        private void Construct(AssetDatabaseRegistry assetDatabaseRegistry) =>
            this.assetDatabaseRegistry = assetDatabaseRegistry;

        protected override void OnStart()
        {
            base.OnStart();
            goods = Array.Empty<IItem>();
        }

        public override void Open()
        {
            base.Open();
            AddGoods();
        }

        protected void AddGoods()
        {
            ClearSlots();
            for (int i = 0; i < goods.Length; i++) {
                AddItem(goods[i], goods[i].MaxQuantity);
            }
        }
    }
}
