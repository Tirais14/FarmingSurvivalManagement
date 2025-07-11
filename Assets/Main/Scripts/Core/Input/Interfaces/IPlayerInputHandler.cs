using System;
using System.Buffers;
using UnityEngine;

#nullable enable
namespace Core
{
    public interface IPlayerInputHandler
    {
        Vector2 PointerPosition { get; }

        event Action<bool> OnPrimaryAction;
        event Action<bool> OnSecondaryAction;
        event Action<bool> OnSwitchGameMode;
    }
}
