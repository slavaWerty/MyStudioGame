using System.Collections.Generic;
using UnityEngine;

public class IdleEnemyState : IEnemyState
{
    private Transform _transform;
    private float _speed;
    private List<Vector3> _positions;
    private int _index = 0;
    private Vector3 _currentPosition;

    public IdleEnemyState(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;

        _positions = new List<Vector3>
        {
            new Vector3(_transform.position.x + 3, _transform.position.y, _transform.position.z),
            new Vector3(_transform.position.x - 3, _transform.position.y, _transform.position.z)
        };

        Debug.Log(_transform.position);

        _currentPosition = _positions[_index];
    }

    public void Work()
    {
        _transform.position = Vector2.MoveTowards(_transform.position,
            _currentPosition, _speed * Time.deltaTime);

        if (Vector2.Distance(_transform.position, _currentPosition) < 0.2f)
        {
            if (_index >= _positions.Count - 1)
            {
                _index = 0;
                Debug.Log(0);
            }
            else if(_index < _positions.Count - 1)
            {
                _index++;
                Debug.Log("++");
            }

            _currentPosition = _positions[_index];
        }
    }
}

