#nullable enable

using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine.Networking;

namespace MinimalUtility.WebRequest
{
    /// <summary>
    /// UniTaskのサポートを活用した、<see cref="UnityWebRequest"/>を使用する<see cref="HttpMessageHandler"/>.
    /// </summary>
    /// <exception cref="UnityWebRequestException">通信結果のエラーはUniTask製のExceptionクラスで発行される.</exception>
    public sealed class UnityWebRequestHttpMessageHandler : HttpMessageHandler
    {
        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            var data = requestMessage.Content == null ? null : await requestMessage.Content.ReadAsByteArrayAsync();
            using var uploadHandler = new UploadHandlerRaw(data);
            uploadHandler.contentType = requestMessage.Headers.Accept.ToString();

            using var downloadHandler = new DownloadHandlerBuffer();
            using var webRequest = new UnityWebRequest(requestMessage.RequestUri, requestMessage.Method.ToString(), downloadHandler, uploadHandler);

            foreach (var (key, values) in requestMessage.Headers)
            {
                webRequest.SetRequestHeader(key, StringUtils.Join(',', values));
            }

            await webRequest.SendWebRequest().WithCancellation(cancellationToken);

            var response = new HttpResponseMessage((HttpStatusCode)webRequest.responseCode)
            {
                RequestMessage = requestMessage,
                Content = new NativeArrayContent(downloadHandler.nativeData),
                Version = HttpVersion.Version10
            };

            var responseHeader = response.Headers;
            foreach (var (key, value) in webRequest.GetResponseHeaders())
            {
                responseHeader.TryAddWithoutValidation(key, value);
            }

            return response;
        }
    }

    /// <summary>
    /// <see cref="NativeArray{T}"/>を基盤とする<see cref="HttpContent"/>.
    /// </summary>
    public sealed class NativeArrayContent : HttpContent
    {
        private readonly NativeArray<byte>.ReadOnly _data;

        public NativeArrayContent(NativeArray<byte>.ReadOnly data)
        {
            _data = data;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext _)
        {
            stream.Write(_data.AsReadOnlySpan());
            return Task.CompletedTask;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _data.Length;
            return true;
        }
    }
}