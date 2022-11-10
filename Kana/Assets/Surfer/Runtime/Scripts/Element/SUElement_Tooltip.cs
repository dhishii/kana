using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SUNew
using UnityEngine.InputSystem;
#endif

namespace Surfer
{

    public partial class SUElement
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

