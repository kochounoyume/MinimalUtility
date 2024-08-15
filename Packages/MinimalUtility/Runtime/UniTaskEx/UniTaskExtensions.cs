using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MinimalUtility.UniTaskEx
{
    public static class UniTaskExtensions
    {
        /// <summary>
        /// <see cref="UniTask.WaitForEndOfFrame(MonoBehaviour)"/>は内部にコルーチンを含む.
        /// そのためコルーチンの駆動元である<see cref="MonoBehaviour"/>が非表示になるとエラーが発生する.
        /// それを回避したうえで安全にフレーム終わりまで待機する.
        /// </summary>
        /// <param name="monoBehaviour"></param>
        /// <param name="cancelImmediately"></param>
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
        /// <param name="monoBehaviour"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="cancelImmediately"></param>
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