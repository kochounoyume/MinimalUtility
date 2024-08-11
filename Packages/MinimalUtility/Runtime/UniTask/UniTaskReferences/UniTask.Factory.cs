using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
    public partial struct UniTask
    {
        /// <summary>
        /// Defer the task creation just before call await.
        /// </summary>
        public static UniTask Defer<T>(T state, Func<T, UniTask> factory)
        {
            return new UniTask(new DeferPromiseWithState<T>(state, factory), 0);
        }

        /// <summary>
        /// Defer the task creation just before call await.
        /// </summary>
        public static UniTask<TResult> Defer<TState, TResult>(TState state, Func<TState, UniTask<TResult>> factory)
        {
            return new UniTask<TResult>(new DeferPromiseWithState<TState, TResult>(state, factory), 0);
        }

        sealed class DeferPromiseWithState<T> : IUniTaskSource
        {
            Func<T, UniTask> factory;
            T argument;
            UniTask task;
            UniTask.Awaiter awaiter;

            public DeferPromiseWithState(T argument, Func<T, UniTask> factory)
            {
                this.argument = argument;
                this.factory = factory;
            }

            public void GetResult(short token)
            {
                awaiter.GetResult();
            }

            public UniTaskStatus GetStatus(short token)
            {
                var f = Interlocked.Exchange(ref factory, null);
                if (f != null)
                {
                    task = f(argument);
                    awaiter = task.GetAwaiter();
                }

                return task.Status;
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                awaiter.SourceOnCompleted(continuation, state);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return task.Status;
            }
        }

        sealed class DeferPromiseWithState<TState, TResult> : IUniTaskSource<TResult>
        {
            Func<TState, UniTask<TResult>> factory;
            TState argument;
            UniTask<TResult> task;
            UniTask<TResult>.Awaiter awaiter;

            public DeferPromiseWithState(TState argument, Func<TState, UniTask<TResult>> factory)
            {
                this.argument = argument;
                this.factory = factory;
            }

            public TResult GetResult(short token)
            {
                return awaiter.GetResult();
            }

            void IUniTaskSource.GetResult(short token)
            {
                awaiter.GetResult();
            }

            public UniTaskStatus GetStatus(short token)
            {
                var f = Interlocked.Exchange(ref factory, null);
                if (f != null)
                {
                    task = f(argument);
                    awaiter = task.GetAwaiter();
                }

                return task.Status;
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                awaiter.SourceOnCompleted(continuation, state);
            }

            public UniTaskStatus UnsafeGetStatus()
            {
                return task.Status;
            }
        }
    }
}