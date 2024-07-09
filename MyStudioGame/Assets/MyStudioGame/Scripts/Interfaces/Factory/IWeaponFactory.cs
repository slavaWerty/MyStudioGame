using UnityEngine;

public interface IWeaponFactory
{
    public GunView CreateGun(string path, Camera camera);
    public SwordView CreateSword(string path);
}

