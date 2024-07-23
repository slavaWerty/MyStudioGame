using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public void SetActivePanel(GameObject gameObject) =>
        gameObject.SetActive(!gameObject.activeSelf);
}
