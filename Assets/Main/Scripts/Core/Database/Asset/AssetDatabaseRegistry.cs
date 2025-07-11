using System;
using UTIRLib;
using UTIRLib.DB;

#nullable enable
namespace Game.Core.DatabaseSystem
{
    public sealed class AssetDatabaseRegistry : DatabaseRegistry<Enum>
    {
        private AssetDatabaseGroupGameObject? gameObjects;
        private AssetDatabaseGroupScriptableObject? scriptableObjects;
        private AssetDatabaseGroupScene? scenes;

        public AssetDatabaseGroupGameObject GameObjects {
            get {
                gameObjects ??= (AssetDatabaseGroupGameObject)databaseGroups[AssetType.GameObject];

                return gameObjects;
            }
        }
        public AssetDatabaseGroupScriptableObject ScriptableObjects {
            get {
                scriptableObjects ??= (AssetDatabaseGroupScriptableObject)databaseGroups[AssetType.ScriptableObject];

                return scriptableObjects;
            }
        }
        public AssetDatabaseGroupScene Scenes {
            get {
                scenes ??= (AssetDatabaseGroupScene)databaseGroups[AssetType.Scene];

                return scenes;
            }
        }
    }
}
