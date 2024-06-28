using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWebRequestHttpMessageHandler : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
    {
        using var uploadHandler = new UploadHandlerRaw(requestMessage.Content == null ? null : await requestMessage.Content.ReadAsByteArrayAsync());
        uploadHandler.contentType = requestMessage.Headers.Accept.ToString();

        using var downloadHandler = new DownloadHandlerBuffer();
        using var webRequest = new UnityWebRequest(requestMessage.RequestUri, requestMessage.Method.ToString(), downloadHandler, uploadHandler);

        foreach (var header in requestMessage.Headers)
        {
            webRequest.SetRequestHeader(header.Key, string.Join(",", header.Value));
        }

        CancellationTokenRegistration cancellationTokenRegistration = default;
        if (cancellationToken.CanBeCanceled)
        {
            cancellationTokenRegistration = cancellationToken.Register(static state =>
            {
                var req = (UnityWebRequest)state;
                req.Abort();
                req.Dispose();
            }, webRequest);
        }

        using (cancellationTokenRegistration)
        {
            await new AwaitableAsyncOperation(webRequest.SendWebRequest());
        }
        cancellationToken.ThrowIfCancellationRequested();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            throw new InvalidOperationException($"UnityWebRequest.SendWebRequest is failed, Result: {webRequest.result}");
        }

        var response = new HttpResponseMessage((HttpStatusCode)webRequest.responseCode)
        {
            RequestMessage = requestMessage,
            Content = new ByteArrayContent(downloadHandler.data),
            Version = HttpVersion.Version10
        };

        var responseHeader = response.Headers;
        foreach (var header in webRequest.GetResponseHeaders())
        {
            responseHeader.TryAddWithoutValidation(header.Key, header.Value);
        }

        return response;
    }

    readonly struct AwaitableAsyncOperation
    {
        readonly AsyncOperation operation;

        public AwaitableAsyncOperation(AsyncOperation operation)
        {
            this.operation = operation;
        }

        public AsyncOperationAwaiter GetAwaiter() => new AsyncOperationAwaiter(operation);
    }

    struct AsyncOperationAwaiter : ICriticalNotifyCompletion
    {
        AsyncOperation asyncOperation;
        Action<AsyncOperation> continuationAction;

        public AsyncOperationAwaiter(AsyncOperation asyncOperation)
        {
            this.asyncOperation = asyncOperation;
            this.continuationAction = null;
        }

        public bool IsCompleted => asyncOperation.isDone;

        public void GetResult()
        {
            if (continuationAction != null)
            {
                asyncOperation.completed -= continuationAction;
                continuationAction = null;
                asyncOperation = null;
            }
            else
            {
                asyncOperation = null;
            }
        }

        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            if (continuationAction != null) throw new InvalidOperationException("continuation is already registered.");

            continuationAction = _ => continuation();
            asyncOperation.completed += continuationAction;
        }
    }
}