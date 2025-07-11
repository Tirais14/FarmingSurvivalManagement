using UTIRLib.TickerX;

#nullable enable
namespace Game.AlternativeTicker
{
    public class UITicker : Ticker
    {
        private void Update() => Tick();

        private void LateUpdate() => LateTick();
    }
}