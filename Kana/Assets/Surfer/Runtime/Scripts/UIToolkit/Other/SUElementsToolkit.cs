#if UNITY_2021_2_OR_NEWER

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;


namespace Surfer
{

    public partial class SUElementsToolkit : MonoBehaviour
    {
        

        [SerializeField]
        UIDocument _doc = default;
        public UIDocument Document { get => _doc; }

        [SerializeField]
        bool _isPersistent = default;

        [SerializeField]
        List<SUElementToolkit> _elementsTk = new List<SUElementToolkit>();
        public List<SUElementToolkit> ElementsTk { get => _elementsTk; }

        //collection to easy register events of tkElements
        Dictionary<SUEvent.Type_ID, HashSet<Tuple<SUElementData, SUBehavioursData>>> _events = new Dictionary<SUEvent.Type_ID, HashSet<Tuple<SUElementData, SUBehavioursData>>>();

        string _mySceneName = default;

#region  MonoBehaviour

        private void Awake() 
        {
            if(_isPersistent)
                DontDestroyOnLoad(gameObject);

            _mySceneName = gameObject.scene.name;

            SetupElementsAvailableNow();
            RegisterElementsAvailableLater();
            RegisterElementsRemoved();

            RunEventBehaviour(SUEvent.Type_ID.MonoBehaviour_OnAwake);
        }


        private void Start() 
        {
            RunEventBehaviour(SUEvent.Type_ID.MonoBehaviour_OnStart);
        }

        private void OnEnable()
        {
            RunEventBehaviour(SUEvent.Type_ID.MonoBehaviour_OnEnable);
        }

        private void OnDisable() 
        {
            RunEventBehaviour(SUEvent.Type_ID.MonoBehaviour_OnDisable);
        }

        private void OnApplicationFocus(bool focusStatus) 
        {

            if (focusStatus)
            {
                RunEventBehaviour(SUEvent.Type_ID.MonoBehaviour_OnApplicationGainFocus);
            }
            else
            {
                RunEventBehaviour(SUEvent.Type_ID.MonoBehaviour_OnApplicationLoseFocus);
            }
            
        }

        private void OnApplicationPause(bool pauseStatus) 
        {
            if (pauseStatus)
            {
                RunEventBehaviour(SUEvent.Type_ID.MonoBehaviour_OnApplicationPaused);
            }
            else
            {
                RunEventBehaviour(SUEvent.Type_ID.MonoBehaviour_OnApplicationUnpaused);
            }
        }

        #endregion



        void SaveEvents(SUElementToolkit tkData)
        {

            foreach(KeyValuePair<SUEvent.Type_ID,SUBehavioursData> pair in tkData.Events)
            {
                if(_events.TryGetValue(pair.Key, out var hashset))
                {
                    hashset.Add(new Tuple<SUElementData, SUBehavioursData>(tkData.ElementData,pair.Value));
                }
                else
                {
                        
                    _events.Add(pair.Key,new HashSet<Tuple<SUElementData,SUBehavioursData>>()
                    {
                        new Tuple<SUElementData, SUBehavioursData>(tkData.ElementData,pair.Value)
                    });

                }
            }
        }

#region Behaviours Run

        void RunEventBehaviour(SUEvent.Type_ID eventID)
        {

            if(_events.TryGetValue(eventID,out var hash))
            {
                foreach(var tup in hash)
                {
                    tup.Item2.Run(tup.Item1);
                }
                        
            }

        }

        void RunEventBehaviour(SUEvent.Type_ID eventID,object evtData)
        {

            if(_events.TryGetValue(eventID,out var hash))
            {
                foreach(var tup in hash)
                {
                    tup.Item2.Run(tup.Item1,evtData);
                }
                        
            }

        }

#endregion


        void RegisterElementsAvailableLater()
        {

            if(_doc == null)
                return;

            _doc.rootVisualElement.RegisterCallback<SUTKNewElementEvent>((evt) =>
            {
                for (int i = 0; i < _elementsTk.Count;i++)
                {

                    var item = _elementsTk[i];

                    if(item.ElementData.IsAvailableNow())
                        continue;
                    if(!item.ElementData.Query.HasQuerableTkName)
                        continue;
                    if(item.ElementData.Query.TKName != evt.ElementAdded.name)
                        continue;

                    //if multiple visualElements with the same name are found (like list items), 
                    //easy clone/copy with JsonUtility the SUElementToolkit class.
                    AddNewElementToolkitLogic(item,evt.ElementAdded);
                    break;
                }

            });


        }


        void SetupElementsAvailableNow()
        {
            if(_doc == null)
                return;

            for (int i = 0; i < _elementsTk.Count; i++)
            {
                //an item contains a VisualElement with a specified name and has its list of events
                var item = _elementsTk[i];

                if (item.ElementData.VElement != null)
                    continue;

                item.ElementData.IsToolkit = true;
                item.MySceneName = _mySceneName;

                if (item.ElementData.IsTkRoot())
                {
                    item.ElementData.SetUp(_doc.rootVisualElement, gameObject, _isPersistent);
                    item.Initialize();
                    SaveEvents(item);
                }
                else
                {
                    if(!item.ElementData.IsAvailableNow())
                        continue;
                    if(!item.ElementData.Query.HasQuerableTkName)
                        continue;

                    _doc.rootVisualElement.Query(name : item.ElementData.Query.TKName).ForEach(x =>
                    {
                        if (item.ElementData.VElement == null)
                        {
                            item.ElementData.SetUp(x, gameObject,_isPersistent);
                            item.Initialize();

                            //save events for easy-registering to them later
                            SaveEvents(item);
                        }
                        else
                        {
                            //if multiple visualElements with the same name are found (like list items), 
                            //easy clone/copy with JsonUtility the SUElementToolkit class.
                            AddNewElementToolkitLogic(item,x);
                        }

                    });
                }

            }

        }

        void RegisterElementsRemoved()
        {

            if(_doc == null)
                return;

            _doc.rootVisualElement.RegisterCallback<SUTKOldElementEvent>((value) =>
            {

                foreach(var item in _elementsTk)
                {
                    if(item.ElementData.VElement == value.ElementRemoved)
                    {
                        item.ResetAll();
                        break;
                    }
                }

            });


        } 

        void AddNewElementToolkitLogic(SUElementToolkit logicToCopy,VisualElement element)
        {
            SUElementToolkit copy = JsonUtility.FromJson<SUElementToolkit>(JsonUtility.ToJson(logicToCopy));
            copy.ElementData.IsToolkit = true;
            copy.MySceneName = _mySceneName;

            copy.ElementData.SetUp(element, gameObject,_isPersistent);
            copy.Initialize();

            _elementsTk.Add(copy);

            SaveEvents(copy);
        }

        private void OnDestroy() 
        {
            foreach(var ele in _elementsTk)
            {
                ele.ResetAll();
            }
        }

    }




}

#endif
