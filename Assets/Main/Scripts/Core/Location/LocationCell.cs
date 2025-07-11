#nullable enable
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Map
{
    public class LocationCell : ScriptableObject, ILocationCell
    {
        [field: SerializeField] public TileBase Tile { get; private set; } = null!;
    }
}
