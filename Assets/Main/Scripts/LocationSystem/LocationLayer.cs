using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Core;
using UnityEngine;
using UnityEngine.Tilemaps;
using UTIRLib;
using UTIRLib.Collections;
using UTIRLib.Diagnostics;
using UTIRLib.Injector;
using UTIRLib.TwoD;
using UTIRLib.UExtensions;

#nullable enable
namespace Game.LocationSystem
{
    [RequireComponent(typeof(Tilemap), typeof(TilemapRenderer))]
    public class LocationLayer : MonoX, ILocationLayer
    {
        protected Map<ILocationCell> cellMap = null!;

        public event Action<ILocationCell>? OnCellChanged;

        [field: SerializeField]
        [field: Tooltip("If field empty, uses transform.name")]
        public string LayerName { get; protected set; } = null!;

        public virtual BoundsInt Bounds => LocationMap.Bounds;

        [GetComponent]
        public Tilemap TilemapComponent { get; protected set; } = null!;

        [GetComponent]
        public TilemapRenderer TilemapRendererComponent { get; protected set; } = null!;

        [GetComponentInParent]
        public ILocationMap LocationMap { get; protected set; } = null!;
        public ILocationCell this[Vector2Int position] => GetCell(position);
        public ILocationCell this[Vector3 position] => GetCell(position);

        IReadOnlyLocationCell IReadOnlyLocationLayer.this[Vector2Int position] => GetCell(position);
        IReadOnlyLocationCell IReadOnlyLocationLayer.this[Vector3 position] => GetCell(position);

        protected override void OnAwake()
        {
            base.OnAwake();
            TilemapComponent.size = Bounds.size;
        }

        protected override void OnStart()
        {
            base.OnStart();

            CreateLocationCells();

            Tilemap.tilemapTileChanged += OnTileChanged;
            if (LayerName.IsNullOrEmpty()) {
                LayerName = TilemapComponent.name;
            }
        }

        public void SetTile(Vector2Int position, TileBase? tile)
        {
            TilemapComponent.SetTile(position.ToVector3Int(), tile);
        }
        public void SetTile(Vector3 position, TileBase? tile)
        {
            SetTile(WorldToCell(position), tile);
        }

        public TileBase? GetTile(Vector2Int position)
        {
            return TilemapComponent.GetTile(position.ToVector3Int());
        }
        public TileBase? GetTile(Vector3 position)
        {
            return GetTile(WorldToCell(position));
        }
        public T? GetTile<T>(Vector2Int position)
            where T : TileBase
        {
            return GetTile(position) as T;
        }
        public T? GetTile<T>(Vector3 position) 
            where T : TileBase
        {
            return GetTile<T>(WorldToCell(position));
        }

        public bool TryGetTile(Vector2Int position, [NotNullWhen(true)] out TileBase? tile)
        {
            tile = GetTile(position);

            return tile != null;
        }
        public bool TryGetTile(Vector3 position, [NotNullWhen(true)] out TileBase? tile)
        {
            tile = GetTile(position);

            return tile != null;
        }
        public bool TryGetTile<T>(Vector2Int position, [NotNullWhen(true)] out T? tile) where T : TileBase
        {
            tile = GetTile<T>(position);

            return tile != null;
        }
        public bool TryGetTile<T>(Vector3 position, [NotNullWhen(true)] out T? tile) where T : TileBase
        {
            tile = GetTile<T>(position);

            return tile != null;
        }

        public ILocationCell GetCell(Vector2Int position) => cellMap[position];
        public ILocationCell GetCell(Vector3 position)
        {
            return GetCell(WorldToCell(position));
        }
        public T? GetCell<T>(Vector2Int position) where T : IReadOnlyLocationCell
        {
            if (GetCell(position) is T typedCell)
                return typedCell;

            return default;
        }
        public T? GetCell<T>(Vector3 position)
            where T : IReadOnlyLocationCell
        {
            return GetCell<T>(WorldToCell(position));
        }

