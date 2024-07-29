using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerEntryPoint : ITickable
{
    private const string Horizontal = "Horizontal";

    [Inject] public MovementHandler _movementHandler;
    [Inject] public RotatePlayer _playerRotater;
    [Inject] public Movement _movement;

    public void Tick()
    {
        _movement.Idle(_movementHandler.layerMask);

        PlayerMove();

        PlayerJump();

        PlayerRotate();
    }

    private void PlayerRotate()
    {
        _playerRotater.Rotate();
    }

    private void PlayerMove()
    {
        if (Input.GetAxis(Horizontal) != 0)
        {
            _movementHandler.IInput.OnMove();
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _movementHandler.IInput.OnJump();
        }
    }
}

