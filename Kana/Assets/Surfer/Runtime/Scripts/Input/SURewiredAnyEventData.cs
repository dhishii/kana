using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SURew
using Rewired;
#endif

namespace Surfer
{

    public struct SURewiredAnyEventData 
    {
        #if SURew
        public InputActionEventData ActionData { get; private set; }

        public SURewiredAnyEventData(InputActionEventData actionData)
        {
            ActionData = actionData;
        }
        #endif
    }

}


