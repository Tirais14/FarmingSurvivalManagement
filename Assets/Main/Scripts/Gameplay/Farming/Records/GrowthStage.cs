using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Tilemaps;
using UTIRLib.UI;

#nullable enable
namespace Game.Farming
{
    [CreateAssetMenu(fileName = "GrowthStage", menuName = "Growables/Growt")]
    public class GrowthStage : ScriptableObject, IGrowthStage
    {
        public int StageIndex { get; private set; }
        public float TimeToNextStage { get; private set; }
        public bool CanHarvest { get; private set; }
        public TileBase? Tile { get; private set; }
        public IReadOnlyItemStack OnGatherItems { get; private set; }

        public bool HasTile => throw new System.NotImplementedException();

        public GrowthStage(int stageIndex, float timeToNextStage, bool canHarvest, Tile tile,
            IReadOnlyItemStack? onGatherItems)
        {
            StageIndex = stageIndex;
            TimeToNextStage = timeToNextStage;
            CanHarvest = canHarvest;
            Tile = tile;
            OnGatherItems = onGatherItems ?? ReadOnlyItemStack.empty;
        }

        public T? GetTile<T>() where T : TileBase => Tile as T;

        public bool TryGetTile<T>([NotNullWhen(true)] out T? tile) where T : TileBase
        {
            tile = GetTile<T>();

            return tile != null;
        }
    }
}
