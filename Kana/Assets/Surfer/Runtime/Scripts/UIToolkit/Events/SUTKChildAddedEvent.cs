#if UNITY_2021_2_OR_NEWER


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace  Surfer
{
    public class SUTKChildAddedEvent : EventBase<SUTKChildAddedEvent>
    {
        public VisualElement ElementAdded  { get; set; } = default;

    }

}

#endif

