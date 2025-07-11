using Game.Core;
using Game.Core.InputSystem;
using UnityEngine;
using UTIRLib.InputSystem;
using Zenject;

namespace Game.Zenject.Installers
{
#nullable enable
    public class ProjectContextInstaller : MonoInstaller
    {
        private GameStateMachine gameStateMachine = null!;

        public override void InstallBindings()
        {
            gameStateMachine = FindAnyObjectByType<GameStateMachine>();

            BindAssetDatabaseRegistry();
            BindGameStateMachine();
            BindInputHandlers();

            Debug.Log("Global bindings installed.");
        }

        private void BindAssetDatabaseRegistry() =>
            Container.BindInstance(gameStateMachine.GetState<AssetDatabaseRegistryLoadGameState>().AssetDatabaseRegistry).
                AsSingle();

        private void BindGameStateMachine() => Container.BindInstance(gameStateMachine).
            AsSingle();

        private void BindInputHandlers()
        {
            var playerInputHandler = FindAnyObjectByType<PlayerInputHandler>();
            var uiInputHandler = FindAnyObjectByType<UIInputHandler>();

            Container.BindInstance(playerInputHandler as IPlayerInputHandler).AsSingle();
            Container.BindInstance(uiInputHandler as IUIInputHandler).AsSingle();
            Container.BindInstance(uiInputHandler as IPointerHandler).AsSingle();
        }
    }
}
