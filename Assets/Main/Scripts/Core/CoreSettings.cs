using UnityEngine;
using UTIRLib;
using UTIRLib.Diagnostics;

#nullable enable
namespace Game.Core
{
    [DefaultExecutionOrder(-8888)]
    public sealed class CoreSettings : MonoXStatic<CoreSettings>
    {
        [SerializeField]
        private bool debugModeEnabled;

        public bool DebugModeEnabled {
            get => debugModeEnabled;
            set {
                debugModeEnabled = value;

                TirLibDebug.Enabled = value;
            }
        }

        protected override void OnAwake()
        {
            MonoXStatic.Parent = GameObject.Find("Core").transform;

            TirLibDebug.Enabled = debugModeEnabled;

            base.OnAwake();
        }
    }
}
