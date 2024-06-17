using DI;
using UnityEngine;

public class DIExampleScene : MonoBehaviour
{
    public void Initzialize(DIContainer projectContainer)
    {
       // var serviceWithoutTag = projectContainer.Resovle<MyAwesomeProjectService>();
      //  var service1 = projectContainer.Resovle<MyAwesomeProjectService>("option 1");
        //var service2 = projectContainer.Resovle<MyAwesomeProjectService>("option 2");

        var sceneContainer = new DIContainer(projectContainer);
        sceneContainer.RegisterSingleton(c => new MySceneService(c.Resovle<MyAwesomeProjectService>()));
        sceneContainer.RegisterSingleton(_ => new MyAwesomeFactory());
        sceneContainer.RegisterInstance( new MyAwesomeObject("instance", 10));

        var factory = sceneContainer.Resovle<MyAwesomeFactory>();

        for (var i = 0; i < 3; i++)
        {
            var id = $"0{i}";
            var o = factory.InstanceObject(id, i);
            Debug.Log("Create" + o.ToString());
        }

        var instance = sceneContainer.Resovle<MyAwesomeObject>();
        Debug.Log("Created" + instance.ToString());
    }
}

