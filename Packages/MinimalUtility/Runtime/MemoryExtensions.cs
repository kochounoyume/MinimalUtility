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
        /// <returns>要素を列挙する<see cref="MemoryExtensions.Enumerator{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T> GetEnumerator<T>(in this Memory<T> memory) => new (memory);

        /// <summary>
        /// <see cref="ReadOnlyMemory{T}"/>のforeach対応.
        /// </summary>
        /// <param name="memory">対象の<see cref="ReadOnlyMemory{T}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="MemoryExtensions.Enumerator{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T> GetEnumerator<T>(in this ReadOnlyMemory<T> memory) => new (memory);

        /// <summary>
        /// <see cref="ReadOnlyMemory{T}"/>のforeach対応.
        /// </summary>
        /// <typeparam name="T">要素の型.</typeparam>
        public struct Enumerator<T>
        {
            private readonly ReadOnlyMemory<T> memory;
            private int index;

            /// <summary>
            /// <see cref="System.Collections.Generic.IEnumerator{T}.Current"/>に同じ.
            /// </summary>
            public T Current => memory.Span[index];

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator{T}"/> struct.
            /// </summary>
            /// <param name="memory"><see cref="ReadOnlyMemory{T}"/>.</param>
            internal Enumerator(in ReadOnlyMemory<T> memory)
            {
                this.memory = memory;
                index = -1;
            }

            /// <summary>
            /// <see cref="System.Collections.Generic.IEnumerator{T}.MoveNext"/>に同じ.
            /// </summary>
            /// <returns>列挙が可能な場合はtrue.</returns>
            public bool MoveNext() => index < memory.Length && ++index < memory.Length;
        }
    }
}