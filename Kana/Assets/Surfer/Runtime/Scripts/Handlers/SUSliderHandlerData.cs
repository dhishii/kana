using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;
using TkSlider = UnityEngine.UIElements.Slider;
#endif

namespace Surfer
{
    public class SUSliderHandlerData
    {
        public event System.Action<float> OnChanged;
        public event System.Action OnMax;
        public event System.Action OnMin;
        /// <summary>
        /// Event triggered when the slider value is greater than a "chosen" value.
        /// The float passed is the "chosen" value
        /// </summary>
        public event System.Action<float> OnGreaterThan;
        /// <summary>
        /// Event triggered when the slider value is lower than a "chosen" value.
        /// The float passed is the "chosen" value
        /// </summary>
        public event System.Action<float> OnLowerThan;

#if UNITY_2021_2_OR_NEWER
        TkSlider _tkSlider = default;
#endif

        Slider _canvasSlider = default;
        List<bool> _greatersDone = new List<bool>();
        List<float> _greatersValues = new List<float>();
        List<bool> _lowersDone = new List<bool>();
        List<float> _lowersValues = new List<float>();
        bool _isVolume = default;
        float _valueToCheck = default;
        float _offset = 0.01f;
        bool _reachedMax = default, _reachedMin = default;

        public void AddSlider(SUElementData data,SUElement.DictEvents events)
        {
            if(data == null)
                return;
            if(events == null)
                return;

            _isVolume = data.Type == SUElementData.Type_ID.Slider_OverallVolume;

            bool hasEvent = false;

            if (events.TryGetValue(SUEvent.Type_ID.Slider_OnGreaterThan, out var valueActG))
            {

                hasEvent = true;

                for (int i = 0; i < valueActG.Behaviours.Count; i++)
                {
                    AddGreaterThanCheck(valueActG.Behaviours[i].Event.FloatVal);
                }
            }

            if (events.TryGetValue(SUEvent.Type_ID.Slider_OnLowerThan, out var valueActL))
            {
                hasEvent = true;

                for (int i = 0; i < valueActL.Behaviours.Count; i++)
                {
                    AddLowerThanCheck(valueActL.Behaviours[i].Event.FloatVal);
                }
            }
            
            if (events.ContainsKey(SUEvent.Type_ID.Slider_OnMax))
            {
                hasEvent = true;
            }
            if (events.ContainsKey(SUEvent.Type_ID.Slider_OnMin))
            {
                hasEvent = true;
            }
            if (events.ContainsKey(SUEvent.Type_ID.Slider_OnValueChanged))
            {
                hasEvent = true;
            }


            if (hasEvent || _isVolume)
            {
                if (data.IsToolkit)
                {
#if UNITY_2021_2_OR_NEWER
                    AddSlider(data.VElement as TkSlider,_isVolume);
                    if (_tkSlider == null)
                        return;
#endif
                }
                else
                {
                    AddSlider(data.ObjOwner.GetComponent<Slider>(),_isVolume);
                    if (_canvasSlider == null)
                        return;
                }

                SetUpForOverallAudio();
            }

        }

        void OnValueChanged(float value)
        {
            OnValueChanged(value,_canvasSlider.minValue,_canvasSlider.maxValue);
        }


