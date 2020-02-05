namespace ProjectCore.PoolManager
{
    public interface IPoolObject
    {
        /// <summary>
        /// Function called after Unity.Awake() and before Unity.Start().
        /// </summary>
        /// <param name="poolObject">Object was represent container for GameObject. Support component caching, and logic for Destroying pool object.</param>
        void PostAwake(PoolObject poolObject);

        /// <summary>
        /// Called before object grabbing object from pool.
        /// </summary>
        /// <param name="poolObject"></param>
        void OnReuseObject(PoolObject poolObject);

        /// <summary>
        /// Called after object returned into pool.
        /// </summary>
        /// <param name="poolObject"></param>
        void OnDisposeObject(PoolObject poolObject);
    }
}