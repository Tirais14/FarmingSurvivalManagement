using UTIRLib.Patterns.States;

#nullable enable
namespace Core.GameModes
{
    public class IdleMode : StateBase, IGameMode
    {
        public override void Enter() { }

        public void Execute() { }

        public override void Exit() { }
    }
}
