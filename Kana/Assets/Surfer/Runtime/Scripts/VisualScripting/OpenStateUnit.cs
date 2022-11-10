#if SUVis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


namespace Surfer
{
    [UnitTitle("Open State")]
    [UnitCategory("Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class OpenStateUnit: BaseStateUnit
    {
        protected override void OnInput()
        {
            SurferManager.I.OpenPlayerState(PlayerID,StateName,Version,Delay);
        }
    }
}


#endif