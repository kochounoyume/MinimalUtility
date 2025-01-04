#nullable enable

using System;
using System.Runtime.CompilerServices;

namespace MinimalUtility
{
    /// <summary>
    /// ValueTupleの拡張メソッド.
    /// </summary>
    public static class ValueTupleExtensions
    {
        /// <summary>
        /// <see cref="ValueTuple{T1, T2}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T> GetEnumerator<T>(in this (T, T) tuple)
        {
            return new ((tuple.Item1, tuple.Item2, default, default, default, default, default)!, 2);
        }

        /// <summary>
        /// <see cref="ValueTuple{T1, T2, T3}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2, T3}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T> GetEnumerator<T>(in this (T, T, T) tuple)
        {
            return new ((tuple.Item1, tuple.Item2, tuple.Item3, default, default, default, default)!, 3);
        }

        /// <summary>
        /// <see cref="ValueTuple{T1, T2, T3, T4}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2, T3, T4}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T> GetEnumerator<T>(in this (T, T, T, T) tuple)
        {
            return new ((tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, default, default, default)!, 4);
        }

        /// <summary>
        /// <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T> GetEnumerator<T>(in this (T, T, T, T, T) tuple)
        {
            return new ((tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, default, default)!, 5);
        }

        /// <summary>
        /// <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T> GetEnumerator<T>(in this (T, T, T, T, T, T) tuple)
        {
            return new ((tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, default)!, 6);
        }

        /// <summary>
        /// <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T> GetEnumerator<T>(in this (T, T, T, T, T, T, T) tuple)
        {
            return new (tuple, 7);
        }

        /// <summary>
        /// ValueTupleのforeach対応.
        /// </summary>
        /// <typeparam name="T">要素の型.</typeparam>
        public struct Enumerator<T>
        {
            private readonly (T, T, T, T, T, T, T) _tuple;
            private readonly int _length;
            private int _index;

            /// <summary>
            /// <see cref="System.Collections.Generic.IEnumerator{T}.Current"/>に同じ.
            /// </summary>
            public T Current => _index switch
            {
                0 => _tuple.Item1,
                1 => _tuple.Item2,
                2 => _tuple.Item3,
                3 => _tuple.Item4,
                4 => _tuple.Item5,
                5 => _tuple.Item6,
                6 => _tuple.Item7,
                _ => throw new IndexOutOfRangeException(),
            };

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator{T}"/> struct.
            /// </summary>
            /// <param name="tuple"></param>
            /// <param name="length"></param>
            internal Enumerator(in (T, T, T, T, T, T, T) tuple, int length)
            {
                this._tuple = tuple;
                this._length = length;
                this._index = -1;
            }

            /// <summary>
            /// <see cref="System.Collections.Generic.IEnumerator{T}.MoveNext"/>に同じ.
            /// </summary>
            /// <returns>列挙が可能な場合はtrue.</returns>
            public bool MoveNext() => _index < _length && ++_index < _length;
        }
    }
}
