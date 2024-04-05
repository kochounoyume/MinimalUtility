using R3;
using R3.Triggers;
using UnityEngine;

namespace MinimalUtility.R3
{
    [DisallowMultipleComponent]
    public sealed class ObservableOnGUITrigger : ObservableTriggerBase
    {
        Subject<Unit> onGUI;

        /// <summary>OnGUI is called for rendering and handling GUI events.</summary>
        void OnGUI()
        {
            if (onGUI != null) onGUI?.OnNext(Unit.Default);
        }

        /// <summary>OnGUI is called for rendering and handling GUI events.</summary>
        public Observable<Unit> OnGUIAsObservable()
        {
            return onGUI ??= new Subject<Unit>();
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onGUI != null)
            {
                onGUI.OnCompleted();
            }
        }
    }
}