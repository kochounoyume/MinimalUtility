﻿#if ENABLE_UGUI
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

        /// <summary>
        /// ゲージの値(0.0 ～ 1.0).
        /// </summary>
        public float value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp01(value);
                padding = (int)_mode switch
                {
                    (int)RectTransform.Edge.Left => new Vector4(rectTransform.rect.width * (1 - _value), 0, 0, 0),
                    (int)RectTransform.Edge.Right => new Vector4(0, 0, rectTransform.rect.width * (1 - _value), 0),
                    (int)RectTransform.Edge.Top => new Vector4(0, 0, 0, rectTransform.rect.height * (1 - _value)),
                    (int)RectTransform.Edge.Bottom => new Vector4(0, rectTransform.rect.height * (1 - _value), 0, 0),
                    _ => throw new ArgumentOutOfRangeException(nameof(this.value))
                };
            }
        }

        /// <inheritdoc/>
        protected override void Start()
        {
            base.Start();
            if (TryGetComponent<MaskableGraphic>(out var graphic))
            {
                AddClippable(graphic);
            }
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