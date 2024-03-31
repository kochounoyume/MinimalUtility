using System;
using System.Diagnostics;

namespace MinimalUtility
{
    /// <summary>
    /// 指定したメソッドをUnityのInspector上に表示したボタンでテスト実行できるようになる属性
    /// </summary>
    [Conditional("UNITY_EDITOR"), AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ButtonAttribute : Attribute
    {
        /// <summary>
        /// 引数
        /// </summary>
        public readonly object[] parameters;

        private string buttonName = "";

        /// <summary>
        /// ボタンの名前
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
        /// 指定したメソッドをUnityのInspector上に表示したボタンでテスト実行できるようになる属性
        /// </summary>
        /// <param name="buttonName">ボタンの名前</param>
        /// <param name="parameters">引数</param>
        public ButtonAttribute(string buttonName, params object[] parameters)
        {
            ButtonName = buttonName;
            this.parameters = parameters;
        }

        /// <summary>
        /// 指定したメソッドをUnityのInspector上に表示したボタンでテスト実行できるようになる属性
        /// </summary>
        /// <param name="parameters">引数</param>
        public ButtonAttribute(params object[] parameters)
        {
            this.parameters = parameters;
        }
    }
}