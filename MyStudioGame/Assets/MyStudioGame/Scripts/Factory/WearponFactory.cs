using UnityEngine;

public class WearponFactory : IWeaponFactory
{

    public GunView CreateGun(string path, Camera camera)
    {
        var go = CreateObject(path);
        var wearpon = go.AddComponent<GunView>();
        wearpon.Initzialize(camera);
        return wearpon;
    }

    public SwordView CreateSword(string path)
    {
        var go = CreateObject(path);
        var wearpon = go.AddComponent<SwordView>();
        wearpon.Initzialize();
        return wearpon;
    }
    private GameObject CreateObject(string path)
    {
        var prefap = Resources.Load<GameObject>(path);
        var go = GameObject.Instantiate(prefap);
        return go;
    }
}

