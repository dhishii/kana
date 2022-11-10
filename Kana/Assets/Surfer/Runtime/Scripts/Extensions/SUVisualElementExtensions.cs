
#if UNITY_2021_2_OR_NEWER

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


namespace  Surfer
{
    //all add/remove methods miss variable that says who is the visualElement added or removed 
    public static class SUVisualElementExtensions 
    {
        
        public static void SUAdd(this VisualElement vEle, VisualElement child)
        {
            vEle.Add(child);

            using (SUTKNewElementEvent rootEvt = SUTKNewElementEvent.GetPooled())
            {
                rootEvt.target = child.GetRootVisualElement();
                rootEvt.ElementAdded = child;
                child.SendEvent(rootEvt);
            }

            using (SUTKChildAddedEvent parentEvt = SUTKChildAddedEvent.GetPooled())
            {
                parentEvt.target = vEle;
                parentEvt.ElementAdded = child;
                child.SendEvent(parentEvt);
            }
        }


        public static void SURemove(this VisualElement vEle, VisualElement child)
        {

            var oldTKEvent = new SUTKOldElementEvent() { target = child.GetRootVisualElement(), ElementRemoved = child };

            using (SUTKOldElementEvent evt = SUTKOldElementEvent.GetPooled())
            {
                child.SendEvent(evt);
            }

            var oldChildEvent = new SUTKChildRemovedEvent() { target = vEle , ElementRemoved = child};

            using (SUTKChildRemovedEvent evt2 = SUTKChildRemovedEvent.GetPooled())
            {
                child.SendEvent(evt2);
            }


            vEle.Remove(child);

            oldTKEvent.Dispose();
            oldChildEvent.Dispose();
            
        }


        public static VisualElement GetRootVisualElement(this VisualElement vEle)
        {
            if (vEle == null)
                return null;

            VisualElement root = vEle;

            VisualElement parent = vEle.parent;

            while( parent != null)
            {
                if(parent is TemplateContainer)
                {
                    root = parent;
                    break;
                }

                parent = parent.parent;

            }

            return root;
        }


        public static VisualElement GetPanelSettingsElement(this VisualElement vEle)
        {
            if (vEle == null)
                return null;

            VisualElement panel = vEle.parent;

            while( panel != null)
            {
                if(panel.parent != null)
                    panel = panel.parent;
                else
                    break;
            }

            return panel;
        }


        public static void SURemoveFromHierarchy(this VisualElement vEle)
        {

            var oldTkEvent = new SUTKOldElementEvent() { target = vEle.GetRootVisualElement(), ElementRemoved = vEle };

            using (SUTKOldElementEvent evt = SUTKOldElementEvent.GetPooled())
            {
                vEle.SendEvent(evt);
            }


            var oldChildEvent = new SUTKChildRemovedEvent() { target = vEle.parent , ElementRemoved = vEle};

            using (SUTKChildRemovedEvent evt2 = SUTKChildRemovedEvent.GetPooled())
            {
                vEle.SendEvent(evt2);
            }


            vEle.RemoveFromHierarchy();
            
            oldTkEvent.Dispose();
            oldChildEvent.Dispose();

        }

        public static void SURemoveAt(this VisualElement vEle,int index)
        {
            if(index >= vEle.childCount)
                return;


            var oldTkEvent = new SUTKOldElementEvent() { target = vEle.GetRootVisualElement(), ElementRemoved = vEle.ElementAt(index) };

            using(SUTKOldElementEvent evt = SUTKOldElementEvent.GetPooled())
            {
                vEle.SendEvent(evt);
            }


            var oldChildEvent = new SUTKChildRemovedEvent(){ target = vEle, ElementRemoved = vEle.ElementAt(index)};

            using (SUTKChildRemovedEvent evt2 = SUTKChildRemovedEvent.GetPooled())
            {
                vEle.SendEvent(evt2);
            }

            vEle.RemoveAt(index);

            oldTkEvent.Dispose();
            oldChildEvent.Dispose();

        }


#region SUElementData
        public static string GetVEleStateName(this VisualElement visualElement)
        {
            if(visualElement == null)
                return string.Empty;

            var data = visualElement.parent.GetVEleStateElementData();

            return data != null ? data.StateName : string.Empty;
        }

        public static SUElementData GetVEleStateElementData(this VisualElement visualElement)
        {
            if(visualElement == null)
                return null;

            foreach(var item in SUElementData.AllUIStates)
            {
                if(!item.IsToolkit)
                    continue;
                if(item.VElement == visualElement)
                {
                    return item;
                }
            }

            return GetVEleStateElementData(visualElement.parent);
        }


        public static int GetVElePlayerID(this VisualElement visualElement)
        {
            if(visualElement == null)
                return SurferHelper.kPlayerIDFallback;

            var data = visualElement.parent.GetVEleStateElementData();

            return data != null ? data.PlayerID : SurferHelper.kPlayerIDFallback;
        }

        public static void GetChildStates(this VisualElement visualElement,ref List<SUStateInfo> childStates)
        {
            if(visualElement == null)
                return;

            var list = visualElement.Children().ToList();

            foreach(var item in list)
            {
                foreach(var stateData in SUElementData.AllUIStates)
                {
                    if(!stateData.IsToolkit)
                        continue;
                    if(stateData.VElement == item)
                    {
                        childStates.Add(new SUStateInfo(stateData));
                        break;
                    }
                }

                item.GetChildStates(ref childStates);
            }

        }


        public static void GetParentStates(this VisualElement visualElement,ref List<SUStateInfo> parentStates)
        {
            if(visualElement == null)
                return;


            foreach(var stateData in SUElementData.AllUIStates)
            {
                if(!stateData.IsToolkit)
                    continue;
                if(stateData.VElement == visualElement.parent)
                {
                    parentStates.Add(new SUStateInfo(stateData));
                }
            }

            visualElement.parent.GetParentStates(ref parentStates);

        }

        public static void GetSiblingStates(this VisualElement visualElement,string myState,ref List<SUStateInfo> siblingStates)
        {

            if(visualElement == null)
                return;
            if(visualElement.parent == null)
                return;

            var list = visualElement.parent.Children().ToList();

            foreach(var item in list)
            {
                foreach(var data in SUElementData.AllUIStates)
                {
                    if(!data.IsToolkit)
                        continue;
                    if (data.StateName.Equals(myState))
                        continue;

                    if(data.VElement == item)
                    {
                        siblingStates.Add(new SUStateInfo(data));
                    }
                }
            }

        }

#endregion
        
        public static bool HasUserDefinedName(this VisualElement vEle)
        {
            if (string.IsNullOrEmpty(vEle.name))
                return false;
            if (vEle.name.Contains("unity"))
                return false;

            return true;
        }

        public static void RegisterSingleOrDoubleClickEvent(this VisualElement vEle,System.Action OnClick,System.Action OnDoubleClick)
        {

            SUInputManager.I.RegisterSingleOrDoubleClickEvent(vEle,OnClick,OnDoubleClick);

        }
    }

}


#endif
