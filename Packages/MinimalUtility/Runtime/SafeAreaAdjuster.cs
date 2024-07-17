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
            Rect safeArea = Screen.safeArea;
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

            if (ObjectUtility.TryGetNullCheck(this, static s => s.transform as RectTransform, out var r))
            {
                r.anchorMin = safeArea.min / screenSize;
                r.anchorMax = safeArea.max / screenSize;
            }
        }
    }
}