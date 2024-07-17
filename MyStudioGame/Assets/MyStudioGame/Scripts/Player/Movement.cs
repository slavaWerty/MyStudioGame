using System;
using UnityEngine;

public class Movement : MonoBehaviour, IMovable, IJumpable
{
    public event Action Moved;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump(float jumpPower, LayerMask layerMask)
    {
        if (IsJump(layerMask))
            _rigidbody.velocity += Vector2.up * jumpPower;
    }

    public void Move(Vector3 direction, float speed)
    {
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
}

