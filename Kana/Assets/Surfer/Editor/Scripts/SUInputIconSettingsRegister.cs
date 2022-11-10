using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;


namespace Surfer
{

    public static class SUInputIconSettingsRegister
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {

            Vector2 _scrollPos = default;

            string[] _platforms = default;
            string[] _chosenPlatforms = default;

            int _currentPlatformIdx = default;
            int _newPlatformIdx = default;

            bool _platformAlreadyExists = default;

            SUPlatform_ID _platformThatAlreadyExists = default;
            SUInputIconsData _inputIcons = default;

            float _deleteBtnWidth = SurferHelper.lineHeight * 3;

            Rect _buttonRect = default;

            return new SettingsProvider("Preferences/Surfer/Input Icons Manager", SettingsScope.User)
            {
                guiHandler = (searchContext) =>
                {

                    _platforms = System.Enum.GetNames(typeof(SUPlatform_ID));
                    _chosenPlatforms = SurferHelper.SO.GetInputIconsPlatformNames();

                    AddNewPlatformSection();

                    DrawLine();

                    EditorGUILayout.Space();

                    if (_chosenPlatforms.Length <= 0)
                        return;

                    AddTitle("Choose Platfom Icons to edit");

                    AddChooseDeleteSection();

                    EditorGUILayout.Space();

                    if (_currentPlatformIdx <= 0)
                        return;

                    AddTitle("Icons :");
                    AddPropertySection();
                },

                keywords = new HashSet<string>(new[] { "Surfer", "Input", "Icons" })
            };

            void AddNewPlatformSection()
            {

                EditorGUILayout.Space();

                using (var vert = new GUILayout.HorizontalScope())
                {

                    _newPlatformIdx = EditorGUILayout.Popup("New Platform : ", _newPlatformIdx, _platforms, GUILayout.Width(350));

                    if (GUILayout.Button("Add"))
                    {
                        SurferHelper.SO.AddInputIconsPlatform(_newPlatformIdx.ToPlatformID(), () =>
                        {
                            _platformAlreadyExists = true;
                            _platformThatAlreadyExists = _newPlatformIdx.ToPlatformID();
                        }, () =>
                        {
                            _platformAlreadyExists = false;
                            EditorUtility.SetDirty(SurferHelper.SO);
                            _chosenPlatforms = SurferHelper.SO.GetInputIconsPlatformNames();
                            _currentPlatformIdx = System.Array.IndexOf(_chosenPlatforms, _newPlatformIdx.ToPlatformID().ToString());
                        });

                    }
                }

                if (_platformAlreadyExists)
                {
                    EditorGUILayout.LabelField("\" " + _platformThatAlreadyExists + " \" platform already exists", EditorStyles.boldLabel);
                }

                EditorGUILayout.Space();
            }

            void DrawLine()
            {
                var rect = EditorGUILayout.BeginHorizontal();
                Handles.color = Color.gray;
                Handles.DrawLine(new Vector2(rect.x - 15, rect.y), new Vector2(rect.width + 15, rect.y));
                EditorGUILayout.EndHorizontal();
            }

            void AddTitle(string name)
            {
                GUIStyle style = new GUIStyle();
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.white;
                EditorGUILayout.LabelField(name, style);
            }

            void AddPropertySection()
            {
                if (_currentPlatformIdx >= _chosenPlatforms.Length)
                    return;

                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

                if (GetSelectedPlatformID() == null)
                    return;

                _inputIcons = SurferHelper.SO.GetInputIcons(GetSelectedPlatformID().Value);

                if (_inputIcons == null)
                    return;

                //sprites
                using (var horiz = new GUILayout.HorizontalScope())
                {

                    EditorGUILayout.LabelField("Sprites", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));

                    if (GUILayout.Button("+", GUILayout.MaxWidth(SurferHelper.lineHeight), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                    {
                        PopupWindow.Show(_buttonRect, new SUSpritePopup((data) =>
                        {
                            foreach (var pair in SurferHelper.SO.InputIcons)
                            {
                                pair.Value.Sprites.Add(new SUGUIDSpriteData(data as SUGUIDSpriteData));
                            }

                        }));
                    }
                }

                AddIndentation();

                foreach (var spriteData in _inputIcons.Sprites.Reverse<SUGUIDSpriteData>())
                {
                    using (var horiz = new GUILayout.HorizontalScope())
                    {

                        EditorGUILayout.LabelField(spriteData.Name, GUILayout.ExpandWidth(false));

                        if (GUILayout.Button("Edit", GUILayout.MaxWidth(SurferHelper.lineHeight * 2), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            PopupWindow.Show(_buttonRect, new SUSpritePopup(spriteData, (data) =>
                            {
                                ForEachSpriteDataInAllPlatforms<SUGUIDSpriteData>(onCurrentPlatform: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDSpriteData);
                                    dataToUpdate.UpdateSprite(data as SUGUIDSpriteData);
                                }, onOtherPlatform: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDSpriteData);
                                });

                            }));
                        }

                        if (GUILayout.Button("Delete", GUILayout.MaxWidth(_deleteBtnWidth), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            SurferHelper.SO.InputIcons.RemoveSprite(spriteData.GUID);
                            EditorUtility.SetDirty(SurferHelper.SO);
                        }
                    }
                }

                EditorGUILayout.Space();
                ResetIndentation();

                //res sprites
                using (var horiz = new GUILayout.HorizontalScope())
                {

                    EditorGUILayout.LabelField("Sprites (Resources)", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));

                    if (GUILayout.Button("+", GUILayout.MaxWidth(SurferHelper.lineHeight), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                    {
                        PopupWindow.Show(_buttonRect, new SUResourcesSpritePopup((data) =>
                        {
                            foreach (var pair in SurferHelper.SO.InputIcons)
                            {
                                pair.Value.ResSprites.Add(new SUGUIDResourcesSpriteData(data as SUGUIDResourcesSpriteData));
                            }

                        }));
                    }
                }

                AddIndentation();

                foreach (var spriteData in _inputIcons.ResSprites.Reverse<SUGUIDResourcesSpriteData>())
                {
                    using (var horiz = new GUILayout.HorizontalScope())
                    {

                        EditorGUILayout.LabelField(spriteData.Name, GUILayout.ExpandWidth(false));

                        if (GUILayout.Button("Edit", GUILayout.MaxWidth(SurferHelper.lineHeight * 2), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            PopupWindow.Show(_buttonRect, new SUResourcesSpritePopup(spriteData, (data) =>
                            {
                                ForEachSpriteDataInAllPlatforms<SUGUIDResourcesSpriteData>(onCurrentPlatform: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDResourcesSpriteData);
                                    dataToUpdate.UpdatePath(data as SUGUIDResourcesSpriteData);
                                }, onOtherPlatform: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDResourcesSpriteData);
                                });
                            }));
                        }

                        if (GUILayout.Button("Delete", GUILayout.MaxWidth(_deleteBtnWidth), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            SurferHelper.SO.InputIcons.RemoveResSprite(spriteData.GUID);
                            EditorUtility.SetDirty(SurferHelper.SO);
                        }
                    }
                }


