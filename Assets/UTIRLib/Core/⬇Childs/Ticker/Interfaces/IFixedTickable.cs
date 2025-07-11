namespace UTIRLib.TickerX
{
    public interface IFixedTickable : ITickableBase
    {
        public void OnFixedTick();
    }

    public interface IFixedTickable<in T> : ITickableBase
    {
        public void OnFixedTick(T parameter);
    }
}