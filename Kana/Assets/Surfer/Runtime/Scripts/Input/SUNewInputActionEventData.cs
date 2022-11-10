using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{

    public struct SUNewInputActionEventData 
    {
        public string ActionName { get; private set; }

        public SUNewInputActionEventData(string action)
        {
            ActionName = action;
        }
    }

}


