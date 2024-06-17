using UnityEngine;

public class GamePlayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _sceneRootBinder;

    public void Run()
    {
        Debug.Log("Gameplay scene loaded");
    }
}
