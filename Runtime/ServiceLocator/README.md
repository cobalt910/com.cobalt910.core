## ServiceLocator Usage Sample

> ## Services
* Creating services.

If you want to implement your own service you need to create class and implement ```IService``` interface.
```csharp
public class YourManager : MonoBehaviour, IService
{
  // type with which the manager will be registered
  Type IService.ServiceType => typeof(YourManager);
}
```

* Scene setup

You need to create on scene empty object, and assign ServiceLocator component (ServiceRegisterer will be assigned autimatically).

And create two empty objects for ```Managers``` and ```Factories``` as child of ```ServiceLocator```.

Your managers and factories should be placed as child of concrete empty objects.
<p align="center">
  <img width="510" height="524" src="https://image.prntscr.com/image/daytFhQITfuAX7uRyPmESQ.png">
</p>

* Get manager or service
```csharp
public void Awake()
{
  var manager = ServiceLocator.Resolve<YourManager>();
}
```

> ## Factories

Factories also registered in ```ServiceLocator```

* Creating factory.
```csharp
public class EmptyObjectCreationArgs
{
  public readonly Vector3 Position;  
  
  public EmptyObjectCreationArgs(Vector3 position)
  {
    Position = position;
  }
}

public class EmptyObjectFactory : IFactory<GameObject, EmptyObjectCreationArgs>
{
  IService.Type ServiceType => typeof(IFactory<GameObject, EmptyObjectCreationArgs>);

  public GameObject Create(EmptyObjectCreationArgs args)
  {
    var emptyObjectInstance = new GameObject("Empty");
    emptyObjectInstance.transform.position = args.Position;
    return emptyObjectInstance;
  }
}
```

* Using factory
```csharp
private IFactory<GameObject, EmptyObjectCreationArgs> _emptyObjectFactory;

private void Awake()
{
  _emptyObjectFactory = ServiceLocator.Resolve<IFactory<GameObject, EmptyObjectCreationArgs>>();
  _emptyObjectFactory.Create(new EmptyObjectCreationArgs(Random.insideUnitSphere));
}
```
For best performance, it is better to use factories in conjunction with the ```PoolManager```
