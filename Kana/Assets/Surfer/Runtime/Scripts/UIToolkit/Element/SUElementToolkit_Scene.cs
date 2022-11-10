#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surfer
{
    public partial class SUElementToolkit
    {
        SUSceneHandlerData _sceneHandler = default;

        void CheckSceneEvents()
        {

            _sceneHandler = new SUSceneHandlerData();
            _sceneHandler.OnSceneLoading += UpdateLoadingType;
            _sceneHandler.AddInterface(ElementData,Events,MySceneName);
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

#endif