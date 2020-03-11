using System;

namespace com.cobalt910.core.Runtime.ServiceLocator
{
    public interface IService
    {
        Type ServiceType { get; }
    }

    public interface IMonoService : IService { }
    public interface IFactoryService : IService { }
}