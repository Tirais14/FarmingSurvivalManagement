using UniRx;

#nullable enable

namespace UTIRLib.UI
{
    public class InfiniteItemStackView : ItemStackView
    {
        protected override void BindToViewModel()
        {
            viewModel.ItemSpriteView.Subscribe(SetSprite).AddTo(this);
            viewModel.ImageStateView.Subscribe(SetImageEnabledState).AddTo(this);
        }
    }
}