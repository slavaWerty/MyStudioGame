using UnityEngine;

public class CameraMonitoring : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _speed = 1.5f;

    private Vector3 _target;

    private void Update()
    {
        if (_playerTransform)
        {
            Vector3 currentPosition = Vector3.Lerp(transform.position, _target, _speed * Time.deltaTime);
            transform.position = currentPosition;

            _target = new Vector3(_playerTransform.position.x, transform.position.y, transform.position.z);
        }
    }
}