        void OnValueChanged(float value,float min,float max)
        {

            for(int i=0;i<_greatersValues.Count;i++)
            {

                _valueToCheck = _greatersValues[i];

                if(value > _valueToCheck && !_greatersDone[i])
                {
                    OnGreaterThan?.Invoke(_valueToCheck);
                    _greatersDone[i] = true;
                }
                if (value < _valueToCheck && _greatersDone[i])
                {
                    _greatersDone[i] = false;
                }
            }


            for (int i = 0; i < _lowersValues.Count; i++)
            {
                _valueToCheck = _lowersValues[i];

                if (value < _valueToCheck && !_lowersDone[i])
                {
                    OnLowerThan?.Invoke(_valueToCheck);
                    _lowersDone[i] = true;
                }
                if (value > _valueToCheck && _lowersDone[i])
                {
                    _lowersDone[i] = false;
                }
            }

            //max check
            {
                if(_canvasSlider != null)
                {
                    _valueToCheck = _canvasSlider.maxValue;
                }
                else 
                {
#if UNITY_2021_2_OR_NEWER

                    if(_tkSlider != null)
                    {
                        _valueToCheck = _tkSlider.highValue;
                    }
#endif
                }

                if (value >= _valueToCheck - _offset )
                {
                    if(!_reachedMax)
                    {
                        _reachedMax = true;
                        OnMax?.Invoke();
                    }
                }
                else
                {
                    _reachedMax = false;
                }
            }

            //min check
            {
                if(_canvasSlider != null)
                {
                    _valueToCheck = _canvasSlider.minValue;
                }
                else 
                {
#if UNITY_2021_2_OR_NEWER

                    if(_tkSlider != null)
                    {
                        _valueToCheck = _tkSlider.lowValue;
                    }
#endif
                }

                if (value <= _valueToCheck + _offset )
                {
                    if (!_reachedMin)
                    {
                        _reachedMin = true;
                        OnMin?.Invoke();
                    }   
                }
                else
                {
                    _reachedMin = false;
                }
            }

            //overall volume check
            if(_isVolume)
            {
                AudioListener.volume = value;
                PlayerPrefs.SetFloat(SurferHelper.kOverallVolume,value);
            }

            OnChanged?.Invoke(value);

        }

        public void AddGreaterThanCheck(float valueToCheck)
        {
            _greatersValues.Add(valueToCheck);
            _greatersDone.Add(false);
        }

        public void AddLowerThanCheck(float valueToCheck)
        {
            _lowersValues.Add(valueToCheck);
            _lowersDone.Add(false);
        }

        public void AddSlider(Slider canvasSlider,bool isOverallVolume = default)
        {
            if (canvasSlider == null)
                return;
            _canvasSlider = canvasSlider;
            _canvasSlider.onValueChanged.AddListener(OnValueChanged);
            _isVolume = isOverallVolume;

            if (isOverallVolume)
            {
                SetUpForOverallAudio();
            }
            else
            {
                OnValueChanged(_canvasSlider.value);
            }
        }

#if UNITY_2021_2_OR_NEWER
        public void AddSlider(TkSlider toolkitSlider,bool isOverallVolume = default)
        {
            if (toolkitSlider == null)
                return;
            _tkSlider = toolkitSlider;
            _tkSlider.RegisterValueChangedCallback(OnValueChanged);
            _isVolume = isOverallVolume;

            if (isOverallVolume)
            {
                SetUpForOverallAudio();
            }
            else
            {
                CallTkValueChanged();
            }
        }

        void CallTkValueChanged()
        {
            if(_tkSlider == null)
                return;

            var changeEvent = ChangeEvent<float>.GetPooled(_tkSlider.value,_tkSlider.value);
            changeEvent.target = _tkSlider;
            OnValueChanged(changeEvent);
            changeEvent.Dispose(); 
        }

        void OnValueChanged(ChangeEvent<float> evtData)
        {
            var slid = evtData.target as TkSlider;
            OnValueChanged(evtData.newValue,slid.lowValue,slid.highValue);
        }

#endif


        void SetUpForOverallAudio()
        {
            if(!_isVolume)
                return;

            if (_canvasSlider != null)
            {
                _canvasSlider.minValue = 0;

                if (PlayerPrefs.HasKey(SurferHelper.kOverallVolume))
                    _canvasSlider.value = PlayerPrefs.GetFloat(SurferHelper.kOverallVolume);
                else
                    _canvasSlider.value = _canvasSlider.maxValue;

                _canvasSlider.maxValue = 1;

                OnValueChanged(_canvasSlider.value);
            }
            else
            {
#if UNITY_2021_2_OR_NEWER
                if(_tkSlider == null)
                    return;

                _tkSlider.lowValue = 0;

                if (PlayerPrefs.HasKey(SurferHelper.kOverallVolume))
                    _tkSlider.value = PlayerPrefs.GetFloat(SurferHelper.kOverallVolume);
                else
                    _tkSlider.value = _tkSlider.highValue;

                _tkSlider.highValue = 1;

                CallTkValueChanged();
#endif
            }
        }


        public void ResetSlider()
        {

#if UNITY_2021_2_OR_NEWER

            if ( _tkSlider != null)
            {
                _tkSlider.UnregisterValueChangedCallback(OnValueChanged);
            }

#endif
            
            if (_canvasSlider != null)
            {
                _canvasSlider.onValueChanged.RemoveListener(OnValueChanged);
            }

        }


    }
}

