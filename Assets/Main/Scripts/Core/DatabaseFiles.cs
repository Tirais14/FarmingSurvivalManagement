using System;
using System.Collections.Generic;
using Game.Generated;
using UnityEngine;
using UTIRLib;
using UTIRLib.Utils;

namespace Game.Core
{
    public static class DatabaseFiles
    {
        private static readonly Dictionary<string, Dictionary<AssetType, TextAsset>> files = new();

        static DatabaseFiles()
        {
            LoadFiles();
        }

        /// <exception cref="KeyNotFoundException"></exception>
        public static TextAsset GetFile(string databaseName, AssetType assetType)
        {
            if (!files.ContainsKey(databaseName)) {
                throw new KeyNotFoundException($"Not found database file: {databaseName}");
            }

            return files[databaseName][assetType];
        }

        public static (string databaseName, TextAsset)[] GetFiles(AssetType assetType)
        {
            List<(string databaseName, TextAsset)> results = new();

            foreach (var filesItem in files) {
                foreach (var db in filesItem.Value) {
                    if (db.Key == assetType) {
                        results.Add((filesItem.Key, db.Value));
                    }
                }
            }

            return results.ToArray();
        }

        private static AssetType GetAssetType(TextAsset textAsset)
        {
            string assetTypeStr = NameBuilder.GetWord(textAsset.name);

            if (!Enum.TryParse(assetTypeStr, out AssetType assetType)) {
                throw new Exception($"Failed to parse {assetTypeStr}.");
            }

            return assetType;
        }

        private static string GetName(TextAsset textAsset) => NameBuilder.GetPrefix(textAsset.name);

        private static void LoadFiles()
        {
            TextAsset[] databaseFiles = Resources.LoadAll<TextAsset>(ResourcesDirectory.AssetDatabases_Relative);
            if (databaseFiles.Length == 0) {
                throw new Exception("Database files not loaded.");
            }

            AssetType assetType;
            string databaseName;
            foreach (var databaseFile in databaseFiles) {
                assetType = GetAssetType(databaseFile);
                databaseName = GetName(databaseFile);

                if (!files.ContainsKey(databaseName)) {
                    files.Add(databaseName, new Dictionary<AssetType, TextAsset>());
                }

                files[databaseName].Add(assetType, databaseFile);
            }
        }
    }
}