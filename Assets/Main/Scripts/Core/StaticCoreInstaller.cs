using Core.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib;
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
            BindInputHandlers();

            BindPlayer();
        }

        private void BindInputHandlers()
        {
            var inputActions = Resources.Load<InputActionAsset>("Input Actions");

            if (inputActions == null)
                throw new AssetFailedLoadException("Input Actions");

            var playerInputHandler = new PlayerInputHandler(inputActions);
            Container.BindInstance(playerInputHandler)
                     .AsSingle();

            var pointer = new PointerInput(inputActions.FindActionMap("UI",
                throwIfNotFound: true)
                .FindAction("Point", throwIfNotFound: true));

            Container.BindInstance<IPointerInput>(pointer)
                     .AsSingle();
        }

        private void BindPlayer()
        {
            Container.BindFromScene<Player>().AsSingle();
        }
    }
}
