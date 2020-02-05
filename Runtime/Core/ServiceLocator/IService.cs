using System;

namespace ProjectCore.ServiceLocator
{
    public interface IService
    {
        Type ServiceType { get; }
    }
}