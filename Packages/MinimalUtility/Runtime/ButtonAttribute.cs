using System;
using System.Diagnostics;

namespace MinimalUtility
{
    /// <summary>
    /// 指定したメソッドをUnityのInspector上に表示したボタンでテスト実行できるようになる属性.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ButtonAttribute : Attribute
    {
        /// <summary>
        /// 引数.
        /// </summary>
        private readonly object[] parameters;

        private string buttonName = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonAttribute"/> class.
        /// </summary>
        /// <param name="buttonName">ボタンの名前.</param>
        /// <param name="parameters">引数.</param>
        public ButtonAttribute(string buttonName, params object[] parameters)
        {
            this.buttonName = buttonName;
            this.parameters = parameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonAttribute"/> class.
        /// </summary>
        /// <param name="parameters">引数.</param>
        public ButtonAttribute(params object[] parameters)
        {
            this.parameters = parameters;
        }

        /// <summary>
        /// ボタンの名前.
        /// </summary>
        public string ButtonName
        {
            get => buttonName;
            set
            {
                if (string.IsNullOrEmpty(buttonName))
                {
                    buttonName = value;
                }
            }
        }

        /// <summary>
        /// 引数.
        /// </summary>
        public ref readonly object[] Parameters => ref parameters;
    }
}