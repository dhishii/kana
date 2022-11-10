using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

namespace Surfer
{
    [CustomEditor(typeof(SUThemable),true)]
    public class SUThemableEditor : Editor
    {
        SerializedProperty _fontGUID, _fontSizeGUID, _colorGUID, _spriteGUID, _spriteResGUID = default;
        int _fontIdx, _fontSizeIdx, _colorIdx, _spriteIdx, _spriteResIdx = default;

        string[] _values = new string[] { "test", SurferHelper.Unset };

        bool CanShowFontProperties
        {
            get
            {
                return targetGO.GetComponent<TMP_Text>() != null;
            }
        }


        bool CanShowColorProperty
        {
            get
            {
                return targetGO.GetComponent<Graphic>() != null;
            }
        }

        bool CanShowSpriteProperty
        {
            get
            {
                return targetGO.GetComponent<Image>() != null;
            }
        }

        bool CannotShowAnyProperty
        {
            get
            {
                return !CanShowColorProperty &&
                !CanShowFontProperties &&
                !CanShowSpriteProperty;
            }
        }

        GameObject targetGO
        {
            get
            {
                return (serializedObject.targetObject as Component).gameObject;
            }
        }

        private void OnEnable() {
            _fontGUID = serializedObject.FindProperty("_fontGUID");
            _fontSizeGUID = serializedObject.FindProperty("_fontSizeGUID");
            _colorGUID = serializedObject.FindProperty("_colorGUID");
            _spriteGUID = serializedObject.FindProperty("_spriteGUID");
            _spriteResGUID = serializedObject.FindProperty("_spriteResGUID");

            _fontIdx = SurferHelper.SO.Themes.GetFontGUIDIndex(_fontGUID.stringValue);
            _fontSizeIdx = SurferHelper.SO.Themes.GetFontSizeGUIDIndex(_fontSizeGUID.stringValue);
            _colorIdx = SurferHelper.SO.Themes.GetColorGUIDIndex(_colorGUID.stringValue);
            _spriteIdx = SurferHelper.SO.Themes.GetSpriteGUIDIndex(_spriteGUID.stringValue);
            _spriteResIdx = SurferHelper.SO.Themes.GetSpriteResGUIDIndex(_spriteResGUID.stringValue);
        }

        private void OnDisable() {
            
        }

        public override void OnInspectorGUI()
        {
            
            serializedObject.Update();

            if(CannotShowAnyProperty)
            {
                EditorGUILayout.LabelField("Add a Text or Image component \nto style this object!",EditorStyles.boldLabel,GUILayout.Height(EditorGUIUtility.singleLineHeight*2));
            }
            else
            {
                EditorGUILayout.LabelField("Choose the theme properties :",EditorStyles.boldLabel);
            }

            if (CanShowFontProperties)
            {
                _fontIdx = EditorGUILayout.Popup("Font",_fontIdx, SurferHelper.SO.Themes.GetFontsNames());
                _fontGUID.stringValue = SurferHelper.SO.Themes.GetFontGUIDFromIndex(_fontIdx);

                _fontSizeIdx = EditorGUILayout.Popup("Font size",_fontSizeIdx,SurferHelper.SO.Themes.GetFontSizesNames());
                _fontSizeGUID.stringValue = SurferHelper.SO.Themes.GetFontSizeGUIDFromIndex(_fontSizeIdx);
            }

            if(CanShowColorProperty)
            {
                _colorIdx = EditorGUILayout.Popup("Color",_colorIdx,SurferHelper.SO.Themes.GetColorsNames());
                _colorGUID.stringValue = SurferHelper.SO.Themes.GetColorGUIDFromIndex(_colorIdx);
            }
        
            if(CanShowSpriteProperty)
            {
                _spriteIdx = EditorGUILayout.Popup("Sprite",_spriteIdx, SurferHelper.SO.Themes.GetSpritesNames());
                _spriteGUID.stringValue = SurferHelper.SO.Themes.GetSpriteGUIDFromIndex(_spriteIdx);

                _spriteResIdx = EditorGUILayout.Popup("Resources Sprite",_spriteResIdx, SurferHelper.SO.Themes.GetSpritesResNames());
                _spriteResGUID.stringValue = SurferHelper.SO.Themes.GetSpriteResGUIDFromIndex(_spriteResIdx);
            }

            

            serializedObject.ApplyModifiedProperties();
        }



    }
}


