using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


#if UNITY_2021_2_OR_NEWER

using UnityEngine.UIElements;


namespace Surfer
{
    [System.Serializable]
    public class SUTKQueryData
    {

        public enum Availability_ID
        {
            Now,
            Later
        }
        
        [SerializeField]
        string _tkName = default;
        public string TKName { get => _tkName; }

        public bool HasQuerableTkName
        {
            get
            {
                if (string.IsNullOrEmpty(TKName))
                    return false;
                if (string.IsNullOrWhiteSpace(TKName))
                    return false;

                return true;
            }
        }

        [SerializeField]
        Availability_ID _availability = default;
        public Availability_ID Availability { get => _availability; }

        [SerializeField]
        UIDocument _doc = default;
        public UIDocument Doc { get=> _doc ; }

        public const string kDocRootName = "<Root>";
        public const string kChooseEleName = "<Specify Name>";
        public const string kEventElementName = "<Event's Element>";

        public bool IsEventsElement
        {
            get
            {
                return TKName.Equals(kEventElementName);
            }
        }

        public void GetElements(System.Action<VisualElement> OnElement)
        {
            if(_doc == null)
                return;

            if(IsEventsElement)
                return;

            if (TKName.Equals(kDocRootName))
            {
                OnElement?.Invoke(_doc.rootVisualElement);
                return;
            }

            _doc.rootVisualElement.Query(name: TKName).ForEach((item)=>
            {
                OnElement?.Invoke(item);
            });

        }

    }

}

#endif
