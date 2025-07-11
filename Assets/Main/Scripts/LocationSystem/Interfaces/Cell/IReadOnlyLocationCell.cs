using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UTIRLib.TwoD;

#nullable enable
namespace Game.Core
{
    public interface IReadOnlyLocationCell
    {
        Vector2Int Position { get; }
        Vector2 Size { get; }
        Vector2 CenterPosition { get; }
        int X { get; }
        int Y { get; }

        IReadOnlyLocationCell? GetNeighbour(Direction2D direction);

        bool TryGetNeighbour(Direction2D direction, [NotNullWhen(true)] out IReadOnlyLocationCell? locationCell);

        IReadOnlyLocationCell[] GetNeighbours();
    }
}
