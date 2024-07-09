using System;
using UnityEngine;

public interface IMovable
{
    public void Move(Vector3 direction, float speed);
    public void Move(Vector3 direction);
    public event Action Moved;
}

