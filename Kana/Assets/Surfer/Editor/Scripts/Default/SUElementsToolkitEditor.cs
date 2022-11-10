#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.SceneManagement;
using System.Linq;

namespace Surfer
{

    [CustomEditor(typeof(SUElementsToolkit))]
    public class SUElementsToolkitEditor : Editor
    {
        SerializedProperty _doc, _behavs, _selectedBehaviour, _elementsTk, _selectedEleData, _isPersistent = default;
        bool _whoOpen, _whenOpen, _ifOpen, _doOpen, _isSuccessOpen = default;
        Label _lbEvents, _lbElesData = default;
        DropdownField _dropBehavs, _dropElesData = default;
        VisualElement _whenContainer,_ifContainer,_doContainer,_whoContainer,_rowBehavs, _rowElesData, _container,
        _whoSecondContainer = default;
        PropertyField _eventField, _customCondsF, _eleDataField, 
        _customReactionsF, _customFailReactionsF, _unitySuccessEvent,
        _unityFailEvent, _docF, _persistentF = default;
        HelpBox _helpBoxF = default;
        Button _btnDo, _btnWho, _btnIf, _btnWhen, _btnSuccess, _btnFail = default;
        Texture2D _purpleImg, _greenImg, _redImg, _whiteImg = default;
        SUElementsToolkit _cp = default;
        int _lastEleDataIndex = default;

        private void OnEnable() {
            GetSerializedProperties();
            LoadTextures();
        }

        void GetSerializedProperties()
        {
            _doc = serializedObject.FindProperty("_doc");
            _elementsTk = serializedObject.FindProperty("_elementsTk");
            _isPersistent = serializedObject.FindProperty("_isPersistent");
        }

        void LoadTextures()
        {
            _purpleImg = EditorGUIUtility.Load("Assets/Surfer/Editor/Images/Rect.png") as Texture2D;
            _redImg = EditorGUIUtility.Load("Assets/Surfer/Editor/Images/RectRed.png") as Texture2D;
            _greenImg = EditorGUIUtility.Load("Assets/Surfer/Editor/Images/RectGreen.png") as Texture2D;
            _whiteImg = EditorGUIUtility.Load("Assets/Surfer/Editor/Images/RectWhite.png") as Texture2D;
        }

        public override VisualElement CreateInspectorGUI()
        {
            _cp = target as SUElementsToolkit;
            
            _whenOpen = _doOpen = _whoOpen = _ifOpen = false;
            _isSuccessOpen = true;

            _container = new VisualElement();

            _persistentF = new PropertyField(_isPersistent, "Is Persistent");
            _docF = new PropertyField(_doc,"UI Document");
            _helpBoxF = new HelpBox("Add a UIDocument above !", HelpBoxMessageType.Info);

            _container.Add(_docF);
            _container.Add(_persistentF);
            

            _docF.RegisterValueChangeCallback((data) =>
            {
                CheckDocField();
            });

            CheckDocField();

            return _container;

        }


        void CreateEditor()
        {
            _persistentF.style.display = DisplayStyle.Flex;

            CreateWhoSection();
            CreateWhenSection();
            CreateIfSection();
            CreateDOSection();

            UpdateElesDataDropdown();

            _container.RegisterCallback<SerializedPropertyChangeEvent>((changeEvent) =>
            {
                UpdateDictionary();
            } );
        }

        void RemoveOldElements()
        {
            var children = _container.Children().ToList();
            foreach(var ele in children)
            {
                if(ele == _docF
                || ele == _persistentF)
                    continue;
                    
                _container.Remove(ele);
            }
        }

        void CheckDocField()
        {
            RemoveOldElements();

            if(_doc.objectReferenceValue == null)
            {
                if(_doc.objectReferenceValue == null)
                {
                    _doc.objectReferenceValue = (serializedObject.targetObject as SUElementsToolkit).GetComponent<UIDocument>();
                    serializedObject.ApplyModifiedProperties();
                }

                if(_doc.objectReferenceValue == null)
                {
                    _persistentF.style.display = DisplayStyle.None;
                    _container.Add(_helpBoxF);
                }
                else
                {
                    CreateEditor();
                }
            }
            else
            {
                CreateEditor();
            }
            
        }


