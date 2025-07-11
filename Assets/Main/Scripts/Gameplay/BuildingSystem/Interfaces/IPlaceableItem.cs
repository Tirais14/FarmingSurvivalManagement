using UnityEngine.Tilemaps;
using UTIRLib.TwoD;
using UTIRLib.UI;

#nullable enable
namespace Game.Gameplay.BuildingSystem
{
    public interface IPlaceableItem : IItem
    {
        IReadOnlyTileProvider TileProvider { get; }
    }
    public interface IPlaceableItem<T> : IPlaceableItem
        where T : TileBase
    {
        new IReadOnlyTileProvider<T> TileProvider { get; }
    }
}
