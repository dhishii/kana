using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Surfer
{

    [CustomPropertyDrawer(typeof(SUElementData))]
    public class SUElementDataDrawer : PropertyDrawer
    {
        SerializedProperty _type, _persistent, _stateData, _statesData, _closeMode,
            _closeDelay, _vector, _stackable, _stackDelay, _stringVal, _stringVal2,
            _playerID, _indVisualData, _hbVisualData, _groupStates,
            _intVal, _queryData;


        bool IsCustomList
        {
            get
            {
                return _closeMode.enumValueIndex == (int)SUElementData.StateCloseMode_ID.CustomList
                    || _closeMode.enumValueIndex == (int)SUElementData.StateCloseMode_ID.SiblingsAndCustomList;
            }
        }

        bool IsCloseModeOn
        {
            get
            {
                return _closeMode.enumValueIndex != (int)SUElementData.StateCloseMode_ID.None;
            }
        }

        SUElementData.Type_ID EleDataType
        {
            get
            {
                if (_type == null)
                    return SUElementData.Type_ID.Normal;

                return (SUElementData.Type_ID)_type.enumValueIndex;
            }
        }

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Generic)
            {

                position.height = EditorGUIUtility.singleLineHeight;

                property.ResetToolkitIndentation(()=>
                {

#if UNITY_2021_2_OR_NEWER

                    EditorGUI.PropertyField(position,_queryData);
                    position.y += EditorGUI.GetPropertyHeight(_queryData);
#endif
               

                });

                _type.enumValueIndex = (int)(SUElementData.Type_ID)EditorGUI.EnumPopup(position,new GUIContent("Type"),(SUElementData.Type_ID)_type.enumValueIndex,(value)=>
                {
                    if(property.IsFromToolkitCp())
                    {
                        var eleTp = (SUElementData.Type_ID)value;
                        return !eleTp.IsDrag() && !eleTp.IsGroup() 
                        && eleTp != SUElementData.Type_ID.Loading_Image
                        && eleTp != SUElementData.Type_ID.Selection_Indicator
                        && eleTp != SUElementData.Type_ID.HealthBar
                        && eleTp != SUElementData.Type_ID.Indicator;
                    }
                    else
                    {
                        return true;
                    }
                });

                if (EleDataType.IsState())
                {

                    if(EleDataType == SUElementData.Type_ID.Tooltip_State)
                    {

                        position.y += SurferHelper.lineHeight;
                        EditorGUI.PropertyField(position, _vector, new GUIContent("Offset Factor"));

                    }

                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _playerID);

                    if(!property.IsFromToolkitCp())
                    {
                        position.y += SurferHelper.lineHeight;
                        EditorGUI.PropertyField(position, _persistent);
                    }

                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _stackable);

                    if(_stackable.boolValue == true)
                    {
                        position.y += SurferHelper.lineHeight;
                        EditorGUI.PropertyField(position, _stackDelay);
                    }

                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _stateData);
                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _closeMode);



                    if (IsCloseModeOn)
                    {

                        position.y += SurferHelper.lineHeight;
                        EditorGUI.PropertyField(position, _closeDelay,new GUIContent("Close Delay"));

                        if (IsCustomList)
                        {
                            position.y += SurferHelper.lineHeight;
                            EditorGUI.PropertyField(position, _statesData);
                        }

                    }

                }
                else if(EleDataType == SUElementData.Type_ID.Loading_Text)
                {
                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _stringVal, new GUIContent("Prefix"));
                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _stringVal2, new GUIContent("Suffix"));
                }
                else if(EleDataType == SUElementData.Type_ID.Selection_Indicator)
                {

                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _playerID);

                }
                else if (EleDataType == SUElementData.Type_ID.Indicator)
                {

                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _indVisualData,true);

                }
                else if (EleDataType == SUElementData.Type_ID.HealthBar)
                {

                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _hbVisualData, true);

                }
                else if (EleDataType.IsGroup())
                {

                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _playerID);
                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _intVal, new GUIContent("Skip"), true);
                    position.y += SurferHelper.lineHeight;
                    EditorGUI.PropertyField(position, _groupStates, true);

                }

            }
        }


        void GetPropertyRelatives(SerializedProperty property)
        {
            _type = property.FindPropertyRelative("_type");
            _persistent = property.FindPropertyRelative("_persistent");
            _stateData = property.FindPropertyRelative("_stateData");
            _statesData = property.FindPropertyRelative("_statesData");
            _closeMode = property.FindPropertyRelative("_closeMode");
            _closeDelay = property.FindPropertyRelative("_closeDelay");
            _vector = property.FindPropertyRelative("_vector");
            _stackable = property.FindPropertyRelative("_isStackable");
            _stackDelay = property.FindPropertyRelative("_stackDelay");
            _stringVal = property.FindPropertyRelative("_stringVal");
            _stringVal2 = property.FindPropertyRelative("_stringVal2");
            _playerID = property.FindPropertyRelative("_playerID");
            _indVisualData = property.FindPropertyRelative("_indVisualData");
            _hbVisualData = property.FindPropertyRelative("_hbVisualData");
            _groupStates = property.FindPropertyRelative("_groupStates");
            _intVal = property.FindPropertyRelative("_intVal");
#if UNITY_2021_2_OR_NEWER
            _queryData = property.FindPropertyRelative("_query");
#endif
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GetPropertyRelatives(property);

            float height = default;

#if UNITY_2021_2_OR_NEWER

            height += EditorGUI.GetPropertyHeight(_queryData);
      
#endif

            if (EleDataType.IsState())
            {

                if (EleDataType == SUElementData.Type_ID.Tooltip_State)
                {
                    height += SurferHelper.lineHeight;
                }

                height += SurferHelper.lineHeight*6;

                if (_stackable.boolValue == true)
                    height += SurferHelper.lineHeight;

                if (IsCloseModeOn)
                    height += SurferHelper.lineHeight;

                if (IsCustomList)
                    height += EditorGUI.GetPropertyHeight(_statesData);

            }
            else if (EleDataType == SUElementData.Type_ID.Loading_Text)
            {
                height += SurferHelper.lineHeight * 3;
            }
            else if (EleDataType == SUElementData.Type_ID.Selection_Indicator)
            {
                height += SurferHelper.lineHeight * 2;
            }
            else if (EleDataType == SUElementData.Type_ID.Indicator)
            {
                height += EditorGUI.GetPropertyHeight(_indVisualData) + SurferHelper.lineHeight;
            }
            else if (EleDataType == SUElementData.Type_ID.HealthBar)
            {
                height += EditorGUI.GetPropertyHeight(_hbVisualData) + SurferHelper.lineHeight;
            }
            else if (EleDataType.IsGroup())
            {
                height += EditorGUI.GetPropertyHeight(_groupStates);
                height += SurferHelper.lineHeight*3;
            }
            else
            {
                height += EditorGUIUtility.singleLineHeight;
            }

            return height;
        }

    }



}




