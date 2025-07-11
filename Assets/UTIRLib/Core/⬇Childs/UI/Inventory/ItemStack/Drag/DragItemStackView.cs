using UniRx;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

namespace UTIRLib.UI
{
    [RequireComponent(typeof(Image), typeof(DragItemStackViewModel))]
    public class DragItemStackView : View<DragItemStackViewModel>
    {
        protected void OnActiveStateChanged(bool value)
        {
            SetImageEnabledState(value);
            image.sprite = viewModel.SpriteView;
        }

        protected override void BindToViewModel() =>
            viewModel.IsActiveView.Subscribe(OnActiveStateChanged).AddTo(this);
    }
}