#if SUVis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Surfer
{
    [UnitTitle("Set Active Scene")]
    [UnitCategory("Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class SetActiveSceneUnit: BaseSceneUnit
    {
        protected override void OnInput(Flow flow)
        {
            SurferManager.I.SetActiveScene(SceneName,Delay);
        }
    }
}


#endif