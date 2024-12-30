#nullable enable

using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MinimalUtility.Tasks
{
    /// <summary>
    /// <see cref="UniTask"/>の拡張メソッド.
    /// </summary>
    public static class UniTaskExtensions
    {
        /// <summary>
        /// <see cref="UniTask.WaitForEndOfFrame(MonoBehaviour)"/>は内部にコルーチンを含む.
        /// そのためコルーチンの駆動元である<see cref="MonoBehaviour"/>が非表示になるとエラーが発生する.
        /// それを回避したうえで安全にフレーム終わりまで待機する.
        /// </summary>
        /// <param name="monoBehaviour">コルーチン駆動の基盤となる<see cref="MonoBehaviour"/>.</param>
        /// <param name="cancelImmediately">即座にキャンセルするかどうか.</param>
        /// <returns>フレーム終わりまで待機する<see cref="UniTask"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask WaitForEndFrameSafety(this MonoBehaviour monoBehaviour, bool cancelImmediately = false)
        {
            await UniTask.WaitUntil(
                monoBehaviour,
                static m => m.isActiveAndEnabled,
                cancellationToken: monoBehaviour.destroyCancellationToken,
                cancelImmediately: cancelImmediately);
            await UniTask.WaitForEndOfFrame(monoBehaviour, monoBehaviour.destroyCancellationToken, cancelImmediately);
        }

        /// <summary>
        /// <see cref="UniTask.WaitForEndOfFrame(MonoBehaviour)"/>は内部にコルーチンを含む.
        /// そのためコルーチンの駆動元である<see cref="MonoBehaviour"/>が非表示になるとエラーが発生する.
        /// それを回避したうえで安全にフレーム終わりまで待機する.
        /// </summary>
        /// <param name="monoBehaviour">コルーチン駆動の基盤となる<see cref="MonoBehaviour"/>.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <param name="cancelImmediately">即座にキャンセルするかどうか.</param>
        /// <returns>フレーム終わりまで待機する<see cref="UniTask"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask WaitForEndFrameSafety(this MonoBehaviour monoBehaviour, CancellationToken cancellationToken, bool cancelImmediately = false)
        {
            await UniTask.WaitUntil(
                monoBehaviour,
                static m => m.isActiveAndEnabled,
                cancellationToken: cancellationToken,
                cancelImmediately: cancelImmediately);
            await UniTask.WaitForEndOfFrame(monoBehaviour, cancellationToken, cancelImmediately);
        }
    }
}