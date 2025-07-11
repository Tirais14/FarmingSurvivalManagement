using System.Diagnostics.CodeAnalysis;
using Game.Core;
using Game.Gameplay.BuildingSystem;
using UnityEngine;
using UTIRLib;
using UTIRLib.Diagnostics;
using Zenject;

#nullable enable
namespace Game.PlayerSystem
{
    public sealed class PlayerEntity : MonoX
    {
        private PlayerInputBinder inputBinder = null!;
        private IBuildingMode buildingMode = null!;
        [SerializeField] private PlaceableItem placeableItem = null!;

        public IPlaceableItem PlaceableItem => placeableItem;

        [Inject]
        [SuppressMessage("", "IDE0051")]
        private void Construct(PlayerInputBinder playerInputBinder, IBuildingMode buildingMode)
        {
            inputBinder = playerInputBinder;
            this.buildingMode = buildingMode;
        }

        protected override void OnStart()
        {
            inputBinder.BindInputs();
        }

        private void Update()
        {
            if (buildingMode.PlaceableItem.IsNull()) {
                buildingMode.PlaceableItem = placeableItem;
            }
        }

        private void OnDestroy() => inputBinder.UnbindInputs();
    }
}
