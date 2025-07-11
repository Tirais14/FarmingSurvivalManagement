using UnityEngine;
using UnityEngine.Tilemaps;
using UTIRLib;
using UTIRLib.Injector;
using System;
using UTIRLib.Diagnostics;
using System.Collections.Generic;

#nullable enable
namespace Core.Map
{
    public abstract class Location : MonoX, ILocation
    {
        private readonly Dictionary<Vector3Int, ILocationCell> cells = new();
        [GetComponent]
        private readonly Tilemap tilemap = null!;

        public int Height => throw new System.NotImplementedException();
        public int Width => throw new System.NotImplementedException();

        public ILocationCell? this[Vector3Int pos] {
            get => GetCell(pos);
            set => SetCell(pos, value);
        }

        public ILocationCell? GetCell(Vector3Int pos)
        {
            if (!InBounds(pos))
                throw new ArgumentOutOfRangeException(nameof(pos));

            if (cells.TryGetValue(pos, out ILocationCell cell))
                return cell;

            return null;
        }

        public bool InBounds(Vector3Int pos)
        {
            return pos.x < Width && pos.y < Height;
        }

        public bool RemoveCell(Vector3Int pos)
        {
            if (!InBounds(pos))
                return false;

            if (!cells.TryGetValue(pos, out _))
                return false;

            cells.Remove(pos);
            tilemap.SetTile(pos, null);

            return true;
        }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetCell(Vector3Int pos, ILocationCell? cell)
        {
            if (InBounds(pos))
                throw new ArgumentOutOfRangeException(nameof(pos));

            if (cell.IsNull())
            {
                RemoveCell(pos);

                return;
            }

            tilemap.SetTile(pos, cell.Tile);
        }
    }
}
