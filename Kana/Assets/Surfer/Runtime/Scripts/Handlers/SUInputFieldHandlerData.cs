using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if SUNew 
using UnityEngine.InputSystem;
#endif


#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;
#endif

namespace Surfer
{
    public class SUInputFieldHandlerData
    {

        TMP_InputField _canvasInputField = default;
#if UNITY_2021_2_OR_NEWER
        TextField _tkInputField = default;
#endif

        public event System.Action<string> OnSubmit;
        public event System.Action<string> OnEditing;
        public event System.Action<string> OnEndEditing;

        public void AddInputField(SUElementData data,SUElement.DictEvents events)
        {
            if (data == null)
                return;
            if (events == null)
                return;

            if (events.ContainsKey(SUEvent.Type_ID.InputField_OnValueChanged)
            || events.ContainsKey(SUEvent.Type_ID.InputField_OnEndEdit))
            {

                if (data.IsToolkit)
                {
#if UNITY_2021_2_OR_NEWER
                    AddInputField(data.VElement as TextField);
#endif
                }
                else
                {
                    AddInputField(data.ObjOwner.GetComponent<TMP_InputField>());
                }
            } 

        }

        public void AddInputField(TMP_InputField canvasInputField)
        {
            if(canvasInputField == null)
                return;

            _canvasInputField = canvasInputField;
            _canvasInputField.onValueChanged.AddListener(OnValueChanged);
            _canvasInputField.onEndEdit.AddListener(OnEndEdit);
            _canvasInputField.onTouchScreenKeyboardStatusChanged.AddListener(OnMobileKeyboardStatusChanged);
        }

#if UNITY_2021_2_OR_NEWER
        public void AddInputField(TextField tkInputField)
        {
            if(tkInputField == null)
                return;

            _tkInputField = tkInputField;
            _tkInputField.RegisterValueChangedCallback(OnValueChanged);
            _tkInputField.RegisterCallback<FocusOutEvent>(OnEndEdit);
        }

        void OnValueChanged(ChangeEvent<string> evtData)
        {
            OnValueChanged(evtData.newValue);
        }

        void OnEndEdit(FocusOutEvent evtData)
        {
            if(_tkInputField == null)
                return;
            if(_tkInputField.focusController.focusedElement != _tkInputField)
                return;
            if(evtData.relatedTarget != null && evtData.relatedTarget != _tkInputField)
                return;

            OnEndEdit(_tkInputField.text);
        }
#endif

        void OnMobileKeyboardStatusChanged(TouchScreenKeyboard.Status status)
        {
            //FUTURE TO DO : missing Submit from mobile keyboard Done button for uiToolkit
            if (status == TouchScreenKeyboard.Status.Done)
            {
                OnSubmit?.Invoke(_canvasInputField.text);
            }
        }

        void OnValueChanged(string text)
        {
            OnEditing?.Invoke(text);
        }

        void OnEndEdit(string text)
        {

#if SUNew
            if(Keyboard.current.enterKey.wasPressedThisFrame
            || Keyboard.current.numpadEnterKey.wasPressedThisFrame)
            {
                OnSubmit?.Invoke(text);
            }
#else
            if (Input.GetKeyDown(KeyCode.Return)
            || Input.GetKeyDown(KeyCode.KeypadEnter)
            || Input.GetButtonDown("Submit"))
            {
                OnSubmit?.Invoke(text);
            }
#endif

            OnEndEditing?.Invoke(text);
        }

        public void ResetInputField()
        {

            if( _canvasInputField != null )
            {
                _canvasInputField.onValueChanged.RemoveListener(OnValueChanged);
                _canvasInputField.onEndEdit.RemoveListener(OnEndEdit);
            }
            else
            {
#if UNITY_2021_2_OR_NEWER

                if(_tkInputField == null)
                    return;

                _tkInputField.UnregisterValueChangedCallback(OnValueChanged);
                _tkInputField.UnregisterCallback<FocusOutEvent>(OnEndEdit);
#endif
            }
            
        }
    }

}

