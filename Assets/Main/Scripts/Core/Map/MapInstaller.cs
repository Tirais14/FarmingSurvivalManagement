using Core.Map;
using UnityEngine;
using Zenject;

#nullable enable
namespace Core
{
    public class MapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLocation();
        }

        private void BindLocation()
        {
            var location = FindAnyObjectByType<LocationLayer>(FindObjectsInactive.Include);

            location.gameObject.SetActive(true);

            Container.BindInstance<ILocationLayer>(location)
                     .AsSingle();
        }
    }
}
