#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DictEvents = Surfer.SUElement.DictEvents;


namespace Surfer
{
    [System.Serializable]
    public partial class SUElementToolkit 
    {

        [SerializeField]
        SUElementData _elementData = default;
        public SUElementData ElementData { get => _elementData; }


        [SerializeField]
        SUBehavioursData _behaviours = default;
        public SUBehavioursData Behaviours { get => _behaviours; }


        [SerializeField]
        DictEvents _events = new DictEvents();
        public DictEvents Events { get => _events;}

        public string MySceneName { get; set; }


        public void Initialize()
        {
            if(ElementData.VElement == null)
                return;

            CheckSceneEvents();
            CheckStateEvents();
            CheckCustomEvents();
            CheckOrientationEvents();
            CheckInput();

            CheckToggle();
            CheckBuildVersion();
            CheckDropdown();
            CheckSlider();
            CheckScroll();
            CheckInputField();
            CheckUIGenerics();

        }

        public void ResetAll()
        {


            ResetOrientationEvents();
            ResetCustomEvents();
            ResetSceneEvents();
            ResetStateEvents();

            ResetToggle();
            ResetDropdown();
            ResetSlider();
            ResetScroll();
            ResetInputField();
            ResetTooltip();

            _elementData.HandleOnDestroy();

        }
    }
}


#endif