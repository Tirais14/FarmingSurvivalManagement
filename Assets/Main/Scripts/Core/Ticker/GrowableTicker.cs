using UTIRLib.TickerX;

#nullable enable
namespace Game.Farming
{
    public sealed class GrowableTicker : Ticker
    {
        private void Update() => Tick();

        private void LateUpdate() => LateTick();
    }
}
