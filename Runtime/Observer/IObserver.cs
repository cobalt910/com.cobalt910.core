namespace com.cobalt910.core.Runtime.Observer
{
    public interface IObserver
    {
        void OnObjectChanged(IObservable observable);
    }
    
    public interface IObserver<in T>
    {
        void OnObjectChanged(T observable);
    }
}