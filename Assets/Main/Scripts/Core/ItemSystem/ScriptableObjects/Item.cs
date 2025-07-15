using UnityEngine;

#nullable enable
namespace Core
{
    [CreateAssetMenu(fileName = "Item",
        menuName = "Game/Items/Basic", 
        order = G.Editor.GAME_ASSET_MENU_ORDER)]
    public class Item : ScriptableObject, IItem
    {
        [field: SerializeField]
        public string Name { get; private set; } = null!;

        [field: SerializeField]
        public Sprite Icon { get; private set; } = null!;

        [field: SerializeField]
        public int MaxStackCount { get; private set; }

        [field: SerializeField]
        public GameObject WorldModel { get; private set; } = null!;
    }
}
