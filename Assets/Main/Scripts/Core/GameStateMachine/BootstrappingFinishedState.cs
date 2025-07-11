using UnityEngine;
using UTIRLib.Patterns.StateMachine;
using Object = UnityEngine.Object;

#nullable enable
namespace Game.Core
{
    public sealed class BootstrappingFinishedState : ReadOnlyState
    {
        public BootstrappingFinishedState(StateParameters stateParameters) : base(stateParameters)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Object.FindAnyObjectByType<SceneSwitcher>(FindObjectsInactive.Include).gameObject.SetActive(true);
            IsCompleted = true;
        }

        public override void Execute() { }
    }
}
