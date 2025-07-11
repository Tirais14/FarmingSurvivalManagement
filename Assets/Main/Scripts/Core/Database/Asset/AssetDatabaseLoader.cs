using System;
using System.Linq;
using UnityEngine;
using UTIRLib;
using UTIRLib.DB;
using UTIRLib.FileSystem.Json;
using UTIRLib.Json;
using UTIRLib.Utils;

#nullable enable
namespace Game.Core.DatabaseSystem
{
    public sealed class AssetDatabaseLoader
    {
        private readonly AssetDatabaseRegistry assetDatabaseRegistry = new();

        public event Action<AssetDatabaseRegistry>? OnLoaded;

        public AssetDatabaseLoader()
        {
        }

        public AssetDatabaseRegistry LoadDatabases()
        {
            CreateGroups();

            try {
                AssetType[] assetTypes = EnumHelper.GetValues<AssetType>();
                (string name, TextAsset file)[] dbFiles;

                foreach (var assetType in assetTypes) {
                    dbFiles = DatabaseFiles.GetFiles(assetType);

                    foreach ((string name, TextAsset file) in dbFiles) {
                        switch (assetType) {
                            case AssetType.Generic:
                               LoadDatabase<AssetDatabaseGeneric>(name, file, assetType);
                                break;
                            case AssetType.GameObject:
                                LoadDatabase<AssetDatabaseGameObject>(name, file, assetType);
                                break;
                            case AssetType.ScriptableObject:
                                LoadDatabase<AssetDatabaseScriptableObject>(name, file, assetType);
                                break;
                            case AssetType.Scene:
                                LoadDatabase<AssetDatabaseScene>(name, file, assetType);
                                break;
                            default:
                                throw new InvalidOperationException(assetType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex) {
                Debug.LogException(ex);
                throw;
            }

            OnLoaded?.Invoke(assetDatabaseRegistry);
            return assetDatabaseRegistry;
        }

        private static ValuePair[] GetDatabaseItems(TextAsset databaseFile)
        {
            ValuePair[] results = JsonSerializer.Deserialize<AddressableAssetInfo[]>(databaseFile.text).
                                             Select(x => new ValuePair(x.Name, x.AssetType)).
                                             ToArray();

            return results;
        }

        private static TDatabase CreateDatabase<TDatabase>(TextAsset databaseFile)
            where TDatabase : class, IDatabase, new()
        {
            IValuePair[] items = GetDatabaseItems(databaseFile).
                Select((item) => (IValuePair)item).ToArray();

            TDatabase database = new();
            database.AddValues(items);

            return database;
        }

        private void CreateGroups()
        {
            AssetDatabaseGroupGeneric genericGroup = new();
            assetDatabaseRegistry.AddGroup(AssetType.Generic, genericGroup);

            AssetDatabaseGroupGameObject gameObjectGroup = new();
            assetDatabaseRegistry.AddGroup(AssetType.GameObject, gameObjectGroup);

            AssetDatabaseGroupScriptableObject scriptableObjectGroup = new();
            assetDatabaseRegistry.AddGroup(AssetType.ScriptableObject, scriptableObjectGroup);

            AssetDatabaseGroupScene sceneGroup = new();
            assetDatabaseRegistry.AddGroup(AssetType.Scene, sceneGroup);
        }

        private void LoadDatabase<TDatabase>(string databaseName, TextAsset databaseFile, 
            AssetType assetType)
            where TDatabase : class, IDatabase, new()
        {
            TDatabase database;
            try {
                database = CreateDatabase<TDatabase>(databaseFile);
            }
            catch (Exception ex) {
                Debug.LogException(ex);
                return;
            }

            try {
                assetDatabaseRegistry[assetType].AddDatabase(databaseName, database);
            }
            catch (Exception ex) {
                Debug.LogException(ex);
            }
        }
    }
}
