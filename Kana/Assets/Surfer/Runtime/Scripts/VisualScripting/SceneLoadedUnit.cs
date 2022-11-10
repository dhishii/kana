#if SUVis


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Surfer
{
    [UnitTitle("On Scene Loaded")]
    [UnitCategory("Events\\Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class SceneLoadedUnit : EventUnit<SUSceneLoadedEventData>
    {
        [PortLabelHidden]
        [DoNotSerialize]
        public ValueInput SceneName { get; private set; }
        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(SurferManager.kSceneLoadedVisualScripting);
        }
        protected override void Definition()
        {
            base.Definition();
            SceneName = ValueInput<string>("Scene Name","");
        }

        protected override bool ShouldTrigger(Flow flow, SUSceneLoadedEventData args)
        {
            if (args == null)
                return false;

            return args.SceneName == flow.GetValue<string>(SceneName);
        }
    }
}

#endif