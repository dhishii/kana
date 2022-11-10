#if SUVis


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


namespace Surfer
{


    [UnitTitle("On Orientation Changed")]
    [UnitCategory("Events\\Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class OrientationUnit : EventUnit<SUOrientationInfo>
    {
        [PortLabelHidden]
        [DoNotSerialize]
        public ValueInput Orientation { get; private set; }
        
        protected override bool register => true;
        
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(SUOrientationManager.kOrientationChanged);
        }
        protected override void Definition()
        {
            base.Definition();
            Orientation = ValueInput<DeviceOrientation>("Orientation",DeviceOrientation.Unknown);
        }

        protected override bool ShouldTrigger(Flow flow, SUOrientationInfo args)
        {
            return args.ToOrientation == flow.GetValue<DeviceOrientation>(Orientation);
        }
    }


}


#endif
