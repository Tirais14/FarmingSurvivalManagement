using UTIRLib.Patterns.StateMachine;

#nullable enable
namespace Game.Core
{
    public class IdleGameState : ReadOnlyState
    {
        public IdleGameState(StateParameters stateParameters) : base(stateParameters)
        {
        }

        public override void Execute() { }
    }
}