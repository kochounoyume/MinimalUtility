using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MinimalUtility.Editor
{
    public class DocumentCommentEditor : EditorWindow
    {
        [MenuItem("Tool/" + nameof(DocumentCommentEditor))]
        private static void ShowWindow()
        {
            var window = GetWindow<DocumentCommentEditor>();
            window.titleContent = new GUIContent(nameof(DocumentCommentEditor));
            window.Show();
        }

        private void CreateGUI()
        {
            var root = rootVisualElement;
            var inputField = new TextField("Example Code")
            {
                multiline = true
            };
            root.Add(inputField);
            var btn = new Button()
            {
                text = "Parse"
            };
            btn.RegisterCallback<ClickEvent, TextField>(static (_, field) =>
            {
                // 改行文字を見つけたら、改行文字 + /// を挿入する
                var text = field.value
                    .Replace(Environment.NewLine, Environment.NewLine + "        /// ")
                    .Replace(Environment.NewLine + "        /// " + Environment.NewLine, Environment.NewLine + "        ///" + Environment.NewLine);
                field.value = @"/// <example>
        /// <code>
        /// <![CDATA[
        /// " + text + @"
        /// ]]>
        /// </code>
        /// </example>";
                EditorGUIUtility.systemCopyBuffer = field.value;
            }, inputField);
            root.Add(btn);
        }
    }
}