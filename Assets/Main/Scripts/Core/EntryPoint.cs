using UnityEngine;
using UTIRLib;

#nullable enable
namespace Core
{
    [DefaultExecutionOrder(-1)]
    public sealed class EntryPoint : MonoXStatic<EntryPoint>
    {
        protected override void OnAwake()
        {
            base.OnAwake();

            SceneInitializer.InitAllObjects();
        }
    }
}
