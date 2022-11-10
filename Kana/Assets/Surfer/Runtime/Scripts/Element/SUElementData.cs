using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;
#endif

namespace Surfer
{


    [System.Serializable]
    public class SUElementData
    {

        public enum Type_ID
        {
            /// <summary>
            /// Generic type of element
            /// </summary>
            Normal,
            /// <summary>
            /// State element type, like a menu, popup, notification or whatever kind of generic panel
            /// </summary>
            State,
            /// <summary>
            /// State element that behaves like a tooltip shown next to the mouse cursor
            /// </summary>
            Tooltip_State,
            /// <summary>
            /// State element that behaves like a draggable Menu can be dragged to the right
            /// </summary>
            DragRight_State,
            /// <summary>
            /// State element that behaves like a draggable Menu can be dragged to the left
            /// </summary>
            DragLeft_State,
            /// <summary>
            /// State element that behaves like a draggable Menu can be dragged to the top
            /// </summary>
            DragUp_State,
            /// <summary>
            /// State element that behaves like a draggable Menu can be dragged to the bottom 
            /// </summary>
            DragDown_State,
            /// <summary>
            /// If the object is a TextMeshPro element, it will show the progress of whatever scene loading
            /// </summary>
            Loading_Text,
            /// <summary>
            /// If the object is an Image element, it will "fill" itself with the progress of whatever scene loading
            /// </summary>
            Loading_Image,
            /// <summary>
            /// If the object is a Slider element, it will control the overall game audio volume
            /// </summary>
            Slider_OverallVolume,
            /// <summary>
            /// If the object is a TextMeshPro element, it will show the app/game version and build number
            /// </summary>
            BuildVersion_Text,
            /// <summary>
            /// The element will positionate and scale itself to match the current selected UI object position and scale, in order to "indicate" it
            /// </summary>
            Selection_Indicator,
            /// <summary>
            /// On/Off screen indicator-like element
            /// </summary>
            Indicator,
            /// <summary>
            /// HealthBar-like element
            /// </summary>
            HealthBar,
            /// <summary>
            /// Some children will become States and will activate on enter and deactivate on exit
            /// </summary>
            GroupStates_OnOff,
            /// <summary>
            /// Some children will become States and will move in on enter and move out on exit
            /// </summary>
            GroupStates_InOut,
            /// <summary>
            /// Some children will become States and will move in and activate on enter and move out and deactivate on exit
            /// </summary>
            GroupStates_OnOffAndInOut,
            /// <summary>
            /// Some children will become buttons that will open specific states
            /// </summary>
            GroupButtons,
        }

        /// <summary>
        /// When a state has just been opened, should automatically close other states?
        /// Example : a TabMenu panel should automatically close sibling states with the mode set to Siblings
        /// </summary>
        public enum StateCloseMode_ID
        {
            None,
            /// <summary>
            /// It will close all the sibling states
            /// </summary>
            Siblings,
            CustomList,
            SiblingsAndCustomList,
            Myself

        }

        [SerializeField]
        Type_ID _type = default;
        public Type_ID Type { get => _type; }

        [SerializeField]
        bool _persistent = false;

        [SerializeField]
        SUStateData _stateData = default;
        public SUStateData StateData { get => _stateData; }

        //stack
        [SerializeField]
        bool _isStackable = default;
        public bool IsStackable 
        {
            get
            {
                if(!this.IsState())
                    return false;

                return _isStackable;
            }
        }

        [SerializeField]
        float _stackDelay = default;
        public float StackDelay { get => _stackDelay; }

        public bool IsStackDelayRunning;

        //close mode

        [SerializeField]
        StateCloseMode_ID _closeMode = default;

        [SerializeField]
        SUStatesData _statesData = default;
        [SerializeField]
        SUStatesData _groupStates = default;
        public SUStatesData GroupStates { get => _groupStates; }
        [SerializeField]
        [Min(0)]
        float _closeDelay = default;
        public float CloseDelay { get => _closeDelay; }

        //tooltip
        [SerializeField]
        Vector2 _vector = new Vector2(0.65f, 0.65f);
        public Vector2 Vector { get => _vector; }

        //players
        [SerializeField]
        int _playerID = SurferHelper.kDefaultPlayerID;

        /// <summary>
        /// Get the PlayerID of the object linked to this ElementData.
        /// </summary>
        public int PlayerID
        {

            get
            {
                if ( _playerID == SurferHelper.kNestedPlayerID
                || !this.IsState())
                {

                    if (IsToolkit)
                    {
#if UNITY_2021_2_OR_NEWER
                        _playerID = VElement.GetVElePlayerID();
#endif
                    }
                    else
                    {
                        _playerID = _obj.GetObjectStatePlayerID();
                    }
                }

                return _playerID;
            }

        }


        string _stateName = null;
        /// <summary>
        /// Get the StateName of the object linked to this ElementData, 
        /// wheter it is a state or normal element.
        /// </summary>
        public string StateName
        {

            get
            {

                if (_stateName != null)
                    return _stateName;

                if (StateData != null && this.IsState() && !string.IsNullOrEmpty(StateData.Name))
                {
                    _stateName = StateData.Name;
                    return _stateName;
                }

                if (IsToolkit)
                {
#if UNITY_2021_2_OR_NEWER
                    _stateName = VElement.GetVEleStateName();
#endif    
                }
                else
                {
                    _stateName = _obj.GetObjectStateName();
                }


                return _stateName;
            }

        }

        //generic string used for element data types; now for loading text prefix and suffix
        [SerializeField]
        string _stringVal, _stringVal2 = default;
        public string StringVal { get => _stringVal; }
        public string StringVal2 { get => _stringVal2; }

