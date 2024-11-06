using System;
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
        /// <typeparam name="T">破棄時に呼び出す処理の引数の型.</typeparam>
        /// <returns>usingステートメントスコープを抜けた際にコールバックを呼び出す構造体.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Disposable<T> Create<T>(T state1, Action<T> onDispose) => new (state1, onDispose);

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
        /// <typeparam name="T">破棄時に呼び出す処理の引数の型.</typeparam>
        public readonly struct Disposable<T> : IDisposable
        {
            private readonly T state1;
            private readonly Action<T> onDispose;

            /// <summary>
            /// Initializes a new instance of the <see cref="Disposable{T1}"/> struct.
            /// </summary>
            /// <param name="state1">破棄時に呼び出す処理の引数.</param>
            /// <param name="onDispose">破棄時に呼び出す処理.</param>
            internal Disposable(T state1, Action<T> onDispose)
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
    }
}