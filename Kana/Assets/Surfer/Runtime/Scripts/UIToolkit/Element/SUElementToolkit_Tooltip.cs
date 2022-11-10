#if UNITY_2021_2_OR_NEWER


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    public partial class SUElementToolkit
    {
        SUTooltipHandlerData _tooltipHandler = default;

        void CheckTooltipTypeForMyStateEnter()
        {
            if(!_elementData.IsTooltip())
                return;

            if (_tooltipHandler == null)
            {
                _tooltipHandler = new SUTooltipHandlerData();
                _tooltipHandler.AddTooltip(_elementData);
            }

            _tooltipHandler.Show();

        }

        void CheckTooltipTypeForMyStateExit()
        {
            if (!_elementData.IsTooltip())
                return;
            if (_tooltipHandler == null)
                return;

            _tooltipHandler.Hide();
            
        }


        public void ResetTooltip()
        {
            if(_tooltipHandler == null)
                return;
                
            _tooltipHandler.ResetTooltip();
        }
    }
}

#endif