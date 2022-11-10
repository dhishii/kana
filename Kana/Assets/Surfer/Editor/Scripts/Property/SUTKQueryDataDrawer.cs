using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2021_2_OR_NEWER

using UnityEditor;
using UnityEngine.UIElements;


namespace Surfer
{

    [CustomPropertyDrawer(typeof(SUTKQueryData))]
    public class SUTKQueryDataDrawer : PropertyDrawer
    {
        SerializedProperty _tkName, _availability, _doc = default;
        int _tkNamesIndex = default;

        UIDocument _docCp = default;

        bool _calledFromEvents = default;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;

            _docCp = _doc.objectReferenceValue as UIDocument;

            if(_docCp == null)
            {
                _docCp = property.GetUIDocument();
                _doc.objectReferenceValue = _docCp;
            }

            if(_docCp == null)
                return;

            _calledFromEvents = !property.propertyPath.Contains("elementData");

            var list = _docCp.GetElementsNames();
            list.Insert(0, SUTKQueryData.kDocRootName);
            list.Insert(list.Count, SUTKQueryData.kChooseEleName);

            if(_calledFromEvents)
                list.Insert(0, SUTKQueryData.kEventElementName);

            if(EleDataAvailability == SUTKQueryData.Availability_ID.Now)
            {
                int idxName = 0;

                if(!string.IsNullOrEmpty(_tkName.stringValue))
                {
                    idxName = list.IndexOf(_tkName.stringValue);
                }
                else
                {
                    idxName = _calledFromEvents ? 0 : list.Count - 1;
                }
                
                _tkNamesIndex = idxName >= 0 ? idxName : list.Count - 1;
            }
            else
            {
                _tkNamesIndex = list.Count - 1;
            }

            _tkNamesIndex = EditorGUI.Popup(position,"Element",_tkNamesIndex, list.ToArray());
            position.y += SurferHelper.lineHeight;

            if (_tkNamesIndex >= 0 && _tkNamesIndex < list.Count)
            {
                var nameChosen = list[_tkNamesIndex];

                if(nameChosen.Equals(SUTKQueryData.kChooseEleName))
                {
                    EditorGUI.PropertyField(position,_tkName, new GUIContent(" "));
                    position.y += SurferHelper.lineHeight;
                    SetAvailableLater();
                }
                else
                {
                    _tkName.stringValue = list[_tkNamesIndex];
                    SetAvailableNow();
                }
            }

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetPropertyRelatives(property);

            if (EleDataAvailability == SUTKQueryData.Availability_ID.Now)
                return SurferHelper.lineHeight ;
            else
                return SurferHelper.lineHeight * 2;
        }

        void SetAvailableNow()
        {
            _availability.enumValueIndex = (int)SUTKQueryData.Availability_ID.Now;
        }

        void SetAvailableLater()
        {
            if(EleDataAvailability != SUTKQueryData.Availability_ID.Now)
                return;

            _availability.enumValueIndex = (int)SUTKQueryData.Availability_ID.Later;
        }

        SUTKQueryData.Availability_ID EleDataAvailability
        {
            get
            {
                if (_availability == null)
                    return SUTKQueryData.Availability_ID.Now;

                return (SUTKQueryData.Availability_ID)_availability.enumValueIndex;
            }
        }

        void GetPropertyRelatives(SerializedProperty property)
        {
            _tkName = property.FindPropertyRelative("_tkName");
            _availability = property.FindPropertyRelative("_availability");
            _doc = property.FindPropertyRelative("_doc");
        }


    }

}


#endif