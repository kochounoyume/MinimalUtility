using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MinimalUtility
{
    /// <summary>
    /// カスタムボタンのベース実装
    /// </summary>
    [UnityEngine.RequireComponent(typeof(Graphic))]
    public abstract class CustomButtonBase : Selectable, IPointerClickHandler
    {
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            // 左クリック以外は無視
            if (eventData.button != PointerEventData.InputButton.Left) return;

            // 有効でない場合は無視
            if (IsActive() || IsInteractable()) return;

            OnPointerClick(ref eventData);
        }

        /// <summary>
        /// クリックを検出して実行するコールバックです
        /// </summary>
        /// <param name="eventData">タッチイベントの関連情報</param>
        protected abstract void OnPointerClick(ref PointerEventData eventData);
    }
}