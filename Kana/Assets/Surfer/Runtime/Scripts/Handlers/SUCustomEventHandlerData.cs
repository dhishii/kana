using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DictEvents = Surfer.SUElement.DictEvents;


namespace Surfer
{

    public class SUCustomEventHandlerData : ISUCustomEventHandler
    {
        
        DictEvents _events = default;
        SUElementData _eleData = default;

        public void AddInterface(SUElementData eleData,DictEvents events)
        {
            if(eleData == null)
                return;
            if(events == null)
                return;

            if(events.TryGetValue(SUEvent.Type_ID.MyCustomEvent, out var valueCustom))
            {
                for (int i = 0; i < valueCustom.Behaviours.Count; i++)
                {
                    SurferManager.I.RegisterCustomEvent(this, valueCustom.Behaviours[i].Event.CEventsData.AllNamesArray);
                }

                _eleData = eleData;
                _events = events;
            }

        }

        public void OnSUCustomEvent(SUCustomEventEventData eventInfo)
        {
            _events.RunCustomEventBehaviour(_eleData,eventInfo);
        }


        public void ResetCustomEvents()
        {
            if(_eleData == null)
                return;
            if(_events == null)
                return;
            
            if (_events.TryGetValue(SUEvent.Type_ID.MyCustomEvent, out var valueCustom))
            {
                for (int i = 0; i < valueCustom.Behaviours.Count; i++)
                {
                    SurferManager.I.UnregisterCustomEvent(this, valueCustom.Behaviours[i].Event.CEventsData.AllNamesArray);
                }
            }
        }
    }

}

