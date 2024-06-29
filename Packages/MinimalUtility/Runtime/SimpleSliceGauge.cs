using System;
using UnityEngine;
using UnityEngine.UI;

namespace MinimalUtility
{
    /// <summary>
    /// <see cref="Image.type"/>を<see cref="Image.Type.Sliced"/>に設定した<see cref="Image"/>を適当にゲージ化させる実装.
    /// <see cref="Image.Type.Filled"/>よりは綺麗な表示のゲージ.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class SimpleSliceGauge : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("子オブジェクトのImageコンポーネントを指定してください.")]
        private Image childFill;

        [SerializeField]
        [Range(0, 1)]
        private float value = 1.0f;

        [SerializeField]
        [HideInInspector]
        private RectTransform rectTransform;

        [SerializeField]
        [HideInInspector]
        private RectTransform childFillRect;

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
                this.value = Math.Clamp01(value);
                childFillRect.SetSafeSizeWidth(sizeDelta.x * value);
            }
        }

        /// <summary>
        /// 子オブジェクトの<see cref="Image"/>コンポーネント（読み取り専用）.
        /// </summary>
        public ref Image ChildFill => ref childFill;

        /// <summary>
        /// このオブジェクトの<see cref="RectTransform"/>（読み取り専用）.
        /// </summary>
        protected ref readonly RectTransform RectTransform => ref rectTransform;

        /// <summary>
        /// 破棄時処理.
        /// </summary>
        protected virtual void OnDestroy()
        {
#if UNITY_EDITOR
            if (isInitialized)
            {
                UnityEditor.EditorApplication.update -= SetValue;
            }
#endif
        }

#if UNITY_EDITOR
#pragma warning disable SA1201
        private DrivenRectTransformTracker tracker;
        private bool isInitialized;
#pragma warning restore SA1201

        private static void InitializeImage(ref Image childFill, ref DrivenRectTransformTracker tracker, in Vector2 sizeDelta, out RectTransform childRect)
        {
            childFill.type = Image.Type.Sliced;
            childRect = childFill.rectTransform;
            childRect.SetFullStretch();
            childRect.SetSafeSize(sizeDelta);
            childRect.pivot = Vector2.zero;
            tracker.Clear();
            tracker.Add(childRect, childRect, DrivenTransformProperties.All);
        }

        private void SetValue()
        {
            if (childFillRect == null)
            {
                isInitialized = false;
                UnityEditor.EditorApplication.update -= SetValue;
                if (this == null) return;
                throw new NullReferenceException("Imageコンポーネントの参照がnullです.");
            }
            Value = value;
        }

        private void Reset()
        {
            rectTransform = (RectTransform)transform;
            if (!this.TryGetComponentInOnlyChild(out childFill))
            {
                throw new NullReferenceException("子オブジェクトにImageコンポーネントが見つかりませんでした.");
            }
            sizeDelta = rectTransform.GetSizeDelta();
            InitializeImage(ref childFill, ref tracker, sizeDelta, out childFillRect);
            isInitialized = true;
        }

        private void OnValidate()
        {
            if (!isActiveAndEnabled) return;
            if (!isInitialized)
            {
                UnityEditor.EditorApplication.update += NextFrameReset;
                void NextFrameReset()
                {
                    Reset();
                    UnityEditor.EditorApplication.update -= NextFrameReset;
                }
            }

            UnityEditor.EditorApplication.update -= SetValue;

            if (childFill == null)
            {
                isInitialized = false;
                throw new NullReferenceException("Imageコンポーネントの参照がnullです.");
            }

            UnityEditor.EditorApplication.update += SetValue;
        }
#endif
    }
}