#if UNITY_2021_2_OR_NEWER

using UnityEditor;
using UnityEngine;


namespace Surfer
{
    [CustomPropertyDrawer(typeof(SUTKPositionData))]
    public class SUTKPositionDataDrawer : SUAnimationDataDrawer
    {

        SerializedProperty _fromLeft, _fromRight, _fromTop, _fromBottom,
        _toLeft, _toRight, _toTop, _toBottom, _positionType = default;

        GUIStyle _boldStyle = default;
        protected override bool OnCanUseDefaultModes(ref Rect position, SerializedProperty property)
        {

            _boldStyle = new GUIStyle();
            _boldStyle.fontStyle = FontStyle.Bold;
            _boldStyle.normal.textColor = Color.white;

            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position,_positionType);

            //from 
            position.y += SurferHelper.lineHeight;
            EditorGUI.LabelField(position,"From",_boldStyle);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _fromLeft);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _fromRight);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _fromTop);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _fromBottom);

            //to
            position.y += SurferHelper.lineHeight;
            EditorGUI.LabelField(position,"To",_boldStyle);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _toLeft);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _toRight);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _toTop);
            position.y += SurferHelper.lineHeight;
            EditorGUI.PropertyField(position, _toBottom);

            return false;
        }

        

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetProperties(property);

            if (!property.isExpanded)
                return SurferHelper.lineHeight;

            float baseHeight = base.GetPropertyHeight(property,label);

            baseHeight += EditorGUI.GetPropertyHeight(_fromLeft);
            baseHeight += EditorGUI.GetPropertyHeight(_fromRight);
            baseHeight += EditorGUI.GetPropertyHeight(_fromTop);
            baseHeight += EditorGUI.GetPropertyHeight(_fromBottom);
            baseHeight += EditorGUI.GetPropertyHeight(_toLeft);
            baseHeight += EditorGUI.GetPropertyHeight(_toRight);
            baseHeight += EditorGUI.GetPropertyHeight(_toTop);
            baseHeight += EditorGUI.GetPropertyHeight(_toBottom);

            return baseHeight ;

        }

        protected override bool IsNone()
        {
            return false;
        }


        void GetProperties(SerializedProperty prop)
        {
            _fromLeft = prop.FindPropertyRelative("_fromLeft");
            _fromRight = prop.FindPropertyRelative("_fromRight");
            _fromTop = prop.FindPropertyRelative("_fromTop");
            _fromBottom = prop.FindPropertyRelative("_fromBottom");
            _toLeft = prop.FindPropertyRelative("_toLeft");
            _toRight = prop.FindPropertyRelative("_toRight");
            _toTop = prop.FindPropertyRelative("_toTop");
            _toBottom = prop.FindPropertyRelative("_toBottom");
            _positionType = prop.FindPropertyRelative("_positionType");
        }

    }
}



#endif
