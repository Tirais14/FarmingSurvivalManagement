using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.AI
{
    public class LocationPath
    {
        public readonly static LocationPath Empty = new();
        public Stack<ILocationCell> CellsStack { get; private set; }
        public bool IsValid => CellsStack != null && CellsStack.Count > 0;
        public LocationPath() => CellsStack = new Stack<ILocationCell>();
        public LocationPath(int capacity) => CellsStack = new Stack<ILocationCell>(capacity);

        public static LocationPath RetraceFromTarget(LocationPathCell targetCell)
        {
            if (!targetCell.IsConnected) return Empty;

            LocationPath path = new();
            LocationPathCell cell = targetCell;
            path.Add(targetCell);
            while (cell.IsConnected) {
                cell = cell.ConnectedCell;
                path.Add(cell);
            }

            return path;
        }

        public void Add(LocationPathCell cell) => CellsStack.Push(cell.LocationCell);

        public ILocationCell Peek() => CellsStack.Peek();

        public Vector2 PeekPosition() => CellsStack.Peek().CenterPosition;

        public bool TryPeek(out ILocationCell cell) => CellsStack.TryPeek(out cell);

        public bool TryPeekPosition(out Vector2? position)
        {
            bool result = CellsStack.TryPeek(out ILocationCell cell);
            if (result) {
                position = cell.CenterPosition;
                return result;
            }

            position = null;
            return result;
        }

        public void RemoveFirst() => CellsStack.Pop();

        public ILocationCell TakeNext()
        {
            if (CellsStack.Count > 0) { return CellsStack.Pop(); }

            return null;
        }

        public Vector2? TakeNextPosition()
        {
            if (CellsStack.Count > 0 && Peek() != null) { return CellsStack.Pop().CenterPosition; }

            return null;
        }

        public bool TryTakeNext(out ILocationCell cell) => CellsStack.TryPop(out cell);

        public bool TryTakeNextPosition(out Vector2? posiition)
        {
            bool result = CellsStack.TryPop(out ILocationCell cell);
            if (result) {
                posiition = cell.CenterPosition;
                return result;
            }

            posiition = null;
            return result;
        }
    }
}