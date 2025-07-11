using System;
using Game.Farming;
using UTIRLib;
using UTIRLib.Diagnostics;
using UTIRLib.Extensions;
using UTIRLib.TickerX;
using UTIRLib.UI;

#nullable enable
namespace Game.Core
{
    [CustomTickable(typeof(GrowableTicker))]
    public class Growable : MonoX, IGrowable, ITickable<float>
    {
        protected readonly IGrowthStage[] growthStages;
        protected float growthTime;
        protected IGrowthStage activeGrowthStage = null!;

        public event Action<IGrowthStage>? OnGrowthStageChanged;

        public float GrowthTime => growthTime;
        public IGrowthStage ActiveGrowthStage => activeGrowthStage;
        public bool IsMature { get; protected set; }

        /// <exception cref="NullOrEmptyCollectionException"></exception>
        public Growable(GrowthStage[] growthStages)
        {
            if (growthStages.IsNullOrEmpty()) {
                throw new CollectionArgumentException(nameof(growthStages), growthStages);
            }

            this.growthStages = growthStages;
            SetGrowthStage(growthStages[0]);
        }

        public void OnTick(float deltaTime)
        {
            AddGrowthTime(deltaTime);
            StageListener();
        }

        public IItemStack Gather() => activeGrowthStage.OnGatherItems.IsEmpty ? ItemStack.empty :
            new ItemStack(activeGrowthStage.OnGatherItems);

        public void ResetGrowthStage() => SetGrowthStage(growthStages[0]);

        private void StageListener()
        {
            if (IsMature) {
                return;
            }

            if (growthTime >= activeGrowthStage.TimeToNextStage) {
                ToNextGrowthStage();
            }
        }

        private void AddGrowthTime(float deltaTime) => growthTime += deltaTime;

        private void ToNextGrowthStage() => SetGrowthStage(growthStages[activeGrowthStage.StageIndex + 1]);

        private void SetGrowthStage(IGrowthStage growthStage)
        {
            activeGrowthStage = growthStage;
            growthTime = 0f;
            OnGrowthStageChanged?.Invoke(growthStage);
            if (IsLastGrowthStage()) {
                IsMature = true;
            }
        }

        private bool IsLastGrowthStage() => activeGrowthStage.StageIndex == growthStages.Length - 1;
    }
}
