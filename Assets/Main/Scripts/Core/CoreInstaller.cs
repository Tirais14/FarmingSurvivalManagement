using UnityEngine.EventSystems;
using Zenject;
using UTIRLib.Zenject;

#nullable enable
namespace Core
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEventSystem();

            BindGameModeFactory();

            Container.Install<MapInstaller>();
        }

        private void BindEventSystem()
        {
            var eventSystem = FindAnyObjectByType<EventSystem>();

            Container.BindInstance(eventSystem).AsSingle();
        }

        private void BindGameStateMachineSwitchStrategy()
        {

        }

        private void BindGameModeFactory()
        {
            Container.Bind<GameModeFactory>().AsSingle();
        }
    }
}
