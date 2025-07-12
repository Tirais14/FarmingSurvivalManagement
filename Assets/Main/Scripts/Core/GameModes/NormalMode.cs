namespace Core.GameModes
{
    public class NormalMode : State, IGameMode
    {
        public override void Enter() { }

        public override void Execute() { }

        public override void Exit() { }

        public void Dispose() => Exit();
    }
}
