using UnityEngine;

public class BasePlayerFactory : IPlayerFactory
{
    private const string Path = "Prefaps/BasePlayer";

    public GameObject Create(Vector3 spawnPoint, float interactLenght)
    {
        var prefap = Resources.Load<GameObject>(Path);
        var go = GameObject.Instantiate(prefap);
        var movement = go.AddComponent<Movement>();
        movement.transform.position = spawnPoint;
        return movement.gameObject;
    }
}

