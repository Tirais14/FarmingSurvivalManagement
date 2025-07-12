using Zenject;

#nullable enable
namespace UI
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInventoryUI();
        }

        private void BindInventoryUI()
        {
            var ui = FindAnyObjectByType<InventoryUI>();

            Container.BindInstance(ui).AsSingle();
        }
    }
}
