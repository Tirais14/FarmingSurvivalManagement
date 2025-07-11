using System;
using Game.Core;
using UnityEngine;

#nullable enable
namespace Game.AI
{
    public class LocationPathCell
    {
        private const int DIAGONAL_TRAVEL_COST = 14;
        private const int STRAIGHT_TRAVEL_COST = 10;
        private readonly ILocationCell locationCell;
        private Vector2Int? targetCellPosition;

        public int TravelCost { get; set; } = int.MaxValue;
        public int HeuristicCost { get; private set; } = int.MaxValue;
        public int TotalCost => TravelCost + HeuristicCost;
        public bool IsConnected => ConnectedCell != null;
        public Vector2Int Position => locationCell.Position;
        public Vector2Int? TargetCellPosition {
            get => targetCellPosition;
            set {
                targetCellPosition = value;
                CalculateHeuristicCost();
            }
        }
        public Vector2Int ConnectedCellPosition => ConnectedCell?.Position ?? new Vector2Int(int.MinValue, int.MinValue);
        public ILocationCell LocationCell => locationCell;
        public LocationPathCell? ConnectedCell { get; set; }

        public LocationPathCell(ILocationCell locationCell) => this.locationCell = locationCell;
        public LocationPathCell(ILocationCell locationCell, LocationPathCell? connectedCell) : this(locationCell) =>
            ConnectedCell = connectedCell;
        public LocationPathCell(ILocationCell locationCell, LocationPathCell? connectedCell, Vector2Int targetCellPosition)
            : this(locationCell, connectedCell) => TargetCellPosition = targetCellPosition;

        private void CalculateHeuristicCost()
        {
            if (TargetCellPosition == null) {
                HeuristicCost = int.MaxValue;
                return;
            }

            int dstX = Math.Abs(Position.x - TargetCellPosition.Value.x);
            int dstY = Math.Abs(Position.y - TargetCellPosition.Value.y);
            if (dstX > dstY) {
                HeuristicCost = DIAGONAL_TRAVEL_COST * dstY + STRAIGHT_TRAVEL_COST * (dstX - dstY);
            }
            else {
                HeuristicCost = DIAGONAL_TRAVEL_COST * dstX + STRAIGHT_TRAVEL_COST * (dstY - dstX);
            }
        }
    }
}