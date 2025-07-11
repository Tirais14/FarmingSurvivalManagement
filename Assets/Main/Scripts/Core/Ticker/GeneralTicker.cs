using UTIRLib.TickerX;

#nullable enable
namespace Game.AlternativeTicker
{
    public sealed class GeneralTicker : Ticker
    {
        private void Update() => Tick();

        private void FixedUpdate() => FixedTick();

        private void LateUpdate() => LateTick();
    }
}