﻿using System;
using System.Runtime.CompilerServices;

namespace MinimalUtility
{
    /// <summary>
    /// 値型で軽量な<see cref="IDisposable"/>のファクトリ.
    /// <remarks>
    /// usingステートメントでの仕様を想定しており、<see cref="IDisposable"/>にキャストするとアロケーションが発生する.
    /// <see cref="IDisposable"/>にキャストして使用したい場合は素直に専用クラス実装するか、Rxライブラリの<see cref="IDisposable"/>ファクトリを使用する.
    /// </remarks>
    /// </summary>
    public static class ValueDisposable
    {
        /// <summary>
        /// usingステートメントスコープを抜けた際にコールバックを呼び出す構造体のファクトリメソッド.
        /// </summary>
        /// <param name="onDispose">破棄時に呼び出す処理.</param>
        /// <returns>usingステートメントスコープを抜けた際にコールバックを呼び出す構造体.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Disposable Create(Action onDispose) => new (onDispose);

        /// <summary>
        /// usingステートメントスコープを抜けた際にコールバックを呼び出す構造体のファクトリメソッド.
        /// </summary>
        /// <param name="state1">第一引数.</param>
        /// <param name="onDispose">破棄時に呼び出す処理.</param>
        /// <typeparam name="T1">破棄時に呼び出す処理の引数の型.</typeparam>
        /// <returns>usingステートメントスコープを抜けた際にコールバックを呼び出す構造体.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Disposable<T1> Create<T1>(T1 state1, Action<T1> onDispose) => new (state1, onDispose);

        /// <summary>
        /// usingステートメントスコープを抜けた際にコールバックを呼び出す構造体のファクトリメソッド.
        /// </summary>
        /// <param name="state1">第一引数.</param>
        /// <param name="state2">第二引数.</param>
        /// <param name="onDispose">破棄時に呼び出す処理.</param>
        /// <typeparam name="T1">破棄時に呼び出す処理の第一引数の型.</typeparam>
        /// <typeparam name="T2">破棄時に呼び出す処理の第二引数の型.</typeparam>
        /// <returns>usingステートメントスコープを抜けた際にコールバックを呼び出す構造体.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Disposable<T1, T2> Create<T1, T2>(T1 state1, T2 state2, Action<T1, T2> onDispose)
            => new (state1, state2, onDispose);

        /// <summary>
        /// usingステートメントスコープを抜けた際にコールバックを呼び出す構造体のファクトリメソッド.
        /// </summary>
        /// <param name="state1">第一引数.</param>
        /// <param name="state2">第二引数.</param>
        /// <param name="state3">第三引数.</param>
        /// <param name="onDispose">破棄時に呼び出す処理.</param>
        /// <typeparam name="T1">破棄時に呼び出す処理の第一引数の型.</typeparam>
        /// <typeparam name="T2">破棄時に呼び出す処理の第二引数の型.</typeparam>
        /// <typeparam name="T3">破棄時に呼び出す処理の第三引数の型.</typeparam>
        /// <returns>usingステートメントスコープを抜けた際にコールバックを呼び出す構造体.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Disposable<T1, T2, T3> Create<T1, T2, T3>(T1 state1, T2 state2, T3 state3, Action<T1, T2, T3> onDispose)
            => new (state1, state2, state3, onDispose);

        /// <summary>
        /// usingステートメントスコープを抜けた際にコールバックを呼び出すための構造体.
        /// </summary>
        public readonly struct Disposable : IDisposable
        {
            private readonly Action onDispose;

            /// <summary>
            /// Initializes a new instance of the <see cref="Disposable"/> struct.
            /// </summary>
            /// <param name="onDispose">破棄時に呼び出す処理.</param>
            internal Disposable(Action onDispose)
            {
                this.onDispose = onDispose;
            }

            /// <summary>
            /// 破棄処理.
            /// </summary>
            void IDisposable.Dispose()
            {
                onDispose?.Invoke();
            }
        }

        /// <summary>
        /// usingステートメントスコープを抜けた際にコールバックを呼び出すための構造体.
        /// </summary>
        /// <typeparam name="T1">破棄時に呼び出す処理の引数の型.</typeparam>
        public readonly struct Disposable<T1> : IDisposable
        {
            private readonly T1 state1;
            private readonly Action<T1> onDispose;

            /// <summary>
            /// Initializes a new instance of the <see cref="Disposable{T1}"/> struct.
            /// </summary>
            /// <param name="state1">破棄時に呼び出す処理の引数.</param>
            /// <param name="onDispose">破棄時に呼び出す処理.</param>
            internal Disposable(T1 state1, Action<T1> onDispose)
            {
                this.state1 = state1;
                this.onDispose = onDispose;
            }

            /// <summary>
            /// 破棄処理.
            /// </summary>
            void IDisposable.Dispose()
            {
                onDispose?.Invoke(state1);
            }
        }

        /// <summary>
        /// usingステートメントスコープを抜けた際にコールバックを呼び出すための構造体.
        /// </summary>
        /// <typeparam name="T1">破棄時に呼び出す処理の第一引数の型.</typeparam>
        /// <typeparam name="T2">破棄時に呼び出す処理の第二引数の型.</typeparam>
        public readonly struct Disposable<T1, T2> : IDisposable
        {
            private readonly T1 state1;
            private readonly T2 state2;
            private readonly Action<T1, T2> onDispose;

            /// <summary>
            /// Initializes a new instance of the <see cref="Disposable{T1,T2}"/> struct.
            /// </summary>
            /// <param name="state1">破棄時に呼び出す処理の第一引数.</param>
            /// <param name="state2">破棄時に呼び出す処理の第二引数.</param>
            /// <param name="onDispose">破棄時に呼び出す処理.</param>
            internal Disposable(T1 state1, T2 state2, Action<T1, T2> onDispose)
            {
                this.state1 = state1;
                this.state2 = state2;
                this.onDispose = onDispose;
            }

            /// <summary>
            /// 破棄処理.
            /// </summary>
            void IDisposable.Dispose()
            {
                onDispose?.Invoke(state1, state2);
            }
        }

        /// <summary>
        /// usingステートメントスコープを抜けた際にコールバックを呼び出すための構造体.
        /// </summary>
        /// <typeparam name="T1">破棄時に呼び出す処理の第一引数の型.</typeparam>
        /// <typeparam name="T2">破棄時に呼び出す処理の第二引数の型.</typeparam>
        /// <typeparam name="T3">破棄時に呼び出す処理の第三引数の型.</typeparam>
        public readonly struct Disposable<T1, T2, T3> : IDisposable
        {
            private readonly T1 state1;
            private readonly T2 state2;
            private readonly T3 state3;
            private readonly Action<T1, T2, T3> onDispose;

            /// <summary>
            /// Initializes a new instance of the <see cref="Disposable{T1,T2,T3}"/> struct.
            /// </summary>
            /// <param name="state1">破棄時に呼び出す処理の第一引数.</param>
            /// <param name="state2">破棄時に呼び出す処理の第二引数.</param>
            /// <param name="state3">破棄時に呼び出す処理の第三引数.</param>
            /// <param name="onDispose">破棄時に呼び出す処理.</param>
            internal Disposable(T1 state1, T2 state2, T3 state3, Action<T1, T2, T3> onDispose)
            {
                this.state1 = state1;
                this.state2 = state2;
                this.state3 = state3;
                this.onDispose = onDispose;
            }

            /// <summary>
            /// 破棄処理.
            /// </summary>
            void IDisposable.Dispose()
            {
                onDispose?.Invoke(state1, state2, state3);
            }
        }
    }
}