using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayEntryPoint : ITickable, IStartable
{
    private const float TimeBetweenWeaponChanges = 0.1f;
    private const string EnemyPath = "Prefaps/BaseEnemy";

    [Inject] public AttackHandler _attackHandler;
    [Inject] public GunRotater _gunRotater;
    [Inject] public WearponSwitch _wearponSwitch;
    [Inject] public EnemySpawner _enemySpanwner;

    private float _timeBetweenWeaponChanges;
    private float _startTimeBtwShots;
    private float _timeBetweenSpawnEnemy;

    public void Start()
    {
        _timeBetweenWeaponChanges = TimeBetweenWeaponChanges;
        _startTimeBtwShots = _wearponSwitch.Wearpon.StartTimeBtwShots;
        _timeBetweenSpawnEnemy = _enemySpanwner.TimeBetweenSpawnEnemy;
    }

    public void Tick()
    {
        if (_wearponSwitch.Wearpon.StartTimeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _attackHandler.IInput.OnAttack();
                _wearponSwitch.Wearpon.StartTimeBtwShots = _startTimeBtwShots;
            }
        }
        else
        {
            _wearponSwitch.Wearpon.StartTimeBtwShots -= Time.deltaTime;
        }

        if (_attackHandler.Wearpon.GetType() == typeof(Gun))
        {
            _gunRotater.Rotate();
        }

        if (_timeBetweenWeaponChanges < 0)
        {
            if (Input.GetKey(KeyCode.E))
            {
                _wearponSwitch.IInput.OnSwitchWearpon();
                _timeBetweenWeaponChanges = TimeBetweenWeaponChanges;
                _startTimeBtwShots = _wearponSwitch.Wearpon.StartTimeBtwShots;
            }
        }
        else
        {
            _timeBetweenWeaponChanges -= Time.deltaTime;
        }

        if (_enemySpanwner.TimeBetweenSpawnEnemy <= 0)
        {
            _enemySpanwner.Spawn(EnemyPath);
            _enemySpanwner.TimeBetweenSpawnEnemy = _timeBetweenSpawnEnemy;
        }
        else
        {
            _enemySpanwner.TimeBetweenSpawnEnemy -= Time.deltaTime;
        }
    }
}