        void CreateWhoSection()
        {

            _whoContainer = new VisualElement();
            _whoContainer.style.display = DisplayStyle.None;

            //who
            _btnWho = new Button(()=>
            {
                _whoOpen = !_whoOpen;
                _whoContainer.style.display = _whoOpen ? DisplayStyle.Flex : DisplayStyle.None;
            });
            _btnWho.text = "<b>WHO!</b>";
            _btnWho.style.height = 25;
            _btnWho.style.backgroundImage = _purpleImg;

            _container.Add(_btnWho);


            {
                //row elements data
                _rowElesData = new VisualElement();
                _rowElesData.style.flexDirection = FlexDirection.Row;
                _rowElesData.style.alignItems = Align.Stretch;

                _lbElesData = new Label();

                _dropElesData = new DropdownField();
                _dropElesData.style.flexGrow = 50;
                _dropElesData.style.flexShrink = 50;
                _dropElesData.style.display = _elementsTk.arraySize <= 0 ? DisplayStyle.None : DisplayStyle.Flex;
                _dropElesData.RegisterValueChangedCallback((value) =>
                {
                    if(_lastEleDataIndex != _dropElesData.index)
                        GetDropElesSelectedData();
                    
                    _lastEleDataIndex = _dropElesData.index;
                });

                Button btnAdd = new Button(()=>
                {
                    _elementsTk.InsertArrayElementAtIndex(Mathf.Clamp(_elementsTk.arraySize - 1, 0, _elementsTk.arraySize - 1));
                    serializedObject.ApplyModifiedProperties();

                    _cp.ElementsTk[_cp.ElementsTk.Count-1] = new SUElementToolkit();
                    serializedObject.Update();
                    UpdateElesDataDropdown(_cp.ElementsTk.Count-1);

                });
                btnAdd.text = "+";
                btnAdd.style.flexGrow = 25;
                btnAdd.style.flexShrink = 5;

                Button btnDelete = new Button(()=>
                {

                    if(_dropElesData.index >= 0 && _dropElesData.index < _elementsTk.arraySize )
                    {
                        _elementsTk.DeleteArrayElementAtIndex(_dropElesData.index);
                        UpdateElesDataDropdown(Mathf.Clamp(_dropElesData.index-1,0,_dropElesData.index));
                        serializedObject.ApplyModifiedProperties();
                    }

                });
                btnDelete.text = "-";
                btnDelete.style.flexGrow = 25;
                btnDelete.style.flexShrink = 5;
                //btnDelete.style.backgroundImage = _redImg;

                _rowElesData.Add(_dropElesData);
                _rowElesData.Add(btnAdd);
                _rowElesData.Add(btnDelete);

                _whoContainer.Add(_lbElesData);
                _whoContainer.Add(_rowElesData);

                _whoContainer.Add(CreateHorizontalLine());

                _eleDataField = new PropertyField();
                _eleDataField.style.display = DisplayStyle.None;
                _eleDataField.RegisterValueChangeCallback((value) =>
                {
                    int idx = _dropElesData.index;
                    _dropElesData.choices = GetElesDataList();
                    _dropElesData.index = idx;
                });

                _whoContainer.Add(_eleDataField);

            }


            {

                _whoSecondContainer = new VisualElement();

                //row behavs
                _rowBehavs = new VisualElement();
                _rowBehavs.style.flexDirection = FlexDirection.Row;
                _rowBehavs.style.alignItems = Align.Stretch;

                _lbEvents = new Label();


                _dropBehavs = new DropdownField();
                _dropBehavs.style.flexGrow = 50;
                _dropBehavs.style.flexShrink = 50;
                _dropBehavs.RegisterValueChangedCallback((value) =>
                {
                    GetDropBehavsSelectedData();
                });

                Button btnAdd = new Button(()=>
                {
                    _behavs.InsertArrayElementAtIndex(Mathf.Clamp(_behavs.arraySize - 1, 0, _behavs.arraySize - 1));
                    serializedObject.ApplyModifiedProperties();


                    _cp.ElementsTk[_dropElesData.index].Behaviours.Behaviours[_cp.ElementsTk[_dropElesData.index].Behaviours.Behaviours.Count - 1] = new SUBehaviourData();
                    serializedObject.Update();
                    UpdateBehavsDropdown();

                });
                btnAdd.text = "+";
                btnAdd.style.flexGrow = 25;
                btnAdd.style.flexShrink = 5;

                Button btnDelete = new Button(()=>
                {

                    if(_dropBehavs.index >= 0 && _dropBehavs.index < _behavs.arraySize )
                    {
                        _behavs.DeleteArrayElementAtIndex(_dropBehavs.index);
                        UpdateBehavsDropdown();
                        serializedObject.ApplyModifiedProperties();
                    }

                });
                btnDelete.text = "-";
                btnDelete.style.flexGrow = 25;
                btnDelete.style.flexShrink = 5;

                _rowBehavs.Add(_dropBehavs);
                _rowBehavs.Add(btnAdd);
                _rowBehavs.Add(btnDelete);
                _whoSecondContainer.Add(CreateHorizontalLine());
                _whoSecondContainer.Add(_lbEvents);
                _whoSecondContainer.Add(_rowBehavs);

                _whoContainer.Add(_whoSecondContainer);


            }
            

            _container.Add(_whoContainer);


        }

