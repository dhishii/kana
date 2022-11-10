#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Surfer
{
    public partial class SUElementToolkit
    {
        public void UpdateLoadingType(SUSceneLoadingEventData eventInfo)
        {
            
            if (_elementData.Type == SUElementData.Type_ID.Loading_Text)
            {
                if(ElementData.VElement == null)
                    return;
                
                if(ElementData.VElement is Label)
                {
                    var lab = ElementData.VElement as Label;
                    lab.text = _elementData.StringVal + eventInfo.Progress + _elementData.StringVal2;
                }
            }

        }
    }
}

#endif
