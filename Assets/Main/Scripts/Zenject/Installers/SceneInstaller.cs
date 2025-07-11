using Game.Core;
using Game.Gameplay.BuildingSystem;
using Game.InventorySystem;
using Game.LocationSystem;
using Game.PlayerSystem;
using Game.StoreSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UTIRLib.Zenject;
using Zenject;

#nullable enable
namespace Game.Zenject.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEventSystem();
            BindLocationMap();
            BindBuildableLocationMap();

            BindPlayerEntity();
            BindPlayerInputBinder();

            BindBuildingMode();
            BindInventoryUI();
            BindStoreUI();

            BindCorePrefabFactory();
            BindLocationLayerFactory();
            BindInventorySlotFactory();

            Debug.Log("Scene bindings installed.", this);
        }

        private void BindEventSystem() => Container.BindFromScene<EventSystem>()?.AsSingle();
        private void BindLocationMap() => Container.BindFromScene<LocationMap, ILocationMap>()?.AsSingle();
        private void BindBuildableLocationMap() =>
            Container.BindFromScene<BuildableLocationMap, IBuildableLocationMap>()?.AsSingle();

        private void BindPlayerEntity() => Container.BindFromScene<PlayerEntity>()?.AsSingle();
        private void BindPlayerInputBinder() => Container.Bind<PlayerInputBinder>().AsSingle();

        private void BindBuildingMode() => Container.BindFromScene<BuildingMode, IBuildingMode>()?.AsSingle();
        private void BindInventoryUI() => Container.BindFromScene<InventoryUI>()?.AsSingle();
        private void BindStoreUI() => Container.BindFromScene<StoreUI>()?.AsSingle();

        private void BindCorePrefabFactory() => Container.Bind<CorePrefabFactory>().AsSingle();
        private void BindLocationLayerFactory() => Container.Bind<LocationLayerFactory>().AsSingle();
        private void BindInventorySlotFactory() => Container.Bind<InventorySlotFactory>().AsSingle();
    }
}