        void CreateWhenSection()
        {
            _whenContainer = new VisualElement();
            _whenContainer.style.display = DisplayStyle.None;

            //when
            _btnWhen = new Button(()=>
            {
                _whenOpen = !_whenOpen;

                if(_whenOpen)
                {
                    _whenContainer.style.display = DisplayStyle.Flex;

                }
                else
                {
                    _whenContainer.style.display = DisplayStyle.None;

                }

            });
            _btnWhen.text = "<b>WHEN!</b>";
            _btnWhen.style.height = 25;
            _btnWhen.style.backgroundImage = _purpleImg;

            _eventField = new PropertyField();
            _eventField.RegisterValueChangeCallback((value) =>
            {
                int idx = _dropBehavs.index;
                _dropBehavs.choices = GetEventsList();
                _dropBehavs.index = idx;
            });

            

            _container.Add(_btnWhen);
            _whenContainer.Add(_eventField);
            _container.Add(_whenContainer);

        }


        void CreateIfSection()
        {
            _ifContainer = new VisualElement();
            _ifContainer.style.display = DisplayStyle.None;
            //if
            _btnIf = new Button(()=>
            {
                _ifOpen = !_ifOpen;

                if(_ifOpen)
                {
                    _customCondsF.BindProperty(_selectedBehaviour.FindPropertyRelative("_customConds"));
                    _customCondsF.label = SurferHelper.kNoFoldout;
                    _ifContainer.style.display = DisplayStyle.Flex;
                }
                else
                {
                    _ifContainer.style.display = DisplayStyle.None;
                }

            });
            _btnIf.text = "<b>IF!</b>";
            _btnIf.style.height = 25;
            _btnIf.style.backgroundImage = _purpleImg;

            _customCondsF = new PropertyField();

            _container.Add(_btnIf);
            _ifContainer.Add(_customCondsF);
            _container.Add(_ifContainer);
        }


        void CreateDOSection()
        {
            _doContainer = new VisualElement();
            _doContainer.style.display = DisplayStyle.None;

            //do
            _btnDo = new Button(()=>
            {
                _doOpen = !_doOpen;
                _doContainer.style.display = _doOpen ? DisplayStyle.Flex : DisplayStyle.None;
                UpdateSuccessFailSections();
            });
            _btnDo.text = "<b>DO!</b>";
            _btnDo.style.height = 25;
            _btnDo.style.backgroundImage = _purpleImg;


            VisualElement rowResult = new VisualElement();
            rowResult.style.flexDirection = FlexDirection.Row;
            _doContainer.Add(rowResult);

            //success
            _btnSuccess = new Button(()=>
            {
                _isSuccessOpen = true;
                UpdateSuccessFailSections();
            });
            _btnSuccess.text = "On Success";
            _btnSuccess.style.flexGrow = 50;
            _btnSuccess.style.flexShrink = 10;

            _customReactionsF = new PropertyField();
            _customReactionsF.label = SurferHelper.kNoFoldout;
            _unitySuccessEvent = new PropertyField();

            _doContainer.Add(_customReactionsF);
            _doContainer.Add(_unitySuccessEvent);


            //fail
            _btnFail = new Button(()=>
            {
                _isSuccessOpen = false;
                UpdateSuccessFailSections();
            });
            _btnFail.text = "On Fail";
            _btnFail.style.flexGrow = 50;
            _btnFail.style.flexShrink = 10;

            _customFailReactionsF = new PropertyField();
            _customFailReactionsF.label = SurferHelper.kNoFoldout;
            _unityFailEvent = new PropertyField();

            _doContainer.Add(_customFailReactionsF);
            _doContainer.Add(_unityFailEvent);

            rowResult.Add(_btnSuccess);
            rowResult.Add(_btnFail);

            _container.Add(_btnDo);
            _container.Add(_doContainer);

        }


