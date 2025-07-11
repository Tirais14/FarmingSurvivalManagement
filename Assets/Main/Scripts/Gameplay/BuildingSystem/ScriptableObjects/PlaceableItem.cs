#nullable enable
using Game.Core;
using UnityEngine;
using UnityEngine.Tilemaps;
using UTIRLib.TwoD;

namespace Game.Gameplay.BuildingSystem
{
    [CreateAssetMenu(fileName = "PlaceableItem", menuName = "Items/Placeable Item")]
    public class PlaceableItem : Item, IPlaceableItem
    {
        [SerializeField] protected TileProvider tileProvider = null!;

        public IReadOnlyTileProvider TileProvider => tileProvider;
    }
    public class PlaceableItem<T> : Item, IPlaceableItem<T>
        where T : TileBase
    {
        [SerializeField] protected TileProvider<T> tileProvider = null!;

        public IReadOnlyTileProvider<T> TileProvider => tileProvider;

        IReadOnlyTileProvider IPlaceableItem.TileProvider => TileProvider;
    }
}
