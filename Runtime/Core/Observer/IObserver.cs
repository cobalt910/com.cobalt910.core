namespace ProjectCore.Observer
{
    public interface IObserver
    {
        void OnObjectChanged(IObservable observable);
    }
    
    public interface IObserver<in T> //where T : IObservable<T>
    {
        void OnObjectChanged(T observable);
    }
}