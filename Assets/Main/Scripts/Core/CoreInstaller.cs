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
        }

        private void BindEventSystem()
        {
            EventSystem eventSystem = FindAnyObjectByType<EventSystem>();

            Container.BindInstance(eventSystem).AsSingle();
        }
    }
}
