using System;
using System.Runtime.CompilerServices;

namespace MinimalUtility
{
    public static class DisposableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref IDisposable AddTo(this IDisposable disposable, ref IDisposable disposableContainer)
        {
            disposableContainer = disposable;
            return ref disposableContainer;
        }
    }
}