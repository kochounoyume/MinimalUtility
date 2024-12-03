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
        /// <returns>要素を列挙する<see cref="Enumerator{T, TTuple}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T, (T, T)> GetEnumerator<T>(in this (T, T) tuple)
        {
            return new (tuple, static (t, i) => i switch
            {
                0 => t.Item1,
                1 => t.Item2,
                _ => throw new IndexOutOfRangeException(),
            });
        }

        /// <summary>
        /// <see cref="ValueTuple{T1, T2, T3}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2, T3}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T, TTuple}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T, (T, T, T)> GetEnumerator<T>(in this (T, T, T) tuple)
        {
            return new (tuple, static (t, i) => i switch
            {
                0 => t.Item1,
                1 => t.Item2,
                2 => t.Item3,
                _ => throw new IndexOutOfRangeException(),
            });
        }

        /// <summary>
        /// <see cref="ValueTuple{T1, T2, T3, T4}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2, T3, T4}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T, TTuple}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T, (T, T, T, T)> GetEnumerator<T>(in this (T, T, T, T) tuple)
        {
            return new (tuple, static (t, i) => i switch
            {
                0 => t.Item1,
                1 => t.Item2,
                2 => t.Item3,
                3 => t.Item4,
                _ => throw new IndexOutOfRangeException(),
            });
        }

        /// <summary>
        /// <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T, TTuple}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T, (T, T, T, T, T)> GetEnumerator<T>(in this (T, T, T, T, T) tuple)
        {
            return new (tuple, static (t, i) => i switch
            {
                0 => t.Item1,
                1 => t.Item2,
                2 => t.Item3,
                3 => t.Item4,
                4 => t.Item5,
                _ => throw new IndexOutOfRangeException(),
            });
        }

        /// <summary>
        /// <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T, TTuple}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T, (T, T, T, T, T, T)> GetEnumerator<T>(in this (T, T, T, T, T, T) tuple)
        {
            return new (tuple, static (t, i) => i switch
            {
                0 => t.Item1,
                1 => t.Item2,
                2 => t.Item3,
                3 => t.Item4,
                4 => t.Item5,
                5 => t.Item6,
                _ => throw new IndexOutOfRangeException(),
            });
        }

        /// <summary>
        /// <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/>のforeach対応.
        /// </summary>
        /// <param name="tuple">対象の<see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7}"/>.</param>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <returns>要素を列挙する<see cref="Enumerator{T, TTuple}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Enumerator<T, (T, T, T, T, T, T, T)> GetEnumerator<T>(in this (T, T, T, T, T, T, T) tuple)
        {
            return new (tuple, static (t, i) => i switch
            {
                0 => t.Item1,
                1 => t.Item2,
                2 => t.Item3,
                3 => t.Item4,
                4 => t.Item5,
                5 => t.Item6,
                6 => t.Item7,
                _ => throw new IndexOutOfRangeException(),
            });
        }

        /// <summary>
        /// ValueTupleのforeach対応.
        /// </summary>
        /// <typeparam name="T">要素の型.</typeparam>
        /// <typeparam name="TTuple">ValueTupleの型.</typeparam>
        public struct Enumerator<T, TTuple> where TTuple : struct, ITuple
        {
            private readonly TTuple tuple;
            private readonly Func<TTuple, int, T> current;
            private int index;

            /// <summary>
            /// <see cref="System.Collections.Generic.IEnumerator{T}.Current"/>に同じ.
            /// </summary>
            public T Current => current(tuple, index);

            /// <summary>
            /// Initializes a new instance of the <see cref="ValueTupleExtensions.Enumerator{T, Tuple}"/> struct.
            /// </summary>
            /// <param name="tuple">ValueTuple.</param>
            /// <param name="current"><see cref="Current"/>で実行する処理.</param>
            internal Enumerator(in TTuple tuple, Func<TTuple, int, T> current)
            {
                this.tuple = tuple;
                this.current = current;
                index = -1;
            }

            /// <summary>
            /// <see cref="System.Collections.Generic.IEnumerator{T}.MoveNext"/>に同じ.
            /// </summary>
            /// <returns>列挙が可能な場合はtrue.</returns>
            public bool MoveNext() => index < tuple.Length && ++index < tuple.Length;
        }
    }
}
