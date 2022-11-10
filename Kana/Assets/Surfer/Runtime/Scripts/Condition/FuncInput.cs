using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;
#endif

namespace Surfer
{
    /// <summary>
    /// Input parameters of a custom condition or reaction
    /// </summary>
    public struct FuncInput
    {

        public GameObject gameObj { get; private set; }
        public SUFieldValuesData fieldsValues { get; private set; }
        public SUReactionData reactionData { get; private set; }
        public SUConditionData conditionData { get; private set; }
        public SUElementData elementData { get; private set; }
        public object eventData { get; private set; }

        #if UNITY_2021_2_OR_NEWER
        public VisualElement visualElement { get; private set; }
        #endif

        public FuncInput(SUElementData eleData,SUReactionData data,object evtData)
        {
            gameObj = eleData.ObjOwner;
            fieldsValues = data.FieldsValues;
            reactionData = data;
            conditionData = null;
            eventData = evtData;
            elementData = eleData;

#if UNITY_2021_2_OR_NEWER
            visualElement = eleData.VElement;
#endif
        }

        public FuncInput(SUElementData eleData,SUConditionData data,object evtData)
        {
            gameObj = eleData.ObjOwner;
            fieldsValues = data.FieldsValues;
            conditionData = data;
            reactionData = null;
            eventData = evtData;
            elementData = eleData;

#if UNITY_2021_2_OR_NEWER
            visualElement = eleData.VElement;
#endif

        }

    }
}


