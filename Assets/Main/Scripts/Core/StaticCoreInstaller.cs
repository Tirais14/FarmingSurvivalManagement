using Core.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib.InputSystem;
using UTIRLib.Zenject;
using Zenject;
using UTIRLib.Diagnostics;
using UTIRLib;

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

            var pointerHandler = new PointerHandler(inputActions);
            Container.BindInstance(pointerHandler)
                     .AsSingle();
        }

        private void BindPlayer()
        {
            Container.BindFromScene<Player>().AsSingle();
        }
    }
}
