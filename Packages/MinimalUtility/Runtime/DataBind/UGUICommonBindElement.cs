#if ENABLE_UGUI
#nullable enable

using MinimalUtility.UGUI;
#if ENABLE_TEXTMESHPRO
using TMPro;
#endif
using UnityEngine.UI;

namespace MinimalUtility.DataBind
{

    [DataBindMenu(nameof(Button) + "/" + nameof(Button.enabled))]
    internal sealed class ButtonEnableElement : EnableElement<Button>
    {
        private ButtonEnableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(Dropdown) + "/" + nameof(Dropdown.enabled))]
    internal sealed class DropdownEnableElement : EnableElement<Dropdown>
    {
        private DropdownEnableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(Image) + "/" + nameof(Image.enabled))]
    internal sealed class ImageEnableElement : EnableElement<Image>
    {
        private ImageEnableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(RawImage) + "/" + nameof(RawImage.enabled))]
    internal sealed class RawImageEnableElement : EnableElement<RawImage>
    {
        private RawImageEnableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(Scrollbar) + "/" + nameof(Scrollbar.enabled))]
    internal sealed class ScrollbarEnableElement : EnableElement<Scrollbar>
    {
        private ScrollbarEnableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(ScrollRect) + "/" + nameof(ScrollRect.enabled))]
    internal sealed class ScrollRectEnableElement : EnableElement<ScrollRect>
    {
        private ScrollRectEnableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(SimpleGauge) + "/" + nameof(SimpleGauge.enabled))]
    internal sealed class SimpleGaugeEnableElement : EnableElement<SimpleGauge>
    {
        private SimpleGaugeEnableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(Slider) + "/" + nameof(Slider.enabled))]
    internal sealed class SliderEnableElement : EnableElement<Slider>
    {
        private SliderEnableElement() : base()
        {
        }
    }

#if ENABLE_TEXTMESHPRO
    [DataBindMenu(nameof(TextMeshProUGUI) + "/" + nameof(TextMeshProUGUI.enabled))]
    internal sealed class TextMeshProUGUIEnableElement : EnableElement<TextMeshProUGUI>
    {
        private TextMeshProUGUIEnableElement() : base()
        {
        }
    }
#endif
#if ENABLE_TEXTMESHPRO
    [DataBindMenu(nameof(TMP_InputField) + "/" + nameof(TMP_InputField.enabled))]
    internal sealed class TMP_InputFieldEnableElement : EnableElement<TMP_InputField>
    {
        private TMP_InputFieldEnableElement() : base()
        {
        }
    }
#endif

    [DataBindMenu(nameof(Toggle) + "/" + nameof(Toggle.enabled))]
    internal sealed class ToggleEnableElement : EnableElement<Toggle>
    {
        private ToggleEnableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(Image) + "/" + nameof(Image.color))]
    internal sealed class ImageColorElement : ColorElement<Image>
    {
        private ImageColorElement() : base()
        {
        }
    }

    [DataBindMenu(nameof(Image) + "/" + nameof(Image.raycastTarget))]
    internal sealed class ImageRaycastTargetElement : RaycastTargetElement<Image>
    {
        private ImageRaycastTargetElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(RawImage) + "/" + nameof(RawImage.color))]
    internal sealed class RawImageColorElement : ColorElement<RawImage>
    {
        private RawImageColorElement() : base()
        {
        }
    }

    [DataBindMenu(nameof(RawImage) + "/" + nameof(RawImage.raycastTarget))]
    internal sealed class RawImageRaycastTargetElement : RaycastTargetElement<RawImage>
    {
        private RawImageRaycastTargetElement() : base()
        {
        }
    }

#if ENABLE_TEXTMESHPRO
    [DataBindMenu(nameof(TextMeshProUGUI) + "/" + nameof(TextMeshProUGUI.color))]
    internal sealed class TextMeshProUGUIColorElement : ColorElement<TextMeshProUGUI>
    {
        private TextMeshProUGUIColorElement() : base()
        {
        }
    }

    [DataBindMenu(nameof(TextMeshProUGUI) + "/" + nameof(TextMeshProUGUI.raycastTarget))]
    internal sealed class TextMeshProUGUIRaycastTargetElement : RaycastTargetElement<TextMeshProUGUI>
    {
        private TextMeshProUGUIRaycastTargetElement() : base()
        {
        }
    }
#endif

    [DataBindMenu(nameof(Button) + "/" + nameof(Button.interactable))]
    internal sealed class ButtonInteractableElement : InteractableElement<Button>
    {
        private ButtonInteractableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(Dropdown) + "/" + nameof(Dropdown.interactable))]
    internal sealed class DropdownInteractableElement : InteractableElement<Dropdown>
    {
        private DropdownInteractableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(Scrollbar) + "/" + nameof(Scrollbar.interactable))]
    internal sealed class ScrollbarInteractableElement : InteractableElement<Scrollbar>
    {
        private ScrollbarInteractableElement() : base()
        {
        }
    }


    [DataBindMenu(nameof(Slider) + "/" + nameof(Slider.interactable))]
    internal sealed class SliderInteractableElement : InteractableElement<Slider>
    {
        private SliderInteractableElement() : base()
        {
        }
    }

#if ENABLE_TEXTMESHPRO
    [DataBindMenu(nameof(TMP_InputField) + "/" + nameof(TMP_InputField.interactable))]
    internal sealed class TMP_InputFieldInteractableElement : InteractableElement<TMP_InputField>
    {
        private TMP_InputFieldInteractableElement() : base()
        {
        }
    }
#endif

    [DataBindMenu(nameof(Toggle) + "/" + nameof(Toggle.interactable))]
    internal sealed class ToggleInteractableElement : InteractableElement<Toggle>
    {
        private ToggleInteractableElement() : base()
        {
        }
    }

}

#endif
