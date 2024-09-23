using System.Net;
using System.Net.Http;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace MinimalUtility.WebRequest
{
    /// <summary>
    /// UniTaskのサポートを活用した、<see cref="UnityWebRequest"/>を使用する<see cref="HttpMessageHandler"/>.
    /// </summary>
    /// <exception cref="UnityWebRequestException">通信結果のエラーはUniTask製のExceptionクラスで発行される.</exception>
    public class UnityWebRequestHttpMessageHandler : HttpMessageHandler
    {
        /// <inheritdoc/>
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
        {
            byte[] data = requestMessage.Content == null ? null : await requestMessage.Content.ReadAsByteArrayAsync();
            using UploadHandlerRaw uploadHandler = new (data);
            uploadHandler.contentType = requestMessage.Headers.Accept.ToString();

            using DownloadHandlerBuffer downloadHandler = new ();
            using UnityWebRequest webRequest = new (requestMessage.RequestUri, requestMessage.Method.ToString(), downloadHandler, uploadHandler);

            foreach (var (key, values) in requestMessage.Headers)
            {
                webRequest.SetRequestHeader(key, StringUtils.Join(',', values));
            }

            await webRequest.SendWebRequest().WithCancellation(cancellationToken);

            var response = new HttpResponseMessage((HttpStatusCode)webRequest.responseCode)
            {
                RequestMessage = requestMessage,
                // HACK:本当はdownloadHandler.nativeDataを活用できるとよいのだが、今のところ思いつかない
                Content = new ByteArrayContent(downloadHandler.data),
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
}