#if SUVis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Surfer
{
    [UnitTitle("Toggle State")]
    [UnitCategory("Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class ToggleStateUnit: BaseStateUnit
    {
        protected override void OnInput()
        {
            SurferManager.I.TogglePlayerState(PlayerID,StateName,Version,Delay);
        }
    }
}


#endif