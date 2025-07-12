#nullable enable
using System;
using UTIRLib.Diagnostics;

namespace Core
{
    public abstract class StateMachineBase<T> : IStateMachine<T>
        where T : class, IStateBase
    {
        private T previousState;
        private readonly T defaultState;

        protected T playingState;

        protected StateMachineBase(T defaultState)
        {
            playingState = defaultState;

            previousState = defaultState;
            this.defaultState = defaultState;
        }

        public abstract void Execute();

        /// <exception cref="ArgumentNullException"></exception>
        public void PlayState(T state)
        {
            if (state.IsNull())
                throw new ArgumentNullException(nameof(state));

            if (playingState == state)
                return;

            previousState = playingState;

            playingState.Exit();
            playingState = state;
            playingState.Enter();
        }

        public void PlayPrevious() => PlayState(previousState);

        public void Reset() => PlayState(defaultState);
    }
}
