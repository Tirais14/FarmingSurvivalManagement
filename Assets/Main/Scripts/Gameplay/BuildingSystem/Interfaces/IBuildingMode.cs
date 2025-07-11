using UTIRLib;

#nullable enable
namespace Game.Gameplay.BuildingSystem
{
    public interface IBuildingMode : ISwitchable, IStateNotifier<IBuildingMode>
    {
        IPlaceableItem? PlaceableItem { get; set; }

        void PlaceItem();
    }
}