        void UpdateSuccessFailSections()
        {

            _btnSuccess.style.backgroundImage = _isSuccessOpen ? _greenImg : _whiteImg;
            _btnFail.style.backgroundImage = _isSuccessOpen ? _whiteImg : _redImg;

            _btnSuccess.style.color = _isSuccessOpen ? Color.white : Color.gray;
            _btnFail.style.color = _isSuccessOpen ? Color.grey : Color.white;

            if(_selectedBehaviour == null)
            {
                _doOpen = false;
                _doContainer.style.display = DisplayStyle.None;
            }
            else
            {
                if(!_doOpen)
                    return;

                _customReactionsF.BindProperty(_selectedBehaviour.FindPropertyRelative("_reactions"));
                _customReactionsF.style.display = _isSuccessOpen ? DisplayStyle.Flex : DisplayStyle.None;
                // _unitySuccessEvent.BindProperty(_selectedBehaviour.FindPropertyRelative("OnSuccess"));
                // _unitySuccessEvent.style.display = _isSuccessOpen ? DisplayStyle.Flex : DisplayStyle.None;

                _customFailReactionsF.BindProperty(_selectedBehaviour.FindPropertyRelative("_failReactions"));
                _customFailReactionsF.style.display = !_isSuccessOpen ? DisplayStyle.Flex : DisplayStyle.None;
                // _unityFailEvent.BindProperty(_selectedBehaviour.FindPropertyRelative("OnFail"));
                // _unityFailEvent.style.display = _isSuccessOpen ? DisplayStyle.None : DisplayStyle.Flex;

            }

        }

        void UpdateElesDataDropdown(int index = 0)
        {
            //element data section
            _lbElesData.text = "Create or edit an element !";
            _dropElesData.choices = GetElesDataList();
            _dropElesData.index = index;
            _dropElesData.style.display = _elementsTk.arraySize <= 0 ? DisplayStyle.None : DisplayStyle.Flex;
            _eleDataField.style.display = _elementsTk.arraySize <= 0 ? DisplayStyle.None : DisplayStyle.Flex;

            GetDropElesSelectedData();

        }


        void GetDropElesSelectedData()
        {

            if(_dropElesData.index >= 0 && _elementsTk.arraySize > _dropElesData.index)
            {
                //if there's a selected elementData, get its data and show it
                _selectedEleData = _elementsTk.GetArrayElementAtIndex(_dropElesData.index);

                _eleDataField.BindProperty(_selectedEleData.FindPropertyRelative("_elementData"));

            }
            else
            {
                //no elementData selected therefore not even any behaviour selected
                // or maybe no elementData created (elementsTk list emtpy)
                _selectedEleData = null;
                _selectedBehaviour = null;
            }

            UpdateBehavsDropdown();
        }

        void UpdateBehavsDropdown()
        {
            if(_selectedEleData != null)
            {
                _whoSecondContainer.style.display = DisplayStyle.Flex;
                _behavs = _selectedEleData.FindPropertyRelative("_behaviours").FindPropertyRelative("_behaviours");
                _lbEvents.text = _behavs.arraySize <= 0 ? "No event behaviours created! Add one! " : "Choose the element's event to edit! ";
                _dropBehavs.choices = GetEventsList();
                _dropBehavs.index = _behavs.arraySize-1;
                _dropBehavs.style.display = _behavs.arraySize <= 0 ? DisplayStyle.None : DisplayStyle.Flex;
            }
            else
            {
                _whoSecondContainer.style.display = DisplayStyle.None;
            }
            
            GetDropBehavsSelectedData();
        }

