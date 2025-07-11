#nullable enable
using UTIRLib.Patterns.State;
using UTIRLib.Patterns.StateMachine;

#nullable enable
namespace Game.Core
{
    public sealed class GameStateMachine : MonoReadOnlyStateMachine
    {
        private AssetDatabaseRegistryLoadGameState databaseLoadingGameState = null!;
        private InstallBootstrappingSceneContextGameState installBootstrappingSceneContextGameState = null!;
        private BootstrappingFinishedState bootstrappingFinishedState = null!;

        protected override IReadOnlyState GetIdleState()
        {
            StateParameters stateParameters = new(isAbortable: true, typeof(AssetDatabaseRegistryLoadGameState));
            return new IdleGameState(stateParameters);
        }

        protected override IReadOnlyState[] GetStates()
        {
            StateParameters stateParameters = new(typeof(InstallBootstrappingSceneContextGameState));
            databaseLoadingGameState = new AssetDatabaseRegistryLoadGameState(stateParameters);

            stateParameters.SetTransitions(typeof(BootstrappingFinishedState));
            installBootstrappingSceneContextGameState = new InstallBootstrappingSceneContextGameState(stateParameters);
            bootstrappingFinishedState = new BootstrappingFinishedState(stateParameters);

            return new IReadOnlyState[] { databaseLoadingGameState, installBootstrappingSceneContextGameState, bootstrappingFinishedState };
        }

        protected override StateTransition[] GetTransitions()
        {
            var toDatabaseLoading = StateTransition.Create<AssetDatabaseRegistryLoadGameState>(
                IsNotDatabaseLoaded);

            var toCoreInitializing = StateTransition.Create<InstallBootstrappingSceneContextGameState>(
                IsReadyToInstallSceneContext);

            var toBootstrappingFinished = StateTransition.Create<BootstrappingFinishedState>(
                IsReadToFinishBootsrapping);

            return new StateTransition[] { toDatabaseLoading, toCoreInitializing, toBootstrappingFinished };
        }

        private bool IsDatabaseLoaded() => databaseLoadingGameState.IsCompleted;

        private bool IsNotDatabaseLoaded() => !IsDatabaseLoaded();

        private bool IsReadyToInstallSceneContext() => IsDatabaseLoaded() 
                && !installBootstrappingSceneContextGameState.IsCompleted;

        private bool IsReadToFinishBootsrapping() => !bootstrappingFinishedState.IsCompleted
                                    && installBootstrappingSceneContextGameState.IsCompleted;

        private void Update() => OnUpdate();
    }
}