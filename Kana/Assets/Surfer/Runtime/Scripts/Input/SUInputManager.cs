using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;
#endif

#if SUNew
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
#endif

#if SURew
using Rewired;
#endif

namespace Surfer
{

    public class SUInputManager : MonoBehaviour
    {

        public static SUInputManager I { get; private set; } = default;
        Dictionary<KeyCode, List<ISUOldInputKeyHandler>> _oldKeyReg = new Dictionary<KeyCode, List<ISUOldInputKeyHandler>>();
        Dictionary<string, List<ISUOldInputActionHandler>> _oldActionReg = new Dictionary<string, List<ISUOldInputActionHandler>>();
        Dictionary<string, List<ISUNewInputActionHandler>> _newActionReg = new Dictionary<string, List<ISUNewInputActionHandler>>();
        Dictionary<string, List<ISURewiredActionHandler>> _rewiredReg = new Dictionary<string, List<ISURewiredActionHandler>>();
        List<ISUOldInputAnyKeyHandler> _anyOldKeyReg = new List<ISUOldInputAnyKeyHandler>();
        List<ISUNewInputAnyHandler> _anyNewReg = new List<ISUNewInputAnyHandler>();

#if SUNew
        bool _newInputAnyRegistered = default;
        IDisposable _newInputAnyDisposable = default;
        Dictionary<string, UnityEngine.InputSystem.InputAction> _newActionsInfos = new Dictionary<string, UnityEngine.InputSystem.InputAction>();
#endif

        private void Awake() {
            
            if(I==null)
                I = this;
            else
                Destroy(this);

        }


#region Register

        public void RegisterOldInputAnyKey(ISUOldInputAnyKeyHandler interf)
        {
            _anyOldKeyReg.Add(interf);
        }

        public void RegisterOldInputKey(KeyCode kCode,ISUOldInputKeyHandler interf)
        {
            if (_oldKeyReg.TryGetValue(kCode,out var list))
            {
                list.Add(interf);
            }
            else
            {
                _oldKeyReg.Add(kCode, new List<ISUOldInputKeyHandler>(){ interf });
            }
        }

        public void RegisterOldInputAction(string actionName,ISUOldInputActionHandler interf)
        {
            if (_oldActionReg.TryGetValue(actionName,out var list))
            {
                list.Add(interf);
            }
            else
            {
                _oldActionReg.Add(actionName, new List<ISUOldInputActionHandler>(){ interf });
            }
        }


#if SUNew

        public void RegisterNewInputAny(ISUNewInputAnyHandler interf)
        {
            if(!_newInputAnyRegistered)
            {
                _newInputAnyRegistered = true;
                _newInputAnyDisposable = InputSystem.onAnyButtonPress.Call(OnNewInputeAny);
            }

            _anyNewReg.Add(interf);

        }

        void OnNewInputeAny(InputControl evtData)
        {
            foreach(var interf in _anyNewReg)
            {
                if(interf.Equals(null))
                        continue;
                interf.OnNewInputAny();
            }
        }

        public void RegisterNewInputAction(InputActionAsset pInput,string actionName,ISUNewInputActionHandler interf)
        {
            if (pInput == null)
            return;

            foreach (InputActionMap actionMap in pInput.actionMaps)
            {

                foreach (UnityEngine.InputSystem.InputAction action in actionMap.actions)
                {
                    if (action.name.Equals(actionName))
                    {
                        if (_newActionReg.TryGetValue(actionName,out var list))
                        {
                            list.Add(interf);
                        }
                        else
                        {
                            _newActionReg.Add(actionName,new List<ISUNewInputActionHandler>{ interf });

                            _newActionsInfos.Add(actionName,action);

                            action.performed += Performed;
                            action.Enable();
                        }
                        
                    }

                }
            }
        }

        void Performed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            var actionName = ctx.action.name;
            if (_newActionReg.TryGetValue(actionName,out var list))
            {
                var eventData = new SUNewInputActionEventData(actionName);
                foreach(var interf in list)
                {
                    if(interf.Equals(null))
                        continue;

                    interf.OnNewInputAction(eventData);
                }
            }
        }

#endif

#if SURew

