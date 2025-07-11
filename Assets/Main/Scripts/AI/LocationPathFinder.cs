using System.Collections.Generic;
using Game.Core;
using Game.LocationSystem;
using UnityEngine;
using UTIRLib.Collections;

#nullable enable
namespace Game.AI
{
    public sealed class LocationPathFinder
    {
        private readonly PriorityQueue<float, LocationPathCell> candidateCells = new();
        private readonly HashSet<LocationPathCell> proccessedCells = new();
        private LocationPathCell pathCell = null!;
        private LocationPathCell neighbourPathCell = null!;
        private ILocationCell[] neighbourCells = null!;
        private Vector2Int startCellPosition;
        private Vector2Int targetCellPosition;
        private int travelCost;

        public ILocationMap LocationMap { get; private set; }

        public LocationPathFinder(LocationMap location) => LocationMap = location;

        public LocationPath? FindPath(Vector3 startPosition, Vector3 targetPosition)
        {
            SetCellPositions(startPosition, targetPosition);
            if (TrySetStartPathCell() && IsTargetCellExistsOnLocation()) {
                ResetCells();
                while (CandidateCellsExist()) {
                    TakeNextCell();
                    if (IsTargetCell(pathCell)) {
                        return LocationPath.RetraceFromTarget(pathCell);
                    }
                    MarkCellAsProccessed(pathCell);
                    SetNeighbourCells();
                    for (int index = 0; index < neighbourCells.Length; index++) {
                        TakeNeighbourPathCell(index);
                        if (IsProcessedCell(neighbourPathCell)) {
                            continue;
                        }

                        CalculateTravelCost();
                        if (IsNeighbourTravelCostLower() || !IsCandidateCell(neighbourPathCell)) {
                            SetupNeighbourPathCell();

                            if (!IsCandidateCell(neighbourPathCell)) {
                                AddCandidateCell(neighbourPathCell);

#if UNITY_EDITOR
                                DebugDrawLine(neighbourPathCell.Position, pathCell.Position);
#endif
                            }
                        }
                    }
                }
            }

            return null;
        }

        public bool TryFindPath(Vector3 startPosition, Vector3 targetPosition, out LocationPath? path)
        {
            path = FindPath(startPosition, targetPosition);

            return path != null && path.IsValid;
        }

        private void SetCellPositions(Vector3 startPosition, Vector3 targetPosition)
        {
            startCellPosition = LocationMap.WorldToCell(startPosition);
            targetCellPosition = LocationMap.WorldToCell(targetPosition);
        }

        private bool TrySetStartPathCell()
        {
            if (LocationMap.TryFindCell(startCellPosition, out ILocationCell? startCell)) {
                pathCell = startCell.ToPathCell();
                pathCell.TravelCost = 0;
                candidateCells.Enqueue(pathCell.TotalCost, pathCell);
                return true;
            }

            return false;
        }

        private void SetupNeighbourPathCell()
        {
            neighbourPathCell.TravelCost = travelCost;
            neighbourPathCell.TargetCellPosition = targetCellPosition;
            neighbourPathCell.ConnectedCell = pathCell;
        }

        private void CalculateTravelCost() => travelCost = pathCell.HeuristicCost;

        private void SetNeighbourCells() => neighbourCells = pathCell.LocationCell.GetNeighbours();

        private void TakeNextCell() => pathCell = candidateCells.Dequeue();

        private void TakeNeighbourPathCell(int index) => neighbourPathCell = neighbourCells[index].ToPathCell();

        private bool IsTargetCell(LocationPathCell pathCell) => pathCell.Position == targetCellPosition;

        private bool IsTargetCellExistsOnLocation() => LocationMap.TryFindCell(targetCellPosition, out _);

        private bool IsNeighbourTravelCostLower() => travelCost < neighbourPathCell.TravelCost;

        private bool IsProcessedCell(LocationPathCell pathCell) => proccessedCells.Contains(pathCell);

        private bool IsCandidateCell(LocationPathCell pathCell) => candidateCells.Contains(pathCell.TotalCost, pathCell);

        private bool CandidateCellsExist() => candidateCells.Count > 0;

        private void MarkCellAsProccessed(LocationPathCell pathCell) => proccessedCells.Add(pathCell);

        private void AddCandidateCell(LocationPathCell pathCell) => candidateCells.Enqueue(pathCell.TotalCost, pathCell);

        private void ResetCells()
        {
            candidateCells.Clear();
            proccessedCells.Clear();
        }

#if UNITY_EDITOR
        private static void DebugDrawLine(Vector2Int startPosition, Vector2Int endPosition)
        {
            Debug.DrawLine(new Vector3(startPosition.x, startPosition.y),
                new Vector3(endPosition.x, endPosition.y), Color.green, 1800f, false);
        }
#endif
    }
}