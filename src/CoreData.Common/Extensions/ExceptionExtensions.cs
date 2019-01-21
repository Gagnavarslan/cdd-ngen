using System;
using System.Linq;

namespace CoreData.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static bool IsCanceled(this Exception e)
        {
            if (e.GetBaseException() is OperationCanceledException)
            {
                return true;
            }
            if (e is AggregateException aggregateException)
            {
                return aggregateException.InnerExceptions.Any(IsCanceled);
            }
            return false;
        }
    }
}
