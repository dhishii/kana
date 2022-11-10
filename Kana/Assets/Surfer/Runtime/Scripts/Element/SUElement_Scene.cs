using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Surfer
{

    public partial class SUElement 
    {

        SUSceneHandlerData _sceneHandler = default;

        void CheckSceneEvents()
        {

            _sceneHandler = new SUSceneHandlerData();
            _sceneHandler.OnSceneLoading += UpdateLoadingType;
            _sceneHandler.AddInterface(ElementData,Events,_mySceneName);

        }

        void ResetSceneEvents()
        {
            if(_sceneHandler == null)
                return;

            _sceneHandler.OnSceneLoading -= UpdateLoadingType;
            _sceneHandler.ResetSceneEvents();

        }




    }

}


