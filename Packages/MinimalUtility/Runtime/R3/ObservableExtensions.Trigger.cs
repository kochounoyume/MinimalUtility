#if ENABLE_R3
using R3;
using UnityEngine;

namespace MinimalUtility.R3
{
    public static partial class ObservableExtensions
    {
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