using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{

    public interface ISUNewInputActionHandler 
    {
        void OnNewInputAction(SUNewInputActionEventData eventData);
    }

}