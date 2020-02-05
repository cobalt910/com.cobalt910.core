﻿using com.cobalt910.core.Runtime.Core.ServiceLocator;

namespace com.cobalt910.core.Runtime.Core.Factory
{
    public interface IFactory : IService
    {
        
    }

    public interface IFactory<out TReturn, in TArgs> : IFactory 
    {
        TReturn Create(TArgs args);
    }
}