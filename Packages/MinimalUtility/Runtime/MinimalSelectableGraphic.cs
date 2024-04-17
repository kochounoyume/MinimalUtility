using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MinimalUtility
{
    /// <summary>
    /// <see cref="UnityEngine.UI.Selectable"/>を使用するうえで最低限の機能を持つグラフィック.
    /// <remarks>
    /// 子オブジェクトのGraphicをキャッシュして選択時に色変化を伝播させる
    /// </remarks>
    /// </summary>
    public sealed class MinimalSelectableGraphic : EmptyGraphic
    {
        /// <summary>
        /// 子オブジェクト以下のGraphicのキャッシュ.
        /// </summary>
        private IEnumerable<Graphic> childGraphics;

        /// <inheritdoc>
        /// Selectableの押下時の色変化はここを呼び出しているみたいなので、ちょっと処理を上書き.
        /// </inheritdoc>
        public override void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
        {
            foreach (Graphic childGraphic in childGraphics)
            {
                childGraphic.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
            }
        }

        /// <inheritdoc/>
        protected override void Start()
        {
            base.Start();
            List<Graphic> childGraphicList = new List<Graphic>(transform.childCount);
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Graphic graphic))
                {
                    childGraphicList.Add(graphic);
                }
            }
            if (childGraphicList.Count > 0)
            {
                childGraphicList.TrimExcess();
                childGraphics = childGraphicList;
            }
        }
    }
}