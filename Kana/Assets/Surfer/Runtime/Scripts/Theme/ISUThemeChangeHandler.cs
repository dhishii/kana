using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    public interface ISUThemeChangeHandler 
    {
        void OnThemeChanged(SUThemeChangeEventData evtData);
    }
}

