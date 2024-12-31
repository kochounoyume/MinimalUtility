#nullable enable

using UnityEngine;
#if UNITY_EDITOR
using Screen = UnityEngine.Device.Screen;
#else
using Screen = UnityEngine.Screen;
#endif

namespace MinimalUtility
{
    /// <summary>
    /// セーフエリア調整スクリプト.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public sealed class SafeAreaAdjuster : MonoBehaviour
    {
        private void Start() => Padding();

        [Button("Adjust SafeArea")]
        private void Padding()
        {
            var safeArea = Screen.safeArea;
            var screenSize = new Vector2(Screen.width, Screen.height);

            var rectTransform = transform as RectTransform;
            if (rectTransform != null)
            {
                rectTransform.anchorMin = safeArea.min / screenSize;
                rectTransform.anchorMax = safeArea.max / screenSize;
            }
        }
    }
}