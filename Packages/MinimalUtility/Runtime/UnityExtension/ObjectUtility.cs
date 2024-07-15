using System;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

namespace MinimalUtility
{
    /// <summary>
    /// <see cref="UnityEngine.Object"/>に関連するユーティリティ.
    /// </summary>
    public static class ObjectUtility
    {
        /// <summary>
        /// <see cref="UnityEngine.Object"/>のnullチェックを行う.
        /// </summary>
        /// <param name="target">対象の<see cref="UnityEngine.Object"/>.</param>
        /// <typeparam name="T">対象の<see cref="UnityEngine.Object"/>の型.</typeparam>
        /// <returns>対象の<see cref="UnityEngine.Object"/>がnullの場合はnullを返し、そうでなければそのまま返す.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Chk<T>(this T target) where T : Object
        {
            return target == null ? null : target;
        }

        /// <summary>
        /// <see cref="UnityEngine.Object"/>のnullチェックを行い、nullの場合は指定のファクトリメソッドで生成する.
        /// </summary>
        /// <param name="target">対象の<see cref="UnityEngine.Object"/>.</param>
        /// <param name="factory">nullの場合に生成するファクトリメソッド.</param>
        /// <typeparam name="T">生成する<see cref="UnityEngine.Object"/>の型.</typeparam>
        /// <returns>生成した<see cref="UnityEngine.Object"/>の参照.</returns>
        public static ref T GetNullCheck<T>(ref T target, Func<T> factory) where T : Object
        {
            if (target == null)
            {
                target = factory();
            }
            return ref target;
        }

        /// <summary>
        /// <see cref="UnityEngine.Object"/>のnullチェックを行い、nullの場合は指定のファクトリメソッドで生成する.
        /// </summary>
        /// <param name="target">対象の<see cref="UnityEngine.Object"/>.</param>
        /// <param name="state">ファクトリメソッドに渡す引数.</param>
        /// <param name="factory">nullの場合に生成するファクトリメソッド.</param>
        /// <typeparam name="T">生成する<see cref="UnityEngine.Object"/>の型.</typeparam>
        /// <typeparam name="TState">ファクトリメソッドに渡す引数の型.</typeparam>
        /// <returns>生成した<see cref="UnityEngine.Object"/>の参照.</returns>
        public static ref T GetNullCheck<T, TState>(ref T target, TState state, Func<TState, T> factory) where T : Object
        {
            if (target == null)
            {
                target = factory(state);
            }
            return ref target;
        }

        /// <summary>
        /// 特定の<see cref="UnityEngine.Object"/>について、指定の処理の実行で生成できたかnullチェックを行う.
        /// </summary>
        /// <param name="factory">生成処理.</param>
        /// <param name="target">生成した<see cref="UnityEngine.Object"/>の参照.</param>
        /// <typeparam name="T">生成する<see cref="UnityEngine.Object"/>の型.</typeparam>
        /// <returns>生成できた場合はtrue.</returns>
        public static bool TryGetNullCheck<T>(Func<T> factory, out T target) where T : Object
        {
            target = factory();
            return target != null;
        }

        /// <summary>
        /// 特定の<see cref="UnityEngine.Object"/>について、指定の処理の実行で生成できたかnullチェックを行う.
        /// </summary>
        /// <param name="state">ファクトリメソッドに渡す引数.</param>
        /// <param name="factory">生成処理.</param>
        /// <param name="target">生成した<see cref="UnityEngine.Object"/>の参照.</param>
        /// <typeparam name="T">生成する<see cref="UnityEngine.Object"/>の型.</typeparam>
        /// <typeparam name="TState">ファクトリメソッドに渡す引数の型.</typeparam>
        /// <returns>生成できた場合はtrue.</returns>
        public static bool TryGetNullCheck<T, TState>(TState state, Func<TState, T> factory, out T target) where T : Object
        {
            target = factory(state);
            return target != null;
        }
    }
}