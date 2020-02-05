using ProjectCore.ServiceLocator;

namespace ProjectCore.Factory
{
    public interface IFactory : IService
    {
        
    }

    public interface IFactory<out TReturn, in TArgs> : IFactory 
    {
        TReturn Create(TArgs args);
    }
}