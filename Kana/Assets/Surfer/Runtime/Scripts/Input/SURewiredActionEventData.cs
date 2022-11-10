using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SURew
using Rewired;
#endif

namespace Surfer
{

    public struct SURewiredActionEventData 
    {
        #if SURew
        public InputActionEventData ActionData { get; private set; }

        public SURewiredActionEventData(InputActionEventData actionData)
        {
            ActionData = actionData;
        }
        #endif
    }

}


