#nullable enable

using System;
using UnityEngine;

namespace MinimalUtility.DataBind
{
    [Serializable]
    internal struct NumberParseOption<T> where T : unmanaged
    {
        [SerializeField, Tooltip("書式指定文字列：Excelなどの書式にほぼ近似")]
        private string _format;

        [SerializeField, Tooltip("下限値")]
        private T _min;

        [SerializeField, Tooltip("上限値")]
        private T _max;

        public ReadOnlySpan<char> format => string.IsNullOrEmpty(_format) ? default : _format.AsSpan();

        public T min => _min;

        public T max => _max;

        public NumberParseOption(string format, T min, T max)
        {
            _format = format;
            _min = min;
            _max = max;
        }
    }

    [Serializable]
    internal struct SimpleParseOption
    {
        [SerializeField, Tooltip("書式指定文字列：Excelなどの書式にほぼ近似")]
        private string _format;

        public ReadOnlySpan<char> format => string.IsNullOrEmpty(_format) ? default : _format.AsSpan();

        public SimpleParseOption(string format)
        {
            _format = format;
        }
    }
}