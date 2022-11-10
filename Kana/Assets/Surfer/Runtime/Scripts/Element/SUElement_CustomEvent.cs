using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{

    public partial class SUElement
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
