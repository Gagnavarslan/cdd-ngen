using NLog;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreData.Desktop.Server.Http
{


    /// <summary>Message default configuration and activation handler.</summary>
    public class ActivationMessageHandler : DelegatingHandler
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        ///// <summary>The number of retries for either i) resp >=400 or ii) an exception is thrown</summary>
        //public int AllowedRetries { get; set; }

        ///// <summary>The number of allowed redirects (3xx).</summary>
        //public int AllowedRedirects { get; set; }

        //public bool FollowRedirect => AllowedRedirects != 0;

        //public ActivationMessageHandler() : this(LogMessageHandler inner)
        //public ActivationMessageHandler(HttpMessageHandler inner)
        //    : base(inner)
        //{
        //    //AllowedRetries = 2;
        //    //AllowedRedirects = 5;
        //}

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
            //var remainingRetries = AllowedRetries;
            //var remainingRedirects = AllowedRedirects;
            //Exception lastException = null;

            //HttpResponseMessage response = null;
            //do // While (triesRemaining > 0)
            //{
            //    response?.Dispose();
            //    response = null;
            //    lastException = null;

            //    // Check after previous response (if any) has been disposed of.
            //    cancellationToken.ThrowIfCancellationRequested();

            //    // We keep a local list of the interceptors, since we can't call await inside lock.
            //    List<IHttpExecuteInterceptor> interceptors;
            //    lock (executeInterceptorsLock)
            //    {
            //        interceptors = executeInterceptors.ToList();
            //    }
            //    if (request.Properties.TryGetValue(ExecuteInterceptorKey, out var interceptorsValue) &&
            //        interceptorsValue is List<IHttpExecuteInterceptor> perCallinterceptors)
            //    {
            //        interceptors.AddRange(perCallinterceptors);
            //    }

            //    // Intercept the request.
            //    foreach (var interceptor in interceptors)
            //    {
            //        await interceptor.InterceptAsync(request, cancellationToken).ConfigureAwait(false);
            //    }
            //    if (loggable)
            //    {
            //        if ((LogEvents & LogEventType.RequestUri) != 0)
            //        {
            //            InstanceLogger.Debug("Request[{0}] (triesRemaining={1}) URI: '{2}'", loggingRequestId, triesRemaining, request.RequestUri);
            //        }
            //        if ((LogEvents & LogEventType.RequestHeaders) != 0)
            //        {
            //            LogHeaders($"Request[{loggingRequestId}] Headers:", request.Headers, request.Content?.Headers);
            //        }
            //        if ((LogEvents & LogEventType.RequestBody) != 0)
            //        {
            //            await LogBody($"Request[{loggingRequestId}] Body: '{{0}}'", request.Content).ConfigureAwait(false);
            //        }
            //    }
            //    try
            //    {
            //        // Send the request!
            //        response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            //    }
            //    catch (Exception ex)
            //    {
            //        lastException = ex;
            //    }

            //    // Decrease the number of retries.
            //    if (response == null || ((int)response.StatusCode >= 400 || (int)response.StatusCode < 200))
            //    {
            //        triesRemaining--;
            //    }

            //    // Exception was thrown, try to handle it.
            //    if (response == null)
            //    {
            //        var exceptionHandled = false;

            //        // We keep a local list of the handlers, since we can't call await inside lock.
            //        List<IHttpExceptionHandler> handlers;
            //        lock (exceptionHandlersLock)
            //        {
            //            handlers = exceptionHandlers.ToList();
            //        }
            //        if (request.Properties.TryGetValue(ExceptionHandlerKey, out var handlersValue) &&
            //            handlersValue is List<IHttpExceptionHandler> perCallHandlers)
            //        {
            //            handlers.AddRange(perCallHandlers);
            //        }

            //        // Try to handle the exception with each handler.
            //        foreach (var handler in handlers)
            //        {
            //            exceptionHandled |= await handler.HandleExceptionAsync(new HandleExceptionArgs
            //            {
            //                Request = request,
            //                Exception = lastException,
            //                TotalTries = NumTries,
            //                CurrentFailedTry = NumTries - triesRemaining,
            //                CancellationToken = cancellationToken
            //            }).ConfigureAwait(false);
            //        }

            //        if (!exceptionHandled)
            //        {
            //            InstanceLogger.Error(lastException,
            //                "Response[{0}] Exception was thrown while executing a HTTP request and it wasn't handled", loggingRequestId);
            //            throw lastException;
            //        }
            //        else if (loggable && (LogEvents & LogEventType.ResponseAbnormal) != 0)
            //        {
            //            InstanceLogger.Debug("Response[{0}] Exception {1} was thrown, but it was handled by an exception handler",
            //                loggingRequestId, lastException.Message);
            //        }
            //    }
            //    else
            //    {
            //        if (loggable)
            //        {
            //            if ((LogEvents & LogEventType.ResponseStatus) != 0)
            //            {
            //                InstanceLogger.Debug("Response[{0}] Response status: {1} '{2}'", loggingRequestId, response.StatusCode, response.ReasonPhrase);
            //            }
            //            if ((LogEvents & LogEventType.ResponseHeaders) != 0)
            //            {
            //                LogHeaders($"Response[{loggingRequestId}] Headers:", response.Headers, response.Content?.Headers);
            //            }
            //            if ((LogEvents & LogEventType.ResponseBody) != 0)
            //            {
            //                await LogBody($"Response[{loggingRequestId}] Body: '{{0}}'", response.Content).ConfigureAwait(false);
            //            }
            //        }
            //        if (response.IsSuccessStatusCode)
            //        {
            //            // No need to retry, the response was successful.
            //            triesRemaining = 0;
            //        }
            //        else
            //        {
            //            bool errorHandled = false;

            //            // We keep a local list of the handlers, since we can't call await inside lock.
            //            List<IHttpUnsuccessfulResponseHandler> handlers;
            //            lock (unsuccessfulResponseHandlersLock)
            //            {
            //                handlers = unsuccessfulResponseHandlers.ToList();
            //            }
            //            if (request.Properties.TryGetValue(UnsuccessfulResponseHandlerKey, out var handlersValue) &&
            //                handlersValue is List<IHttpUnsuccessfulResponseHandler> perCallHandlers)
            //            {
            //                handlers.AddRange(perCallHandlers);
            //            }

            //            // Try to handle the abnormal HTTP response with each handler.
            //            foreach (var handler in handlers)
            //            {
            //                try
            //                {
            //                    errorHandled |= await handler.HandleResponseAsync(new HandleUnsuccessfulResponseArgs
            //                    {
            //                        Request = request,
            //                        Response = response,
            //                        TotalTries = NumTries,
            //                        CurrentFailedTry = NumTries - triesRemaining,
            //                        CancellationToken = cancellationToken
            //                    }).ConfigureAwait(false);
            //                }
            //                catch when (DisposeAndReturnFalse(response)) { }

            //                bool DisposeAndReturnFalse(IDisposable disposable)
            //                {
            //                    disposable.Dispose();
            //                    return false;
            //                }
            //            }

            //            if (!errorHandled)
            //            {
            //                if (FollowRedirect && HandleRedirect(response))
            //                {
            //                    if (redirectRemaining-- == 0)
            //                    {
            //                        triesRemaining = 0;
            //                    }

            //                    errorHandled = true;
            //                    if (loggable && (LogEvents & LogEventType.ResponseAbnormal) != 0)
            //                    {
            //                        InstanceLogger.Debug("Response[{0}] Redirect response was handled successfully. Redirect to {1}",
            //                            loggingRequestId, response.Headers.Location);
            //                    }
            //                }
            //                else
            //                {
            //                    if (loggable && (LogEvents & LogEventType.ResponseAbnormal) != 0)
            //                    {
            //                        InstanceLogger.Debug("Response[{0}] An abnormal response wasn't handled. Status code is {1}",
            //                            loggingRequestId, response.StatusCode);
            //                    }

            //                    // No need to retry, because no handler handled the abnormal response.
            //                    triesRemaining = 0;
            //                }
            //            }
            //            else if (loggable && (LogEvents & LogEventType.ResponseAbnormal) != 0)
            //            {
            //                InstanceLogger.Debug("Response[{0}] An abnormal response was handled by an unsuccessful response handler. " +
            //                    "Status Code is {1}", loggingRequestId, response.StatusCode);
            //            }
            //        }
            //    }
            //} while (triesRemaining > 0); // Not a successful status code but it was handled.

            //// If the response is null, we should throw the last exception.
            //if (response == null)
            //{
            //    InstanceLogger.Error(lastException, "Request[{0}] Exception was thrown while executing a HTTP request", loggingRequestId);
            //    throw lastException;
            //}
            //else if (!response.IsSuccessStatusCode && loggable && (LogEvents & LogEventType.ResponseAbnormal) != 0)
            //{
            //    InstanceLogger.Debug("Response[{0}] Abnormal response is being returned. Status Code is {1}", loggingRequestId, response.StatusCode);
            //}

            //return response;
        }
    }
}
