using System.Threading;

namespace CoreData.Common.Threading
{
    /// <summary>Extension methods for CancellationToken.</summary> 
    public static class CancellationTokenExtensions
    {
        /// <summary>Cancels a CancellationTokenSource and throws a corresponding OperationCanceledException.</summary> 
        public static void CancelAndThrow(this CancellationTokenSource source)
        {
            source.Cancel();
            source.Token.ThrowIfCancellationRequested();
        }

        /// <summary> Creates a CancellationTokenSource that will be canceled when the specified token has cancellation requested.</summary> 
        public static CancellationTokenSource CreateLinkedSource(this CancellationToken token)
        {
            return CancellationTokenSource.CreateLinkedTokenSource(token, new CancellationToken());
        }
    }
}
