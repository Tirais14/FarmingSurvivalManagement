using Core;
using UnityEngine;
using UTIRLib.InputSystem;
using Zenject;

#nullable enable
namespace Game
{
    public class StaticCoreInstaller : MonoInstaller
    {
        private PlayerInputHandler playerInputHandler = null!;

        public override void InstallBindings()
        {
            BindPlayerInputHandler();

            BindPointerHandler();

            BindPlayer();
        }

        private void BindPlayerInputHandler()
        {
            playerInputHandler = FindAnyObjectByType<PlayerInputHandler>(
                                 FindObjectsInactive.Include);

            Container.BindInstance(playerInputHandler)
                     .AsSingle();
        }

        private void BindPointerHandler()
        {
            Container.BindInstance<IPointerHandler>(playerInputHandler).AsSingle();
        }

        private void BindPlayer()
        {
            var player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);

            Container.BindInstance<IPlayer>(player)
                     .AsSingle();
        }
    }
}
