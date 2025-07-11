namespace UTIRLib.TickerX
{
    public interface ITickable : ITickableBase
    {
        public void OnTick();
    }

    public interface ITickable<in T> : ITickableBase
    {
        public void OnTick(T parameter);
    }
}