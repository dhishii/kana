#if SUVis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Surfer
{
    [UnitTitle("Load Scene Async")]
    [UnitCategory("Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class LoadSceneAsyncUnit: BaseLoadSceneUnit
    {
        [DoNotSerialize] 
        public ValueInput autoActivate;
        protected bool AutoActivate = default;

        protected override void Definition()
        {
            base.Definition();
            autoActivate = ValueInput<bool>("Auto Activate", true);
        }

        protected override void OnInput(Flow flow)
        {
            base.OnInput(flow);
            AutoActivate = flow.GetValue<bool>(autoActivate);
            SurferManager.I.LoadSceneAsync(SceneName,Delay,Additive,AutoActivate);
        }
    }
}


#endif