using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DictEvents = Surfer.SUElement.DictEvents;

#if SURew
using Rewired;
#endif
namespace Surfer
{
    public class SUInputHandlerData : ISubmitHandler
#if SUOld
    ,ISUOldInputKeyHandler, ISUOldInputActionHandler, ISUOldInputAnyKeyHandler
#endif
#if SUNew
    ,ISUNewInputActionHandler, ISUNewInputAnyHandler
#endif
#if SURew
    , ISURewiredActionHandler
#endif
    {

        DictEvents _events = default;
        SUElementData _eleData = default;

        public void AddInterface(SUElementData eleData, DictEvents events)
        {
            if (eleData == null)
                return;
            if (events == null)
                return;

            _events = events;
            _eleData = eleData;

#if SUOld

            if (_events.TryGetValue(SUEvent.Type_ID.Input_OldInput_OnAnyKeyDown, out var valueAnyKey))
            {
                for (int i = 0; i < valueAnyKey.Behaviours.Count; i++)
                {
                    var item = valueAnyKey.Behaviours[i];
                    SUInputManager.I.RegisterOldInputAnyKey(this);
                }
            }

            if (_events.TryGetValue(SUEvent.Type_ID.Input_OldInput_OnKeyDown, out var valueDown))
            {
                for (int i = 0; i < valueDown.Behaviours.Count; i++)
                {
                    var item = valueDown.Behaviours[i];
                    SUInputManager.I.RegisterOldInputKey(item.Event.KeyCodeVal,this);
                }
            }
            if (_events.TryGetValue(SUEvent.Type_ID.Input_OldInput_OnButtonDown, out var valueName))
            {
                for (int i = 0; i < valueName.Behaviours.Count; i++)
                {
                    var item = valueName.Behaviours[i];
                    SUInputManager.I.RegisterOldInputAction(item.Event.StringVal,this);

                }
            }

#endif

#if SURew


            if (_events.TryGetValue(SUEvent.Type_ID.Input_Rewired_OnAction, out var valueRew))
            {
                if (!ReInput.isReady)
                    return;

                for (int i = 0; i < valueRew.Behaviours.Count; i++)
                {
                    var item = valueRew.Behaviours[i];
                    int playerID = item.Event.IntVal;
                    UpdateLoopType updateLoop = item.Event.UpdateLoop;
                    InputActionEventType eventType = item.Event.EventType;
                    string actionName = item.Event.StringVal;

                    SUInputManager.I.RegisterRewiredAction(actionName, playerID, updateLoop, eventType, this);
                }

            }



#endif


#if SUNew
            if (_events.TryGetValue(SUEvent.Type_ID.Input_NewInput_OnAction, out var valueNew))
            {


                for(int i=0;i<valueNew.Behaviours.Count;i++)
                {
                    var item = valueNew.Behaviours[i];

                    SUInputManager.I.RegisterNewInputAction(item.Event.PInput,item.Event.StringVal,this);

                }

                

            }

            if (_events.TryGetValue(SUEvent.Type_ID.Input_NewInput_OnAnyButtonPressed, out var valueNewAny))
            {
                for(int i=0;i<valueNewAny.Behaviours.Count;i++)
                {
                    var item = valueNewAny.Behaviours[i];

                    SUInputManager.I.RegisterNewInputAny(this);

                }
            }
#endif

        }



        #region Interfaces

        public void OnSubmit(BaseEventData eventData)
        {
            //do nothing
        }

#if SUOld
        public void OnOldInputKey(SUOldInputKeyEventData eventData)
        {
            if(_events == null)
                return;
            if(_eleData == null)
                return;

            _events.RunEventBehaviourParams(_eleData,SUEvent.Type_ID.Input_OldInput_OnKeyDown,eventData.Key);
        }

        public void OnOldInputAction(SUOldInputActionEventData eventData)
        {
            if(_events == null)
                return;
            if(_eleData == null)
                return;

            _events.RunEventBehaviourParams(_eleData,SUEvent.Type_ID.Input_OldInput_OnButtonDown,eventData.ActionName);
        }

        public void OnOldInputAnyKey()
        {
            if(_events == null)
                return;
            if(_eleData == null)
                return;

            _events.RunEventBehaviour(_eleData,SUEvent.Type_ID.Input_OldInput_OnAnyKeyDown);
        }

#endif

#if SUNew

        public void OnNewInputAction(SUNewInputActionEventData eventData)
        {
            if(_events == null)
                return;
            if(_eleData == null)
                return;

            _events.RunEventBehaviourParams(_eleData,SUEvent.Type_ID.Input_NewInput_OnAction, eventData.ActionName);
        }

        public void OnNewInputAny()
        {
            if(_events == null)
                return;
            if(_eleData == null)
                return;

            _events.RunEventBehaviour(_eleData,SUEvent.Type_ID.Input_NewInput_OnAnyButtonPressed);
        }

#endif


#if SURew

        public void OnRewiredAction(SURewiredActionEventData eventData)
        {
            if (_events == null)
                return;
            if (_eleData == null)
                return;

            _events.RunEventBehaviourParams(_eleData, SUEvent.Type_ID.Input_Rewired_OnAction, eventData.ActionData);
        }
#endif


        #endregion

    }
}

