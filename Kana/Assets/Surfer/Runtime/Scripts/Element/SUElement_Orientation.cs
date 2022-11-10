using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Surfer
{

    public partial class SUElement
    {

        SUOrientationHandlerData _orientationHandler = default;

        void CheckOrientationEvents()
        {
            _orientationHandler = new SUOrientationHandlerData();
            _orientationHandler.AddInterface(ElementData,Events);

        }
        
        void ResetOrientationEvents()
        {
            if(_orientationHandler == null)
                return;

            _orientationHandler.ResetOrientationEvents();

        }
        
        
    }


}
