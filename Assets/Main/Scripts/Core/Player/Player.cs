using UTIRLib;
using UTIRLib.Diagnostics;
using UTIRLib.UI;
using Zenject;

#nullable enable
namespace Core
{
    public class Player : MonoX, IPlayer
    {
        public IGameMode GameMode { get; set; } = null!;
        public IItem? HoldItem { get; set; }
        public bool HasHoldItem => HoldItem.IsNotNull();

        [Inject]
        private void Construct(IGameMode gameMode)
        {
            GameMode = gameMode;
        }

        private void Update() => GameMode.Execute();
    }
}
