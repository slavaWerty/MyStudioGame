using UnityEngine;

public class GunView : MonoBehaviour
{
    private Camera _mainCamera;

    public Vector3 Direction => transform.right;

    public void Initzialize(Camera camera)
    {
        _mainCamera = camera;
    }

    private void Update()
    {
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position = transform.position;
        Vector2 direction = mousePosition - position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}

