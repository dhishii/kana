using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Surfer
{

    public partial class SUElement
    {

        SUStateHandlerData _stateHandler = default;

        void CheckStateEvents()
        {
            _stateHandler = new SUStateHandlerData();
            _stateHandler.OnMyStateEnter += OnMyStateEnter;
            _stateHandler.OnMyStateExit += OnMyStateExit;
            _stateHandler.AddInterface(ElementData,Events);
        }

        void OnMyStateEnter()
        {
            CheckTooltipTypeForMyStateEnter();
            CheckDragTypeForMyStateEnter();
        }

        void OnMyStateExit()
        {
            CheckTooltipTypeForMyStateExit();
            CheckDragTypeForMyStateExit();
        }

        void ResetStateEvents()
        {
            if(_stateHandler == null)
                return;

            _stateHandler.OnMyStateEnter -= OnMyStateEnter;
            _stateHandler.OnMyStateExit -= OnMyStateExit;
            _stateHandler.ResetStates();
        }


    }

}

