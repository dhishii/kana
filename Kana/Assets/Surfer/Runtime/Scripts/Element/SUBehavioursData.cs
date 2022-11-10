using System.Collections;
using System.Collections.Generic;

#if SURew
using Rewired;
#endif
using UnityEngine;


namespace Surfer
{

    [System.Serializable]
    public class SUBehavioursData
    {

        [SerializeField]
        List<SUBehaviourData> _behaviours = new List<SUBehaviourData>();
        public List<SUBehaviourData> Behaviours { get => _behaviours; }
                
        public void ForEachBeh(System.Action<SUBehaviourData> OnBehaviour)
        {
            for (int i = 0; i < _behaviours.Count;i++)
            {
                OnBehaviour?.Invoke(_behaviours[i]);
            }
        }


        public void Run(SUElementData data,object evtData = null)
        {
            for(int i=0;i<_behaviours.Count;i++)
            {
                _behaviours[i].Run(data, evtData);
            }
        }



#if SUOld

        public void RunWithParams(SUElementData data, KeyCode keyVal)
        {


            for (int i = 0; i < _behaviours.Count; i++)
            {

                if (keyVal != _behaviours[i].Event.KeyCodeVal)
                    continue;

                _behaviours[i].Run(data);
            }


        }

#endif

        public void RunWithParams(SUElementData data, string stringVal)
        {

            for (int i = 0; i < _behaviours.Count; i++)
            {

                if (stringVal != _behaviours[i].Event.StringVal)
                    continue;

                _behaviours[i].Run(data);
            }

        }

        public void RunWithParamsCustomEvent(SUElementData data, SUCustomEventEventData cEvent)
        {

            for (int i = 0; i < _behaviours.Count; i++)
            {

                if (!_behaviours[i].Event.CEventsData.AllNames.Contains(cEvent.Name))
                    continue;

                _behaviours[i].Run(data,eventName : cEvent.Name,cEvent);
            }

        }

        public void RunWithParamsScene(SUElementData data, string stringVal,object evtData = null)
        {


            for (int i = 0; i < _behaviours.Count; i++)
            {

                if (!_behaviours[i].Event.ScenesData.AllNames.Contains(stringVal) )
                    continue;

                _behaviours[i].Run(data,evtData);
            }

        }


        public void RunWithParamsState(SUElementData data, SUStateEventData eventData, bool onlyVersionCheck = false)
        {

            List<string> _list = default;

            for (int i = 0; i < _behaviours.Count; i++)
            {

                
                if(!onlyVersionCheck)
                {
                    
                    //playerID check 
                    var pID = _behaviours[i].Event.IntVal2;

                    if(pID == SurferHelper.kNestedPlayerID)
                    {
                        pID = data.ObjOwner.GetObjectStatePlayerID(true);
                    }

                    if (pID != eventData.PlayerID)
                        continue;

                    
                    //state name check
                    _list = _behaviours[i].Event.StatesData.AllNames;

                    if (!_list.Contains(eventData.StateName))
                        continue;
                }

                if (_behaviours[i].Event.IntVal != eventData.Version &&
                    _behaviours[i].Event.IntVal != SurferHelper.kWhateverVersion && eventData.Version != SurferHelper.kWhateverVersion)
                    continue;

                _behaviours[i].Run(data,eventData);
            }

        }


#if SURew

        public void RunWithParams(SUElementData data, InputActionEventData eventData)
        {


            for (int i = 0; i < _behaviours.Count; i++)
            {
                if (eventData.updateLoop != _behaviours[i].Event.UpdateLoop)
                    continue;
                if (eventData.eventType != _behaviours[i].Event.EventType)
                    continue;
                if (eventData.playerId != _behaviours[i].Event.IntVal && _behaviours[i].Event.IntVal != 0)
                    continue;
                if (eventData.actionName != _behaviours[i].Event.StringVal)
                    continue;

                _behaviours[i].Run(data,eventData);
            }

        }

#endif



        public void AddCustomReaction(SUReactionData rData,SUEvent.Type_ID eventID)
        {

            SUBehaviourData bhv = new SUBehaviourData();
            bhv.AddCustomReaction(rData);
            bhv.SetEvent(eventID);

            _behaviours.Add(bhv);
        }


    }

}




