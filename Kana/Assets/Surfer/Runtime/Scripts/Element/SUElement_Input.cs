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

    public partial class SUElement
    {

        SUInputHandlerData _inputHandler = default;


        public void CheckInput()
        {

            _inputHandler = new SUInputHandlerData();
            _inputHandler.AddInterface(ElementData,Events);

        }


    }


}


