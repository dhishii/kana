#if SUVis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Surfer
{
    [UnitTitle("Load Scene")]
    [UnitCategory("Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class LoadSceneUnit: BaseLoadSceneUnit
    {
        protected override void OnInput(Flow flow)
        {
            base.OnInput(flow);
            SurferManager.I.LoadScene(SceneName,Delay,Additive);
        }
    }
}


#endif