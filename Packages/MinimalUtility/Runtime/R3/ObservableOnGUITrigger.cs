using R3;
using R3.Triggers;
using UnityEngine;

namespace MinimalUtility.R3
{
    /// <summary>
    /// OnGUIイベントをObservableとして購読するためのコンポーネント.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class ObservableOnGUITrigger : ObservableTriggerBase
    {
        private Subject<Unit> onGUI;

        /// <summary>
        /// OnGUIは、レンダリングとGUIイベントの処理のために呼び出される.
        /// </summary>
        /// <returns>OnGUIイベントを購読するObservable.</returns>
        public Observable<Unit> OnGUIAsObservable()
        {
            return onGUI ??= new Subject<Unit>();
        }

        /// <inheritdoc/>
        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onGUI != null)
            {
                onGUI.OnCompleted();
            }
        }

        /// <summary>
        /// OnGUI is called for rendering and handling GUI events.
        /// </summary>
        private void OnGUI()
        {
            if (onGUI != null) onGUI?.OnNext(Unit.Default);
        }
    }
}