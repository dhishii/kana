#if UNITY_2021_2_OR_NEWER


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Surfer
{
    public class SUTKChildRemovedEvent : EventBase<SUTKChildRemovedEvent>
    {

        public VisualElement ElementRemoved { get; set; }

        protected override void PreDispatch(IPanel panel)
        {
            base.PreDispatch(panel);
        }
    }
}

#endif
