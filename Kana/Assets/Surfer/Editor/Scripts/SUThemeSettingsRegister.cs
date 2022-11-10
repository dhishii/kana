using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;


namespace Surfer
{

    public static class SUThemeSettingsRegister
    {

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {


            Vector2 _scrollPos = default;
            bool _themeNameAlreadyExists = default;
            string[] _themesNames = default;
            string _renamed = default, _newName = default, _nameThatAlreadyExists = default;
            int _idx = default;
            Rect _buttonRect = default;
            SUThemeData _theme = default;
            float _deleteBtnWidth = SurferHelper.lineHeight * 3;

            return new SettingsProvider("Preferences/Surfer/Theme Manager", SettingsScope.User)
            {
                guiHandler = (searchContext) =>
                {

                    ResetIndentation();

                    _themesNames = SurferHelper.SO.Themes.GetNames();
                    GetCurrentSelectedThemeIdx();

                    AddNewThemeSection();

                    DrawLine();

                    EditorGUILayout.Space();

                    if (_themesNames.Length <= 0)
                        return;

                    AddTitle("Choose Theme to edit");
                    AddChooseDeleteRenameSection();

                    if (_idx <= 0)
                    {
                        SaveSelectedThemeIdx();
                        return;
                    }

                    EditorGUILayout.Space();

                    AddTitle("Properties :");
                    AddPropertySection();

                    SaveSelectedThemeIdx();

                },

                keywords = new HashSet<string>(new[] { "Surfer", "Themes" })
            };

            void GetCurrentSelectedThemeIdx()
            {
                if (!string.IsNullOrEmpty(SurferHelper.SO.SelectedThemeGUID))
                {
                    var selectedName = SurferHelper.SO.GetTheme(SurferHelper.SO.SelectedThemeGUID).Name;

                    for (int i = 0; i < _themesNames.Length; i++)
                    {
                        if (_themesNames[i] == selectedName)
                        {
                            _idx = i;
                            break;
                        }
                    }

                }
            }

            void SaveSelectedThemeIdx()
            {
                if (_idx >= _themesNames.Length)
                    return;

                var guid = SurferHelper.SO.GetThemeKey(_themesNames[_idx]);

                if (guid != SurferHelper.SO.SelectedThemeGUID)
                {
                    SurferHelper.SO.SelectedThemeGUID = guid;
                    EditorUtility.SetDirty(SurferHelper.SO);
                }
            }

            void AddNewThemeSection()
            {

                EditorGUILayout.Space();

                using (var vert = new GUILayout.HorizontalScope())
                {

                    _newName = EditorGUILayout.TextField("New Theme : ", _newName, GUILayout.Width(350));

                    if (GUILayout.Button("Add"))
                    {
                        SurferHelper.SO.AddTheme(_newName, () =>
                        {
                            _themeNameAlreadyExists = true;
                            _nameThatAlreadyExists = _newName;
                        }, (newGUID) =>
                        {
                            SurferHelper.SO.SelectedThemeGUID = newGUID;
                            _idx = _themesNames.Length;
                            _themeNameAlreadyExists = false;
                            EditorUtility.SetDirty(SurferHelper.SO);
                        });

                    }
                }

                if (_themeNameAlreadyExists)
                {
                    EditorGUILayout.LabelField("\" " + _nameThatAlreadyExists + " \" theme already exists", EditorStyles.boldLabel);
                }

                EditorGUILayout.Space();
            }

            void AddChooseDeleteRenameSection()
            {
                using (var horiz = new GUILayout.HorizontalScope())
                {
                    _idx = EditorGUILayout.Popup("", _idx, _themesNames, GUILayout.Width(350));

                    if (_idx != 0)
                    {
                        if (GUILayout.Button("Delete"))
                        {
                            if (EditorUtility.DisplayDialog("Delete", "Are you sure you want to delete theme \"" + _themesNames[_idx] + "\" ?", "Yes", "No"))
                            {
                                SurferHelper.SO.RemoveTheme(_themesNames[_idx]);
                                EditorUtility.SetDirty(SurferHelper.SO);
                                _idx = Mathf.Clamp(_idx - 1, 0, _idx);
                            }

                        }
                    }
                }


                using (var hor = new GUILayout.HorizontalScope())
                {

                    if (_idx != 0)
                    {
                        _renamed = EditorGUILayout.TextField("Rename to :", _renamed, GUILayout.Width(350));


                        if (GUILayout.Button("Rename"))
                        {
                            SurferHelper.SO.RenameTheme(SurferHelper.SO.GetThemeKey(_themesNames[_idx]), _renamed, () =>
                            {


                            }, () =>
                            {
                                EditorUtility.SetDirty(SurferHelper.SO);
                                _themesNames = SurferHelper.SO.Themes.GetNames();
                            });

                        }
                    }

                }
            }

            void AddPropertySection()
            {
                if (_idx >= _themesNames.Length)
                    return;

                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

                _theme = SurferHelper.SO.GetTheme(SurferHelper.SO.GetThemeKey(_themesNames[_idx]));


                //fonts
                using (var horiz = new GUILayout.HorizontalScope())
                {

                    EditorGUILayout.LabelField("Fonts", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));

                    if (GUILayout.Button("+", GUILayout.MaxWidth(SurferHelper.lineHeight), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                    {
                        PopupWindow.Show(_buttonRect, new SUFontPopup((data) =>
                        {
                            foreach (var pair in SurferHelper.SO.Themes)
                            {
                                pair.Value.Fonts.Add(new SUGUIDFontData(data as SUGUIDFontData));
                            }

                        }));
                    }
                }

                AddIndentation();

                foreach (var fontData in _theme.Fonts.Reverse<SUGUIDFontData>())
                {
                    using (var horiz = new GUILayout.HorizontalScope())
                    {

                        EditorGUILayout.LabelField(fontData.Name, GUILayout.ExpandWidth(false));

                        if (GUILayout.Button("Edit", GUILayout.MaxWidth(SurferHelper.lineHeight * 2), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            PopupWindow.Show(_buttonRect, new SUFontPopup(fontData, (data) =>
                            {
                                ForEachDataInAllThemes<SUGUIDFontData>(onCurrentTheme: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDFontData);
                                    dataToUpdate.UpdateFont(data as SUGUIDFontData);
                                }, onOtherTheme: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDFontData);
                                });

                            }));
                        }

                        if (GUILayout.Button("Delete", GUILayout.MaxWidth(_deleteBtnWidth), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            SurferHelper.SO.Themes.RemoveFont(fontData.GUID);
                            EditorUtility.SetDirty(SurferHelper.SO);
                        }
                    }
                }

                EditorGUILayout.Space();
                ResetIndentation();

                //font sizes
                using (var horiz = new GUILayout.HorizontalScope())
                {

                    EditorGUILayout.LabelField("Font Sizes", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));

                    if (GUILayout.Button("+", GUILayout.MaxWidth(SurferHelper.lineHeight), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                    {
                        PopupWindow.Show(_buttonRect, new SUFontSizePopup((data) =>
                        {
                            foreach (var pair in SurferHelper.SO.Themes)
                            {
                                pair.Value.FontSizes.Add(new SUGUIDFontSizeData(data as SUGUIDFontSizeData));
                            }
                        }));
                    }
                }

                AddIndentation();

                foreach (var fontSizeData in _theme.FontSizes.Reverse<SUGUIDFontSizeData>())
                {
                    using (var horiz = new GUILayout.HorizontalScope())
                    {

                        EditorGUILayout.LabelField(fontSizeData.Name, GUILayout.ExpandWidth(false));

                        if (GUILayout.Button("Edit", GUILayout.MaxWidth(SurferHelper.lineHeight * 2), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            PopupWindow.Show(_buttonRect, new SUFontSizePopup(fontSizeData, (data) =>
                            {
                                ForEachDataInAllThemes<SUGUIDFontSizeData>(onCurrentTheme: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDFontSizeData);
                                    dataToUpdate.UpdateFontSize(data as SUGUIDFontSizeData);
                                }, onOtherTheme: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDFontSizeData);
                                });
                            }));
                        }

                        if (GUILayout.Button("Delete", GUILayout.MaxWidth(_deleteBtnWidth), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            SurferHelper.SO.Themes.RemoveFontSize(fontSizeData.GUID);
                            EditorUtility.SetDirty(SurferHelper.SO);
                        }
                    }
                }

