using Core.GameModes;
using System;
using UTIRLib;
using UTIRLib.Patterns.States;
using Zenject;

#nullable enable
namespace Core
{
    public sealed partial class GameStateMachine : MonoXInitable, IStateMachine<IGameMode>
    {
        private StateMachine<IGameMode> sm = null!;
        private SwitchStrategy switchStrategy = null!;

        public Type PlayingStateType => sm.PlayingStateType;

        [Inject]
        private void Construct(SwitchStrategy switchStrategy)
        {
            this.switchStrategy = switchStrategy;
        }

        protected override void OnInit()
        {
            sm = new StateMachine<IGameMode>(switchStrategy);
        }

        private void Update() => sm.Execute();

        private void OnDestroy() => switchStrategy.Dispose();

        void IStateMachine<IGameMode>.PlayState(IGameMode state)
        {
            GameDebug.Warning($"This operation is moqed.", this);
        }

        void IStateMachine.PlayPreviousState()
        {
            GameDebug.Warning($"This operation is moqed.", this);
        }

        void IStateMachine.PlayDefaultState()
        {
            GameDebug.Warning($"This operation is moqed.", this);
        }

        void IExecutable.Execute()
        {
            GameDebug.Warning($"This operation is moqed.", this);
        }

        public partial class SwitchStrategy : StateMachine<IGameMode>.SwitchStrategy,
            IDisposable
        {
        }
    }
}
