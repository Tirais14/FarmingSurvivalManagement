using UnityEngine;
using Zenject;

#nullable enable
namespace Core
{
    public class MapInstaller : Installer
    {
        public override void InstallBindings()
        {
            BindLocation();
        }

        private void BindLocation()
        {
            var location = Object.FindAnyObjectByType<Location>(FindObjectsInactive.Include);

            location.gameObject.SetActive(true);

            Container.BindInstance<ILocation>(location)
                     .AsSingle();
        }
    }
}
