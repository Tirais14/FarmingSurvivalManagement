using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Core;
using UnityEngine;
using UnityEngine.Tilemaps;
using UTIRLib.Diagnostics;
using UTIRLib.TwoD;
using UTIRLib.Utils;

#nullable enable
namespace Game.LocationSystem
{
    public class LocationCell : ILocationCell
    {
        protected readonly ILocationLayer locationLayer;

        public ILocationLayer LocationLayer => locationLayer;
        public TileBase? Tile {
            get => locationLayer.GetTile(Position);
            set => SetTile(value);
        }
        public Vector2 Size => locationLayer.TilemapComponent.cellSize;
        public Vector2Int Position { get; protected set; }
        public Vector2 CenterPosition { get; protected set; }
        public bool HasTile => Tile != null;
        public int X => Position.x;
        public int Y => Position.y;

        public LocationCell(ILocationLayer locationLayer, Vector2Int position, TileBase? tile = null)
        {
            this.locationLayer = locationLayer;
            Position = position;
            CalculateCenterPoint();

            if (tile != null) 
                SetTile(tile);
        }

        public T? GetTile<T>() where T : TileBase => Tile as T;

        public bool TryGetTile<T>([NotNullWhen(true)] out T? tile) where T : TileBase
        {
            tile = GetTile<T>();

            return tile != null;
        }

        public void SetTile(TileBase? tile) => locationLayer.SetTile(Position, tile);

        public void Clear() => SetTile(null);

        public ILocationCell? GetNeighbour(Direction2D direction)
        {
            return LocationLayer.GetCell(Position + direction.ToVector2Int());
        }

        public bool TryGetNeighbour(Direction2D direction, [NotNullWhen(true)] out IReadOnlyLocationCell? locationCell)
        {
            locationCell = GetNeighbour(direction);

            return locationCell.IsNotNull();
        }
        public bool TryGetNeighbour(Direction2D direction, [NotNullWhen(true)] out ILocationCell? locationCell)
        {
            locationCell = GetNeighbour(direction);

            return locationCell.IsNotNull();
        }

        public ILocationCell[] GetNeighbours()
        {
            Direction2D[] directions = EnumHelper.GetValues<Direction2D>();
            List<ILocationCell> neighbours = new(directions.Length / 2);
            int directionsCount = directions.Length;
            for (int i = 0; i < directionsCount; i++) {
                if (TryGetNeighbour(directions[i], out ILocationCell? locationCell))
                    neighbours.Add(locationCell);
            }

            return neighbours.Count > 0 ? neighbours.ToArray() : Array.Empty<ILocationCell>();
        }

        private void CalculateCenterPoint() =>
            CenterPosition = new Vector2(Position.x + Size.x / 2f, Position.y + Size.y / 2f);

        public static implicit operator TileBase?(LocationCell locationCell) => ((ILocationCell)locationCell).Tile;

        IReadOnlyLocationCell? IReadOnlyLocationCell.GetNeighbour(Direction2D direction) => GetNeighbour(direction);

        IReadOnlyLocationCell[] IReadOnlyLocationCell.GetNeighbours() => GetNeighbours();
    }
    public class LocationCell<T> : LocationCell, ILocationCell<T>
        where T : TileBase
    {
        new public T? Tile {
            get => locationLayer.GetTile<T>(Position);
            set => SetTile(value);
        }

        TileBase? ITileProvider.Tile { get => Tile; set => SetTile(value); }
        TileBase? IReadOnlyTileProvider.Tile => Tile;

        public void SetTile(T? tile) => SetTile(tile as TileBase);

        public LocationCell(ILocationLayer locationLayer, Vector2Int position, TileBase? tile = null)
            : base(locationLayer, position, tile)
        {
        }

        public static implicit operator TileBase?(LocationCell<T> locationCell)
        {
            return ((ILocationCell)locationCell).Tile;
        }
    }
}