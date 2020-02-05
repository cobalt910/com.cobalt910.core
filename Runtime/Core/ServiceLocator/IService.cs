using System;

namespace com.cobalt910.core.Runtime.Core.ServiceLocator
{
    public interface IService
    {
        Type ServiceType { get; }
    }
}