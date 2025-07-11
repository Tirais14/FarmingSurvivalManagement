namespace UTIRLib.TickerX
{
    public interface ILateTickable : ITickableBase
    {
        public void OnLateTick();
    }

    public interface ILateTickable<in T> : ITickableBase
    {
        public void OnLateTick(T parameter);
    }
}