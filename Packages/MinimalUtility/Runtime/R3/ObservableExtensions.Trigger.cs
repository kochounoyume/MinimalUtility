#if ENABLE_R3
using R3;
using UnityEngine;

namespace MinimalUtility.R3
{
    /// <content>
    /// Observableの拡張メソッド.
    /// </content>
    public static partial class ObservableExtensions
    {
        /// <summary>
        /// OnGUIイベントをObservableとして購読します.
        /// </summary>
        /// <param name="gameObject">任意のGameObject.</param>
        /// <returns>OnGUIイベントを購読するObservable.</returns>
        public static Observable<Unit> OnGUIAsObservable(this GameObject gameObject)
        {
            if (gameObject == null) return Observable.Empty<Unit>();
            return
                gameObject.TryGetComponent(out ObservableOnGUITrigger trigger)
                    ? trigger.OnGUIAsObservable()
                    : gameObject.AddComponent<ObservableOnGUITrigger>().OnGUIAsObservable();
        }
    }
}
#endif