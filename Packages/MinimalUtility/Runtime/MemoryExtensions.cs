using System;
using System.Collections.Generic;
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
        public static Enumerator<T> GetEnumerator<T>(in this Memory<T> memory) => new (memory);

        /// <summary>
        /// <see cref="ReadOnlyMemory{T}"/>のforeach対応.
        /// </summary>
        /// <param name="memory">対象の<see cref="ReadOnlyMemory{T}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="ReadOnlySpan{T}.Enumerator"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T> GetEnumerator<T>(in this ReadOnlyMemory<T> memory) => new (memory);

        /// <summary>
        /// <see cref="ReadOnlyMemory{T}"/>のforeach対応.
        /// </summary>
        /// <typeparam name="T">要素の型.</typeparam>
        public struct Enumerator<T> : IEnumerator<T>
        {
            private readonly ReadOnlyMemory<T> memory;
            private int index;

            /// <inheritdoc />
            public T Current => memory.Span[index];

            /// <inheritdoc />
            object System.Collections.IEnumerator.Current => Current;

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator{T}"/> struct.
            /// </summary>
            /// <param name="memory"><see cref="ReadOnlyMemory{T}"/>.</param>
            internal Enumerator(in ReadOnlyMemory<T> memory)
            {
                this.memory = memory;
                index = -1;
            }

            /// <inheritdoc />
            public bool MoveNext() => index < memory.Length && ++index < memory.Length;

            /// <inheritdoc />
            public void Reset() => index = -1;

            /// <inheritdoc />
            void IDisposable.Dispose()
            {
            }
        }
    }
}