## Factories Usage Sample

Factories registered in ```ServiceLocator```

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
