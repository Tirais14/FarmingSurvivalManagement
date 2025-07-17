using UnityEngine;

#nullable enable
namespace Core.Map.Tiles
{
    [CreateAssetMenu(fileName = nameof(FertileTile),
        menuName = "Game/Tiles/Fertile", 
        order = G.Editor.GAME_ASSET_MENU_ORDER)]
    public class FertileTile : LocationTile
    {
        
    }
}
