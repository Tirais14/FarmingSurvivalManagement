#nullable enable

using System;

namespace UTIRLib.TickerX
{
    public interface ITicker
    {
        float TimeSpeed { get; set; }
        int Count { get; }

        void Register(ITickableBase tickableBase);

        bool Unregister(ITickableBase? tickableBase);

        void UnregisterAll();

        bool CanRegister(Type type);

        bool CanRegister<T>();

        bool CanRegister(ITickableBase tickableBase);

        bool Contains(ITickableBase? tickableBase);

        void SetTimeSpeed(float value);

        void ResetTimeSpeed();
    }

    public interface ITicker<T> : ITicker
        where T : ITickableBase
    {
        void Register(T tickable);

        bool Unregister(T? tickable);
    }
}