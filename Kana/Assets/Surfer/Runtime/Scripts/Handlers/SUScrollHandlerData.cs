using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;
#endif

namespace Surfer
{
    public class SUScrollHandlerData
    {
        public event System.Action<Position_ID> OnPositionUpdate;
        public enum Position_ID
        {
            None,
            Top,
            Bottom,
            Right,
            Left,
        }

        //canvas info
        ScrollRect _canvasScroll = default;
        bool _isHorizontal = default;

        const float _factor = 0.05f;
        const float _canvasMaxLimit = 1;
        const float _canvasMinLimit = default;

        //tk info
#if UNITY_2021_2_OR_NEWER
        ScrollView _tkScroll = default;
#endif

        float _tkMaxHeight = default;
        float _tkMaxWidth = default;
        float _tkMin = default;
        bool _isFirstTkVerticalCall = true;
        bool _isFirstTkHorizontalCall = true;

        Position_ID _positionID = default;

        public void AddScroll(SUElementData data,SUElement.DictEvents events)
        {
            if (data == null)
                return;
            if (events == null)
                return;

            if (events.ContainsKey(SUEvent.Type_ID.ScrollRect_OnNotReachedAnySide)
               || events.ContainsKey(SUEvent.Type_ID.ScrollRect_OnReachedBottom)
               || events.ContainsKey(SUEvent.Type_ID.ScrollRect_OnReachedLeft)
               || events.ContainsKey(SUEvent.Type_ID.ScrollRect_OnReachedRight)
               || events.ContainsKey(SUEvent.Type_ID.ScrollRect_OnReachedTop))
            {

                if (data.IsToolkit)
                {
#if UNITY_2021_2_OR_NEWER
                    AddScroll(data.VElement as ScrollView);
#endif
                }
                else
                {
                    AddScroll(data.ObjOwner.GetComponent<ScrollRect>());
                }

                
            }

        }

        void OnValueChanged(Vector2 vec)
        {
            if(_isHorizontal)
            {
                CheckHorizontalValue(_canvasScroll.horizontalNormalizedPosition,_canvasMinLimit,_canvasMaxLimit);
            }
            else
            {
                CheckVerticalValue(_canvasScroll.verticalNormalizedPosition,_canvasMinLimit,_canvasMaxLimit);
            }
        }

#if UNITY_2021_2_OR_NEWER
        void OnVerticalValueChanged(float value)
        {
            if(_isFirstTkVerticalCall)
            {
                _isFirstTkVerticalCall = false;
                CheckVerticalValue(value, _tkMin, _tkMin,true);
                return;
            }

            if (_tkMaxHeight < Mathf.Epsilon)
            {
                _tkMaxHeight = _tkScroll.verticalScroller.highValue;
            }
            CheckVerticalValue(value,_tkMaxHeight,_tkMin,true);
        }

        void OnHorizontalValueChanged(float value)
        {
            if(_isFirstTkHorizontalCall)
            {
                _isFirstTkHorizontalCall = false;
                CheckHorizontalValue(value, _tkMin, _tkMin);
                return;
            }

            if (_tkMaxWidth < Mathf.Epsilon)
            {
                _tkMaxWidth = _tkScroll.horizontalScroller.highValue;
            }
            CheckHorizontalValue(value,_tkMin,_tkMaxWidth);
        }
#endif

        void CheckVerticalValue(float value,float bottomValue,float topValue, bool inverseCheck = false)
        {
            if ( (!inverseCheck && value >= (int)topValue-_factor) || (inverseCheck && value <= (int)topValue+_factor) )
            {
                CheckTrigger(Position_ID.Top);
            }
            else if ( (!inverseCheck && value <= (int)bottomValue+_factor) || (inverseCheck && value >= (int)bottomValue-_factor) )
            {
                CheckTrigger(Position_ID.Bottom);
            }
            else
            {
                CheckTrigger(Position_ID.None);
            }
        }

        void CheckHorizontalValue(float value,float leftValue,float rightValue)
        {
            if (value <= (int)leftValue+_factor)
            {
                CheckTrigger(Position_ID.Left);
            }
            else if(value >= (int)rightValue-_factor)
            {
                CheckTrigger(Position_ID.Right);
            }
            else
            {
                CheckTrigger(Position_ID.None);
            }
        }

        void CheckTrigger(Position_ID pos)
        {
            if (_positionID != pos)
            {
                _positionID = pos;
                OnPositionUpdate?.Invoke(pos);
            }
        }


        public void AddScroll(ScrollRect canvasScroll)
        {
            if(canvasScroll == null)
                return;

            _canvasScroll = canvasScroll;
            _isHorizontal = _canvasScroll.horizontal;
            _canvasScroll.onValueChanged.AddListener(OnValueChanged);
            OnValueChanged(Vector2.zero);
        }

#if UNITY_2021_2_OR_NEWER
        public void AddScroll(ScrollView tkScroll)
        {
            if(tkScroll == null)
                return;

            _tkScroll = tkScroll;

            if (_tkScroll.mode == ScrollViewMode.Vertical
            || _tkScroll.mode == ScrollViewMode.VerticalAndHorizontal)
            {
                _tkScroll.verticalScroller.valueChanged += OnVerticalValueChanged;
                OnVerticalValueChanged(_tkMin);
            }

            if (_tkScroll.mode == ScrollViewMode.Horizontal
            || _tkScroll.mode == ScrollViewMode.VerticalAndHorizontal)
            {
                _tkScroll.horizontalScroller.valueChanged += OnHorizontalValueChanged;
                OnHorizontalValueChanged(_tkMin);
            }
        }
#endif

        public void ResetScroll()
        {

            if (_canvasScroll != null)
            {
                _canvasScroll.onValueChanged.RemoveListener(OnValueChanged);
            }

#if UNITY_2021_2_OR_NEWER

            if (_tkScroll != null)
            {
                _tkScroll.verticalScroller.valueChanged -= OnVerticalValueChanged;
                _tkScroll.horizontalScroller.valueChanged -= OnHorizontalValueChanged;
            }

#endif
                
        }

    }
}

