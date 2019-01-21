using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CoreData.Common.Threading
{
    /// <summary>Async ctor|initialization
    /// <seealso cref="https://blog.stephencleary.com/2013/01/async-oop-2-constructors.html"/>
    /// <seealso cref="https://github.com/StephenClearyArchive/AsyncEx.Coordination/blob/master/src/Nito.AsyncEx.Coordination/AsyncLazy.cs"/></summary>
    // ???: to be removed???
    public interface IAsyncInitializable
    {
        /// <summary>The result of the asynchronous initialization of this instance.</summary>
        Task Initialization { get; }
    }

    /// <summary>
    /// Flags controlling the behavior of <see cref="AsyncLazy{T}"/>.
    /// </summary>
    [Flags]
    public enum AsyncLazyFlags
    {
        /// <summary>
        /// No special flags. The factory method is executed on a thread pool thread, and does not retry initialization on failures (failures are cached).
        /// </summary>
        None = 1 << 1,

        /// <summary>
        /// Execute the factory method on the calling thread.
        /// </summary>
        ExecuteOnCallingThread = 1 << 1,

        /// <summary>
        /// If the factory method fails, then re-run the factory method the next time instead of caching the failed task.
        /// </summary>
        RetryOnFailure = 1 << 2,
    }

    /// <summary>
    /// Provides support for asynchronous lazy initialization. This type is fully threadsafe.
    /// </summary>
    /// <typeparam name="T">The type of object that is being asynchronously initialized.</typeparam>
    [DebuggerDisplay("Id = {Id}, State = {GetStateForDebugger}")]
    [DebuggerTypeProxy(typeof(AsyncLazy<>.DebugView))]
    public sealed class AsyncLazy<T>
    {
        internal enum LazyState
        {
            NotStarted,
            Executing,
            Completed
        }

        private readonly object _mutex;
        private readonly Func<Task<T>> _factory;
        private Lazy<Task<T>> _instance;

        ///// <summary>The semi-unique identifier for this instance. This is 0 if the id has not yet been created.</summary>
        //private int _id;

        [DebuggerNonUserCode]
        internal LazyState GetStateForDebugger
        {
            get
            {
                if(!_instance.IsValueCreated)
                    return LazyState.NotStarted;
                if(!_instance.Value.IsCompleted)
                    return LazyState.Executing;
                return LazyState.Completed;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="factory">The asynchronous delegate that is invoked to produce the value when it is needed. May not be <c>null</c>.</param>
        /// <param name="flags">Flags to influence async lazy semantics.</param>
        public AsyncLazy(Func<Task<T>> factory, AsyncLazyFlags flags = AsyncLazyFlags.None)
        {
            if(factory == null)
                throw new ArgumentNullException(nameof(factory));
            _factory = factory;
            if((flags & AsyncLazyFlags.RetryOnFailure) == AsyncLazyFlags.RetryOnFailure)
                _factory = RetryOnFailure(_factory);
            if((flags & AsyncLazyFlags.ExecuteOnCallingThread) != AsyncLazyFlags.ExecuteOnCallingThread)
                _factory = RunOnThreadPool(_factory);

            _mutex = new object();
            _instance = new Lazy<Task<T>>(_factory);
        }

        ///// <summary>
        ///// Gets a semi-unique identifier for this asynchronous lazy instance.
        ///// </summary>
        //public int Id
        //{
        //    get { return IdManager<AsyncLazy<object>>.GetId(ref _id); }
        //}

        /// <summary>
        /// Whether the asynchronous factory method has started. This is initially <c>false</c> and becomes <c>true</c> when this instance is awaited or after <see cref="Start"/> is called.
        /// </summary>
        public bool IsStarted
        {
            get
            {
                lock(_mutex)
                    return _instance.IsValueCreated;
            }
        }

        /// <summary>
        /// Starts the asynchronous factory method, if it has not already started, and returns the resulting task.
        /// </summary>
        public Task<T> Task
        {
            get
            {
                lock(_mutex)
                    return _instance.Value;
            }
        }

        private Func<Task<T>> RetryOnFailure(Func<Task<T>> factory)
        {
            return async () =>
            {
                try
                {
                    return await factory().ConfigureAwait(false);
                }
                catch
                {
                    lock(_mutex)
                    {
                        _instance = new Lazy<Task<T>>(_factory);
                    }
                    throw;
                }
            };
        }

        private Func<Task<T>> RunOnThreadPool(Func<Task<T>> factory)
        {
            return () => System.Threading.Tasks.Task.Run(factory);
        }

        /// <summary>
        /// Asynchronous infrastructure support. This method permits instances of <see cref="AsyncLazy&lt;T&gt;"/> to be await'ed.
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public TaskAwaiter<T> GetAwaiter()
        {
            return Task.GetAwaiter();
        }

        /// <summary>
        /// Asynchronous infrastructure support. This method permits instances of <see cref="AsyncLazy&lt;T&gt;"/> to be await'ed.
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public ConfiguredTaskAwaitable<T> ConfigureAwait(bool continueOnCapturedContext)
        {
            return Task.ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Starts the asynchronous initialization, if it has not already started.
        /// </summary>
        public void Start()
        {
            // ReSharper disable UnusedVariable
            var unused = Task;
            // ReSharper restore UnusedVariable
        }

        [DebuggerNonUserCode]
        internal sealed class DebugView
        {
            private readonly AsyncLazy<T> _lazy;

            public DebugView(AsyncLazy<T> lazy)
            {
                _lazy = lazy;
            }

            public LazyState State { get { return _lazy.GetStateForDebugger; } }

            public Task Task
            {
                get
                {
                    if(!_lazy._instance.IsValueCreated)
                        throw new InvalidOperationException("Not yet created.");
                    return _lazy._instance.Value;
                }
            }

            public T Value
            {
                get
                {
                    if(!_lazy._instance.IsValueCreated || !_lazy._instance.Value.IsCompleted)
                        throw new InvalidOperationException("Not yet created.");
                    return _lazy._instance.Value.Result;
                }
            }
        }
    }
    //public abstract class AsyncInitializable : IAsyncInitializable
    //{
    //    private readonly MyFundamentalType _fundamental;

    //    protected MyComposedType(MyFundamentalType fundamental)
    //    {
    //        _fundamental = fundamental;
    //        Initialization = InitializeAsync();
    //    }

    //    public Task Initialization { get; private set; }

    //    private async Task InitializeAsync()
    //    {
    //        // Asynchronously wait for the fundamental instance to initialize.
    //        await _fundamental.Initialization;

    //        // Do our own initialization (synchronous or asynchronous).
    //        await Task.Delay(100);
    //    }
    //}
}
