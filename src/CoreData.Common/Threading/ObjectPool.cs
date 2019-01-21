using System;
using System.Collections.Concurrent;

namespace CoreData.Common.Threading
{
    /// <summary>Provides a thread-safe object pool.</summary> 
    /// <typeparam name="T">Specifies the type of the elements stored in the pool.</typeparam> 
    //[DebuggerDisplay("Count={Count}")]
    public class ObjectPool<T>
    {
        private ConcurrentBag<T> _objects;
        private readonly Func<T> _factory;

        public ObjectPool(Func<T> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _objects = new ConcurrentBag<T>();
        }

        public T GetObject() => _objects.TryTake(out var item) ? item : _factory();

        public void PutObject(T item) => _objects.Add(item);
    }
}
