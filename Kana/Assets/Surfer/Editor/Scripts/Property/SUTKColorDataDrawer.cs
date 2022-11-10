#if UNITY_2021_2_OR_NEWER

using UnityEditor;
using UnityEngine;


namespace Surfer
{
    [CustomPropertyDrawer(typeof(SUTKColorData))]
    public class SUTKColorDataDrawer : SUAnimationDataDrawer
    {

        SerializedProperty _from, _to = default;
        protected override bool OnCanUseDefaultModes(ref Rect position, SerializedProperty property)
        {
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _from,new GUIContent("From"));
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _to, new GUIContent("To"));

            return false;
        }

        

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetProperties(property);

            if (!property.isExpanded)
                return SurferHelper.lineHeight;

            float baseHeight = base.GetPropertyHeight(property,label);

            return baseHeight - SurferHelper.lineHeight ;

        }

        protected override bool IsNone()
        {
            return false;
        }

        void GetProperties(SerializedProperty prop)
        {
            _from = prop.FindPropertyRelative("_from");
            _to = prop.FindPropertyRelative("_to");
        }

    }
}



#endif
