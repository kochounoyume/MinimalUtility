#if ENABLE_R3 && ENABLE_INPUTSYSTEM
using R3;
using UnityEngine.InputSystem;

namespace MinimalUtility.InputSystem
{
    /// <summary>
    /// <see cref="PlayerInput"/>の拡張メソッド.
    /// </summary>
    public static class PlayerInputExtensions
    {
        /// <summary>
        /// <see cref="PlayerInput.onActionTriggered"/>を<see cref="Observable{T}"/>で提供します.
        /// </summary>
        /// <param name="playerInput">対象の<see cref="PlayerInput"/>.</param>
        /// <returns><see cref="PlayerInput.onActionTriggered"/>の<see cref="Observable{T}"/>.</returns>
        public static Observable<InputAction.CallbackContext> OnActionTriggeredAsObservable(this PlayerInput playerInput)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => playerInput.onActionTriggered += h,
                h => playerInput.onActionTriggered -= h,
                playerInput.destroyCancellationToken);
        }
    }
}
#endif