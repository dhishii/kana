using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DictEvents = Surfer.SUElement.DictEvents;

#if SUNew
using UnityEngine.InputSystem;
using InputAction = UnityEngine.InputSystem.InputAction;
#endif

#if SURew
using Rewired;
#endif

namespace Surfer
{
    public static class SUDictEventsExtensions
    {
        
        public static void RunEventBehaviour(this DictEvents events, SUElementData eleData, SUEvent.Type_ID eventID,object evtData = null)
        {
            if(events.TryGetValue(eventID,out var value))
            {
                value.Run(eleData,evtData);
            }
        }

        public static void RunEventBehaviourParamsScene(this DictEvents events, SUElementData eleData, SUEvent.Type_ID eventID,string stringVal,object evtData)
        {
            if(events.TryGetValue(eventID,out var behavs))
            {
                behavs.RunWithParamsScene(eleData,stringVal,evtData);
            }
        }
        
        public static void RunEventBehaviourParamsState(this DictEvents events, SUElementData eleData, SUEvent.Type_ID eventID, SUStateEventData eventInfo, bool onlyVersionCheck = false)
        {

            if(events.TryGetValue(eventID,out var behavs))
            {
                behavs.RunWithParamsState(eleData,eventInfo,onlyVersionCheck);
            }

        }

        public static void RunCustomEventBehaviour(this DictEvents events, SUElementData eleData, SUCustomEventEventData eventData)
        {

            if(events.TryGetValue(SUEvent.Type_ID.MyCustomEvent,out var behavs))
            {
                behavs.RunWithParamsCustomEvent(eleData,eventData);
            }
        }

        
#if SUOld
        public static void RunEventBehaviourParams(this DictEvents events, SUElementData eleData, SUEvent.Type_ID eventID, KeyCode keyCode)
        {

            if (events.TryGetValue(eventID, out SUBehavioursData value))
            {
                value.RunWithParams(eleData, keyCode);
            }

        }
#endif

        

#if SURew

        public static void RunEventBehaviourParams(this DictEvents events, SUElementData eleData, SUEvent.Type_ID eventID, InputActionEventData eventData)
        {

            if (events.TryGetValue(eventID, out SUBehavioursData value))
            {
                value.RunWithParams(eleData, eventData);
            }
        }

#endif

        public static void RunEventBehaviourParams(this DictEvents events, SUElementData eleData, SUEvent.Type_ID eventID,string stringVal)
        {

            if (events.TryGetValue(eventID, out SUBehavioursData value))
            {
                value.RunWithParams(eleData,stringVal);
            }
        }

    }
}

