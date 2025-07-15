using UnityEngine;
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
            Container.BindFromScene<PlayerInputHandler>(FindObjectsInactive.Include)
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
