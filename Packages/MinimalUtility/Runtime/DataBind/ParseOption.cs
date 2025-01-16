#nullable enable

using System;
using UnityEngine;

namespace MinimalUtility.DataBind
{
    [Serializable]
    public struct ParseOption
    {
        [SerializeField, Tooltip("数字書式指定文字列")]
        private string _format;

        [SerializeField]
        private NumberOption _numberOption;

        public ReadOnlySpan<char> format => string.IsNullOrEmpty(_format) ? default : _format.AsSpan();

        public NumberOption numberOption => _numberOption;
    }

    public enum NumberOption
    {
        [InspectorName("設定なし")]
        None,
        [InspectorName("絶対値")]
        Absolute,
        [InspectorName("負の値化")]
        Negative
    }
}