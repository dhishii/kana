#if SUVis


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Surfer
{
    [UnitTitle("On State Enter")]
    [UnitCategory("Events\\Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class StateEnterUnit : EventUnit<SUStateEventData>
    {
        [PortLabelHidden]
        [DoNotSerialize]
        public ValueInput StateName { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(SurferManager.kStateEnterVisualScripting);
        }
        protected override void Definition()
        {
            base.Definition();
            StateName = ValueInput<string>("State Name","");
        }

        protected override bool ShouldTrigger(Flow flow, SUStateEventData args)
        {
            if (args == null)
                return false;

            return args.StateName == flow.GetValue<string>(StateName);
        }
    }
}

#endif