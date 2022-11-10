using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Surfer
{
    public class SUThemable : MonoBehaviour, ISUThemeChangeHandler
    {

        [SerializeField]
        string _fontGUID = default;

        [SerializeField]
        string _fontSizeGUID = default;

        [SerializeField]
        string _colorGUID = default;

        [SerializeField]
        string _spriteGUID = default;

        [SerializeField]
        string _spriteResGUID = default;

        TextMeshProUGUI _textCp = default;

        Image _imgCp = default;

        Graphic _graphicCp = default;

        void Awake() {

            UpdateGraphics(SurferHelper.SO.GetTheme(SurferHelper.SO.SelectedThemeGUID));

            SUThemeManager.I.RegisterThemeChange(this);
        }

        void UpdateGraphics(SUThemeData theme)
        {
            if(theme == null)
                return;

            if(theme.GetFont(_fontGUID,out var font))
            {
                GetTMPComponent();

                if(_textCp == null)
                    return;

                _textCp.font = font;
            }

            if(theme.GetFontSize(_fontSizeGUID,out var fontSize))
            {
                GetTMPComponent();

                if(_textCp == null)
                    return;

                _textCp.enableAutoSizing = false;
                _textCp.fontSize = fontSize;
            }

            if(theme.GetColor(_colorGUID,out var color))
            {
                GetGraphicComponent();

                if(_graphicCp != null)
                {
                    _graphicCp.color = color;
                }

            }

            if(theme.GetSprite(_spriteGUID,out var sprite))
            {
                GetImageComponent();

                if(_imgCp != null)
                {
                    _imgCp.sprite = sprite;
                }

            }

            if(theme.GetSpriteRes(_spriteResGUID,out var spriteRes))
            {
                GetImageComponent();

                if(_imgCp != null)
                {
                    _imgCp.sprite = spriteRes;
                }

            }
        }

        void GetTMPComponent()
        {
            if(_textCp != null)
                return;

            _textCp = GetComponent<TextMeshProUGUI>();
        }

        void GetImageComponent()
        {
            if(_imgCp != null)
                return;

            _imgCp = GetComponent<Image>();

        }

        void GetGraphicComponent()
        {
            if(_graphicCp != null)
                return;

            _graphicCp = GetComponent<Graphic>();

        }

        public void OnThemeChanged(SUThemeChangeEventData evtData)
        {
            UpdateGraphics(evtData.NewTheme);
        }
    }
}
