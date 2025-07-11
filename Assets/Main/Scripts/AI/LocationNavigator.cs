using Game.LocationSystem;
using UnityEngine;

#nullable enable
namespace Game.AI
{
    public class LocationNavigator
    {
        public LocationPath Path { get; private set; }
        public LocationPathFinder PathFinder { get; private set; }
        public Vector2? MovePosition { get; private set; }

        public LocationNavigator(LocationMap locationMap)
        {
            Path = LocationPath.Empty;
            PathFinder = new LocationPathFinder(locationMap);
        }

        public bool IsRouteSet() => MovePosition.HasValue;

        public void SetPath(LocationPath path)
        {
            MovePosition = path.TakeNextPosition();
            Path = path;
        }

        public bool IsPositionReachedOf(Transform movable, float minDistance = 0)
        {
            MovePosition ??= Path.TakeNextPosition();

            if (MovePosition is not null) {
                if (minDistance > 0) {
                    float distance = Vector2.Distance(movable.position, (Vector2)MovePosition);
                    if (distance <= minDistance) {
                        return true;
                    }
                }
                else if (movable.position == MovePosition) {
                    return true;
                }
            }

            return false;
        }

        public void ToNextPosition() => MovePosition = Path.TakeNextPosition();
    }
}