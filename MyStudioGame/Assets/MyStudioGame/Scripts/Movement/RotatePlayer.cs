using UnityEngine;

public class RotatePlayer
{
    private Camera _camera;
    private Transform _transform;

    public RotatePlayer(Camera camera, Transform transform)
    {
        _camera = camera;
        _transform = transform;
    }

    public void Rotate()
    {
        var position = _camera.WorldToScreenPoint(_transform.position);

        if (Input.mousePosition.x < position.x)
        {
            _transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        else
        {
            _transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}