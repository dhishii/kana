#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Surfer
{
    
    public partial class SUElementToolkit
    {

        void CheckBuildVersion()
        {

            if (ElementData.Type != SUElementData.Type_ID.BuildVersion_Text)
                return;

            if (ElementData.VElement == null)
                return;
            
            if (ElementData.VElement is Label)
            {
                var lab = ElementData.VElement as Label;
                lab.text = SurferHelper.GetVersion();
            }
            
        }
    }

}

#endif
