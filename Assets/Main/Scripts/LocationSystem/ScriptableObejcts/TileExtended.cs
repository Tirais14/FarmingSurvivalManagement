using UnityEngine;
using UnityEngine.Tilemaps;

#nullable enable
namespace Game.LocationSystem
{
    [CreateAssetMenu(fileName = "TileExtended", menuName = "Tiles/Extended")]
    public class TileExtended : Tile
    {
        [field: SerializeField]
        public bool IsWalkable { get; protected set; }
    }
}
