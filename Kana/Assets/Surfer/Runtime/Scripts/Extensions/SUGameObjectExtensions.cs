using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    public static class SUGameObjectExtensions
    {
        public static void GetCanvasAndCamerInfo(this GameObject go,out RectTransform canvasRect,out Camera cam,out bool isOverlay)
        {
            if(go == null)
            {
                canvasRect = null;
                cam = null;
                isOverlay = false;
                return;
            }

            var canvas = go.GetComponentInParent<Canvas>()?.rootCanvas;

            if (canvas == null)
            {
                cam = Camera.main;
                canvasRect = null;
                isOverlay = false;
            }
            else
            {
                isOverlay = canvas.renderMode == RenderMode.ScreenSpaceOverlay;
                cam = canvas.worldCamera;
                canvasRect = canvas.GetComponent<RectTransform>();

                if (cam == null)
                    cam = Camera.main;
            }
        }


        public static void GetSiblingStates(this GameObject obj,string myState,ref List<SUStateInfo> siblingStates)
        {

            foreach (Transform child in obj.transform.parent)
            {
                foreach(var data in SUElementData.AllUIStates)
                {
                    if(data.IsToolkit)
                        continue;
                    if (data.StateName.Equals(myState))
                        continue;

                    if(data.ObjOwner == child.gameObject)
                    {
                        siblingStates.Add(new SUStateInfo(data));
                    }
                }
            }  

        }


        /// <summary>
        /// Gets the parent state element of a game object
        /// </summary>
        /// <param name="obj"></param>
        public static SUElementData GetObjectStateElementData(this GameObject obj,bool checkCaller = false)
        {
            if (obj == null)
                return null;

            SUElementData eleData = default;

            if(checkCaller)
            {
                eleData = GetObjectStateElement(obj);
            }
            else
            {
                eleData = GetObjectStateElement(obj.transform.parent?.gameObject);
            }

            return eleData;
        }


        static SUElementData GetObjectStateElement(this GameObject obj)
        {
            if (obj == null)
                return null;

            SUElementData eleData = default;
            
            foreach(var data in SUElementData.AllUIStates)
            {
                if(data.IsToolkit)
                    continue;
                if(data.ObjOwner == obj)
                {
                    eleData = data;
                    break;
                }
            }

            if (eleData == null)
            {
                eleData = GetObjectStateElement(obj.transform.parent?.gameObject);
            }

            return eleData;
        }


        /// <summary>
        /// Gets the parent state name of a game object
        /// </summary>
        /// <param name="obj"></param>
        public static string GetObjectStateName(this GameObject obj, bool checkCaller = false)
        {
            var ele = GetObjectStateElementData(obj, checkCaller);

            if(ele == null)
                return string.Empty;

            return ele.StateName;
        }

        /// <summary>
        /// Gets the parent state of a game object
        /// </summary>
        /// <param name="obj"></param>
        public static Transform GetObjectStateTransfom(this GameObject obj, bool checkCaller = false)
        {
            var ele = GetObjectStateElementData(obj, checkCaller);

            if(ele == null)
                return null;

            return ele.ObjOwner.transform;

        }

        /// <summary>
        /// Gets the playerID of the parent state of a game object
        /// </summary>
        /// <param name="obj"></param>
        public static int GetObjectStatePlayerID(this GameObject obj, bool checkCaller = false)
        {

            var ele = GetObjectStateElementData(obj, checkCaller);

            return ele != null ? ele.PlayerID : SurferHelper.kPlayerIDFallback;
        }


        /// <summary>
        /// Recursively get the parent state of an object
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="output"></param>
        public static SUElementData GetParentState(this GameObject caller)
        {

            if (caller.transform.parent != null)
            {
                foreach(var data in SUElementData.AllUIStates)
                {
                    if(data.IsToolkit)
                        continue;
                    if(data.ObjOwner == caller.transform.parent.gameObject)
                    {
                        return data;
                    }
                }

                return GetParentState(caller.transform.parent.gameObject);

            }

            return null;
        }




        /// <summary>
        /// Get a list of all parent states 
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="output"></param>
        public static void GetParentStates(this GameObject caller, ref List<SUStateInfo> output)
        {

            if (caller.transform.parent != null)
            {
                foreach(var data in SUElementData.AllUIStates)
                {
                    if(data.IsToolkit)
                        continue;
                    if(data.ObjOwner == caller.transform.parent.gameObject)
                    {
                        output.Add(new SUStateInfo(data));
                        break;
                    }
                }

                GetParentStates(caller.transform.parent.gameObject, ref output);

            }

        }

        /// <summary>
        /// Get a list of all child states
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="output"></param>
        public static void GetChildStates(this GameObject caller, ref List<SUStateInfo> output)
        {

            foreach (Transform child in caller.transform)
            {

                foreach(var data in SUElementData.AllUIStates)
                {
                    if(data.IsToolkit)
                        continue;
                    if(data.ObjOwner == child.gameObject)
                    {
                        output.Add(new SUStateInfo(data));
                        break;
                    }
                }

                GetChildStates(child.gameObject, ref output);
            }

        }



        /// <summary>
        /// Check if a state of an object is open or not
        /// </summary>
        /// <param name="state">state name</param>
        /// <returns>true if open, false otherwise</returns>
        public static bool IsMyStateOpen(this GameObject caller,int version = SurferHelper.kWhateverVersion)
        {
            
            return SurferManager.I.IsOpen(GetObjectStateName(caller,true),version,GetObjectStatePlayerID(caller,true));

        }

    }
}

