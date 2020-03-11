using System;
using com.cobalt910.core.Runtime.Misc;
using com.cobalt910.core.Runtime.ServiceLocator;
using Zenject;

namespace com.cobalt910.core.Runtime.Factory
{
    public abstract class MonoFactory<TArgs, TReturn> : CachedBehaviour, IFactoryService
    {
        [Inject] protected DiContainer Container;
        public abstract TReturn Create(TArgs args);
        public abstract Type ServiceType { get; }
    }
}