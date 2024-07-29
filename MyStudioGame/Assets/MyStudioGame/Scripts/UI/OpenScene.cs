using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    public void OpenSceneByName(string sceneName) =>
        SceneManager.LoadScene(sceneName);

    public void OpenSceneByIndex(int index) =>  
        SceneManager.LoadScene(index);

    public void RestartScene() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
