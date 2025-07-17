using Core.InputSystem;
using System;
using UnityEngine;
using UTIRLib;
using Zenject;

#pragma warning disable IDE0051
#nullable enable
namespace Core
{
    public class PlayerCamera : MonoX
    {
        private PlayerInputHandler playerInputHandler = null!;

        [SerializeField]
        private float moveSpeed = 5f;

        [Inject]
        private void Construct(PlayerInputHandler playerInputHandler)
        {
            this.playerInputHandler = playerInputHandler;

            playerInputHandler.MoveInput.OnPerformedValue += MoveCamera;
        }

        private void MoveCamera(Vector2 moveValue)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                transform.position + moveValue.ToVector3(),
                moveSpeed);
        }
    }
}
