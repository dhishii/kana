using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



namespace Surfer
{
    public partial class SUElement
    {

        SUInputFieldHandlerData _fieldHandler = default;

        public void CheckInputField()
        {
            _fieldHandler = new SUInputFieldHandlerData();
            _fieldHandler.OnEditing += OnEditing;
            _fieldHandler.OnEndEditing += OnEndEditing;
            _fieldHandler.OnSubmit += OnSubmit;
            _fieldHandler.AddInputField(ElementData,Events);
        }

        void OnSubmit(string text)
        {
            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.InputField_OnSubmit);
        }

        void OnEditing(string text)
        {
            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.InputField_OnValueChanged);
        }

        void OnEndEditing(string text)
        {
            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.InputField_OnEndEdit);
        }

        public void ResetInputField()
        {
            if(_fieldHandler == null)
                return;

            _fieldHandler.ResetInputField();
            _fieldHandler.OnEditing -= OnEditing;
            _fieldHandler.OnEndEditing -= OnEndEditing;
            _fieldHandler.OnSubmit -= OnSubmit;
        }
    }

}

