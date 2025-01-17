﻿#if ENABLE_UGUI
#nullable enable

using System;
#if ENABLE_TEXTMESHPRO
using TMPro;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace MinimalUtility.DataBind
{
    using UGUI;

    public abstract class ColorElement<T> : TargetBindElement<T> where T : Graphic
    {
        protected ColorElement() : base(nameof(Graphic.color))
        {
        }

        public override void Bind(in Color32 value)
        {
            ThrowIfNull(_target);
            _target!.color = value;
        }
    }

    public abstract class RaycastTargetElement<T> : TargetBindElement<T> where T : Graphic
    {
        protected RaycastTargetElement() : base(nameof(Graphic.raycastTarget))
        {
        }

        public override void Bind(bool value)
        {
            ThrowIfNull(_target);
            _target!.raycastTarget = value;
        }
    }

    public abstract class InteractableElement<T> : TargetBindElement<T> where T : Selectable
    {
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
    internal sealed class DropdownValueElement : TargetBindElement<Dropdown>
    {
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
    internal sealed class ImageSpriteElement : TargetBindElement<Image>
    {
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
    internal sealed class RawImageTextureElement : TargetBindElement<RawImage>
    {
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
    internal sealed class ScrollBarValueElement : TargetBindElement<Scrollbar>
    {
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
    internal sealed class ScrollRectHorizontalNormalizedPositionElement : TargetBindElement<ScrollRect>
    {
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
    internal sealed class ScrollRectVerticalNormalizedPositionElement : TargetBindElement<ScrollRect>
    {
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
    internal sealed class SimpleGaugeValueElement : TargetBindElement<SimpleGauge>
    {
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
    internal sealed class SliderValueElement : TargetBindElement<Slider>
    {
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
    internal sealed class TextMeshProTextElement : TargetBindElement<TextMeshProUGUI>
    {
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

        public override void Bind(in ArraySegment<char> value)
        {
            ThrowIfNull(_target);
            _target!.SetCharArray(value.Array, value.Offset, value.Count);
        }
    }

    [DataBindMenu(nameof(TextMeshProUGUI) + "/" + nameof(TextMeshProUGUI.text) + " (int)")]
    internal sealed class TextMeshProIntTextElement : TargetBindElement<TextMeshProUGUI>
    {
        [SerializeField]
        private NumberParseOption<int> _option = new("", int.MinValue, int.MaxValue);
        private TextMeshProIntTextElement() : base(nameof(TextMeshProUGUI.text))
        {
        }

        public override void Bind(int value)
        {
            ThrowIfNull(_target);
            value = Math.Clamp(value, _option.min, _option.max);
            _target!.SetCharArray(GetCharArray(value, _option.format));

            static char[] GetCharArray(int value, in ReadOnlySpan<char> format, int bufferLength = 16)
            {
                var span = (Span<char>)stackalloc char[bufferLength];
                if (value.TryFormat(span, out var written, format))
                {
                    return span[..written].ToArray();
                }
                return GetCharArray(value, format, bufferLength * 2);
            }
        }
    }

    [DataBindMenu(nameof(TextMeshProUGUI) + "/" + nameof(TextMeshProUGUI.text) + " (float)")]
    internal sealed class TextMeshProFloatTextElement : TargetBindElement<TextMeshProUGUI>
    {
        [SerializeField]
        private NumberParseOption<float> _option = new("", float.MinValue, float.MaxValue);
        private TextMeshProFloatTextElement() : base(nameof(TextMeshProUGUI.text))
        {
        }

        public override void Bind(float value)
        {
            ThrowIfNull(_target);
            value = Math.Clamp(value, _option.min, _option.max);
            _target!.SetCharArray(GetCharArray(value, _option.format));

            static char[] GetCharArray(float value, in ReadOnlySpan<char> format, int bufferLength = 16)
            {
                var span = (Span<char>)stackalloc char[bufferLength];
                if (value.TryFormat(span, out var written, format))
                {
                    return span[..written].ToArray();
                }
                return GetCharArray(value, format, bufferLength * 2);
            }
        }
    }

    [DataBindMenu(nameof(TextMeshProUGUI) + "/" + nameof(TextMeshProUGUI.text) + " (" + nameof(DateTime) + ")")]
    internal sealed class TextMeshProDateTimeTextElement : TargetBindElement<TextMeshProUGUI>
    {
        [SerializeField] private SimpleParseOption _option = new("");
        private TextMeshProDateTimeTextElement() : base(nameof(TextMeshProUGUI.text))
        {
        }

        public override void Bind(in DateTime value)
        {
            ThrowIfNull(_target);
            _target!.SetCharArray(GetCharArray(value, _option.format));

            static char[] GetCharArray(in DateTime value, in ReadOnlySpan<char> format, int bufferLength = 16)
            {
                var span = (Span<char>)stackalloc char[bufferLength];
                if (value.TryFormat(span, out var written, format))
                {
                    return span[..written].ToArray();
                }
                return GetCharArray(value, format, bufferLength * 2);
            }
        }
    }

    [DataBindMenu(nameof(TextMeshProUGUI) + "/" + nameof(TextMeshProUGUI.text) + " (" + nameof(TimeSpan) + ")")]
    internal sealed class TextMeshProTimeSpanTextElement : TargetBindElement<TextMeshProUGUI>
    {
        [SerializeField] private SimpleParseOption _option = new("");
        private TextMeshProTimeSpanTextElement() : base(nameof(TextMeshProUGUI.text))
        {
        }

        public override void Bind(in TimeSpan value)
        {
            ThrowIfNull(_target);
            _target!.SetCharArray(GetCharArray(value, _option.format));
            static char[] GetCharArray(in TimeSpan value, in ReadOnlySpan<char> format, int bufferLength = 16)
            {
                var span = (Span<char>)stackalloc char[bufferLength];
                if (value.TryFormat(span, out var written, format))
                {
                    return span[..written].ToArray();
                }
                return GetCharArray(value, format, bufferLength * 2);
            }
        }
    }

    [DataBindMenu(nameof(TMP_InputField) + "/" + nameof(TMP_InputField.text))]
    internal sealed class TMP_InputFieldTextElement : TargetBindElement<TMP_InputField>
    {
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
    internal sealed class ToggleIsOnElement : TargetBindElement<Toggle>
    {
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