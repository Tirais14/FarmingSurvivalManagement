using UnityEngine;

#nullable enable
namespace Core.Map
{
    public interface ILocation
    {
        int Height { get; }
        int Width { get; }

        ILocationCell? this[Vector3Int pos] { get; set; }

        void SetCell(Vector3Int pos, ILocationCell cell);

        ILocationCell? GetCell(Vector3Int pos);

        bool RemoveCell(Vector3Int pos);

        bool InBounds(Vector3Int pos);
    }
}
