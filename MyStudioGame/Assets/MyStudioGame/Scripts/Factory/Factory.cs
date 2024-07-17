using UnityEngine;

public class Factory<T> where T : MonoBehaviour
{
    public T Create(string path)
    {
        var prefap = Resources.Load<GameObject>(path);
        var go = GameObject.Instantiate(prefap);
        var t = go.AddComponent<T>();
        return t;
    }
}
