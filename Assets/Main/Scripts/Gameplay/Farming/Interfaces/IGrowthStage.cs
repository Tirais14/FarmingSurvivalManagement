using UTIRLib.TwoD;
using UTIRLib.UI;

#nullable enable
namespace Game.Farming
{
    public interface IGrowthStage : IReadOnlyTileProvider
    {
        int StageIndex { get; }
        float TimeToNextStage { get; }
        bool CanHarvest { get; }
        IReadOnlyItemStack OnGatherItems { get; }
    }
}
