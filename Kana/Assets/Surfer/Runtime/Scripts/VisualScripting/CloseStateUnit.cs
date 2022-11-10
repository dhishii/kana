#if SUVis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Surfer
{
    [UnitTitle("Close State")]
    [UnitCategory("Surfer")]
    [TypeIcon(typeof(SurferManager))]
    public class CloseStateUnit: BaseStateUnit
    {
        protected override void OnInput()
        {
            SurferManager.I.ClosePlayerState(PlayerID,StateName,Version,Delay);
        }
    }
}


#endif