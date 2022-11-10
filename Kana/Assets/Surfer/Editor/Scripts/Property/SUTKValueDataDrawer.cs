using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2021_2_OR_NEWER

using UnityEditor;
using UnityEngine.UIElements;


namespace Surfer
{

    [CustomPropertyDrawer(typeof(SUTKValueData))]
    public class SUTKValueDataDrawer : PropertyDrawer
    {
        SerializedProperty _value, _mode = default;

        bool IsNotUnchanged
        {
            get
            {
                return _mode.enumValueIndex != (int)SUTKValueData.Mode.Unchanged;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            position.height = EditorGUIUtility.singleLineHeight;

            GetPropertyRelatives(property);

            if(IsNotUnchanged)
            {
                EditorGUI.PropertyField(new Rect(position.xMax-100,position.y,100,position.height),_mode,GUIContent.none);
                EditorGUI.PropertyField(new Rect(position.xMin,position.y,position.width-100,position.height), _value,label);
            }
            else
            {
                EditorGUI.PropertyField(position, _mode,label);
            }

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetPropertyRelatives(property);

            return SurferHelper.lineHeight ;
        }
        void GetPropertyRelatives(SerializedProperty property)
        {
            _value = property.FindPropertyRelative("_value");
            _mode = property.FindPropertyRelative("_mode");
        }


    }

}


#endif