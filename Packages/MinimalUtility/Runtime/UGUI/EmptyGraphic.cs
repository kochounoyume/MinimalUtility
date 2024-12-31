#if ENABLE_UGUI
#nullable enable

using UnityEngine;
using UnityEngine.UI;

namespace MinimalUtility.UGUI
{
    /// <summary>
    /// 空処理のグラフィック.
    /// <remarks>
    /// 想定用途：マスク時のベース, 画面全体を透明なuGUIで覆いたいとき、Buttonの判定領域を透明にしたいとき
    /// </remarks>
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class EmptyGraphic : Graphic
    {
        /// <inheritdoc/>
        public override void SetMaterialDirty()
        {
            // 空処理を上書き
        }

        /// <inheritdoc/>
        public override void SetVerticesDirty()
        {
            // 空処理を上書き
        }

        /// <inheritdoc/>
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}
#endif