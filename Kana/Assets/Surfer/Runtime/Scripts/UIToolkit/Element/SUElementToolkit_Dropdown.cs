#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Surfer
{
    public partial class SUElementToolkit
    {

        DropdownField _dropD = default;

        void CheckDropdown()
        {

            if (Events.ContainsKey(SUEvent.Type_ID.Dropdown_OnOptionSelected)
                || Events.ContainsKey(SUEvent.Type_ID.Dropdown_OnFirstOptionSelected))
            {

                if (ElementData.VElement == null)
                return;
            
                if (ElementData.VElement is DropdownField)
                {
                    var drop = ElementData.VElement as DropdownField;
                    drop.RegisterValueChangedCallback(OnDropdownValueChanged);
                    _dropD = drop;
                }
            }

        }

        void OnDropdownValueChanged(ChangeEvent<string> evtData)
        {

            var dropdown = evtData.target as DropdownField;

            if (dropdown.index == 0)
            {
                _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Dropdown_OnFirstOptionSelected);
            }
            
            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Dropdown_OnOptionSelected);

        }


        public void ResetDropdown()
        {
            _dropD?.UnregisterValueChangedCallback(OnDropdownValueChanged);
        }
    }
}

#endif
