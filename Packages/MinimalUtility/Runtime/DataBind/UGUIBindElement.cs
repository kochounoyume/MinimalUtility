#if ENABLE_UGUI
#nullable enable

#if ENABLE_TEXTMESHPRO
using System;
using TMPro;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace MinimalUtility.DataBind
{
    using UGUI;

    public abstract class ColorElement<T> : BindElement where T : Graphic
    {
        [SerializeField]
        private T? _target;

        protected ColorElement() : base(nameof(Graphic.color))
        {
        }

        public override void Bind(in Color32 value)
        {
            ThrowIfNull(_target);
            _target!.color = value;
        }
    }

    public abstract class RaycastTargetElement<T> : BindElement where T : Graphic
    {
        [SerializeField]
        private T? _target;

        protected RaycastTargetElement() : base(nameof(Graphic.raycastTarget))
        {
        }

        public override void Bind(bool value)
        {
            ThrowIfNull(_target);
            _target!.raycastTarget = value;
        }
    }

    public abstract class InteractableElement<T> : BindElement where T : Selectable
    {
        [SerializeField]
        private T? _target;

        protected InteractableElement() : base(nameof(Selectable.interactable))
        {
        }

        public override void Bind(bool value)
        {
            ThrowIfNull(_target);
            _target!.interactable = value;
        }
    }

    [DataBindMenu(nameof(Dropdown) + "/" + nameof(Dropdown.value))]
    internal sealed class DropdownValueElement : BindElement
    {
        [SerializeField]
        private Dropdown? _target;

        private DropdownValueElement() : base(nameof(Dropdown.value))
        {
        }

        public override void Bind(int value)
        {
            ThrowIfNull(_target);
            _target!.value = value;
        }
    }

    [DataBindMenu(nameof(Image) + "/" + nameof(Image.sprite))]
    internal sealed class ImageSpriteElement : BindElement
    {
        [SerializeField]
        private Image? _target;

        private ImageSpriteElement() : base(nameof(Image.sprite))
        {
        }

        public override void Bind(Sprite value)
        {
            ThrowIfNull(_target);
            _target!.sprite = value;
        }
    }

    [DataBindMenu(nameof(RawImage) + "/" + nameof(RawImage.texture))]
    internal sealed class RawImageTextureElement : BindElement
    {
        [SerializeField]
        private RawImage? _target;

        private RawImageTextureElement() : base(nameof(RawImage.texture))
        {
        }

        public override void Bind(Texture value)
        {
            ThrowIfNull(_target);
            _target!.texture = value;
        }
    }

    [DataBindMenu(nameof(Scrollbar) + "/" + nameof(Scrollbar.value))]
    internal sealed class ScrollBarValueElement : BindElement
    {
        [SerializeField]
        private Scrollbar? _target;

        private ScrollBarValueElement() : base(nameof(Scrollbar.value))
        {
        }

        public override void Bind(float value)
        {
            ThrowIfNull(_target);
            _target!.value = value;
        }
    }

    [DataBindMenu(nameof(ScrollRect) + "/" + nameof(ScrollRect.horizontalNormalizedPosition))]
    internal sealed class ScrollRectHorizontalNormalizedPositionElement : BindElement
    {
        [SerializeField]
        private ScrollRect? _target;

        private ScrollRectHorizontalNormalizedPositionElement() : base(nameof(ScrollRect.horizontalNormalizedPosition))
        {
        }

        public override void Bind(float value)
        {
            ThrowIfNull(_target);
            _target!.horizontalNormalizedPosition = value;
        }
    }

    [DataBindMenu(nameof(ScrollRect) + "/" + nameof(ScrollRect.verticalNormalizedPosition))]
    internal sealed class ScrollRectVerticalNormalizedPositionElement : BindElement
    {
        [SerializeField]
        private ScrollRect? _target;

        private ScrollRectVerticalNormalizedPositionElement() : base(nameof(ScrollRect.verticalNormalizedPosition))
        {
        }

        public override void Bind(float value)
        {
            ThrowIfNull(_target);
            _target!.verticalNormalizedPosition = value;
        }
    }

    [DataBindMenu(nameof(SimpleGauge) + "/" + nameof(SimpleGauge.value))]
    internal sealed class SimpleGaugeValueElement : BindElement
    {
        [SerializeField]
        private SimpleGauge? _target;

        private SimpleGaugeValueElement() : base(nameof(SimpleGauge.value))
        {
        }

        public override void Bind(float value)
        {
            ThrowIfNull(_target);
            _target!.value = value;
        }
    }

    [DataBindMenu(nameof(Slider) + "/" + nameof(Slider.value))]
    internal sealed class SliderValueElement : BindElement
    {
        [SerializeField]
        private Slider? _target;

        private SliderValueElement() : base(nameof(Slider.value))
        {
        }

        public override void Bind(float value)
        {
            ThrowIfNull(_target);
            _target!.value = value;
        }
    }

#if ENABLE_TEXTMESHPRO
    [DataBindMenu(nameof(TextMeshProUGUI) + "/" + nameof(TextMeshProUGUI.text))]
    internal sealed class TextMeshProTextElement : BindElement
    {
        [SerializeField]
        private TextMeshProUGUI? _target;

        private TextMeshProTextElement() : base(nameof(TextMeshProUGUI.text))
        {
        }

        public override void Bind(string value)
        {
            ThrowIfNull(_target);
            _target!.text = value;
        }

        public override void Bind(char[] value)
        {
            ThrowIfNull(_target);
            _target!.SetCharArray(value);
        }

        public override void Bind(ArraySegment<char> value)
        {
            ThrowIfNull(_target);
            _target!.SetCharArray(value.Array, value.Offset, value.Count);
        }
    }

    [DataBindMenu(nameof(TMP_InputField) + "/" + nameof(TMP_InputField.text))]
    internal sealed class TMP_InputFieldTextElement : BindElement
    {
        [SerializeField]
        private TMP_InputField? _target;

        private TMP_InputFieldTextElement() : base(nameof(TMP_InputField.text))
        {
        }

        public override void Bind(string value)
        {
            ThrowIfNull(_target);
            _target!.text = value;
        }
    }
#endif

    [DataBindMenu(nameof(Toggle) + "/" + nameof(Toggle.isOn))]
    internal sealed class ToggleIsOnElement : BindElement
    {
        [SerializeField]
        private Toggle? _target;

        private ToggleIsOnElement() : base(nameof(Toggle.isOn))
        {
        }

        public override void Bind(bool value)
        {
            ThrowIfNull(_target);
            _target!.isOn = value;
        }
    }
}

#endif