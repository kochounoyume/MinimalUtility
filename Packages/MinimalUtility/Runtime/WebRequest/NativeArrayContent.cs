﻿#if ENABLE_WEBREQUEST && ENABLE_UNITASK
#nullable enable

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace MinimalUtility.WebRequest
{
    /// <summary>
    /// <see cref="NativeArray{T}"/>を基盤とする<see cref="HttpContent"/>.
    /// </summary>
    public sealed class NativeArrayContent : HttpContent
    {
        private readonly NativeArray<byte>.ReadOnly _data;
        private readonly IDisposable _handler;

        public NativeArrayContent(NativeArray<byte>.ReadOnly data, IDisposable handler)
        {
            _data = data;
            _handler = handler;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext _)
        {
            await using (UniTask.ReturnToMainThread())
            {
                stream.Write(_data.AsReadOnlySpan());
            }
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _data.Length;
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _handler.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
#endif