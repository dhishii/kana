using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace Surfer
{

    [DefaultExecutionOrder(-50)]
    public partial class SUElement : MonoBehaviour,
        //ILastStateSelectionHandler,
        IResetLastStateSelectionHandler
    {



        [System.Serializable]
        public class DictEvents : SerializableDictionary<SUEvent.Type_ID, SUBehavioursData> { }

        [SerializeField]
        bool _raycast2D3D = default;


        //only for editor
        [SerializeField]
        SUBehavioursData _behaviours = new SUBehavioursData();
        public SUBehavioursData Behaviours { get => _behaviours; }


        [SerializeField]
        SUElementData _elementData = default;
        public SUElementData ElementData { get => _elementData; }

        public bool IsState
        {
            get
            {
                return _elementData.IsState();
            }
        }

        [SerializeField]
        DictEvents _events = new DictEvents();
        public DictEvents Events { get => _events; }

        string _mySceneName = default;

        //cached components
        Camera _myCam = default;
        Image _img = default;
        TextMeshProUGUI _tmp = default;
        bool _isOverlay = default;
        RectTransform _canvasRect = default;
        Vector3 _dragPosIn = default;
        Vector3 _dragPosOut = default;
        float _dragLimitIn = default;
        float _dragSpeed = 0.15f;
        Vector3 _dragOffset = default;
        SUElement _parentState = default;
        public SUElement ParentState
        {
            get
            {

                if (_parentState == null)
                    _parentState = gameObject.GetObjectStateElementData()?.ObjOwner.GetComponent<SUElement>();

                return _parentState;
            }
        }

        #region Mono

        protected virtual void Awake()
        {

            Debug.LogWarning("Surfer Warning : Hey! You're using SUElement to manage your Canvas object; please consider migrating to SUElementCanvas instead : SUElement will be deprecated starting from v3");
            Initialize();

        }


        public void Initialize()
        {

            if (_elementData == null)
                return;

            _mySceneName = gameObject.scene.name;

            _elementData.SetUp(gameObject);

            CheckInput();
            CheckScroll();
            CheckInputField();
            CheckDropdown();
            CheckToggle();
            CheckSlider();
            CheckBuildVersion();
            CheckOrientationEvents();
            CheckChildrenEvents();
            CheckGroups();
            CheckCustomEvents();
            CheckStateEvents();
            CheckSceneEvents();
            CheckAnimations();
            CheckRaycast();
            CheckSelectionIndicator();

            _events.RunEventBehaviour(ElementData, SUEvent.Type_ID.MonoBehaviour_OnAwake);
        }

        // Start is called before the first frame update
        void Start()
        {
            CheckDragState();
            _events.RunEventBehaviour(ElementData, SUEvent.Type_ID.MonoBehaviour_OnStart);
        }


        private void OnEnable()
        {
            _events.RunEventBehaviour(ElementData, SUEvent.Type_ID.MonoBehaviour_OnEnable);
        }

        private void OnDisable()
        {
            _events.RunEventBehaviour(ElementData, SUEvent.Type_ID.MonoBehaviour_OnDisable);

        }

        private void OnApplicationFocus(bool focusStatus)
        {

            if (focusStatus)
            {
                _events.RunEventBehaviour(ElementData, SUEvent.Type_ID.MonoBehaviour_OnApplicationGainFocus);
            }
            else
            {
                _events.RunEventBehaviour(ElementData, SUEvent.Type_ID.MonoBehaviour_OnApplicationLoseFocus);
            }

        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _events.RunEventBehaviour(ElementData, SUEvent.Type_ID.MonoBehaviour_OnApplicationPaused);
            }
            else
            {
                _events.RunEventBehaviour(ElementData, SUEvent.Type_ID.MonoBehaviour_OnApplicationUnpaused);
            }
        }


        private void OnValidate()
        {

#if UNITY_EDITOR

            if (EditorApplication.isPlaying)
                return;

            if (this == null)
                return;

            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (EditorApplication.isPlaying)
                    return;
                if (this == null)
                    return;
                if (_elementData == null)
                    return;

                if (!_elementData.IsState() && !_elementData.IsGroupStates())
                {

                    var cpCanvas = gameObject.GetComponent<Canvas>();
                    if (cpCanvas != null && cpCanvas.isRootCanvas)
                        return;

                    if (gameObject.GetComponent<CanvasScaler>() != null)
                        GameObject.DestroyImmediate(gameObject.GetComponent<CanvasScaler>());
                    if (gameObject.GetComponent<GraphicRaycaster>() != null)
                        GameObject.DestroyImmediate(gameObject.GetComponent<GraphicRaycaster>());
                    if (cpCanvas != null)
                        GameObject.DestroyImmediate(cpCanvas);
                }

                if (_elementData.IsState())
                {
                    if (gameObject.GetComponent<CanvasGroup>() == null)
                        gameObject.AddComponent<CanvasGroup>();
                    if (gameObject.GetComponent<Canvas>() == null)
                        gameObject.AddComponent<Canvas>();
                    if (gameObject.GetComponent<CanvasScaler>() == null)
                        gameObject.AddComponent<CanvasScaler>();
                    if (gameObject.GetComponent<GraphicRaycaster>() == null)
                        gameObject.AddComponent<GraphicRaycaster>();
                }


            };



#endif


        }



        private void OnDestroy()
        {

            ResetScroll();
            ResetInputField();
            ResetDropdown();
            ResetToggle();
            ResetSlider();
            ResetOrientationEvents();
            ResetChildrenEvents();
            ResetCustomEvents();
            ResetStateEvents();
            ResetSceneEvents();
            ResetAnimations();
            ResetTooltip();
            _elementData.HandleOnDestroy();
        }



        #endregion


        #region Runtime Injection

        public void InjectStateElementData(string stateName, int playerID, SUElementData.StateCloseMode_ID closeModeID)
        {
            _elementData = new SUElementData();
            _elementData.InjectStateData(stateName, playerID, closeModeID);
        }

        public void InjectNormalElementData()
        {
            _elementData = new SUElementData();
        }

        #endregion


        #region ResetLogic


        void ResetAnimations()
        {
            foreach (KeyValuePair<SUEvent.Type_ID, SUBehavioursData> pair in _events)
            {
                foreach (SUBehaviourData item in pair.Value.Behaviours)
                    item.KillAnimations();
            }
        }


        #endregion



        #region LogicAndChecks



        void GetMyCamera()
        {
            if (_myCam != null)
                return;

            gameObject.GetCanvasAndCamerInfo(out _canvasRect, out _myCam, out _isOverlay);
        }


        void CheckAnimations()
        {
            foreach (KeyValuePair<SUEvent.Type_ID, SUBehavioursData> pair in _events)
            {
                foreach (SUBehaviourData item in pair.Value.Behaviours)
                    item.CacheAnimations(gameObject);
            }
        }


        void CheckRaycast()
        {

            if (_raycast2D3D)
            {
                GetMyCamera();

                if (_myCam == null)
                    return;


                if (GetComponent<MeshRenderer>() != null)
                {
                    if (GetComponent<Collider>() == null)
                        gameObject.AddComponent<BoxCollider>().isTrigger = true;

                    if (_myCam.GetComponent<PhysicsRaycaster>() == null)
                        _myCam.gameObject.AddComponent<PhysicsRaycaster>();


                    CheckGraphicRaycaster();
                }
                else if (GetComponent<SpriteRenderer>() != null)
                {
                    if (GetComponent<Collider2D>() == null)
                        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;


                    if (_myCam.GetComponent<Physics2DRaycaster>() == null)
                        _myCam.gameObject.AddComponent<Physics2DRaycaster>();

                    CheckGraphicRaycaster();

                }
            }


            void CheckGraphicRaycaster()
            {
                var cp = GetComponentInParent<GraphicRaycaster>();
                if (cp != null)
                    cp.blockingObjects = GraphicRaycaster.BlockingObjects.All;
            }


        }

        #endregion


        #region Cached Components


        TextMeshProUGUI GetTmp()
        {
            if (_tmp == null)
                _tmp = gameObject.GetComponent<TextMeshProUGUI>();

            return _tmp;
        }

        Image GetImg()
        {
            if (_img == null)
                _img = gameObject.GetComponent<Image>();

            return _img;
        }


        #endregion


    }


}

