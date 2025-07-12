using System;
using UTIRLib.Collections;
using UTIRLib.Diagnostics;

#nullable enable
namespace Core
{
    public abstract class State : IState
    {
        private readonly Flags<Type> transitionTypes;

        protected State(params Type[] transitionTypes)
        {
            this.transitionTypes = new Flags<Type>(transitionTypes);
        }

        public abstract void Enter();

        public abstract void Execute();

        public abstract void Exit();

        /// <exception cref="ArgumentNullException"></exception>
        public bool CanTransitionTo(IStateBase stateBase)
        {
            if (stateBase.IsNull())
                throw new ArgumentNullException();

            return transitionTypes.Contains(stateBase.GetType());
        }
    }
}