                EditorGUILayout.Space();
                ResetIndentation();

                //colors
                using (var horiz = new GUILayout.HorizontalScope())
                {

                    EditorGUILayout.LabelField("Colors", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));

                    if (GUILayout.Button("+", GUILayout.MaxWidth(SurferHelper.lineHeight), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                    {
                        PopupWindow.Show(_buttonRect, new SUColorPopup((data) =>
                        {
                            foreach (var pair in SurferHelper.SO.Themes)
                            {
                                pair.Value.Colors.Add(new SUGUIDColorData(data as SUGUIDColorData));
                            }
                        }));
                    }
                }

                AddIndentation();

                foreach (var colorData in _theme.Colors.Reverse<SUGUIDColorData>())
                {

                    using (var horiz = new GUILayout.HorizontalScope())
                    {

                        EditorGUILayout.LabelField(colorData.Name, GUILayout.ExpandWidth(false));

                        if (GUILayout.Button("Edit", GUILayout.MaxWidth(SurferHelper.lineHeight * 2), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            PopupWindow.Show(_buttonRect, new SUColorPopup(colorData, (data) =>
                            {
                                ForEachDataInAllThemes<SUGUIDColorData>(onCurrentTheme: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDColorData);
                                    dataToUpdate.UpdateColor(data as SUGUIDColorData);
                                }, onOtherTheme: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDColorData);
                                });
                            }));
                        }

