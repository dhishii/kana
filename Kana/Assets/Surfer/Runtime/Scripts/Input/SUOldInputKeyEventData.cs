using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{

    public struct SUOldInputKeyEventData 
    {
        public KeyCode Key { get; private set; }

        public SUOldInputKeyEventData(KeyCode kCode)
        {
            Key = kCode;
        }
    }

}


