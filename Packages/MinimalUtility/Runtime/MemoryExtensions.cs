using System;
using System.Runtime.CompilerServices;

namespace MinimalUtility
{
    /// <summary>
    /// <see cref="Memory{T}"/>, <see cref="ReadOnlyMemory{T}"/>の拡張メソッド.
    /// </summary>
    public static class MemoryExtensions
    {
        /// <summary>
        /// <see cref="Memory{T}"/>のforeach対応.
        /// </summary>
        /// <param name="memory">対象の<see cref="Memory{T}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Span{T}.Enumerator"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T>.Enumerator GetEnumerator<T>(in this Memory<T> memory) => memory.Span.GetEnumerator();

        /// <summary>
        /// <see cref="ReadOnlyMemory{T}"/>のforeach対応.
        /// </summary>
        /// <param name="memory">対象の<see cref="ReadOnlyMemory{T}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="ReadOnlySpan{T}.Enumerator"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T>.Enumerator GetEnumerator<T>(in this ReadOnlyMemory<T> memory) => memory.Span.GetEnumerator();
    }
}