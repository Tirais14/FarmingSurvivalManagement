#nullable enable
namespace Core
{
    public class StateMachine : StateMachineBase<IState>
    {
        public StateMachine(IState defaultState) : base(defaultState)
        {
        }

        public override void Execute() => playingState.Execute();
    }
}
