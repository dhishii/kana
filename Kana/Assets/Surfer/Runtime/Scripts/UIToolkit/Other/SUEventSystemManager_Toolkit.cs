using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SUNew
using UnityEngine.InputSystem;
#endif

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;
#endif

namespace Surfer
{
    public partial class SUEventSystemManager
    {
        float _screenHeight = Screen.height;
        Vector2 _mousePos = default;

        float _ratioPanel = default;

#if UNITY_2021_2_OR_NEWER

        public bool IsPointerOverElement(VisualElement vEle)
        {
            return vEle.worldBound.Contains(GetMousePosition(vEle,vEle.GetPanelSettingsElement()));
        }

        
        public Vector2 GetMousePosition(VisualElement vEle, VisualElement elementPanel)
        {
            _mousePos = default;
#if SUNew
            _mousePos = Mouse.current.position.ReadValue();
#else
            _mousePos = Input.mousePosition;
#endif
            _mousePos.y = _screenHeight - _mousePos.y;

            _ratioPanel = elementPanel.resolvedStyle.height / _screenHeight;

            _mousePos *= _ratioPanel;

            return _mousePos;
        }

#endif

    }
}

