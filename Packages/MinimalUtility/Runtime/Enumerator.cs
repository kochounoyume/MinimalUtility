using System;
using System.Collections;
using System.Collections.Generic;

namespace MinimalUtility
{
    /// <summary>
    /// 汎用<see cref="IEnumerator{T}"/>構造体.
    /// </summary>
    /// <typeparam name="T">要素の型.</typeparam>
    public struct Enumerator<T> : IEnumerator<T>
    {
        /// <summary>
        /// <see cref="IEnumerator{T}.MoveNext"/>登録用デリゲート.
        /// </summary>
        /// <param name="current">参照渡し引数.</param>
        /// <returns>戻り値(bool).</returns>
        public delegate bool RefFunc(ref T current);

        private readonly RefFunc moveNext;
        private T current;

        /// <inheritdoc/>
        public T Current => current;

        /// <inheritdoc/>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enumerator{T}"/> struct.
        /// </summary>
        /// <param name="moveNext"><see cref="IEnumerator{T}.MoveNext"/>.</param>
        /// <param name="current"><see cref="IEnumerator{T}.Current"/>.</param>
        public Enumerator(RefFunc moveNext, in T current = default)
        {
            this.moveNext = moveNext;
            this.current = current;
        }

        /// <inheritdoc/>
        public bool MoveNext() => moveNext(ref current);

        /// <inheritdoc/>
        void IEnumerator.Reset() => throw new NotSupportedException();

        /// <inheritdoc/>
        void IDisposable.Dispose()
        {
        }
    }
}