        public void RegisterRewiredAction(string actionName,int playerID,UpdateLoopType updateLoop,InputActionEventType inputType,ISURewiredActionHandler interf)
        {
            if (!ReInput.isReady)
                    return;

            if (_rewiredReg.TryGetValue(actionName,out var list))
            {
                list.Add(interf);
            }
            else
            {
                _rewiredReg.Add(actionName,new List<ISURewiredActionHandler>(){ interf });

                if (playerID == 0)
                {

                    foreach (Player playerItem in ReInput.players.Players)
                    {
                        ReInput.players.GetPlayer(playerItem.id).AddInputEventDelegate(OnActionPerformed,
                            updateLoop,
                            inputType,
                            actionName);
                    }

                }
                else
                {

                    ReInput.players.GetPlayer(playerID).AddInputEventDelegate(OnActionPerformed,
                            updateLoop,
                            inputType,
                            actionName);

                }
            }
        }

        void OnActionPerformed(InputActionEventData args)
        {
            if (_rewiredReg.TryGetValue(args.actionName,out var list))
            {
                var evtData = new SURewiredActionEventData(args);
                foreach(var interf in list)
                {
                    if(interf.Equals(null))
                        continue;

                    interf.OnRewiredAction(evtData);
                }
            }
        }

#endif


#endregion



        public void MainLoop()
        {

#if SUOld

            if(Input.anyKeyDown)
            {
                foreach(var interf in _anyOldKeyReg)
                {
                    if(interf.Equals(null))
                        continue;
                    interf.OnOldInputAnyKey();
                }
            }

            foreach(var pair in _oldKeyReg)
            {
                if (Input.GetKeyDown(pair.Key))
                {
                    var eventData = new SUOldInputKeyEventData(pair.Key);
                    foreach(var interf in pair.Value)
                    {
                        if(interf.Equals(null))
                        continue;

                        interf.OnOldInputKey(eventData);
                    }
                }
            }

            foreach(var pair in _oldActionReg)
            {
                if (Input.GetButtonDown(pair.Key))
                {
                    var eventData = new SUOldInputActionEventData(pair.Key);
                    foreach(var interf in pair.Value)
                    {
                        if(interf.Equals(null))
                        continue;

                        interf.OnOldInputAction(eventData);
                    }
                }
            }

#endif

        }


        #region Unregister

        public void ResetInput()
        {

#if SUOld

            _oldKeyReg.Clear();
            _oldActionReg.Clear();
            _anyOldKeyReg.Clear();

#endif

#if SURew

            if (ReInput.isReady)
            {
                foreach (Player playerItem in ReInput.players.Players)
                {
                    ReInput.players.GetPlayer(playerItem.id).RemoveInputEventDelegate(OnActionPerformed);
                }
            }

#endif

#if SUNew

            foreach(var pair in _newActionsInfos)
            {
                pair.Value.performed -= Performed;
                pair.Value.Disable();
            }

            _newInputAnyDisposable?.Dispose();

#endif


        }

        #endregion


#if UNITY_2021_2_OR_NEWER

        public void RegisterSingleOrDoubleClickEvent(VisualElement vEle,System.Action OnClick,System.Action OnDoubleClick)
        {
            var totalClicks = 0;
            var isCoroutineRunning = false;

            vEle.RegisterCallback<PointerUpEvent>((evtData) =>
            {
                totalClicks += 1;

                if(!isCoroutineRunning)
                {                    
                    isCoroutineRunning = true;
                    StartCoroutine(WaitForClick(SurferHelper.SO.DoubleClickDelay, () =>
                    {
                        if (totalClicks > 1)
                        {
                            OnDoubleClick?.Invoke();
                        }
                        else
                        {
                            OnClick?.Invoke();
                        }

                        totalClicks = 0;
                        isCoroutineRunning = false;

                    }));
                }

            });
        }

        IEnumerator WaitForClick(float seconds,System.Action OnWaitCompleted)
        {
            yield return new WaitForSeconds(seconds);

            OnWaitCompleted?.Invoke();
        }


#endif

        private void OnDestroy() {

            ResetInput();

        }



    }

}
