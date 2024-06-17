using DI;
using UnityEngine;

public class DIExampleProject : MonoBehaviour
{
    private void Awake()
    {
        var projectContainer = new DIContainer();
        projectContainer.RegisterSingleton(
            _ => new MyAwesomeProjectService());
        projectContainer.RegisterSingleton("option 1", _ => new MyAwesomeProjectService());
        projectContainer.RegisterSingleton("option 2", _ => new MyAwesomeProjectService());

        var sceneRoot = FindObjectOfType<DIExampleScene>();
        sceneRoot.Initzialize(projectContainer);
    }
}

public class MyAwesomeProjectService { }

public class MySceneService
{
    private readonly MyAwesomeProjectService _myAwesomeProject;

    public MySceneService(MyAwesomeProjectService myAwesomeProjectService)
    {
        _myAwesomeProject = myAwesomeProjectService;
    }
}

public class MyAwesomeFactory 
{
    public MyAwesomeObject InstanceObject(string text, int number)
    {
        return new MyAwesomeObject(text, number);
    }
}

public class MyAwesomeObject
{
    private string _text;
    private int _number;

    public MyAwesomeObject(string text, int number)
    {
        _text = text;
        _number = number;
    }
}

