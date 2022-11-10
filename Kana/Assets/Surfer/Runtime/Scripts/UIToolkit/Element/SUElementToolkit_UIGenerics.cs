#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Surfer
{
    public partial class SUElementToolkit
    {

        void CheckUIGenerics()
        {
            if (_elementData.VElement == null)
                return;
            
            if(_events.ContainsKey(SUEvent.Type_ID.UIGeneric_OnClick)
            || _events.ContainsKey(SUEvent.Type_ID.UIGeneric_OnDoubleClick))
            {
                _elementData.VElement.RegisterSingleOrDoubleClickEvent(()=>
                {
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnClick);
                },()=>
                {
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnDoubleClick);
                });
            }


            if(_events.ContainsKey(SUEvent.Type_ID.UIGeneric_OnPointerDown))
            {
                _elementData.VElement.RegisterCallback<PointerDownEvent>((evtData) =>
                {
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnPointerDown,evtData);
                });
            }

            if(_events.ContainsKey(SUEvent.Type_ID.UIGeneric_OnPointerUp)
            || _events.ContainsKey(SUEvent.Type_ID.UIGeneric_OnMouseRightClick))
            {
                _elementData.VElement.RegisterCallback<PointerUpEvent>((evtData) =>
                {
                    if(evtData.button == 1)
                    {
                        _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnMouseRightClick,evtData);
                    }
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnPointerUp,evtData);

                });
            }

            if(_events.ContainsKey(SUEvent.Type_ID.UIGeneric_OnEnter))
            {
                _elementData.VElement.RegisterCallback<MouseEnterEvent>((evtData) =>
                {
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnEnter,evtData);
                });
            }

            if(_events.ContainsKey(SUEvent.Type_ID.UIGeneric_OnExit))
            {
                _elementData.VElement.RegisterCallback<MouseLeaveEvent>((evtData) =>
                {
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnExit,evtData);
                });
            }


            if(_events.ContainsKey(SUEvent.Type_ID.UIGeneric_OnFocusIn))
            {
                _elementData.VElement.RegisterCallback<FocusInEvent>((evtData) =>
                {
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnFocusIn,evtData);
                });
            }

            if(_events.ContainsKey(SUEvent.Type_ID.UIGeneric_OnFocusOut))
            {
                _elementData.VElement.RegisterCallback<FocusOutEvent>((evtData) =>
                {
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnFocusOut,evtData);
                });
            }
        }
    }
}

#endif
