#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#if SUNew
using UnityEngine.InputSystem;
using InputAction = UnityEngine.InputSystem.InputAction;
#endif

#if SURew
using Rewired;
#endif

namespace Surfer
{
    public partial class SUElementToolkit 
    {

        SUInputHandlerData _inputHandler = default;

        public void CheckInput()
        {

            _inputHandler = new SUInputHandlerData();
            _inputHandler.AddInterface(ElementData,Events);

        }

    }
}

#endif
