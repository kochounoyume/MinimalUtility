using System;
using R3;
using UnityEngine;

namespace MinimalUtility.R3
{
    /// <summary>
    /// Observableの拡張メソッド.
    /// </summary>
    public static partial class ObservableExtensions
    {
        /// <summary>
        /// 指定した時間で指定した値から指定した値まで補間するObservableを生成する.
        /// </summary>
        /// <param name="source">元のObservable.</param>
        /// <param name="start">開始値.</param>
        /// <param name="end">終了値.</param>
        /// <param name="duration">補間時間.</param>
        /// <param name="timeProvider">時間プロバイダ.</param>
        /// <typeparam name="T">元のObservableの型.</typeparam>
        /// <returns>補間するObservable.</returns>
        public static Observable<float> Tween<T>(this Observable<T> source, float start, float end, float duration, TimeProvider timeProvider = default)
        {
            return new FloatTweenObservable<T>(source, start, end, duration, timeProvider ?? ObservableSystem.DefaultTimeProvider);
        }

        /// <summary>
        /// 指定した時間で指定した値から指定した値まで補間するObservableを生成する.
        /// </summary>
        /// <param name="source">元のObservable.</param>
        /// <param name="start">開始値.</param>
        /// <param name="end">終了値.</param>
        /// <param name="duration">補間時間.</param>
        /// <param name="timeProvider">時間プロバイダ.</param>
        /// <typeparam name="T">元のObservableの型.</typeparam>
        /// <returns>補間するObservable.</returns>
        public static Observable<Vector3> Tween<T>(this Observable<T> source, Vector3 start, Vector3 end, float duration, TimeProvider timeProvider = default)
        {
            return new Vector3TweenObservable<T>(source, start, end, duration, timeProvider ?? ObservableSystem.DefaultTimeProvider);
        }

        /// <summary>
        /// 指定した時間で指定した値から指定した値まで補間するObservableを生成する.
        /// </summary>
        /// <param name="source">元のObservable.</param>
        /// <param name="start">開始値.</param>
        /// <param name="end">終了値.</param>
        /// <param name="duration">補間時間.</param>
        /// <param name="timeProvider">時間プロバイダ.</param>
        /// <typeparam name="T">元のObservableの型.</typeparam>
        /// <returns>補間するObservable.</returns>
        public static Observable<Vector2> Tween<T>(this Observable<T> source, Vector2 start, Vector2 end, float duration, TimeProvider timeProvider = default)
        {
            return new Vector2TweenObservable<T>(source, start, end, duration, timeProvider ?? ObservableSystem.DefaultTimeProvider);
        }

        /// <summary>
        /// 指定した時間で指定した値から指定した値まで補間するObservableを生成する.
        /// </summary>
        /// <param name="source">元のObservable.</param>
        /// <param name="start">開始値.</param>
        /// <param name="end">終了値.</param>
        /// <param name="duration">補間時間.</param>
        /// <param name="timeProvider">時間プロバイダ.</param>
        /// <typeparam name="T">元のObservableの型.</typeparam>
        /// <returns>補間するObservable.</returns>
        public static Observable<Color> Tween<T>(this Observable<T> source, Color start, Color end, float duration, TimeProvider timeProvider = default)
        {
            return new ColorTweenObservable<T>(source, start, end, duration, timeProvider ?? ObservableSystem.DefaultTimeProvider);
        }

        private sealed class FloatTweenObservable<T> : Observable<float>
        {
            private readonly Observable<T> source;
            private readonly float start;
            private readonly float end;
            private readonly float duration;
            private readonly TimeProvider timeProvider;

            public FloatTweenObservable(Observable<T> source, float start, float end, float duration, TimeProvider timeProvider)
            {
                this.source = source;
                this.start = start;
                this.end = end;
                this.duration = duration;
                this.timeProvider = timeProvider;
            }

            protected override IDisposable SubscribeCore(Observer<float> observer)
            {
                return source.Subscribe(new FloatTweenObserver(start, end, duration, timeProvider, observer));
            }

