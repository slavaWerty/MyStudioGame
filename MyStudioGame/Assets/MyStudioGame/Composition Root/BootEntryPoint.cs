using UnityEngine.SceneManagement;
using VContainer.Unity;

public class BootEntryPoint : IStartable
{
    private const string GamePlay = "GamePlay";
    private LifetimeScope _lifetime;

    public BootEntryPoint(LifetimeScope lifetime)
    {
        _lifetime = lifetime;
    }

    public void Start()
    {
        SceneManager.LoadScene(GamePlay);

    }
}

