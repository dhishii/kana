#if SUVis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


namespace Surfer
{

    [UnitCategory("Surfer")]
    public class BaseStateUnit : Unit
    {
        [PortLabelHidden]
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [PortLabelHidden]
        [DoNotSerialize]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput stateName;
        protected string StateName = default;

        [DoNotSerialize]
        public ValueInput version;
        protected int Version = default;

        [DoNotSerialize]
        public ValueInput playerID;
        protected int PlayerID = default;

        [DoNotSerialize]
        public ValueInput delay;
        protected float Delay = default;

        protected override void Definition()
        {

            inputTrigger = ControlInput("inputTrigger", (flow) =>
            {
                StateName = flow.GetValue<string>(stateName);
                Version = flow.GetValue<int>(version);
                PlayerID = flow.GetValue<int>(playerID);
                Delay = flow.GetValue<float>(delay);
                OnInput();
                return outputTrigger;
            });

            stateName = ValueInput<string>("State", "");
            version = ValueInput<int>("Version", 0);
            playerID = ValueInput<int>("PlayerID", 0);
            delay = ValueInput<float>("Delay", 0f);

            outputTrigger = ControlOutput("outputTrigger");
        }

        protected virtual void OnInput() { }
    }

}

#endif