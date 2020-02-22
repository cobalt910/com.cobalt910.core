## ServiceLocator Usage Sample

> ## Global Services
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

> ## Local Services

* Creating services is the same like in ```Global```.

* Object or prefab setup.

You need to add on object ```LocalServiceLocator``` component and all your services.
<p align="center">
  <img width="510" height="524" src="https://image.prntscr.com/image/ZZ0x3rDvRBam6DXhTrDP9w.png">
</p>

After this, and after changing your child hierarchy you should press "Re-Bake Child Id's" button in ```LocalServiceLocator``` component. This additional step made for optimization.

* Get manager or service in local context.
```csharp
public void Awake()
{
  var manager = ServiceLocator.Resolve<YourManager>(gameObject); // add additional parameter, gameObject
}
```

At this stage local services support only one depth level resolving.
