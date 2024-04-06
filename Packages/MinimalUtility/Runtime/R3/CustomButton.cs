#if ENABLE_R3
using R3;

namespace MinimalUtility.R3
{
    /// <summary>
    /// R3を使ったカスタムボタン.
    /// </summary>
    public sealed class CustomButton : CustomButtonBase
    {
        private readonly Subject<Unit> subject = new Subject<Unit>();

        /// <summary>
        /// ボタンの購読登録.
        /// <remarks>
        /// ボタンの破棄時にOnCompletedを発行します.
        /// </remarks>
        /// </summary>
        /// <returns>ボタンの購読を登録するObservable.</returns>
        public Observable<Unit> OnClickObservable() => subject;

        /// <inheritdoc/>
        protected override void OnPointerClick(ref UnityEngine.EventSystems.PointerEventData eventData)
        {
            subject.OnNext(Unit.Default);
        }

        /// <inheritdoc/>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            subject.OnCompleted();
            subject.Dispose();
        }
    }
}
#endif
