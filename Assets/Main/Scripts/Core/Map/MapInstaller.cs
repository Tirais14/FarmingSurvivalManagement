using UnityEngine;
using UTIRLib.TwoD.Map;
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
            ILocation location = Object.FindAnyObjectByType<Location>(FindObjectsInactive.Include);

            Container.BindInstance(location)
                     .AsSingle();
        }
    }
}
