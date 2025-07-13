using Core;
using UnityEngine;
using UTIRLib.InputSystem;
using Zenject;

#nullable enable
namespace Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerInputHandler();

            BindPointerHandler();

            BindPlayer();

            BindGameModeFactory();
        }

        private void BindPlayerInputHandler()
        {
            var inputHandler = FindAnyObjectByType<PlayerInputHandler>(
                               FindObjectsInactive.Include);

            Container.BindInstance(inputHandler)
                     .AsSingle();
        }

        private void BindPointerHandler()
        {
            var inputHandler = FindAnyObjectByType<PlayerInputHandler>(
                               FindObjectsInactive.Include);

            Container.BindInstance<IPointerHandler>(inputHandler).AsSingle();
        }

        private void BindPlayer()
        {
            var player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);

            Container.BindInstance<IPlayer>(player)
                     .AsSingle();
        }

        private void BindGameModeFactory()
        {
            Container.Bind<GameModeFactory>().AsSingle();
        }
    }
}
