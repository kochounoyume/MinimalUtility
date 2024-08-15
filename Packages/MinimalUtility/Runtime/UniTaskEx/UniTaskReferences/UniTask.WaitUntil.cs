using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
        public static UniTask WaitUntil<T>(T state, Func<T, bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
        {
            return new UniTask(WaitUntilPromise<T>.Create(state, predicate, timing, cancellationToken, cancelImmediately, out var token), token);
        }

        public static UniTask WaitWhile<T>(T state, Func<T, bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
        {
            return new UniTask(WaitWhilePromise<T>.Create(state, predicate, timing, cancellationToken, cancelImmediately, out var token), token);
        }

        sealed class WaitUntilPromise<T> : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<WaitUntilPromise<T>>
        {
            static TaskPool<WaitUntilPromise<T>> pool;
            WaitUntilPromise<T> nextNode;
            public ref WaitUntilPromise<T> NextNode => ref nextNode;

            static WaitUntilPromise()
            {
                TaskPool.RegisterSizeGetter(typeof(WaitUntilPromise<T>), () => pool.Size);
            }

            Func<T, bool> predicate;
            T argument;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;
            bool cancelImmediately;

            UniTaskCompletionSourceCore<object> core;

            WaitUntilPromise()
            {
            }

            public static IUniTaskSource Create(T argument, Func<T, bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new WaitUntilPromise<T>();
                }

                result.predicate = predicate;
                result.argument = argument;
                result.cancellationToken = cancellationToken;
                result.cancelImmediately = cancelImmediately;

                if (cancelImmediately && cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (WaitUntilPromise<T>)state;
                        promise.core.TrySetCanceled(promise.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    if (!(cancelImmediately && cancellationToken.IsCancellationRequested))
                    {
                        TryReturn();
                    }
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                try
                {
                    if (!predicate(argument))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    core.TrySetException(ex);
                    return false;
                }

                core.TrySetResult(null);
                return false;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                predicate = default;
                argument = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                cancelImmediately = default;
                return pool.TryPush(this);
            }
        }

        sealed class WaitWhilePromise<T> : IUniTaskSource, IPlayerLoopItem, ITaskPoolNode<WaitWhilePromise<T>>
        {
            static TaskPool<WaitWhilePromise<T>> pool;
            WaitWhilePromise<T> nextNode;
            public ref WaitWhilePromise<T> NextNode => ref nextNode;

            static WaitWhilePromise()
            {
                TaskPool.RegisterSizeGetter(typeof(WaitWhilePromise<T>), () => pool.Size);
            }

            Func<T, bool> predicate;
            T argument;
            CancellationToken cancellationToken;
            CancellationTokenRegistration cancellationTokenRegistration;
            bool cancelImmediately;

            UniTaskCompletionSourceCore<object> core;

            WaitWhilePromise()
            {
            }

            public static IUniTaskSource Create(T argument, Func<T, bool> predicate, PlayerLoopTiming timing, CancellationToken cancellationToken, bool cancelImmediately, out short token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new WaitWhilePromise<T>();
                }

                result.predicate = predicate;
                result.argument = argument;
                result.cancellationToken = cancellationToken;

                if (cancelImmediately && cancellationToken.CanBeCanceled)
                {
                    result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(state =>
                    {
                        var promise = (WaitWhilePromise<T>)state;
                        promise.core.TrySetCanceled(promise.cancellationToken);
                    }, result);
                }

                TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    if (!(cancelImmediately && cancellationToken.IsCancellationRequested))
                    {
                        TryReturn();
                    }
                }
            }

            public UniTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return core.UnsafeGetStatus();
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public bool MoveNext()
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    core.TrySetCanceled(cancellationToken);
                    return false;
                }

                try
                {
                    if (predicate(argument))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    core.TrySetException(ex);
                    return false;
                }

                core.TrySetResult(null);
                return false;
            }

            bool TryReturn()
            {
                TaskTracker.RemoveTracking(this);
                core.Reset();
                predicate = default;
                argument = default;
                cancellationToken = default;
                cancellationTokenRegistration.Dispose();
                cancelImmediately = default;
                return pool.TryPush(this);
            }
        }
    }
}