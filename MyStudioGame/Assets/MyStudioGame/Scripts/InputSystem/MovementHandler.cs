using System;
using UnityEngine;

public class MovementHandler : IDisposable
{
    private const string Horizontal = "Horizontal";

    private IInput _input;
    private IMovable _playerMovable;
    private IJumpable _playerJumpable;
    private MovementConfig _config;

    public IInput IInput => _input;

    public MovementHandler(IInput input, Movement playerMovable, MovementConfig config)
    {
        _input = input;
        _playerMovable = playerMovable;
        _playerJumpable = playerMovable;
        _config = config;

        _input.Move += Move;
        _input.Jump += Jump;
    }

    private void Move()
    {
        _playerMovable.Move(new Vector2(Input.GetAxis(Horizontal), 0), _config.Speed);
    }

    private void Jump()
    {
        _playerJumpable.Jump(_config.JumpPower, _config.LayerMask);
    }

    public void Dispose()
    {
        _input.Move -= Move;
        _input.Jump -= Jump;
    }
}