                        if (GUILayout.Button("Delete", GUILayout.MaxWidth(_deleteBtnWidth), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            SurferHelper.SO.Themes.RemoveColor(colorData.GUID);
                            EditorUtility.SetDirty(SurferHelper.SO);
                        }
                    }
                }

                EditorGUILayout.Space();
                ResetIndentation();

                //sprites
                using (var horiz = new GUILayout.HorizontalScope())
                {

                    EditorGUILayout.LabelField("Sprites", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));

                    if (GUILayout.Button("+", GUILayout.MaxWidth(SurferHelper.lineHeight), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                    {
                        PopupWindow.Show(_buttonRect, new SUSpritePopup((data) =>
                        {
                            foreach (var pair in SurferHelper.SO.Themes)
                            {
                                pair.Value.Sprites.Add(new SUGUIDSpriteData(data as SUGUIDSpriteData));
                            }
                        }));
                    }
                }

                AddIndentation();

                foreach (var spriteData in _theme.Sprites.Reverse<SUGUIDSpriteData>())
                {
                    using (var horiz = new GUILayout.HorizontalScope())
                    {

                        EditorGUILayout.LabelField(spriteData.Name, GUILayout.ExpandWidth(false));

                        if (GUILayout.Button("Edit", GUILayout.MaxWidth(SurferHelper.lineHeight * 2), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            PopupWindow.Show(_buttonRect, new SUSpritePopup(spriteData, (data) =>
                            {
                                ForEachDataInAllThemes<SUGUIDSpriteData>(onCurrentTheme: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDSpriteData);
                                    dataToUpdate.UpdateSprite(data as SUGUIDSpriteData);
                                }, onOtherTheme: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDSpriteData);
                                });
                            }));
                        }

