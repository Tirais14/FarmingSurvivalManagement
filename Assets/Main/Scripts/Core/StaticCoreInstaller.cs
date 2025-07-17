using Core.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib.InputSystem;
using UTIRLib.Zenject;
using Zenject;

#nullable enable
namespace Core
{
    public class StaticCoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerInputHandler();

            BindPointerHandler();

            BindPlayer();
        }

        private void BindPlayerInputHandler()
        {
            var inputActions = Resources.Load<InputActionAsset>("Input Actions");
            var inputHandler = new PlayerInputHandler(inputActions);

            Container.BindInstance(inputHandler)
                     .AsSingle();
        }

        private void BindPointerHandler()
        {
            Container.BindFromScene<PointerHandler, IPointerHandler>(FindObjectsInactive.Include)
                     .AsSingle();
        }

        private void BindPlayer()
        {
            Container.BindFromScene<Player>().AsSingle();
        }
    }
}
