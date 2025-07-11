#nullable enable
using System;

namespace Core.GameModes
{
    public interface IGameMode
    {
        void Enter();

        void Execute();

        void Exit();
    }
}
