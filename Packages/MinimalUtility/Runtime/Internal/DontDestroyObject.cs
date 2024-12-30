#nullable enable

using UnityEngine;

namespace MinimalUtility.Internal
{
    public static class DontDestroyObject
    {
        public static Transform Root => Default.transform;

        public static GameObject Default
        {
            get
            {
                if (s_default == null)
                {
                    s_default = new GameObject("MinimalUtility." + nameof(DontDestroyObject));
                    Object.DontDestroyOnLoad(s_default);
                }
                return s_default;
            }
        }

        private static GameObject? s_default;
    }
}