        public bool TryGetCell(Vector2Int position, [NotNullWhen(true)] out ILocationCell? locationCell)
        {
            if (!InBounds(position)) {
                locationCell = null;
                return false;
            }

            locationCell = GetCell(position);

            return locationCell.IsNotNull();
        }
        public bool TryGetCell(Vector3 position, [NotNullWhen(true)] out ILocationCell? locationCell)
        {
            if (!InBounds(position)) {
                locationCell = null;
                return false;
            }

            locationCell = GetCell(position);

            return locationCell.IsNotNull();
        }
        public bool TryGetCell(Vector2Int position, [NotNullWhen(true)] out IReadOnlyLocationCell? locationCell)
        {
            if (!InBounds(position)) {
                locationCell = null;
                return false;
            }

            locationCell = GetCell(position);

            return locationCell.IsNotNull();
        }
        public bool TryGetCell(Vector3 position, [NotNullWhen(true)] out IReadOnlyLocationCell? locationCell)
        {
            if (!InBounds(position)) {
                locationCell = null;
                return false;
            }

            locationCell = GetCell(position);

            return locationCell.IsNotNull();
        }
        public bool TryGetCell<T>(Vector2Int position, [NotNullWhen(true)] out T? locationCell)
            where T : IReadOnlyLocationCell
        {
            if (!InBounds(position)) {
                locationCell = default;
                return false;
            }

            locationCell = GetCell<T>(position);

            return locationCell.IsNotDefault();
        }
        public bool TryGetCell<T>(Vector3 position, [NotNullWhen(true)] out T? locationCell)
            where T : IReadOnlyLocationCell
        {
            if (!InBounds(position)) {
                locationCell = default;
                return false;
            }

            locationCell = GetCell<T>(position);

            return locationCell.IsNotDefault();
        }

        public void ClearCell(Vector2Int position)
        {
            GetCell(position).Clear();
        }
        public void ClearCell(Vector3 position)
        {
            GetCell(position).Clear();
        }

        public bool HasTile(Vector2Int position)
        {
            return TilemapComponent.HasTile(new Vector3Int(position.x, position.y));
        }
        public bool HasTile(Vector3 position)
        {
            return HasTile(WorldToCell(position));
        }
        public bool HasTile<T>(Vector2Int position)
            where T : TileBase
        {
            return GetCell(position)?.Tile is T;
        }
        public bool HasTile<T>(Vector3 position)
            where T : TileBase
        {
            return GetCell(position)?.Tile is T;
        }

        public bool InBounds(Vector2Int position) => cellMap.Contains(position);
        public bool InBounds(Vector3 position) => cellMap.Contains(WorldToCell(position));

        public Vector2Int WorldToCell(Vector3 position) => position.FloorToVector2Int();

        public Vector3 CellToWorld(Vector2Int position) => TilemapComponent.CellToWorld(position);

        public IEnumerator<ILocationCell> GetEnumerator() => cellMap.GetEnumerator();

        public static implicit operator Tilemap(LocationLayer locationLayer) => locationLayer.TilemapComponent;

        private void OnTileChanged(Tilemap _, Tilemap.SyncTile[] tiles)
        {
            for (int i = 0; i < tiles.Length; i++) {
                if (TryGetCell(tiles[i].position, out ILocationCell? locationCell))
                    OnCellChanged?.Invoke(locationCell);
            }
        }

        private void CreateLocationCells()
        {
            cellMap = new Map<ILocationCell>(Bounds);

            int xMax = Bounds.xMax;
            int yMax = Bounds.yMax;
            Vector2Int position;
            for (int y = Bounds.yMin; y < yMax; y++) {
                for (int x = Bounds.xMin; x < xMax; x++) {
                    position = new Vector2Int(x, y);
                    cellMap[position] = new LocationCell(this, position);
                }
            }
        }

        private void OnDestroy() => Tilemap.tilemapTileChanged -= OnTileChanged;

        IReadOnlyLocationCell IReadOnlyLocationLayer.GetCell(Vector2Int position) => GetCell(position);
        IReadOnlyLocationCell IReadOnlyLocationLayer.GetCell(Vector3 position) => GetCell(position);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IEnumerator<IReadOnlyLocationCell> IEnumerable<IReadOnlyLocationCell>.GetEnumerator() => GetEnumerator();
    }
}
