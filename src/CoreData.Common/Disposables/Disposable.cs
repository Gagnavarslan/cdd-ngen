using System;

namespace CoreData.Common.Disposables
{
    public abstract class Disposable : IDisposable
    {
        public static readonly IDisposable Nop = new NopObject();
        class NopObject : IDisposable { public void Dispose() { } }

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            if (IsDisposed) return;

            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            finally
            {
                IsDisposed = true;
            }
        }

        protected abstract void Dispose(bool disposing);
    }
}
