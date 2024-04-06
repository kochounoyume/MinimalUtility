using System;
using System.Runtime.CompilerServices;

namespace MinimalUtility
{
    /// <summary>
    /// <see cref="IDisposable"/>の拡張メソッド.
    /// </summary>
    public static class DisposableExtensions
    {
        /// <summary>
        /// <see cref="IDisposable"/>を指定した<see cref="IDisposable"/>コンテナに追加します.
        /// </summary>
        /// <param name="disposable">任意の<see cref="IDisposable"/>.</param>
        /// <param name="disposableContainer">追加先の<see cref="IDisposable"/>コンテナ.</param>
        /// <returns>追加した<see cref="IDisposable"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref IDisposable AddTo(this IDisposable disposable, ref IDisposable disposableContainer)
        {
            disposableContainer = disposable;
            return ref disposableContainer;
        }
    }
}