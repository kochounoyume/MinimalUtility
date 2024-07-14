using System;

namespace MinimalUtility
{
    /// <summary>
    /// usingステートメントスコープを抜けた際にコールバックを呼び出すための構造体.
    /// </summary>
    public readonly ref struct SloppyDisposable
    {
        private readonly Action onDispose;

        /// <summary>
        /// Initializes a new instance of the <see cref="SloppyDisposable"/> struct.
        /// </summary>
        /// <param name="onDispose">破棄時に呼び出す処理.</param>
        public SloppyDisposable(Action onDispose)
        {
            this.onDispose = onDispose;
        }

        /// <summary>
        /// 破棄処理.
        /// </summary>
        public void Dispose()
        {
            onDispose?.Invoke();
        }
    }

    /// <summary>
    /// usingステートメントスコープを抜けた際にコールバックを呼び出すための構造体.
    /// </summary>
    /// <typeparam name="T0">破棄時に呼び出す処理の引数の型.</typeparam>
    public readonly ref struct SloppyDisposable<T0>
    {
        private readonly T0 state0;
        private readonly Action<T0> onDispose;

        /// <summary>
        /// Initializes a new instance of the <see cref="SloppyDisposable{T}"/> struct.
        /// </summary>
        /// <param name="state0">破棄時に呼び出す処理の引数.</param>
        /// <param name="onDispose">破棄時に呼び出す処理.</param>
        public SloppyDisposable(T0 state0, Action<T0> onDispose)
        {
            this.state0 = state0;
            this.onDispose = onDispose;
        }

        /// <summary>
        /// 破棄処理.
        /// </summary>
        public void Dispose()
        {
            onDispose?.Invoke(state0);
        }
    }

    /// <summary>
    /// usingステートメントスコープを抜けた際にコールバックを呼び出すための構造体.
    /// </summary>
    /// <typeparam name="T0">破棄時に呼び出す処理の第一引数の型.</typeparam>
    /// <typeparam name="T1">破棄時に呼び出す処理の第二引数の型.</typeparam>
    public readonly ref struct SloppyDisposable<T0, T1>
    {
        private readonly T0 state0;
        private readonly T1 state1;
        private readonly Action<T0, T1> onDispose;

        /// <summary>
        /// Initializes a new instance of the <see cref="SloppyDisposable{T0, T1}"/> struct.
        /// </summary>
        /// <param name="state0">破棄時に呼び出す処理の第一引数.</param>
        /// <param name="state1">破棄時に呼び出す処理の第二引数.</param>
        /// <param name="onDispose">破棄時に呼び出す処理.</param>
        public SloppyDisposable(T0 state0, T1 state1, Action<T0, T1> onDispose)
        {
            this.state0 = state0;
            this.state1 = state1;
            this.onDispose = onDispose;
        }

        /// <summary>
        /// 破棄処理.
        /// </summary>
        public void Dispose()
        {
            onDispose?.Invoke(state0, state1);
        }
    }

    /// <summary>
    /// usingステートメントスコープを抜けた際にコールバックを呼び出すための構造体.
    /// </summary>
    /// <typeparam name="T0">破棄時に呼び出す処理の第一引数の型.</typeparam>
    /// <typeparam name="T1">破棄時に呼び出す処理の第二引数の型.</typeparam>
    /// <typeparam name="T2">破棄時に呼び出す処理の第三引数の型.</typeparam>
    public readonly ref struct SloppyDisposable<T0, T1, T2>
    {
        private readonly T0 state0;
        private readonly T1 state1;
        private readonly T2 state2;
        private readonly Action<T0, T1, T2> onDispose;

        /// <summary>
        /// Initializes a new instance of the <see cref="SloppyDisposable{T0, T1, T2}"/> struct.
        /// </summary>
        /// <param name="state0">破棄時に呼び出す処理の第一引数.</param>
        /// <param name="state1">破棄時に呼び出す処理の第二引数.</param>
        /// <param name="state2">破棄時に呼び出す処理の第三引数.</param>
        /// <param name="onDispose">破棄時に呼び出す処理.</param>
        public SloppyDisposable(T0 state0, T1 state1, T2 state2, Action<T0, T1, T2> onDispose)
        {
            this.state0 = state0;
            this.state1 = state1;
            this.state2 = state2;
            this.onDispose = onDispose;
        }

        /// <summary>
        /// 破棄処理.
        /// </summary>
        public void Dispose()
        {
            onDispose?.Invoke(state0, state1, state2);
        }
    }
}