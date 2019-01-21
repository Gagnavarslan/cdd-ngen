using System;

namespace CoreData.Common.Extensions
{
    /// <summary>IDisposable wrapper around object which need to be used with USING keyword</summary>
    public class Scoped<T> : IDisposable
    {
        private readonly Action<T> _dispose;

        public Scoped(T context, Action<T> dispose) : this(() => context, dispose) { }
        public Scoped(Func<T> context, Action<T> dispose)
        {
            _dispose = dispose;
            Context = context();
        }

        public T Context { get; }

        public void Dispose() => _dispose(Context);
    }

    // todo: Lock-free container https://github.com/dadhi/ImTools#usage-example
    //public static class DelegateExtensions
    //{
    //    #region Delegate decorators
    //    // todo: Fast ExpressionTree compiler to delegate https://github.com/dadhi/FastExpressionCompiler#examples

    //    //public static Func<T> With<T>(this Func<T> with, Func<T, Func<T>> outter) =>
    //    //    outter(with());
    //    public static Func<T> With<T>(this Func<T> inner, Func<T, T> outter) =>
    //        () => outter(inner());
    //    public static Func<T> With<T>(this Func<T> inner, Action<T> outter) =>
    //        () => { var t = inner(); outter(t); return t; };
    //    public static Func<T> With<T>(this T inner, Func<T, T> outter) =>
    //        () => outter(inner);
    //    public static Func<T> With<T>(this T inner, Action<T> outter) =>
    //        () => { outter(inner); return inner; };

    //    public static Task<T> WithAsync<T>(this T with, Func<T, Task<T>> outterAsync) =>
    //        outterAsync(with);

    //    #endregion Delegate decorators

    //    public static TOut Build<TIn, TOut>(this Func<TIn> setup, Func<TIn, TOut> transform) =>
    //        transform(setup());
    //    public static T Build<T>(this Func<T> setup) => setup();
    //}
}
