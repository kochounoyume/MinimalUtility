#if ENABLE_R3
using R3;

namespace MinimalUtility.R3
{
    public static partial class ObservableExtensions
    {
        public static Observable<T> Debug<T>(this Observable<T> source, string label = null)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            string l = label == null ? "" : $"[{label}]";
            return source
                .Do(
                    state: l,
                    onNext: (x, lb) => UnityEngine.Debug.Log($"{lb}OnNext({x})"),
                    onErrorResume: (x, lb) => UnityEngine.Debug.Log($"{lb}OnErrorResume({x})"),
                    onCompleted: (x, lb) => UnityEngine.Debug.Log($"{lb}OnCompleted({x})"),
                    onSubscribe: lb => UnityEngine.Debug.Log($"{lb}OnSubscribe"),
                    onDispose: lb => UnityEngine.Debug.Log($"{lb}OnDispose"));
#else
            return source;
#endif
        }
    }
}
#endif