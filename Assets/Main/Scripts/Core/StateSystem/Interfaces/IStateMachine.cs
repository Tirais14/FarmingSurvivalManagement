#nullable enable
namespace Core
{
    public interface IStateMachine
    {
        void PlayPrevious();

        void Execute();

        void Reset();
    }
    public interface IStateMachine<T> : IStateMachine
        where T : IStateBase
    {
        void PlayState(T state);
    }
}
