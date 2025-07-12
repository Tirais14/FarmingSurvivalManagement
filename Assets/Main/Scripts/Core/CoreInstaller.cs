using Core.Map;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

#nullable enable
namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEventSystem();

            BindLocation();
        }

        private void BindEventSystem()
        {
            EventSystem eventSystem = FindAnyObjectByType<EventSystem>();

            Container.BindInstance(eventSystem).AsSingle();
        }

        private void BindLocation()
        {
            var location = FindAnyObjectByType<Location>(FindObjectsInactive.Include);

            location.gameObject.SetActive(true);

            Container.BindInstance<ILocation>(location)
                     .AsSingle();
        }
    }
}
