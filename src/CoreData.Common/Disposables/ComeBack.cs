using System;
using System.Threading.Tasks;

namespace CoreData.Common.Disposables
{
    /// <summary>Class that swaps the value with a temporary item, and replaces it upon disposal with the original.
    /// <para>Stolen from Scientist.net </para>
    /// <see cref="https://github.com/github/Scientist.net/blob/master/test/Scientist.Test/Swap.cs"/></summary>
    public class ComeBack<T> : IDisposable
    {
        readonly T _original;
        readonly Action<T> _set;

        public ComeBack(T temporary, Func<T> get, Action<T> set)
        {
            _original = get();
            _set = set;
            set(temporary);
        }

        public void Dispose() => _set(_original);
    }

    //public static class ComeBack
    //{
    //    /// <summary>
    //    /// Swaps <see cref="Scientist"/> enabled value with the input
    //    /// parameter, and upon disposal exchanges the enabled back.
    //    /// </summary>
    //    /// <param name="enabled">The delegate to swap temporarily.</param>
    //    /// <returns>A new <see cref="Swap{Func{Task{bool}}}"/> instance.</returns>
    //    public static IDisposable Enabled(Func<Task<bool>> enabled) =>
    //        new ComeBack<Func<Task<bool>>>(enabled, () => () => Task.FromResult(true), (del) => Scientist.Enabled(del));
    //}
}
