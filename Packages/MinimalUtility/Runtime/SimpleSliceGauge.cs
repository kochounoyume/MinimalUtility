using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MinimalUtility
{
    /// <summary>
    /// <see cref="Image.type"/>を<see cref="Image.Type.Sliced"/>に設定した<see cref="Image"/>を適当にゲージ化させる実装.
    /// <see cref="Image.Type.Filled"/>よりは綺麗な表示のゲージ.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class SimpleSliceGauge : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 1)]
        private float value = 1.0f;

        [FormerlySerializedAs("fill")]
        [SerializeField]
        [HideInInspector]
        private Image fillImage;

        [SerializeField]
        [HideInInspector]
        private RectTransform rectTransform;

        [SerializeField]
        [HideInInspector]
        private Vector2 sizeDelta;

        /// <summary>
        /// ゲージの値.
        /// </summary>
        public virtual float Value
        {
            get => value;
            set
            {
                rectTransform.SetSafeSizeWidth(sizeDelta.x * value);
                this.value = Math.Clamp01(value);
            }
        }

        /// <summary>
        /// <see cref="Image"/>コンポーネント（読み取り専用）.
        /// </summary>
        public ref Image FillImage => ref fillImage;

        /// <summary>
        /// <see cref="RectTransform"/>コンポーネント（読み取り専用）.
        /// </summary>
        public ref RectTransform RectTransform => ref rectTransform;

#if UNITY_EDITOR
        private void SetValue()
        {
            if (this == null)
            {
                UnityEditor.EditorApplication.update -= SetValue;
                return;
            }
            rectTransform.SetFullStretch();
            sizeDelta = ((RectTransform)rectTransform.parent).GetSizeDelta();
            Value = value;
        }

        [Button("シミュレーション")]
        private void Reset()
        {
            fillImage = GetComponent<Image>();
            fillImage.type = Image.Type.Sliced;
            rectTransform = fillImage.rectTransform;
            rectTransform.pivot = Vector2.zero;
            RectTransform parent = (RectTransform)rectTransform.parent;
            if (parent.TryGetComponent(out Canvas _))
            {
                Debug.LogError("任意の親オブジェクトを設定するべきです.");
            }
            sizeDelta = parent.GetSizeDelta();
            value = 1.0f;
            UnityEditor.EditorApplication.update += SetValue;
        }
#endif
    }
}