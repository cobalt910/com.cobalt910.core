## PoolManager Usage Sample

**Pool Manager work only with ServiceLocator.**

> Pool Manager
* Get instance of PoolManager
```csharp
private PoolManager _poolManager;

private void Awake()
{
  _poolManager = ServiceLocator.Resolve<PoolManager>();
}
```

* Create new pool of objects.
```csharp
public GameObject Prefab;

private PoolManager _poolManager;

private void Awake()
{
  ...
  int poolInitialSize = 10;
  int poolIncreaseSizeBy = 2; // optional parameter, default value is zero.
  _poolManager.CreatePool(Prefab, poolInitialSize, poolIncreaseSizeBy);
}
```


PoolManager use ```gameObject.GetInstanceID();``` to identify which pool the object belongs to.

If ```int poolIncreaseSizeBy = 0```, pool not beed increased and new object will be obtained from the queue, 
even if these object active on scene.

* Delete pool queue
```csharp
public GameObject Prefab;

private void Awake()
{
  ...
  _poolManager.DisposePool(Prefab); // for destroy instantly
  _poolManager.DisposePool(Prefab, 10f); // or to delete objects in groups within 10 seconds
                                         // for avoiding preformance spikes
}
```

* Get object from pool and return it back.
```csharp
public GameObject Prefab;
private PoolManager _poolManager;

...

private void Spawn()
{
  // spawning
  var spawnPosition = Vector3.zero;
  var spawnRotation = Quaternion.identity;
  PoolObject poolObject = _poolManager.InstantiateFromPool(Prefab, spawnPosition, spawnRotation);
  
  ReturnToPool(poolObject);
}

private void ReturnToPool(PoolObject poolObject)
{
  poolObject.Destroy();
}
```



> ### PoolObject
PoolObject works like a wrapper on GameObject.

PoolObject also have helpful parameters for easy control of object.

```csharp
public sealed class PoolObject
{
  public Transform Transform { get; }         // cached transfrom of prefab instance
  public GameObject GameObject { get; }       // cached gameObject 
  public Transform PoolParent { get; }        // default parent of object
                                              // (if you change parent, this parent will be applied 
                                              // when object returned into pool).

  public int InstanceId { get; }                  // instanceId of current gameObject instance.
  public bool IsInsidePool { get; private set; }  
  public ObjectLocator ObjectLocator { get; }     // struct for caching components, more about that below.

  public event Action OnDestroyed;                // event called when object return into pool.
                                                  // and reset, so you don't need unsubscribe.
}
```



> ### Pool Objects Events
```csharp
public interface IPoolObject
{
  void PostAwake(PoolObject poolObject);        // called after unity Awake()
  void OnReuseObject(PoolObject poolObject);    // called when object was grabbed from pool
  void OnDisposeObject(PoolObject poolObject);  // called when object returned into pool
}
```
When you create pool unity call ```Awake()``` method and after this 
PoolManager call ```PostAwake(...)``` method in each class who has implement ```IPoolObject``` interface.

Each method of interface has PoolObject as parameter, use this object you can cache any component or objects what tou need.

```csharp
void PostAwake(PoolObject poolObject)
{
  // for save any component
  var rigidbody = gameObject.GetComponent<Rigidbody>();
  // Register<register type>(instance)
  poolObject.ObjectLocator.Register<Rigidbody>(rigidbody);
  
  // or forget if this is temporary object
  poolObject.Forget<Rigidbody>();
}
```

And in another you can get this component/object withoud call expensive ```GetComponent()```
```csharp
private void Spawn()
{
  PoolObject poolObject = _poolManager.InstantiateFromPool(Prefab);
  var rigidbody = poolObject.Resolve<Rigidbody>();
  rigidbody.AddForce(Vector3.up * 1000f);
}
```




