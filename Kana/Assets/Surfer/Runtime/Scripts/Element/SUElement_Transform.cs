using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{

    public partial class SUElement : ISUChildrenMonitorHandler
    {
        
        void CheckChildrenEvents()
        {

            if(_events.ContainsKey(SUEvent.Type_ID.Transform_OnChildAdded)
            || _events.ContainsKey(SUEvent.Type_ID.Transform_OnChildRemoved)
            || _events.ContainsKey(SUEvent.Type_ID.Transform_OnChildrenCountChanged))
            {
                SUChildMonitorManager.I?.RegisterChildrenEvents(this,transform);
            }
        }


        public void OnChildrenChanged(SUChildrenMonitorData data)
        {

            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Transform_OnChildrenCountChanged,data);

            if(data.Mode == SUChildrenMonitorData.Mode_ID.Added)
                _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Transform_OnChildAdded,data);
            else if(data.Mode == SUChildrenMonitorData.Mode_ID.Removed)
                _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Transform_OnChildRemoved,data);

        }


        void ResetChildrenEvents()
        {

            if(_events.ContainsKey(SUEvent.Type_ID.Transform_OnChildAdded)
            || _events.ContainsKey(SUEvent.Type_ID.Transform_OnChildRemoved)
            || _events.ContainsKey(SUEvent.Type_ID.Transform_OnChildrenCountChanged))
            {
                SUChildMonitorManager.I?.UnregisterChildrenEvents(transform);
            }

        }

    
        private void OnTransformParentChanged() {

            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Transform_OnParentChanged);

            if(transform.parent == null)
                _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.Transform_OnParentLost);
            
        }

    }

}

