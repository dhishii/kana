using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{

    public interface ISURewiredActionHandler 
    {
        void OnRewiredAction(SURewiredActionEventData eventData);
    }

}