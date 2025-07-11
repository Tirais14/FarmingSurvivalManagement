using UniRx;
using UnityEngine;
using UTIRLib.UI;

#nullable enable
namespace Game.InventorySystem
{
    public class InventoryUIViewModel : ViewModel
    {
        [SerializeField] protected InventoryUI model = null!;

        public IReadOnlyReactiveProperty<bool> IsOpenedView => model.IsOpenedProp;

        protected override void BindToModel() { }
    }
}
