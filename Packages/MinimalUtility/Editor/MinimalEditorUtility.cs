using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// エディタ拡張のユーティリティクラス.
    /// </summary>
    public static class MinimalEditorUtility
    {
        /// <summary>
        /// Unity組み込みリソースからGUIContentを取得するか、GUI要素のGUIContentを作成する.
        /// </summary>
        /// <param name="text">GUIContentのテキスト.</param>
        /// <param name="tooltip">カーソルを合わせたときに表示されるツールチップ.</param>
        /// <param name="icon">GUIContentのアイコン.</param>
        /// <returns>GUIContent.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GUIContent TrTextContent(string text, string tooltip = null, Texture icon = null)
        {
            return EditorGUIUtility.TrTextContent($"{text ?? ""}|{tooltip ?? ""}", text, tooltip, icon);
        }
    }
}