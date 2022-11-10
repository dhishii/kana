#if SUVis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Surfer
{
    [UnitTitle("Unload Scene Async")]
    [UnitCategory("Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class UnloadSceneAsyncUnit: BaseSceneUnit
    {
        protected override void OnInput(Flow flow)
        {
            SurferManager.I.UnloadSceneAsync(SceneName,Delay);
        }
    }
}


#endif