                EditorGUILayout.Space();
                ResetIndentation();

                EditorGUILayout.EndScrollView();
            }

            void AddIndentation()
            {
                EditorGUI.indentLevel = 1;
            }

            void ResetIndentation()
            {
                EditorGUI.indentLevel = 0;
            }

            void AddChooseDeleteSection()
            {
                using (var horiz = new GUILayout.HorizontalScope())
                {

                    _currentPlatformIdx = EditorGUILayout.Popup(_currentPlatformIdx, _chosenPlatforms, GUILayout.Width(350));

                    if (GetSelectedPlatformID() != null)
                    {
                        if (GUILayout.Button("Delete"))
                        {
                            if (EditorUtility.DisplayDialog("Delete", "Are you sure you want to delete platform \"" + _chosenPlatforms[_currentPlatformIdx] + "\" ?", "Yes", "No"))
                            {
                                SurferHelper.SO.RemoveInputIconsPlatform(GetSelectedPlatformID().Value);
                                EditorUtility.SetDirty(SurferHelper.SO);
                                _currentPlatformIdx = Mathf.Clamp(_currentPlatformIdx - 1, 0, _currentPlatformIdx);
                            }

                        }
                    }
                }
            }

            SUPlatform_ID? GetSelectedPlatformID()
            {
                return _chosenPlatforms[_currentPlatformIdx].ToPlatformID();
            }

            void ForEachSpriteDataInAllPlatforms<T>(System.Action<T> onCurrentPlatform,
            System.Action<T> onOtherPlatform) where T : SUGUIDElementBaseData
            {
                var areResSprites = typeof(T) == typeof(SUGUIDResourcesSpriteData);
                for (int i = 0; i < _chosenPlatforms.Length; i++)
                {
                    var pName = _chosenPlatforms[i];
                    var pID = pName.ToPlatformID();
                    if (pID == null) continue;

                    var allIcons = SurferHelper.SO.GetInputIcons(pID.Value);

                    if (areResSprites)
                    {
                        foreach (var data in allIcons.ResSprites)
                            if (_currentPlatformIdx == i)
                                onCurrentPlatform?.Invoke(data as T);
                            else
                                onOtherPlatform?.Invoke(data as T);
                    }
                    else
                    {
                        foreach (var data in allIcons.Sprites)
                            if (_currentPlatformIdx == i)
                                onCurrentPlatform?.Invoke(data as T);
                            else
                                onOtherPlatform?.Invoke(data as T);
                    }

                }
            }
        }

    }

}

