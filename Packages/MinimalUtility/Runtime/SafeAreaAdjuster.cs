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
    public class SafeAreaAdjuster : MonoBehaviour
    {
        private void Start() => Padding();

        [Button("Adjust SafeArea")]
        private void Padding()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            Rect safeArea = Screen.safeArea;
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

            rectTransform.anchorMin = safeArea.min / screenSize;
            rectTransform.anchorMax = safeArea.max / screenSize;
        }
    }
}