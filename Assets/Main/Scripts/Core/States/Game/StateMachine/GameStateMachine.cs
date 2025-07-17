using Core.InputSystem;
using System;
using UTIRLib;
using UTIRLib.Patterns.States;
using Zenject;

#nullable enable
namespace Core
{
    [InitFirst]
    public sealed partial class GameStateMachine : MonoXInitable, IStateMachine<IGameMode>
    {
        private StateMachine<IGameMode> sm = null!;
        private SwitchStrategy switchStrategy = null!;

        public Type PlayingStateType => sm.PlayingStateType;

        [Inject]
        private void Construct(PlayerInputHandler playerInputHandler,
                               GameModeFactory gameModeFactory)
        {
            switchStrategy = new SwitchStrategy(playerInputHandler, gameModeFactory);
        }

        protected override void OnInit()
        {
            sm = new StateMachine<IGameMode>(switchStrategy);
        }

        private void Update() => sm.Execute();

        void IStateMachine<IGameMode>.PlayState(IGameMode state)
        {
            GameDebug.Warning($"{nameof(IStateMachine<IGameMode>.PlayState)} operation is moqed.", this);
        }

        void IStateMachine.PlayPreviousState()
        {
            GameDebug.Warning($"{nameof(IStateMachine.PlayPreviousState)} operation is moqed.", this);
        }

        void IStateMachine.PlayDefaultState()
        {
            GameDebug.Warning($"{nameof(IStateMachine.PlayDefaultState)} operation is moqed.", this);
        }

        void IExecutable.Execute()
        {
            GameDebug.Warning($"{nameof(IExecutable.Execute)} operation is moqed.", this);
        }

        public partial class SwitchStrategy
        {
        }
    }
}
