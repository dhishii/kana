using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{

    public struct SUOldInputActionEventData 
    {
        public string ActionName { get; private set; }

        public SUOldInputActionEventData(string action)
        {
            ActionName = action;
        }
    }

}


