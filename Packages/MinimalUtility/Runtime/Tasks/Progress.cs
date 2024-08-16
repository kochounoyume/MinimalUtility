using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MinimalUtility.Tasks
{
    /// <summary>
    /// 簡易<see cref="IProgress{T}"/>ファクトリ.
    /// </summary>
    public static class Progress
    {
        /// <summary>
        /// 進捗状況の値を通知するProgressを生成します.
        /// <remarks>
        /// <see cref="Cysharp.Threading.Tasks.Progress.Create{T}(Action{T})"/>の引数を設定できる版.
        /// </remarks>
        /// </summary>
        /// <param name="state">引数として渡したい値.</param>
        /// <param name="handler">進捗状況が変更された時に呼び出されるコールバック.</param>
        /// <typeparam name="T">進捗状況の値の型.</typeparam>
        /// <typeparam name="TState">渡したい引数の型.</typeparam>
        /// <returns>進捗状況の値を通知するProgress.</returns>
        public static IProgress<T> Create<T, TState>(TState state, Action<T, TState> handler)
        {
            return handler == null ? EmptyProgress<T>.Default : new AnonymousProgress<T, TState>(handler, state);
        }

        /// <summary>
        /// 進捗状況の値が変更された時だけ通知するProgressを生成します.
        /// <remarks>
        /// <see cref="Cysharp.Threading.Tasks.Progress.CreateOnlyValueChanged{T}(Action{T}, IEqualityComparer{T})"/>の引数を設定できる版.
        /// </remarks>
        /// </summary>
        /// <param name="state">引数として渡したい値.</param>
        /// <param name="handler">進捗状況が変更された時に呼び出されるコールバック.</param>
        /// <param name="comparer">進捗状況の値を比較するための<see cref="EqualityComparer{T}"/>.</param>
        /// <typeparam name="T">進捗状況の値の型.</typeparam>
        /// <typeparam name="TState">渡したい引数の型.</typeparam>
        /// <returns>進捗状況の値が変更された時だけコールバックを呼び出すProgress.</returns>
        public static IProgress<T> CreateOnlyValueChanged<T, TState>(TState state, Action<T, TState> handler, IEqualityComparer<T> comparer = null)
        {
            if (handler == null) return EmptyProgress<T>.Default;
#if UNITY_2018_3_OR_NEWER
            return new OnlyValueChangedProgress<T, TState>(handler, state, comparer ?? UniTaskInternalAccessor.GetEqualityComparer<T>());
#else
            return new OnlyValueChangedProgress<T, TState>(handler, state, comparer ?? EqualityComparer<T>.Default);
#endif
        }

        private sealed class EmptyProgress<T> : IProgress<T>
        {
            public static readonly IProgress<T> Default = new EmptyProgress<T>();

            /// <summary>
            /// Initializes a new instance of the <see cref="EmptyProgress{T}"/> class.
            /// </summary>
            private EmptyProgress()
            {
            }

            void IProgress<T>.Report(T value)
            {
            }
        }

        private sealed class AnonymousProgress<T, TState> : IProgress<T>
        {
            private readonly Action<T, TState> action;
            private readonly TState state;

            public AnonymousProgress(Action<T, TState> action, TState state)
            {
                this.action = action;
                this.state = state;
            }

            void IProgress<T>.Report(T value)
            {
                action(value, state);
            }
        }

        private sealed class OnlyValueChangedProgress<T, TState> : IProgress<T>
        {
            private readonly Action<T, TState> action;
            private readonly TState state;
            private readonly IEqualityComparer<T> comparer;
            private bool isFirstCall;
            private T latestValue;

            public OnlyValueChangedProgress(Action<T, TState> action, TState state, IEqualityComparer<T> comparer)
            {
                this.action = action;
                this.state = state;
                this.comparer = comparer;
                this.isFirstCall = true;
            }

            void IProgress<T>.Report(T value)
            {
                if (isFirstCall)
                {
                    isFirstCall = false;
                }
                else if (comparer.Equals(value, latestValue))
                {
                    return;
                }

                latestValue = value;
                action(value, state);
            }
        }
    }
}