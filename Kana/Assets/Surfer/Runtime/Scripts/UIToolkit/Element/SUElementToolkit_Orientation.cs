#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surfer
{
    public partial class SUElementToolkit 
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

#endif
