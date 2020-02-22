using System.Collections.Generic;

namespace com.cobalt910.core.Runtime.ServiceLocator.Local
{
    public class LocalServiceRegisterer : ServiceRegister
    {
        protected override void RegisterServices(List<IService> services)
        {
            var locator = GetComponent<LocalServiceLocator>();
            services.ForEach(locator.Register);
        }
    }
}