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

            Container.Install<MapInstaller>();

            Container.Install<PlayerInstaller>();
        }

        private void BindEventSystem()
        {
            var eventSystem = FindAnyObjectByType<EventSystem>();

            Container.BindInstance(eventSystem).AsSingle();
        }
    }
}
