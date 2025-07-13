using UTIRLib;
using UnityEngine;

#nullable enable
namespace Core
{
    [DefaultExecutionOrder(-1)]
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
