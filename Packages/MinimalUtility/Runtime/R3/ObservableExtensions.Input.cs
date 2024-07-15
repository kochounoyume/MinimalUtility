#if ENABLE_R3
using System;
using System.Threading;
using R3;
#if ENABLE_UNITASK
using Task = Cysharp.Threading.Tasks.UniTask;
#else
using Task = System.Threading.Tasks.ValueTask;
#endif

namespace MinimalUtility
{
    /// <content>
    /// Observableの拡張メソッド.
    /// </content>
    public static partial class ObservableExtensions
    {
        /// <summary>
        /// 連打禁止クール時間定数.
        /// </summary>
        private static readonly TimeSpan ThrottleSecs = TimeSpan.FromSeconds(0.3);

        /// <summary>
        /// 連打禁止・同時押し禁止のための排他的なSubscribeAwaitを提供します.
        /// </summary>
        /// <param name="source">任意のObservable.</param>
        /// <param name="gate">排他制御用の<see cref="ReactiveProperty{T}"/>.</param>
        /// <param name="onNextAsync">非同期処理.</param>
        /// <typeparam name="T">Observableの型.</typeparam>
        /// <typeparam name="TGate">排他制御用の<see cref="ReactiveProperty{T}"/>の型.</typeparam>
        /// <returns>排他的な購読.</returns>
        public static IDisposable SubscribeLockAwait<T, TGate>(this Observable<T> source, TGate gate, Func<T, CancellationToken, Task> onNextAsync) where TGate : ReactiveProperty<bool>
        {
            return source
                .ThrottleFirst(ThrottleSecs)
                .Select((gate, onNextAsync), static (arg, param) => (arg, param.gate, param.onNextAsync))
                .Where(static param => param.gate.CurrentValue)
                .SubscribeAwait(static async (param, ct) =>
                {
                    param.gate.Value = false;
                    await param.onNextAsync(param.arg, ct);
                    param.gate.Value = true;
                }, AwaitOperation.Drop);
        }

        /// <summary>
        /// 連打禁止・同時押し禁止のための排他的なSubscribeを提供します.
        /// </summary>
        /// <param name="source">任意のObservable.</param>
        /// <param name="gate">排他制御用の<see cref="ReactiveProperty{T}"/>.</param>
        /// <param name="onNext">非同期処理.</param>
        /// <typeparam name="T">Observableの型.</typeparam>
        /// <typeparam name="TGate">排他制御用の<see cref="ReactiveProperty{T}"/>の型.</typeparam>
        /// <returns>排他的な購読.</returns>
        public static IDisposable SubscribeLock<T, TGate>(this Observable<T> source, TGate gate, Action<T> onNext) where TGate : ReactiveProperty<bool>
        {
            return source
                .ThrottleFirst(ThrottleSecs)
                .Select((gate, onNext), static (arg, param) => (arg, param.gate, param.onNext))
                .Where(static param => param.gate.CurrentValue)
                .Subscribe(static param =>
                {
                    param.gate.Value = false;
                    param.onNext(param.arg);
                    param.gate.Value = true;
                });
        }
    }
}
#endif