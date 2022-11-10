using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    
    public static class SUEventExtensions 
    {
        
        public static bool IsMyState(this SUEvent.Type_ID evt)
        {
            return evt == SUEvent.Type_ID.State_MyStateEnter || evt == SUEvent.Type_ID.State_MyStateExit;
        }

        public static bool IsCustomState(this SUEvent.Type_ID evt)
        {
            return evt == SUEvent.Type_ID.State_Enter || evt == SUEvent.Type_ID.State_Exit;
        }

        public static bool IsState(this SUEvent.Type_ID evt)
        {
            return evt.IsMyState() || evt.IsCustomState();
        }

        public static bool IsCustomScene(this SUEvent.Type_ID evt)
        {
            return evt == SUEvent.Type_ID.Scene_Activated || evt == SUEvent.Type_ID.Scene_Deactivated
                || evt == SUEvent.Type_ID.Scene_Loaded || evt == SUEvent.Type_ID.Scene_Loading
                || evt == SUEvent.Type_ID.Scene_Unloaded || evt == SUEvent.Type_ID.Scene_Unloading;
        }

        public static bool IsMyScene(this SUEvent.Type_ID evt)
        {
            return evt == SUEvent.Type_ID.Scene_MySceneActivated || evt == SUEvent.Type_ID.Scene_MySceneDeactivated
                || evt == SUEvent.Type_ID.Scene_MySceneLoaded || evt == SUEvent.Type_ID.Scene_MySceneLoading
                || evt == SUEvent.Type_ID.Scene_MySceneUnloaded || evt == SUEvent.Type_ID.Scene_MySceneUnloading;
        }

    }

}