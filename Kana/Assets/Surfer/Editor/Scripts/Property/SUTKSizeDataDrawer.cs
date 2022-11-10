#if UNITY_2021_2_OR_NEWER

using UnityEditor;
using UnityEngine;


namespace Surfer
{
    [CustomPropertyDrawer(typeof(SUTKSizeData))]
    public class SUTKSizeDataDrawer : SUAnimationDataDrawer
    {

        SerializedProperty _fromWidth, _toWidth, _fromHeight, _toHeight = default;
        GUIStyle _boldStyle = default;

        protected override bool OnCanUseDefaultModes(ref Rect position, SerializedProperty property)
        {
            _boldStyle = new GUIStyle();
            _boldStyle.fontStyle = FontStyle.Bold;
            _boldStyle.normal.textColor = Color.white;

            //from 
            position.y += SurferHelper.lineHeight;
            EditorGUI.LabelField(position,"From",_boldStyle);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _fromWidth);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _fromHeight);

            //to
            position.y += SurferHelper.lineHeight;
            EditorGUI.LabelField(position,"To",_boldStyle);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _toWidth);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _toHeight);

            return false;
        }

        

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetProperties(property);

            if (!property.isExpanded)
                return SurferHelper.lineHeight;

            float baseHeight = base.GetPropertyHeight(property,label);

            return baseHeight + SurferHelper.lineHeight*3 ;

        }

        protected override bool IsNone()
        {
            return false;
        }

        void GetProperties(SerializedProperty prop)
        {
            _fromWidth = prop.FindPropertyRelative("_fromWidth");
            _toWidth = prop.FindPropertyRelative("_toWidth");

            _fromHeight = prop.FindPropertyRelative("_fromHeight");
            _toHeight = prop.FindPropertyRelative("_toHeight");
        }

    }
}



#endif
