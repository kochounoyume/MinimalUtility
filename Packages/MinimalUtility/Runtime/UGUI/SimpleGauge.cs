#if ENABLE_UGUI
#nullable enable

using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MinimalUtility.UGUI
{
    /// <summary>
    /// <see cref="RectMask2D"/>を継承利用したシンプルで綺麗なゲージ表示.
    /// </summary>
    [RequireComponent(typeof(MaskableGraphic))]
    public class SimpleGauge : RectMask2D
    {
        [FormerlySerializedAs("mode")]
        [SerializeField]
        [Tooltip("ゲージの表示方向")]
        private RectTransform.Edge _mode = RectTransform.Edge.Right;

        [FormerlySerializedAs("value")]
        [SerializeField]
        [Tooltip("ゲージの値(0.0 ～ 1.0)")]
        [Range(0, 1)]
        private float _value = 1.0f;

        private MaskableGraphic? _graphic;

        /// <summary>
        /// ゲージの値(0.0 ～ 1.0).
        /// </summary>
        public float value
        {
            get => _value;
            set
            {
                this._value = Mathf.Clamp01(value);
                padding = (int)_mode switch
                {
                    (int)RectTransform.Edge.Left => new Vector4(rectTransform.rect.width * this._value, 0, 0, 0),
                    (int)RectTransform.Edge.Right => new Vector4(0, 0, rectTransform.rect.width * this._value, 0),
                    (int)RectTransform.Edge.Top => new Vector4(0, rectTransform.rect.height * this._value, 0, 0),
                    (int)RectTransform.Edge.Bottom => new Vector4(0, 0, 0, rectTransform.rect.height * this._value),
                    _ => throw new ArgumentOutOfRangeException(nameof(this.value))
                };
            }
        }

        /// <summary>
        /// ゲージの表示に使用する<see cref="MaskableGraphic"/>.
        /// </summary>
        public MaskableGraphic graphic => _graphic == null ? _graphic = this.SafeGetComponent<MaskableGraphic>() : _graphic;

        /// <inheritdoc/>
        protected override void OnEnable()
        {
            base.OnEnable();
            AddClippable(graphic);
        }

#if UNITY_EDITOR
        /// <inheritdoc/>
        protected override void OnValidate()
        {
            value = _value;
            base.OnValidate();
        }
#endif
    }
}
#endif