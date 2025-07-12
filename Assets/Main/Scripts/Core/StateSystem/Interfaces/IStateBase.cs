#nullable enable
namespace Core
{
    public interface IStateBase
    {
        void Enter();

        void Exit();

        bool CanTransitionTo(IStateBase stateBase);
    }
}
