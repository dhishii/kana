using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DictEvents = Surfer.SUElement.DictEvents;


namespace Surfer
{
    public class SUOrientationHandlerData : ISUOrientationHandler
    {

        DictEvents _events = default;
        SUElementData _eleData = default;

        public void AddInterface(SUElementData eleData,DictEvents events)
        {
            if(events == null)
                return;
            if(eleData == null)
                return;

            if(events.ContainsKey(SUEvent.Type_ID.Orientation_ToLandscapeLeft)
            || events.ContainsKey(SUEvent.Type_ID.Orientation_ToLandscapeRight)
            || events.ContainsKey(SUEvent.Type_ID.Orientation_ToPortrait)
            || events.ContainsKey(SUEvent.Type_ID.Orientation_ToPortraitUpsideDown))
            {
                SUOrientationManager.I?.RegisterOrientationEvent(this);
                _events = events;
                _eleData = eleData;
            }

        }

        public void OnOrientationChanged(SUOrientationInfo info)
        {
            if(_events == null)
                return;
            if(_eleData == null)
                return;

            if(info.ToOrientation == DeviceOrientation.Portrait)
            {
                _events.RunEventBehaviour(_eleData,SUEvent.Type_ID.Orientation_ToPortrait,info);
            }
            else if(info.ToOrientation == DeviceOrientation.PortraitUpsideDown)
            {
                _events.RunEventBehaviour(_eleData,SUEvent.Type_ID.Orientation_ToPortraitUpsideDown,info);
            }
            else if(info.ToOrientation == DeviceOrientation.LandscapeLeft)
            {
                _events.RunEventBehaviour(_eleData,SUEvent.Type_ID.Orientation_ToLandscapeLeft,info);
            }
            else if(info.ToOrientation == DeviceOrientation.LandscapeRight)
            {
                _events.RunEventBehaviour(_eleData,SUEvent.Type_ID.Orientation_ToLandscapeRight,info);
            }
        }

        public void ResetOrientationEvents()
        {
            if(_events == null)
                return;
            if(_eleData == null)
                return;

            SUOrientationManager.I?.UnregisterOrientationEvent(this);
        }
        
    }
}

