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
    public class SUTooltipHandlerData
    {


        Coroutine _tooltipRoutine = default;

        Vector3 _tooltipPos = default;
        Vector2 _tooltipSize = default;
        Vector3 _mousePos = default;
        RectTransform _objRectT = default, _canvasRect = default;
        Vector2 _offset = Vector2.one;
        Camera _myCam = default;
        bool _isOverlay = default;

#if UNITY_2021_2_OR_NEWER
        VisualElement _vEle = default;
        VisualElement _elementPanel = default;
#endif
        float _xDistance = default;
        float _yDistance = default;


        public void AddTooltip(SUElementData eleData)
        {
            if (!eleData.IsTooltip())
                return;

            if (eleData.IsToolkit)
            {
#if UNITY_2021_2_OR_NEWER
                AddTooltip(eleData.VElement,eleData.Vector);
#endif
            }
            else
            {
                AddTooltip(eleData.ObjOwner.GetComponent<RectTransform>(),eleData.Vector);
            }
            
        }

        public void AddTooltip(RectTransform rectT,Vector2 offset)
        {
            if(rectT == null)
                return;
            if(_objRectT != null)
                return;
            
            _objRectT = rectT;
            _offset = offset;
            rectT.gameObject.GetCanvasAndCamerInfo(out _canvasRect,out _myCam, out _isOverlay);
        }

#if UNITY_2021_2_OR_NEWER
        public void AddTooltip(VisualElement vEle,Vector2 offset)
        {
            if(vEle == null)
                return;

            _vEle = vEle;
            _vEle.style.position = Position.Absolute;
            _offset = offset;
            _elementPanel = vEle.GetPanelSettingsElement();
        } 
#endif


        public void Show()
        {
            if(_tooltipRoutine != null)
                return;
            
            _tooltipRoutine = SurferManager.I.StartCoroutine(TooltipRoutine());

        }

        public void Hide()
        {
            
            if(_tooltipRoutine == null)
                return;

            SurferManager.I.StopCoroutine(_tooltipRoutine);
            _tooltipRoutine = null;

            if (_objRectT != null)
            {
                _objRectT.transform.position = SurferHelper.OutPos;
            }
            else
            {
#if UNITY_2021_2_OR_NEWER
                if(_vEle == null)
                    return;

                _vEle.style.top = SurferHelper.OutPos.y ;
                _vEle.style.left = SurferHelper.OutPos.y ;
#endif
            }
        }

        public void ResetTooltip()
        {
            if (_tooltipRoutine != null)
                SurferManager.I.StopCoroutine(_tooltipRoutine);
            
            _tooltipRoutine = null;
        }
        
        IEnumerator TooltipRoutine()
        {

            while(true)
            {

                if(_objRectT != null)
                {
                    RoutineCanvas();
                }
                else
                {

                    RoutineToolkit();
                }

                yield return new WaitForEndOfFrame();

            }


        }

        void RoutineToolkit()
        {

#if UNITY_2021_2_OR_NEWER
            if (_vEle == null)
                return;

            _tooltipSize.x = _vEle.layout.width * _offset.x * _vEle.transform.scale.x ;
            _tooltipSize.y = _vEle.layout.height * _offset.y * _vEle.transform.scale.y ;

            _mousePos = SUEventSystemManager.I.GetMousePosition(_vEle,_elementPanel);

            _tooltipPos.x = _mousePos.x + _tooltipSize.x;
            _tooltipPos.y = _mousePos.y - _tooltipSize.y;

            _tooltipPos.x -= _vEle.layout.width / 2.0f ;
            _tooltipPos.y -= _vEle.layout.height / 2.0f ;

            _xDistance = -((1 - _vEle.transform.scale.x) * _vEle.layout.width / 2.0f);
            _yDistance = -((1 - _vEle.transform.scale.y) * _vEle.layout.height / 2.0f);

            //stick to the wall
            _tooltipPos.x = Mathf.Clamp( _tooltipPos.x, _xDistance, _elementPanel.layout.width - _vEle.layout.width - _xDistance );
            _tooltipPos.y = Mathf.Clamp( _tooltipPos.y, _yDistance, _elementPanel.layout.height - _vEle.layout.height - _yDistance );


            _vEle.style.top = _tooltipPos.y ;
            _vEle.style.left = _tooltipPos.x ;
#endif
        }

        void RoutineCanvas()
        {
            if(_canvasRect == null)
                return;
            if(_objRectT == null)
                return;
            if(_myCam == null)
                return;

            _tooltipSize.x = _objRectT.rect.width * _offset.x * _objRectT.transform.localScale.x;
            _tooltipSize.y = _objRectT.rect.height * _offset.y * _objRectT.transform.localScale.y;

#if SUNew
            
            _mousePos = Mouse.current.position.ReadValue();

#elif SURew
            
            _mousePos = Input.mousePosition;
#else

            _mousePos = UnityEngine.Input.mousePosition;
#endif

            _tooltipPos.x = _mousePos.x + _tooltipSize.x;
            _tooltipPos.y = _mousePos.y + _tooltipSize.y;

            _xDistance = (_objRectT.transform.localScale.x * _objRectT.rect.width /2.0f);
            _yDistance = (_objRectT.transform.localScale.y * _objRectT.rect.height /2.0f);

            //stick to the wall
            _tooltipPos.x = Mathf.Clamp(_tooltipPos.x, _xDistance, _canvasRect.rect.width - _xDistance);
            _tooltipPos.y = Mathf.Clamp(_tooltipPos.y, _yDistance, _canvasRect.rect.height - _yDistance);

            RectTransformUtility.ScreenPointToWorldPointInRectangle(_objRectT, _tooltipPos, _isOverlay ? null : _myCam,out _tooltipPos);

            _objRectT.transform.position = _tooltipPos;
        }

    }
}

