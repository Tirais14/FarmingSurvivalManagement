using UnityEngine;
using UnityEngine.Tilemaps;

#nullable enable
namespace Core.Map.Tiles
{
    [CreateAssetMenu(fileName = nameof(LocationTile),
        menuName = "Game/Tiles/Basic",
        order = G.Editor.GAME_ASSET_MENU_ORDER)]
    public class LocationTile : Tile
    {
    }
}
