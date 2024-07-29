using System;
using UnityEngine;

public class Movement : MonoBehaviour, IMovable, IJumpable
{
    private Animator _animator;
    public event Action Moved;
    private Rigidbody2D _rigidbody;

    private States State
    {
        get { return (States)_animator.GetInteger("state"); }
        set { _animator.SetInteger("state", (int)value); }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void Jump(float jumpPower, LayerMask layerMask)
    {
        if (IsJump(layerMask))
            _rigidbody.velocity += Vector2.up * jumpPower;
    }

    public void Move(Vector3 direction, float speed, LayerMask layerMask)
    {
        if (IsJump(layerMask))
            State = States.run;

        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        transform.position += direction * Time.deltaTime * speed;

        Moved?.Invoke();
    }

    public void Move(Vector3 direction)
    {
        Debug.Log("Ne tot method");
    }

    private bool IsJump(LayerMask layerMask)
    {
        Vector3 point = transform.position - new Vector3(0, 1);

        return Physics2D.OverlapCircle(point, 0.1f, layerMask);
    }

    public void Move(Vector3 direction, float speed)
    {
        Debug.Log("Ne tot method");
    }

    public void Idle(LayerMask layerMask)
    {
        if (IsJump(layerMask))
            State = States.idle;
    }
}

public enum States
{
    idle,
    run
}

