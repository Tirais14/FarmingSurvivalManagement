using UTIRLib;

#nullable enable
namespace Core
{
    public sealed class EntryPoint : MonoXStatic<EntryPoint>
    {
        protected override void OnAwake()
        {
            base.OnAwake();

            SceneInitializer.Init<PlayerInputHandler>();
            SceneInitializer.Init<GameStateMachine>();
        }
    }
}