            private sealed class FloatTweenObserver : Observer<T>
            {
                private readonly float start;
                private readonly float end;
                private readonly float duration;
                private readonly TimeProvider timeProvider;
                private readonly Observer<float> observer;
                private readonly long initialTimestamp;

                public FloatTweenObserver(float start, float end, float duration, TimeProvider timeProvider, Observer<float> observer)
                {
                    this.start = start;
                    this.end = end;
                    this.duration = duration;
                    this.timeProvider = timeProvider;
                    this.observer = observer;
                    this.initialTimestamp = timeProvider.GetTimestamp();
                }

#pragma warning disable SA1313
                protected override void OnNextCore(T _)
#pragma warning restore SA1313
                {
                    var currentTimestamp = timeProvider.GetTimestamp();
                    var elapsed = timeProvider.GetElapsedTime(initialTimestamp, currentTimestamp);

                    if (elapsed.TotalSeconds >= duration)
                    {
                        observer.OnNext(end);
                        observer.OnCompleted();
                    }
                    else
                    {
                        var t = (float)elapsed.TotalSeconds / duration;
                        observer.OnNext(start + ((end - start) * t));
                    }
                }

                protected override void OnErrorResumeCore(Exception error)
                {
                    observer.OnErrorResume(error);
                }

                protected override void OnCompletedCore(Result result)
                {
                    observer.OnCompleted(result);
                }
            }
        }

        private sealed class Vector3TweenObservable<T> : Observable<Vector3>
        {
            private readonly Observable<T> source;
            private readonly Vector3 start;
            private readonly Vector3 end;
            private readonly float duration;
            private readonly TimeProvider timeProvider;

            public Vector3TweenObservable(Observable<T> source, Vector3 start, Vector3 end, float duration, TimeProvider timeProvider)
            {
                this.source = source;
                this.start = start;
                this.end = end;
                this.duration = duration;
                this.timeProvider = timeProvider;
            }

            protected override IDisposable SubscribeCore(Observer<Vector3> observer)
            {
                return source.Subscribe(new Vector3TweenObserver(start, end, duration, timeProvider, observer));
            }

            private sealed class Vector3TweenObserver : Observer<T>
            {
                private readonly Vector3 start;
                private readonly Vector3 end;
                private readonly float duration;
                private readonly TimeProvider timeProvider;
                private readonly Observer<Vector3> observer;
                private readonly long initialTimestamp;

                public Vector3TweenObserver(Vector3 start, Vector3 end, float duration, TimeProvider timeProvider, Observer<Vector3> observer)
                {
                    this.start = start;
                    this.end = end;
                    this.duration = duration;
                    this.timeProvider = timeProvider;
                    this.observer = observer;
                    this.initialTimestamp = timeProvider.GetTimestamp();
                }

#pragma warning disable SA1313
                protected override void OnNextCore(T _)
#pragma warning restore SA1313
                {
                    var currentTimestamp = timeProvider.GetTimestamp();
                    var elapsed = timeProvider.GetElapsedTime(initialTimestamp, currentTimestamp);

                    if (elapsed.TotalSeconds >= duration)
                    {
                        observer.OnNext(end);
                        observer.OnCompleted();
                    }
                    else
                    {
                        var t = (float)elapsed.TotalSeconds / duration;
                        observer.OnNext(start + ((end - start) * t));
                    }
                }

                protected override void OnErrorResumeCore(Exception error)
                {
                    observer.OnErrorResume(error);
                }

                protected override void OnCompletedCore(Result result)
                {
                    observer.OnCompleted(result);
                }
            }
        }

        private sealed class Vector2TweenObservable<T> : Observable<Vector2>
        {
            private readonly Observable<T> source;
            private readonly Vector2 start;
            private readonly Vector2 end;
            private readonly float duration;
            private readonly TimeProvider timeProvider;

