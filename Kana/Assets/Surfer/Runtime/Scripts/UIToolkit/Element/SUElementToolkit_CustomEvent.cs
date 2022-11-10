#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Surfer
{
    public partial class SUElementToolkit 
    {

        SUCustomEventHandlerData _customEventHandler = default;

        void CheckCustomEvents()
        {

            _customEventHandler = new SUCustomEventHandlerData();
            _customEventHandler.AddInterface(ElementData,Events);

        }

        void ResetCustomEvents()
        {
            if(_customEventHandler == null)
                return;

            _customEventHandler.ResetCustomEvents();

        }
    }
}

#endif

