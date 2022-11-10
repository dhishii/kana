using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Surfer
{
    [CustomEditor(typeof(SUInputIcon),true)]
    public class SUInputIconEditor : Editor
    {

        SerializedProperty _spriteGUID, _spriteResGUID = default;
        int _spriteIdx, _spriteResIdx = default;

        bool CanShowSpriteProperty
        {
            get
            {
                return targetGO.GetComponent<Image>() != null;
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
            _spriteGUID = serializedObject.FindProperty("_spriteGUID");
            _spriteResGUID = serializedObject.FindProperty("_spriteResGUID");

            _spriteIdx = SurferHelper.SO.InputIcons.GetSpriteGUIDIndex(_spriteGUID.stringValue);
            _spriteResIdx = SurferHelper.SO.InputIcons.GetSpriteResGUIDIndex(_spriteResGUID.stringValue);
        }

        public override void OnInspectorGUI()
        {
            
            serializedObject.Update();
        
            if(CanShowSpriteProperty)
            {
                EditorGUILayout.LabelField("Choose the input icon :",EditorStyles.boldLabel);

                _spriteIdx = EditorGUILayout.Popup("Sprite",_spriteIdx, SurferHelper.SO.InputIcons.GetSpritesNames());
                _spriteGUID.stringValue = SurferHelper.SO.InputIcons.GetSpriteGUIDFromIndex(_spriteIdx);

                _spriteResIdx = EditorGUILayout.Popup("Resources Sprite",_spriteResIdx, SurferHelper.SO.InputIcons.GetSpritesResNames());
                _spriteResGUID.stringValue = SurferHelper.SO.InputIcons.GetSpriteResGUIDFromIndex(_spriteResIdx);
            }
            else
            {
                EditorGUILayout.LabelField("Add an Image component \nto choose an Input Icon!",EditorStyles.boldLabel,GUILayout.Height(EditorGUIUtility.singleLineHeight*2));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

