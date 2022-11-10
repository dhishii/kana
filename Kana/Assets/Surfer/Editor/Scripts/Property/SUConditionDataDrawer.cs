using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Surfer
{

    [CustomPropertyDrawer(typeof(SUConditionData))]
    public class SUConditionDataDrawer : PropertyDrawer
    {

        string[] _conditionsNames = default;
        int _idsIndex = default;
        SerializedProperty _name, _key, _vals = default;

        List<PathField> _fields = new List<PathField>();


        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
            if (property.propertyType == SerializedPropertyType.Generic)
            {
                GetProperties(property);

                _conditionsNames = property.IsFromToolkitCp() ? DefaultConditionChecks.UnifiedUIToolkitNames : DefaultConditionChecks.UnifiedCanvasNames;

                if(_name.stringValue.Equals(SurferHelper.Unset))
                {
                    _idsIndex = 0;
                }
                else
                {
                    _idsIndex = Mathf.Clamp(System.Array.IndexOf(_conditionsNames,ConditionChecksExtensions.GetNameFromUnion(_key.stringValue)),0,_conditionsNames.Length);
                }


                position.height = SurferHelper.lineHeight;
                _idsIndex = EditorGUI.Popup(position,!string.IsNullOrEmpty(label.text) ? "Condition" : label.text,_idsIndex,_conditionsNames);

                _name.stringValue =  _conditionsNames[_idsIndex];

                _key.stringValue = ConditionChecksExtensions.GetKeyFromUnion(_conditionsNames[_idsIndex],
                property.IsFromToolkitCp() ? SUCompatibility_ID.UIToolkit : SUCompatibility_ID.Canvas);


                if (_fields == null || _vals == null)
                    return;

                property.AddCustomUserFields(ref position, _fields,ref _vals);


            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetProperties(property);

            if (_fields != null)
            {
                var height = SurferHelper.lineHeight;
                
                foreach (var item in _fields)
                {
                    if(item.Field_ID == PathFieldType_ID.SerializedField)
                    {
                        var propField = property.FindPropertyRelative(item.SerializedFieldName);
                        
                        if(propField != null)
                            height += EditorGUI.GetPropertyHeight(propField);
                    }
                    else
                        height += SurferHelper.lineHeight;
                }

                return height;

            }
            

            return SurferHelper.lineHeight;


        }


        void GetProperties(SerializedProperty property)
        {
            _name = property.FindPropertyRelative("_name");
            _key = property.FindPropertyRelative("_key");
            _vals = property.FindPropertyRelative("_fieldsValues");
            _fields = ConditionChecksExtensions.GetFieldsList(_key.stringValue);
        }
    }


}