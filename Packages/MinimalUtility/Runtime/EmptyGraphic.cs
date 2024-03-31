using UnityEngine;
using UnityEngine.UI;

namespace MinimalUtility
{
    /// <summary>
    /// 空処理のグラフィック
    /// <remarks>
    /// 想定用途：マスク時のベース, 画面全体を透明なuGUIで覆いたいとき、Buttonの判定領域を透明にしたいとき
    /// </remarks>
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    internal class EmptyGraphic : Graphic
    {
        public override void SetMaterialDirty()
        {
            // 空処理を上書き
        }

        public override void SetVerticesDirty()
        {
            // 空処理を上書き
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}