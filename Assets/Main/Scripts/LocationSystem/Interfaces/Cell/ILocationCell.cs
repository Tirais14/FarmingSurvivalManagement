using System.Diagnostics.CodeAnalysis;
using UnityEngine.Tilemaps;
using UTIRLib.TwoD;

#nullable enable
namespace Game.Core
{
    public interface ILocationCell : IReadOnlyLocationCell, ITileProvider
    {
        new ILocationCell? GetNeighbour(Direction2D direction);

        bool TryGetNeighbour(Direction2D direction, [NotNullWhen(true)] out ILocationCell? locationCell);

        new ILocationCell[] GetNeighbours();

        void Clear();
    }
    public interface ILocationCell<T> : ITileProvider<T>, ILocationCell
        where T : TileBase
    {
    }
}
