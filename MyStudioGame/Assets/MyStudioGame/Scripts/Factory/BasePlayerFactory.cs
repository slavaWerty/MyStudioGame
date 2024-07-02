using UnityEngine;

public class BasePlayerFactory : IPlayerFactory
{
    private const string Path = "Prefaps/BasePlayer";

    public GameObject Create(Vector3 spawnPoint)
    {
        var prefap = Resources.Load<GameObject>(Path);
        var go = GameObject.Instantiate(prefap);
        var player = go.AddComponent<Movement>();
        player.transform.position = spawnPoint;
        return player.gameObject;
    }
}

