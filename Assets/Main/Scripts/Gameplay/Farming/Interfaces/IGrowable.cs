using UTIRLib.UI;

namespace Game.Farming
{
    public interface IGrowable
    {
        IGrowthStage ActiveGrowthStage { get; }
        float GrowthTime { get; }
        bool IsMature { get; }

        IItemStack Gather();
    }
}
