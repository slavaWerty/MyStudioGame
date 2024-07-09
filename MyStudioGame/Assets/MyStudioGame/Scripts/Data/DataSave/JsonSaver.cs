using System.IO;
using UnityEngine;

public class JsonSaver<T>
{
    private const string Extensions = ".json";
    private string _name;

    public JsonSaver(string name)
    {
        _name = name;
    }

    public void Save(T dataSaved)
    {
        string json = JsonUtility.ToJson(dataSaved);

        using(StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + _name + Extensions))
        {
            writer.Write(json);
        }
    }

    public T Load()
    {
        string json = string.Empty;

        using (StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + _name + Extensions))
        {
            json = reader.ReadToEnd();
        }

        object data = JsonUtility.FromJson<T>(json);
        return (T)data;
    }
}