            public Vector2TweenObservable(Observable<T> source, Vector2 start, Vector2 end, float duration, TimeProvider timeProvider)
            {
                this.source = source;
                this.start = start;
                this.end = end;
                this.duration = duration;
                this.timeProvider = timeProvider;
            }

            protected override IDisposable SubscribeCore(Observer<Vector2> observer)
            {
                return source.Subscribe(new Vector2TweenObserver(start, end, duration, timeProvider, observer));
            }

            private sealed class Vector2TweenObserver : Observer<T>
            {
                private readonly Vector2 start;
                private readonly Vector2 end;
                private readonly float duration;
                private readonly TimeProvider timeProvider;
                private readonly Observer<Vector2> observer;
                private readonly long initialTimestamp;

                public Vector2TweenObserver(Vector2 start, Vector2 end, float duration, TimeProvider timeProvider, Observer<Vector2> observer)
                {
                    this.start = start;
                    this.end = end;
                    this.duration = duration;
                    this.timeProvider = timeProvider;
                    this.observer = observer;
                    this.initialTimestamp = timeProvider.GetTimestamp();
                }

#pragma warning disable SA1313
                protected override void OnNextCore(T _)
#pragma warning restore SA1313
                {
                    var currentTimestamp = timeProvider.GetTimestamp();
                    var elapsed = timeProvider.GetElapsedTime(initialTimestamp, currentTimestamp);

                    if (elapsed.TotalSeconds >= duration)
                    {
                        observer.OnNext(end);
                        observer.OnCompleted();
                    }
                    else
                    {
                        var t = (float)elapsed.TotalSeconds / duration;
                        observer.OnNext(start + ((end - start) * t));
                    }
                }

                protected override void OnErrorResumeCore(Exception error)
                {
                    observer.OnErrorResume(error);
                }

                protected override void OnCompletedCore(Result result)
                {
                    observer.OnCompleted(result);
                }
            }
        }

        private sealed class ColorTweenObservable<T> : Observable<Color>
        {
            private readonly Observable<T> source;
            private readonly Color start;
            private readonly Color end;
            private readonly float duration;
            private readonly TimeProvider timeProvider;

            public ColorTweenObservable(Observable<T> source, Color start, Color end, float duration, TimeProvider timeProvider)
            {
                this.source = source;
                this.start = start;
                this.end = end;
                this.duration = duration;
                this.timeProvider = timeProvider;
            }

            protected override IDisposable SubscribeCore(Observer<Color> observer)
            {
                return source.Subscribe(new ColorTweenObserver(start, end, duration, timeProvider, observer));
            }

            private sealed class ColorTweenObserver : Observer<T>
            {
                private readonly Color start;
                private readonly Color end;
                private readonly float duration;
                private readonly TimeProvider timeProvider;
                private readonly Observer<Color> observer;
                private readonly long initialTimestamp;

                public ColorTweenObserver(Color start, Color end, float duration, TimeProvider timeProvider, Observer<Color> observer)
                {
                    this.start = start;
                    this.end = end;
                    this.duration = duration;
                    this.timeProvider = timeProvider;
                    this.observer = observer;
                    this.initialTimestamp = timeProvider.GetTimestamp();
                }

#pragma warning disable SA1313
                protected override void OnNextCore(T _)
#pragma warning restore SA1313
                {
                    var currentTimestamp = timeProvider.GetTimestamp();
                    var elapsed = timeProvider.GetElapsedTime(initialTimestamp, currentTimestamp);

                    if (elapsed.TotalSeconds >= duration)
                    {
                        observer.OnNext(end);
                        observer.OnCompleted();
                    }
                    else
                    {
                        var t = (float)elapsed.TotalSeconds / duration;
                        observer.OnNext(start + ((end - start) * t));
                    }
                }

                protected override void OnErrorResumeCore(Exception error)
                {
                    observer.OnErrorResume(error);
                }

                protected override void OnCompletedCore(Result result)
                {
                    observer.OnCompleted(result);
                }
            }
        }
    }
}