        [SerializeField]
        SUIndicatorVisualData _indVisualData = default;
        public SUIndicatorVisualData IndVisualData { get => _indVisualData; }

        [SerializeField]
        SUHealthBarVisualData _hbVisualData = default;
        public SUHealthBarVisualData HbVisualData { get => _hbVisualData; }


        [SerializeField]
        int _intVal = default;
        public int IntVal { get => _intVal; }

#if UNITY_2021_2_OR_NEWER

        [SerializeField]
        SUTKQueryData _query = default;
        public SUTKQueryData Query { get => _query; }

        public VisualElement VElement { get; set; } = default;

#endif

        GameObject _obj = default;
        public GameObject ObjOwner { get => _obj; set => _obj = value; }
        public bool IsToolkit { get; set; } = default;

        #region Collections

        public List<SUStateInfo> StatesToClose
        {

            get
            {

                if (_closeMode == StateCloseMode_ID.Siblings)
                {
                    return SiblingStates;
                }
                else if (_closeMode == StateCloseMode_ID.CustomList)
                {

                    return _statesData.GetStateInfos(this);

                }
                else if (_closeMode == StateCloseMode_ID.SiblingsAndCustomList)
                {

                    List<SUStateInfo> _states = _statesData.GetStateInfos(this);
                    _states.AddRange(SiblingStates);

                    return _states;

                }
                else if(_closeMode == StateCloseMode_ID.Myself)
                {
                    return new List<SUStateInfo> { new SUStateInfo(this) };
                }

                return default;

            }

        }

        List<SUStateInfo> SiblingStates
        {

            get
            {
                List<SUStateInfo> _statesToClose = new List<SUStateInfo>();

                if (IsToolkit)
                {
#if UNITY_2021_2_OR_NEWER
                    VElement.GetSiblingStates(StateName,ref _statesToClose);
#endif
                }
                else
                {
                    if (_obj.transform.parent == null)
                    return _statesToClose;

                    _obj.GetSiblingStates(StateName,ref _statesToClose);
                }

            
                return _statesToClose;
            }

        }


        List<SUStateInfo> _parentUIStates = null;
        public List<SUStateInfo> ParentUIStates
        {

            get
            {

                if (_parentUIStates != null)
                    return _parentUIStates;
                

                _parentUIStates = new List<SUStateInfo>();

                if (IsToolkit)
                {
#if UNITY_2021_2_OR_NEWER
                    VElement.GetParentStates(ref _parentUIStates);
#endif  
                }
                else
                {
                    _obj.GetParentStates(ref _parentUIStates);
                }



                return _parentUIStates;
            }

        }

        List<SUStateInfo> _childUIStates = null;
        public List<SUStateInfo> ChildUIStates
        {

            get
            {

                if (_childUIStates != null)
                    return _childUIStates;

                _childUIStates = new List<SUStateInfo>();

                if (IsToolkit)
                {
#if UNITY_2021_2_OR_NEWER
                    VElement.GetChildStates(ref _childUIStates);
#endif  
                }
                else
                {
                    _obj.GetChildStates(ref _childUIStates);
                }

                return _childUIStates;
            }

        }

        /// <summary>
        /// List that contains all StatesUI loaded in the scene/game
        /// </summary>
        public static HashSet<SUElementData> AllUIStates { get; private set; } = new HashSet<SUElementData>();


        #endregion

        public void SetUp(GameObject go, bool? persistent = null)
        {

            if (go == null)
                return;

            _obj = go;


            if (this.IsState())
                AllUIStates.Add(this);
            
            if(persistent != null)
                _persistent = (bool)persistent;

            if (_persistent && this.IsState())
                Object.DontDestroyOnLoad(_obj.transform.root);

        }


#if UNITY_2021_2_OR_NEWER

        public void SetUp(VisualElement vEle,GameObject go, bool? persistent = null)
        {

            if (go == null)
                return;
            if (vEle == null)
                return;

            IsToolkit = true;
            VElement = vEle;
            SetUp(go, persistent);

        }

#endif


        public void HandleOnDestroy()
        {
            if (!this.IsState())
                return;

            AllUIStates.Remove(this);

            SUEventSystemManager.I.ResetStateHistoryFocus(PlayerID,StateName);
            SurferManager.I.ClosePlayerState(PlayerID,StateName);
        }

        

        #region Runtime Injection

        public void InjectStateData(string stateName,int playerID,StateCloseMode_ID closeModeID)
        {
            _type = Type_ID.State;
            _playerID = playerID;
            _stateData = new SUStateData(stateName,SurferManager.I.SO.GetStateKey(stateName));
            _closeMode = closeModeID;
        }

        public void SetUpState(GameObject rectGO,string stateName,int playerID, bool? persistent = null)
        {
            _type = Type_ID.State;
            _playerID = playerID;
            _stateName = stateName;
            SetUp(rectGO, persistent);
        }


#if UNITY_2021_2_OR_NEWER
        public void SetUpState(VisualElement vEle,GameObject rectGO,string stateName,int playerID, bool? persistent = null)
        {
            _type = Type_ID.State;
            _playerID = playerID;
            _stateName = stateName;
            SetUp(vEle,rectGO, persistent);
        }
#endif

        public void SetStacking(float stackDelay)
        {
            if(!this.IsState())
                return;

            _isStackable = true;
            _stackDelay = stackDelay;
        }

        public void SetCloseMode(StateCloseMode_ID closeModeID,float closeDelay = 0f,List<string> customListToClose = default)
        {
            _closeMode = closeModeID;
            _closeDelay = closeDelay;
            _statesData = new SUStatesData(customListToClose);
        }

        #endregion

    }




}




