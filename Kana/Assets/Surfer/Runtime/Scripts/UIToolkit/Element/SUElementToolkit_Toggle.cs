#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Surfer
{
    
    public partial class SUElementToolkit
    {

        Toggle _toggle = default;

        void CheckToggle()
        {

            if (Events.ContainsKey(SUEvent.Type_ID.Toggle_OnFalse)
                || Events.ContainsKey(SUEvent.Type_ID.Toggle_OnTrue))
            {
                if(ElementData.VElement == null)
                    return;
                
                if(ElementData.VElement is Toggle)
                {
                    var togg = ElementData.VElement as Toggle;
                    togg.RegisterValueChangedCallback(OnValueChanged);
                    _toggle = togg;
                }
            }

        }

        void OnValueChanged(ChangeEvent<bool> evtData)
        {

            if (evtData.newValue)
            {
                _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Toggle_OnTrue);
            }
            else
            {
                _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Toggle_OnFalse);
            }

        }

        public void ResetToggle()
        {
            _toggle?.UnregisterValueChangedCallback(OnValueChanged);
        }
    }

}

#endif
