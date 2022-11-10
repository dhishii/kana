using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Surfer
{


    public partial class SUElement
    {
        Toggle _toggle = default;

        void CheckToggle()
        {

            if (Events.ContainsKey(SUEvent.Type_ID.Toggle_OnFalse)
                || Events.ContainsKey(SUEvent.Type_ID.Toggle_OnTrue))
            {

                _toggle = gameObject.GetComponent<Toggle>();

                if (_toggle == null)
                    return;

                _toggle.onValueChanged.AddListener(OnValueChanged);
            }


        }

        void OnValueChanged(bool value)
        {

            if (value)
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
            if (_toggle == null)
                return;

            _toggle.onValueChanged.RemoveListener(OnValueChanged);

        }

    }


}
