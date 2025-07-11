using System;
using UnityEngine;
using UTIRLib.UI;

#nullable enable
namespace Game.Core
{
    [Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
    public class Item : ScriptableObject, IItem
    {
        [field: SerializeField] public string ItemName { get; protected set; } = null!;
        [field: SerializeField] public Sprite Sprite { get; protected set; } = null!;
        [field: SerializeField] public int MaxQuantity { get; protected set; }
    }
}