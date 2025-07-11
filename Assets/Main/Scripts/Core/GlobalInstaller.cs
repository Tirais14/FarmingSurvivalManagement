using Core.Map;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerInputHandler();

            BindLocation();

            BindPlayer();
        }

        private void BindPlayerInputHandler()
        {
            var inputHandler = FindAnyObjectByType<PlayerInputHandler>(FindObjectsInactive.Include);

            inputHandler.gameObject.SetActive(true);

            Container.BindInstance(inputHandler)
                     .AsSingle();
        }

        private void BindLocation()
        {
            var location = FindAnyObjectByType<Location>(FindObjectsInactive.Include);

            location.gameObject.SetActive(true);

            Container.BindInstance<ILocation>(location)
                     .AsSingle();
        }

        private void BindPlayer()
        {
            var player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);

            player.gameObject.SetActive(true);

            Container.BindInstance<IPlayer>(player)
                     .AsSingle();
        }
    }
}
