using UnityEngine;

public interface IPlayerFactory 
{
    public GameObject Create(Vector3 spawnPoint, float inetractLenght);
}
