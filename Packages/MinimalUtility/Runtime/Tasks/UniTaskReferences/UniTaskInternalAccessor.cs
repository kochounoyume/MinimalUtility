using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks
{
    using Internal;

    public static class UniTaskInternalAccessor
    {
        /// <summary>
        /// Get default equality comparer for T.
        /// </summary>
        /// <typeparam name="T">Type of T.</typeparam>
        /// <returns>Default equality comparer for T.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEqualityComparer<T> GetEqualityComparer<T>()
        {
            return UnityEqualityComparer.GetDefault<T>();
        }
    }
}