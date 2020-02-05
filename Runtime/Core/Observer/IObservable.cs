namespace ProjectCore.Observer
{
    public interface IObservable
    {
        void SetChanged();
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
    }
    
    public interface IObservable<out T> where T : IObservable<T>
    {
        void SetChanged();
        void AddObserver(IObserver<T> observer);
        void RemoveObserver(IObserver<T> observer);
    }
}