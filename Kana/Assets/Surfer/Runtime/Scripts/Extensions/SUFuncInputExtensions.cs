using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;
#endif

namespace Surfer
{
    public static class SUFuncInputExtensions
    {
        
#if UNITY_2021_2_OR_NEWER

        public static void GetQueryElements(this FuncInput input,System.Action<VisualElement> OnElement)
        {
            var query = input.reactionData != null ? input.reactionData.Query : input.conditionData.Query;

            if(query.IsEventsElement)
            {
                if(input.visualElement == null)
                return;

                OnElement?.Invoke(input.visualElement);
            }
            else
            {
                query.GetElements((element)=>
                {
                    OnElement?.Invoke(element);
                });
            }
        }

#endif
    }
}

