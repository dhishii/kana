#if SUVis


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Surfer
{
    [UnitTitle("On Scene Unloaded")]
    [UnitCategory("Events\\Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class SceneUnloadedUnit : EventUnit<SUSceneUnloadedEventData>
    {
        [PortLabelHidden]
        [DoNotSerialize]
        public ValueInput SceneName { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(SurferManager.kSceneUnloadedVisualScripting);
        }
        protected override void Definition()
        {
            base.Definition();
            SceneName = ValueInput<string>("Scene Name","");
        }

        protected override bool ShouldTrigger(Flow flow, SUSceneUnloadedEventData args)
        {
            if (args == null)
                return false;

            return args.SceneName == flow.GetValue<string>(SceneName);
        }
    }
}


#endif