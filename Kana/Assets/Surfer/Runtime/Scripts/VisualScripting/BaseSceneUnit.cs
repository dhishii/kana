#if SUVis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


namespace Surfer
{
    [UnitCategory("Surfer")]
    public class BaseSceneUnit: Unit
    {
        [PortLabelHidden]
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [PortLabelHidden]
        [DoNotSerialize]
        public ControlOutput outputTrigger;

        [DoNotSerialize] 
        public ValueInput sceneName;
        protected string SceneName = default;

        [DoNotSerialize] 
        public ValueInput delay;
        protected float Delay = default;

        protected override void Definition()
        {

            inputTrigger = ControlInput("inputTrigger", (flow) => 
            {
                SceneName = flow.GetValue<string>(sceneName);
                Delay = flow.GetValue<float>(delay);
                OnInput(flow);
                return outputTrigger; 
            });

            sceneName = ValueInput<string>("Scene", "");
            delay = ValueInput<float>("Delay", 0f);

            outputTrigger = ControlOutput("outputTrigger");
        }

        protected virtual void OnInput(Flow flow) { }
    }



    [UnitCategory("Surfer")]
    public class BaseLoadSceneUnit: BaseSceneUnit
    {

        [DoNotSerialize] 
        public ValueInput additive;
        protected bool Additive = default;

        protected override void Definition()
        {
            base.Definition();
            additive = ValueInput<bool>("Additive", false);
        }
        protected override void OnInput(Flow flow)
        {
            Additive = flow.GetValue<bool>(additive);
        }

    }

}




#endif