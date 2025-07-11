using System;
using System.Threading.Tasks;
using Game.Core.DatabaseSystem;
using Game.Generated;
using UnityEngine;
using UTIRLib;
using UTIRLib.DB;
using UTIRLib.EnumFlags;
using UTIRLib.Utils;

#nullable enable
namespace Game.Core
{
    public class AssetDatabasePreloader : MonoX
    {
        [SerializeField]
        private AssetDatabaseName databaseNames;

        public event Action? OnLoaded;

        public AssetDatabaseRegistry AssetDatabaseRegistry { get; set; } = null!;

        public async Task LoadAssetsAsync()
        {
            try {
                AssetDatabaseName[] databaseNamesArray = databaseNames.ToArrayByFlags();
                AssetType[] dbTypes = EnumHelper.GetValues<AssetType>();
                foreach (var dbType in dbTypes) {
                    if (!AssetDatabaseRegistry.Contains(dbType)) continue;

                    IAssetDatabaseGroup? assetDatabaseGroup =
                    AssetDatabaseRegistry.GetDatabaseGroup<IAssetDatabaseGroup>(dbType);

                    if (assetDatabaseGroup == null) continue;

                    foreach (var dbName in databaseNamesArray) {
                        if (!assetDatabaseGroup.ContainsKey(dbName.ToString())) continue;

                        IAssetDatabase assetDatabase = assetDatabaseGroup.GetDatabase(dbName.ToString());

                        await assetDatabase.LoadAllAssetsAsync();
                    }
                }

                OnLoaded?.Invoke();

                Destroy(gameObject);
            }
            catch (Exception ex) {
                Debug.LogException(ex);
                throw;
            }
        }
    }
}
