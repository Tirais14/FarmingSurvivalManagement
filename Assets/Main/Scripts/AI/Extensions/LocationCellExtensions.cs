using Game.Core;

#nullable enable
namespace Game.AI
{
    public static class LocationCellExtensions
    {
        public static LocationPathCell ToPathCell(this ILocationCell locationCell,
                                                  LocationPathCell? connectedCell = null)
        {
            if (connectedCell != null) {
                return new LocationPathCell(locationCell, connectedCell);
            }
            else {
                return new LocationPathCell(locationCell);
            }
        }
    }
}
