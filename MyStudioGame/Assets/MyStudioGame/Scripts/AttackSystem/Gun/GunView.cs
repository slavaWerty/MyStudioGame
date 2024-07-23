using UnityEngine;

public class GunView : MonoBehaviour
{
    [SerializeField] private GunConfig _config;
    [SerializeField] private float _timeBetweenAttack;

    private float _tempTimeBetweenAttack;
    private Camera _mainCamera;
    private Gun _gun;

    public Vector3 Direction => transform.right;

    private void Start()
    {
        _tempTimeBetweenAttack = _timeBetweenAttack;

        _mainCamera = FindObjectOfType<Camera>();

        var bulletFactory = new BaseBulletFactory(FindObjectOfType<Coroutines>());

        _gun = new Gun(bulletFactory, this, FindObjectOfType<Camera>().transform.GetChild(0), _config.DataGun);

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

        if (_timeBetweenAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _gun.Attack();
                _timeBetweenAttack = _tempTimeBetweenAttack;
            }
        }
        else
        {
            _timeBetweenAttack -= Time.deltaTime;
        }
    }
}

