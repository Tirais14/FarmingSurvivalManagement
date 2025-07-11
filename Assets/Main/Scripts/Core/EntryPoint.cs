using UTIRLib;

#nullable enable
namespace Game.Core
{
    public sealed class EntryPoint : MonoX
    {
        protected override void OnAwake() => Destroy(this);
    }
}