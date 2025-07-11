using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Core;
using UnityEngine;
using UTIRLib;
using UTIRLib.Collections;
using UTIRLib.Diagnostics;
using UTIRLib.Extensions;

#nullable enable
namespace Game.LocationSystem
{
    [RequireComponent(typeof(Grid))]
    public class LocationMap : MonoX, ILocationMap
    {
#if UNITY_EDITOR
        [SerializeField] protected Color boundsColor = Color.red;
        [SerializeField] protected float boundsDuration = 5f;
        [SerializeField] protected bool boundsSizeLocked = true;
#endif 

        [Space]
        protected ILocationLayer[] locationLayers = null!;
        [SerializeField] protected BoundsInt bounds;

        public ILocationLayer FirstLayer => locationLayers[0];
        public BoundsInt Bounds => bounds;
        public int Count => locationLayers.Length;
        public ILocationLayer this[int index] => locationLayers[index];
        public ILocationCell this[int index, Vector2Int position] => locationLayers[index].GetCell(position);
        public ILocationCell this[int index, Vector3 position] => locationLayers[index].GetCell(position);
        public Grid GridComponent { get; protected set; } = null!;

        protected override void OnAwake()
        {
            base.OnAwake();
            GridComponent = GetComponent<Grid>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            InitializeTilemapLayers();
        }

        public ILocationLayer GetLayer(int index) => locationLayers[index];

        public ILocationLayer? FindLayer(string layerName)
        {
            for (int i = 0; i < locationLayers.Length; i++) {
                if (locationLayers[i].LayerName == layerName) {
                    return locationLayers[i];
                }
            }

            return null;
        }
        public bool TryFindLayer(string layerName, [NotNullWhen(true)] out ILocationLayer? locationLayer)
        {
            locationLayer = FindLayer(layerName);

            return locationLayer.IsNotNull();
        }

        public ILocationCell? FindCell(Vector2Int position)
        {
            for (int i = 0; i < locationLayers.Length; i++) {
                if (!locationLayers[i].TryGetCell(position, out ILocationCell? locationCell)) {
                    return locationCell;
                }
            }

            return null;
        }
        public ILocationCell? FindCell(Vector3 position) => FindCell(WorldToCell(position));

        public bool TryFindCell(Vector2Int positon, [NotNullWhen(true)] out ILocationCell? cell)
        {
            cell = FindCell(positon);

            return cell.IsNotNull();
        }
        public bool TryFindCell(Vector3 positon, [NotNullWhen(true)] out ILocationCell? cell)
        {
            cell = FindCell(positon);

            return cell.IsNotNull();
        }

        public ILocationCell GetCell(int layerIndex, Vector2Int position) => GetLayer(layerIndex).GetCell(position);
        public ILocationCell GetCell(int layerIndex, Vector3 position) => GetLayer(layerIndex).GetCell(position);
        public T? GetCell<T>(int layerIndex, Vector2Int position) where T : ILocationCell =>
            GetLayer(layerIndex).GetCell<T>(position);
        public T? GetCell<T>(int layerIndex, Vector3 position) where T : ILocationCell =>
            GetLayer(layerIndex).GetCell<T>(position);

        public bool TryGetCell<T>(int layerIndex, Vector2Int position, [NotNullWhen(true)] out T? locationCell)
            where T : ILocationCell
        {
            locationCell = GetCell<T>(layerIndex, position);

            return locationCell is not null;
        }
        public bool TryGetCell<T>(int layerIndex, Vector3 position, [NotNullWhen(true)] out T? locationCell)
            where T : ILocationCell
        {
            locationCell = GetCell<T>(layerIndex, position);

            return locationCell is not null;
        }

        public Vector2Int WorldToCell(Vector3 position) => FirstLayer.WorldToCell(position);

        public Vector3 CellToWorld(Vector2Int position) => FirstLayer.CellToWorld(position);

        public IEnumerator<ILocationLayer> GetEnumerator() => new DefaultEnumerator<ILocationLayer>(locationLayers);

        private void InitializeTilemapLayers()
        {
            locationLayers = GetComponentsInChildren<ILocationLayer>();
            if (locationLayers.IsNullOrEmpty()) {
                Debug.LogError("Cannot find any tilemap layer.");
            }
        }

#if UNITY_EDITOR
        private void LockBoundsSize() => bounds.position =
            new Vector3Int(Bounds.size.x / 2 * -1, Bounds.size.y / 2 * -1, Bounds.size.z / 2 * -1);

        private void DrawBounds()
        {
            Vector3 min = Bounds.min;
            Vector3 max = Bounds.max;

            Debug.DrawLine(new Vector3(min.x, min.y, min.z), new Vector3(max.x, min.y, min.z),
                boundsColor, boundsDuration);
            Debug.DrawLine(new Vector3(max.x, min.y, min.z), new Vector3(max.x, max.y, min.z),
                boundsColor, boundsDuration);
            Debug.DrawLine(new Vector3(max.x, max.y, min.z), new Vector3(min.x, max.y, min.z),
                boundsColor, boundsDuration);
            Debug.DrawLine(new Vector3(min.x, max.y, min.z), new Vector3(min.x, min.y, min.z),
                boundsColor, boundsDuration);
        }

        private void OnValidate()
        {
            if (boundsSizeLocked) {
                LockBoundsSize();
            }
        }

        private void OnDrawGizmos() => DrawBounds();
#endif

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}