        void GetDropBehavsSelectedData()
        {
            if(_selectedEleData != null && _dropBehavs.index >= 0 && _behavs.arraySize > _dropBehavs.index)
            {
                //the element has events
                _selectedBehaviour = _behavs.GetArrayElementAtIndex(_dropBehavs.index);
                _eventField.BindProperty(_selectedBehaviour.FindPropertyRelative("_event"));
                ShowBehavioursSection();

            }
            else
            {
                //the element has no events or there is no elementData created (elementsTk empty)
                ShowBehavioursSection(false);
            }

        }


        void ShowBehavioursSection(bool show = true)
        {

            if(show)
            {
                _btnWhen.style.display = DisplayStyle.Flex;
                _btnIf.style.display = DisplayStyle.Flex;
                _btnDo.style.display = DisplayStyle.Flex;
            }
            else
            {
                _btnWhen.style.display = DisplayStyle.None;
                _btnIf.style.display = DisplayStyle.None;
                _btnDo.style.display = DisplayStyle.None;

                _whenContainer.style.display = DisplayStyle.None;
                _ifContainer.style.display = DisplayStyle.None;
                _doContainer.style.display = DisplayStyle.None;

                _whenOpen = false;
                _ifOpen = false;
                _doOpen = false;
            }
            

            UpdateSuccessFailSections();
        }

        List<string> GetEventsList()
        {

            List<string> eventNames = new List<string>();
            int counter = 1;


            for (int f = 0; f < _behavs.arraySize; f++)
            {
                var eventType = (SUEvent.Type_ID)_behavs.GetArrayElementAtIndex(f).FindPropertyRelative("_event").FindPropertyRelative("_type").enumValueIndex;

                eventNames.Add(counter + ") " + (eventType).ToString().Replace("_", " -> "));
                counter++;
            }
            return eventNames;

        }


        List<string> GetElesDataList()
        {

            List<string> output = new List<string>();

            int counter = 1;


            for (int f = 0; f < _elementsTk.arraySize; f++)
            {
                var elementData = _elementsTk.GetArrayElementAtIndex(f).FindPropertyRelative("_elementData");
                var type = (SUElementData.Type_ID)elementData.FindPropertyRelative("_type")?.enumValueIndex;
                var tkName = elementData.FindPropertyRelative("_query").FindPropertyRelative("_tkName")?.stringValue;

                output.Add(counter + ") "+tkName+"   ( Type : "+(type).ToString()+")");
                counter++;
            }
            return output;

        }


        VisualElement CreateHorizontalLine(float height = 5)
        {
            
            Image img = new Image();
            img.style.height = height;
            img.style.borderTopWidth = 1;
            img.style.borderTopColor = Color.grey;

            return img;
        }

        void UpdateDictionary()
        {
            serializedObject.ApplyModifiedProperties();

            var mainCp = (target as SUElementsToolkit);

            if(mainCp == null)
                return;
            if(mainCp.ElementsTk == null)
                return;
            if(mainCp.ElementsTk.Count <= 0)
                return;

            foreach(var tkData in mainCp.ElementsTk)
            {
                if(tkData == null)
                    continue;

                tkData.Events.Clear();

                SUBehavioursData _copy = JsonUtility.FromJson<SUBehavioursData>(JsonUtility.ToJson(tkData.Behaviours));

                for (int i = 0; i < _copy.Behaviours.Count; i++)
                {
                    var item = _copy.Behaviours[i];


                    if (tkData.Events.TryGetValue(item.EventType, out SUBehavioursData value))
                    {
                        value.Behaviours.Add(item);
                    }
                    else
                    {
                        var all = new SUBehavioursData();
                        all.Behaviours.Add(item);
                        tkData.Events.Add(item.EventType, all);
                    }
                }
            }

            
            if(!EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorUtility.SetDirty(target);
                EditorSceneManager.MarkSceneDirty(mainCp.gameObject.scene);
            }
        }

    }

}

#endif