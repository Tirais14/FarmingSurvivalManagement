using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UTIRLib.UI;

#nullable enable
namespace Game.InventorySystem
{
    [RequireComponent(typeof(Image))]
    public class InventoryUIView : View<InventoryUIViewModel>
    {
        protected override void BindToViewModel()
        {
            viewModel.IsOpenedView.Subscribe(SetImageEnabledState).AddTo(this);
        }
    }
}
