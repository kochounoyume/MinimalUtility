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
        /// <param name="disposable">任意の<see cref="IDisposable"/>実装クラスの参照.</param>
        /// <param name="disposableContainer">追加先の<see cref="IDisposable"/>コンテナ.</param>
        /// <typeparam name="T">追加した<see cref="IDisposable"/>.</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTo<T>(this T disposable, ref IDisposable disposableContainer) where T : class, IDisposable
        {
            disposableContainer = disposable;
        }
    }
}