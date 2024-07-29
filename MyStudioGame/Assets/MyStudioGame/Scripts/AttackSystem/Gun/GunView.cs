using System;
using TMPro;
using UnityEngine;

public class GunView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private GunInitzializer _gunInitzializer;

    public Gun Gun => _gunInitzializer.Gun;

    private void OnReloaded(int currentAmmo, int allAmmo)
    {
        _ammoText.text = currentAmmo.ToString() + "/" + allAmmo.ToString();
    }

    private void Start()
    {
        Gun.Reloaded += OnReloaded;

        Gun.OnReloaded();
    }

    private void OnDisable()
    {
        Gun.Reloaded -= OnReloaded;
    }
}

