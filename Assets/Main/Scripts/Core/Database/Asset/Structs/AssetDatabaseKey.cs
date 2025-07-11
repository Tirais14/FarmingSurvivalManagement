#nullable enable
using System;
using System.Collections.Generic;
using UTIRLib.Extensions;

namespace Game.Core.DatabaseSystem
{
    public readonly struct AssetDatabaseKey
    {
        public readonly string DatabaseName;
        public readonly string AssetName;

        public AssetDatabaseKey(string databaseName, string assetName)
        {
            DatabaseName = databaseName;
            AssetName = assetName;
        }
        public AssetDatabaseKey(Enum databaseNameEnum, string assetName) : this(databaseNameEnum.ToString(), assetName)
        { }
        public AssetDatabaseKey(string databaseName, Enum assetNameEnum) :
            this(databaseName, assetNameEnum.ToString())
        { }
        public AssetDatabaseKey(Enum databaseNameEnum, Enum assetNameEnum) :
            this(databaseNameEnum.ToString(), assetNameEnum.ToString())
        { }

        public static implicit operator KeyValuePair<string, string>(AssetDatabaseKey assetDatabaseKey) =>
            new(assetDatabaseKey.DatabaseName, assetDatabaseKey.AssetName);
    }
}