                        if (GUILayout.Button("Delete", GUILayout.MaxWidth(_deleteBtnWidth), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            SurferHelper.SO.Themes.RemoveSprite(spriteData.GUID);
                            EditorUtility.SetDirty(SurferHelper.SO);
                        }
                    }
                }

                EditorGUILayout.Space();
                ResetIndentation();

                //sprites (resources)
                using (var horiz = new GUILayout.HorizontalScope())
                {

                    EditorGUILayout.LabelField("Sprites (Resources)", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));

                    if (GUILayout.Button("+", GUILayout.MaxWidth(SurferHelper.lineHeight), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                    {
                        PopupWindow.Show(_buttonRect, new SUResourcesSpritePopup((data) =>
                        {
                            foreach (var pair in SurferHelper.SO.Themes)
                            {
                                pair.Value.ResSprites.Add(new SUGUIDResourcesSpriteData(data as SUGUIDResourcesSpriteData));
                            }
                        }));
                    }
                }

                AddIndentation();

                foreach (var spriteResData in _theme.ResSprites.Reverse<SUGUIDResourcesSpriteData>())
                {
                    using (var horiz = new GUILayout.HorizontalScope())
                    {

                        EditorGUILayout.LabelField(spriteResData.Name, GUILayout.ExpandWidth(false));

                        if (GUILayout.Button("Edit", GUILayout.MaxWidth(SurferHelper.lineHeight * 2), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            PopupWindow.Show(_buttonRect, new SUResourcesSpritePopup(spriteResData, (data) =>
                            {
                                ForEachDataInAllThemes<SUGUIDResourcesSpriteData>(onCurrentTheme: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDResourcesSpriteData);
                                    dataToUpdate.UpdatePath(data as SUGUIDResourcesSpriteData);
                                }, onOtherTheme: (dataToUpdate) =>
                                {
                                    dataToUpdate.UpdateName(data as SUGUIDResourcesSpriteData);
                                });
                            }));
                        }

                        if (GUILayout.Button("Delete", GUILayout.MaxWidth(_deleteBtnWidth), GUILayout.MaxHeight(SurferHelper.lineHeight)))
                        {
                            SurferHelper.SO.Themes.RemoveResSprite(spriteResData.GUID);
                            EditorUtility.SetDirty(SurferHelper.SO);
                        }
                    }
                }

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

            void AddTitle(string name)
            {
                GUIStyle style = new GUIStyle();
                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = Color.white;
                EditorGUILayout.LabelField(name, style);
            }

            void DrawLine()
            {
                var rect = EditorGUILayout.BeginHorizontal();
                Handles.color = Color.gray;
                Handles.DrawLine(new Vector2(rect.x - 15, rect.y), new Vector2(rect.width + 15, rect.y));
                EditorGUILayout.EndHorizontal();
            }

            void ForEachDataInAllThemes<T>(System.Action<T> onCurrentTheme,
            System.Action<T> onOtherTheme) where T : SUGUIDElementBaseData
            {

                for (int i = 0; i < _themesNames.Length; i++)
                {
                    var tName = _themesNames[i];
                    if (tName == SurferHelper.Unset)
                        continue;

                    var theme = SurferHelper.SO.GetTheme(SurferHelper.SO.GetThemeKey(tName));

                    if (typeof(T) == typeof(SUGUIDFontData))
                    {
                        foreach (var data in theme.Fonts)
                            if (_idx == i)
                                onCurrentTheme?.Invoke(data as T);
                            else
                                onOtherTheme?.Invoke(data as T);
                    }
                    else if (typeof(T) == typeof(SUGUIDFontSizeData))
                    {
                        foreach (var data in theme.FontSizes)
                            if (_idx == i)
                                onCurrentTheme?.Invoke(data as T);
                            else
                                onOtherTheme?.Invoke(data as T);
                    }
                    else if (typeof(T) == typeof(SUGUIDColorData))
                    {
                        foreach (var data in theme.Colors)
                            if (_idx == i)
                                onCurrentTheme?.Invoke(data as T);
                            else
                                onOtherTheme?.Invoke(data as T);
                    }
                    else if (typeof(T) == typeof(SUGUIDSpriteData))
                    {
                        foreach (var data in theme.Sprites)
                            if (_idx == i)
                                onCurrentTheme?.Invoke(data as T);
                            else
                                onOtherTheme?.Invoke(data as T);
                    }
                    else if (typeof(T) == typeof(SUGUIDResourcesSpriteData))
                    {
                        foreach (var data in theme.ResSprites)
                            if (_idx == i)
                                onCurrentTheme?.Invoke(data as T);
                            else
                                onOtherTheme?.Invoke(data as T);
                    }

                }
            }
        }

    }

}

