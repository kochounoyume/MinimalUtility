#if ENABLE_UGUI
#nullable enable

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MinimalUtility.UGUI
{
    using Internal;

    /// <summary>
    /// uGUIの全入力を一時的に剥奪するなどの入力制御を行う.
    /// </summary>
    public sealed class UIInputHandler
    {
        private sealed class EmptyBaseInput : BaseInput
        {
            public override string compositionString => "";
            public override IMECompositionMode imeCompositionMode
            {
                get => IMECompositionMode.Auto;
                set { }
            }
            public override Vector2 compositionCursorPos
            {
                get => Vector2.zero;
                set { }
            }
            public override bool mousePresent => false;
            public override bool GetMouseButtonDown(int _) => false;
            public override bool GetMouseButtonUp(int _) => false;
            public override bool GetMouseButton(int _) => false;
            public override Vector2 mousePosition => Vector2.zero;
            public override Vector2 mouseScrollDelta => Vector2.zero;
            public override bool touchSupported => false;
            public override int touchCount => 0;
            public override Touch GetTouch(int _) => default;
            public override float GetAxisRaw(string _) => 0;
            public override bool GetButtonDown(string _) => false;
        }

        private readonly Lazy<EmptyBaseInput> _emptyInput = new(static () => DontDestroyObject.Shared.AddComponent<EmptyBaseInput>());
        private readonly Lazy<BaseInput> _originalInput = new(static () => EventSystem.current.currentInputModule.input);

        private int _refCount;

        /// <summary>
        /// 入力を一時的に剥奪します.
        /// </summary>
        /// <returns>入力を復元するためのハンドル.</returns>
        public ValueDisposable.Disposable<UIInputHandler> Acquire()
        {
            if (_refCount == 0)
            {
                _ = _originalInput.Value;
                EventSystem.current.currentInputModule.inputOverride = _emptyInput.Value;
            }
            _refCount++;
            return ValueDisposable.Create(this, static self =>
            {
                self._refCount = Math.Max(0, self._refCount - 1);
                if (self._refCount == 0)
                {
                    EventSystem.current.currentInputModule.inputOverride = self._originalInput.Value;
                }
            });
        }
    }
}

#endif