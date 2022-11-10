#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Surfer
{
    public partial class SUElementToolkit
    {

        SUSliderHandlerData _sliderHandler = default;

        void CheckSlider()
        {

            _sliderHandler = new SUSliderHandlerData();
            _sliderHandler.OnLowerThan += OnLowerThan;
            _sliderHandler.OnGreaterThan += OnGreaterThan;
            _sliderHandler.OnMax += OnMax;
            _sliderHandler.OnMin += OnMin;
            _sliderHandler.OnChanged += OnChanged;
            _sliderHandler.AddSlider(ElementData, Events);

        }

        void OnChanged(float value)
        {
            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Slider_OnValueChanged);
        }

        void OnLowerThan(float value)
        {
            if (Events.TryGetValue(SUEvent.Type_ID.Slider_OnLowerThan,out var behavs))
            {
                foreach(var item in behavs.Behaviours)
                {
                    if(value.IsEqualTo(item.Event.FloatVal))
                    {
                        item.Run(ElementData);
                    }
                }
            }
        }   

        void OnGreaterThan(float value)
        {
            if (Events.TryGetValue(SUEvent.Type_ID.Slider_OnGreaterThan,out var behavs))
            {
                foreach(var item in behavs.Behaviours)
                {
                    if(value.IsEqualTo(item.Event.FloatVal))
                    {
                        item.Run(ElementData);
                    }
                }
            }
        }

        void OnMax()
        {
            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Slider_OnMax);
        }

        void OnMin()
        {
            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Slider_OnMin);
        }

        public void ResetSlider()
        {
            if(_sliderHandler == null)
                return;

            _sliderHandler.ResetSlider();
            _sliderHandler.OnLowerThan -= OnLowerThan;
            _sliderHandler.OnGreaterThan -= OnGreaterThan;
            _sliderHandler.OnMax -= OnMax;
            _sliderHandler.OnMin -= OnMin;

        }
    }
}

#endif
