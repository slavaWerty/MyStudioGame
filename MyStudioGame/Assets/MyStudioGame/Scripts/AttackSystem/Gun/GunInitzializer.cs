using Infentory;
using UnityEngine;

public class GunInitzializer : MonoBehaviour, IInitzializer
{
    [SerializeField] private GunConfig _config;
    [SerializeField] private float _timeBetweenAttack;
    [SerializeField] private Transform _spawnPosition;

    private float _tempTimeBetweenAttack;
    private Camera _mainCamera;
    private Gun _gun;

    public Transform SpawnPosition => _spawnPosition;
    public Vector3 Direction => transform.right;
    public Gun Gun => _gun;

    public void Initzialize(InfentoryService service, Coroutines coroutines, Camera camera)
    {
        _tempTimeBetweenAttack = _timeBetweenAttack;

        _mainCamera = camera;

        var bulletFactory = new BaseBulletFactory(coroutines);

        _gun = new Gun(bulletFactory, this, _mainCamera.transform.GetChild(0), _config.DataGun, service);
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

        if (_timeBetweenAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _gun.Attack();
                _timeBetweenAttack = _tempTimeBetweenAttack;
            }
        }
        else
        {
            _timeBetweenAttack -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(_gun.Reload(_config.DataGun.TimeBetweenReload));
        }
    }
}

