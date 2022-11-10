using UnityEditor;
using UnityEngine;

namespace Surfer
{


    [CustomPropertyDrawer(typeof(SUStatesData))]
    public class SUStatesDataDrawer : PropertyDrawer
    {

        SerializedProperty _list = default;

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            position.height = SurferHelper.lineHeight;
            property.isExpanded = EditorGUI.Foldout(position,property.isExpanded,"States",true);

            if(property.isExpanded)
            {
                GetPropertyRelative(property);

                for (int i=0;i<_list.arraySize;i++)
                {
                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position,_list.GetArrayElementAtIndex(i),new GUIContent(""));

                    if(string.IsNullOrEmpty(_list.GetArrayElementAtIndex(i).FindPropertyRelative("_guid").stringValue)
                    && _list.arraySize > 1)
                    {
                        _list.DeleteArrayElementAtIndex(i);
                    }
                }

                if(_list.arraySize <=0 || !string.IsNullOrEmpty(_list.GetArrayElementAtIndex(_list.arraySize-1).FindPropertyRelative("_guid").stringValue) )
                {
                    _list.InsertArrayElementAtIndex(_list.arraySize);
                    _list.GetArrayElementAtIndex(_list.arraySize-1).FindPropertyRelative("_guid").stringValue = "";
                }
            }
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetPropertyRelative(property);

            return property.isExpanded ? (_list.arraySize+1) * SurferHelper.lineHeight : SurferHelper.lineHeight;
        }


        void GetPropertyRelative(SerializedProperty property)
        {
            _list = property.FindPropertyRelative("_list");
        }

    }

}