using System;
using UnityEngine;
using UTIRLib;
using UTIRLib.Collections;
using UTIRLib.Extensions;
using UTIRLib.Patterns.State;
using UTIRLib.Patterns.StateMachine;
using Zenject;
using Object = UnityEngine.Object;

#nullable enable
namespace Game.Core
{
    public sealed class InstallBootstrappingSceneContextGameState : State
    {

        public event Action<InstallBootstrappingSceneContextGameState>? OnInitialized;

        public InstallBootstrappingSceneContextGameState(StateParameters stateParameters) : base(stateParameters)
        {
        }

        public override void Enter()
        {
            base.Enter();

            InstallBootstrappingSceneContext();

            IsCompleted = true;
        }

        public override void Execute() { }

        public override void Exit()
        {
            base.Exit();
            OnInitialized?.Invoke(this);
        }

        private static void InstallBootstrappingSceneContext()
        {
            SceneContext sceneContext = Object.FindObjectsByType<SceneContext>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None).
                Find((context) => context.gameObject.name.Contains("boot", StringComparison.InvariantCultureIgnoreCase));

            sceneContext.Install();
        }
    }
}
