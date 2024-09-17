using System;
using UnityEngine;
using UnityEngine.UI;

namespace MinimalUtility.UGUI
{
    /// <summary>
    /// <see cref="RectMask2D"/>を継承利用したシンプルで綺麗なゲージ表示.
    /// </summary>
    [RequireComponent(typeof(MaskableGraphic))]
    public class SimpleGauge : RectMask2D
    {
        [SerializeField]
        [Tooltip("ゲージの表示方向")]
        private RectTransform.Edge mode = RectTransform.Edge.Right;

        [SerializeField]
        [Tooltip("ゲージの値(0.0 ～ 1.0)")]
        [Range(0, 1)]
        private float value = 1.0f;

        /// <summary>
        /// ゲージの値(0.0 ～ 1.0).
        /// </summary>
        public float Value
        {
            get => value;
            set
            {
                this.value = Mathf.Clamp01(value);
                var currentPadding = padding;
                switch (mode)
                {
                    case RectTransform.Edge.Left:
                        currentPadding.x = rectTransform.rect.width * (1.0f - this.value);
                        break;
                    case RectTransform.Edge.Right:
                        currentPadding.z = rectTransform.rect.width * (1.0f - this.value);
                        break;
                    case RectTransform.Edge.Top:
                        currentPadding.w = rectTransform.rect.height * (1.0f - this.value);
                        break;
                    case RectTransform.Edge.Bottom:
                        currentPadding.y = rectTransform.rect.height * (1.0f - this.value);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Value));
                }
                padding = currentPadding;
            }
        }

        private MaskableGraphic graphic;

        /// <summary>
        /// ゲージの表示に使用する<see cref="MaskableGraphic"/>.
        /// </summary>
        public MaskableGraphic Graphic => graphic == null ? graphic = this.SafeGetComponent<MaskableGraphic>() : graphic;

        /// <inheritdoc/>
        protected override void OnEnable()
        {
            base.OnEnable();
            AddClippable(Graphic);
        }

#if UNITY_EDITOR
        /// <inheritdoc/>
        protected override void OnValidate()
        {
            Value = value;
            base.OnValidate();
        }
#endif
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(SimpleGauge))]
    public class SimpleGaugeEditor : UnityEditor.Editor
    {
        /// <inheritdoc/>
        public override UnityEngine.UIElements.VisualElement CreateInspectorGUI()
        {
            UnityEngine.UIElements.VisualElement root = new ();
            root.Add(new UnityEditor.UIElements.PropertyField(serializedObject.FindProperty("mode")));
            root.Add(new UnityEditor.UIElements.PropertyField(serializedObject.FindProperty("value")));
            return root;
        }
    }
#endif
}