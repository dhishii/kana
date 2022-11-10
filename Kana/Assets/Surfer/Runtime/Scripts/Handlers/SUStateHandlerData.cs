using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DictEvents = Surfer.SUElement.DictEvents;


namespace Surfer
{

    public class SUStateHandlerData : ISUStateEnterHandler, ISUStateExitHandler
    {

        DictEvents _events = default;
        SUElementData _eleData = default;
        public event System.Action OnMyStateEnter;
        public event System.Action OnMyStateExit;

        public void AddInterface(SUElementData eleData,DictEvents events)
        {
            if(eleData == null)
                return;
            if(events == null)
                return;

            _events = events;
            _eleData = eleData;

            if (_events.TryGetValue(SUEvent.Type_ID.State_Enter, out var valueEnter))
            {
                for (int i = 0; i < valueEnter.Behaviours.Count; i++)
                {
                    SurferManager.I.RegisterStateEnter(this, valueEnter.Behaviours[i].Event.StatesData.AllNamesArray);
                }

            }
            if (_events.TryGetValue(SUEvent.Type_ID.State_Exit, out var valueExit))
            {
                for (int i = 0; i < valueExit.Behaviours.Count; i++)
                {
                    SurferManager.I.RegisterStateExit(this, valueExit.Behaviours[i].Event.StatesData.AllNamesArray);
                }

            }

            if (_events.ContainsKey(SUEvent.Type_ID.State_MyStateExit)
                || _eleData.IsTooltip() || _eleData.IsDrag())
            {
                SurferManager.I.RegisterStateExit(this, _eleData.StateName);
            }

            if (_events.ContainsKey(SUEvent.Type_ID.State_MyStateEnter)
                || _eleData.IsTooltip() || _eleData.IsDrag())
            {
                SurferManager.I.RegisterStateEnter(this, _eleData.StateName);
            }
        }


        public void OnSUStateEnter(SUStateEventData eventInfo)
        {
            if(_eleData == null)
                return;
            if(_events == null)
                return;

            _events.RunEventBehaviourParamsState(_eleData,SUEvent.Type_ID.State_Enter,eventInfo);


            if (_eleData.StateName == eventInfo.StateName && _eleData.PlayerID == eventInfo.PlayerID)
            {
                _events.RunEventBehaviourParamsState(_eleData,SUEvent.Type_ID.State_MyStateEnter, eventInfo, true);

                OnMyStateEnter?.Invoke();
            }
            
                
        }

        public void OnSUStateExit(SUStateEventData eventInfo)
        {
            if(_eleData == null)
                return;
            if(_events == null)
                return;


            _events.RunEventBehaviourParamsState(_eleData,SUEvent.Type_ID.State_Exit, eventInfo);

            if (_eleData.StateName == eventInfo.StateName && _eleData.PlayerID == eventInfo.PlayerID)
            {
                _events.RunEventBehaviourParamsState(_eleData,SUEvent.Type_ID.State_MyStateExit, eventInfo, true);

                OnMyStateExit?.Invoke();
            }
        }


        public void ResetStates()
        {
            if(_eleData == null)
                return;
            if(_events == null)
                return;


            if (_events.TryGetValue(SUEvent.Type_ID.State_Enter, out var valueEnter))
            {
                for (int i = 0; i < valueEnter.Behaviours.Count; i++)
                {
                    SurferManager.I.UnregisterStateEnter(this, valueEnter.Behaviours[i].Event.StatesData.AllNamesArray);
                }

            }
            if (_events.TryGetValue(SUEvent.Type_ID.State_Exit, out var valueExit))
            {
                for (int i = 0; i < valueExit.Behaviours.Count; i++)
                {
                    SurferManager.I.UnregisterStateExit(this, valueExit.Behaviours[i].Event.StatesData.AllNamesArray);
                }

            }
            if (_events.ContainsKey(SUEvent.Type_ID.State_MyStateExit))
            {
                SurferManager.I.UnregisterStateExit(this, _eleData.StateName);
            }

            if (_events.ContainsKey(SUEvent.Type_ID.State_MyStateEnter))
            {
                SurferManager.I.UnregisterStateEnter(this, _eleData.StateName);
            }

        }
    }

}

