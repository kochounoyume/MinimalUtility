using System;
using System.Runtime.CompilerServices;

namespace MinimalUtility;

/// <summary>
/// <see cref="IDisposable"/>の拡張メソッド.
/// </summary>
public static class DisposableExtensions
{
    /// <summary>
    /// <see cref="IDisposable"/>を指定した<see cref="IDisposable"/>コンテナに追加します.
    /// </summary>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// using System;
    /// using MinimalUtility;
    /// using R3;
    /// using UnityEngine;
    ///
    /// public class AddToExample : MonoBehaviour
    /// {
    ///     private IDisposable _subscription;
    ///
    ///     private void Start()
    ///     {
    ///         // Same as the following code:
    ///         // _subscription = Observable
    ///         //     .EveryUpdate(destroyCancellationToken)
    ///         //     .Subscribe(static _ => Debug.Log(Time.frameCount));
    ///
    ///         Observable
    ///             .EveryUpdate(destroyCancellationToken)
    ///             .Subscribe(static _ => Debug.Log(Time.frameCount))
    ///             .AddTo(ref _subscription);
    ///     }
    ///
    ///     public void CancelSubscription()
    ///     {
    ///         _subscription.Dispose();
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    /// <param name="disposable">任意の<see cref="IDisposable"/>実装クラスの参照.</param>
    /// <param name="disposableContainer">追加先の<see cref="IDisposable"/>コンテナ.</param>
    /// <typeparam name="T">追加した<see cref="IDisposable"/>.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddTo<T>(this T disposable, ref IDisposable disposableContainer) where T : class, IDisposable
    {
        disposableContainer = disposable;
    }

    /// <summary>
    /// <see cref="IDisposable"/>を指定した<see cref="IDisposable"/>コンテナに追加します.
    /// </summary>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// using System.Threading;
    /// using MinimalUtility;
    /// using R3;
    /// using UnityEngine;
    ///
    /// public class AddToExample : MonoBehaviour
    /// {
    ///     private CancellationTokenRegistration _subscription;
    ///
    ///     private void Start()
    ///     {
    ///         // Same as the following code:
    ///         // _subscription = Observable
    ///         //     .EveryUpdate()
    ///         //     .Subscribe(static _ => Debug.Log(Time.frameCount))
    ///         //     .RegisterTo(destroyCancellationToken);
    ///
    ///         Observable
    ///             .EveryUpdate()
    ///             .Subscribe(static _ => Debug.Log(Time.frameCount))
    ///             .RegisterTo(destroyCancellationToken) // return CancellationTokenRegistration that is struct
    ///             .AddTo(ref _subscription);
    ///     }
    ///
    ///     public void CancelSubscription()
    ///     {
    ///         _subscription.Dispose();
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    /// <param name="disposable">任意の<see cref="IDisposable"/>実装構造体.</param>
    /// <param name="disposableContainer">追加先の<see cref="IDisposable"/>コンテナ.</param>
    /// <typeparam name="T">追加した<see cref="IDisposable"/>.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddTo<T>(this T disposable, ref T disposableContainer) where T : struct, IDisposable
    {
        disposableContainer = disposable;
    }
}