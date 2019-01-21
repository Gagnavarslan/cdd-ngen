using System;
using System.Collections.Generic;

namespace CoreData.Common.Disposables
{
    public class Scope : IDisposable
    {
        private readonly Action _dispose;

        public Scope(Action dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException();
        }
        
        public bool IsDisposed { get; private set; }

        public void Dispose() => _dispose();
    }

    /// <summary>Container of items to be disposed with this scope, similar to Autofac's Owned.</summary>
    public class Owned : Disposable
    {
        private readonly List<IDisposable> _items;

        public Owned(List<IDisposable> items) =>
            _items = items ?? throw new ArgumentNullException();

        public Owned(IDisposable item) : this(new List<IDisposable>() { item }) { }

        public Owned() : this(new List<IDisposable>()) { }

        protected override void Dispose(bool disposing) =>
            _items.ForEach(item => item.Dispose());
    }
}
