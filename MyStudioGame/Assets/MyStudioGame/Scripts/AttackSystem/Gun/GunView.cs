using UnityEngine;

public class GunView : MonoBehaviour
{
    [SerializeField] private GunConfig _config;

    private Camera _mainCamera;
    private Transform _container;
    private Gun _gun;

    public Vector3 Direction => transform.right;

    private void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
        var bulletFactory = new BaseBulletFactory(FindObjectOfType<Coroutines>());

        if (bulletFactory == null)
            Debug.Log("Bullet Null");

        _container = new GameObject("[Gun Container]").transform;

        _gun = new Gun(bulletFactory, this, FindObjectOfType<Camera>().transform, _config.DataGun);

        _mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position = transform.position;
        Vector2 direction = mousePosition - position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }

        transform.localScale = localScale;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _gun.Attack();
        }
    }

    private void OnDestroy()
    {
        Destroy(_container.gameObject);
    }
}

