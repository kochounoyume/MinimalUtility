#if ENABLE_R3 && ENABLE_INPUTSYSTEM
using R3;
using UnityEngine.InputSystem;

namespace MinimalUtility.InputSystem
{
    public static class PlayerInputExtensions
    {
        public static Observable<InputAction.CallbackContext> OnActionTriggeredAsObservable(this PlayerInput playerInput)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => playerInput.onActionTriggered += h,
                h => playerInput.onActionTriggered -= h,
                playerInput.destroyCancellationToken
            );
        }
    }
}
#endif