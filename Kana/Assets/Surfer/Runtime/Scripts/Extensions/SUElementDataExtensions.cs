using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Surfer.SUElementData;

namespace Surfer
{
    public static class SUElementDataExtensions
    {
        //from type
        public static bool IsState(this Type_ID type)
        {
            return type == Type_ID.State
                    || type.IsTooltip()
                    || type.IsDrag();
        }

        public static bool IsTooltip(this Type_ID type)
        {
            return type == Type_ID.Tooltip_State;
        }

        public static bool IsDrag(this Type_ID type)
        {
            return type == Type_ID.DragRight_State
                    || type == Type_ID.DragLeft_State
                    || type == Type_ID.DragUp_State
                    || type == Type_ID.DragDown_State;
        }

        public static bool IsLoading(this Type_ID type)
        {
            return type == Type_ID.Loading_Image
                    || type == Type_ID.Loading_Text;
        }


        public static bool IsGroupStates(this Type_ID type)
        {
            return type == Type_ID.GroupStates_OnOff
                    || type == Type_ID.GroupStates_InOut
                    || type == Type_ID.GroupStates_OnOffAndInOut;
        }


        public static bool IsGroup(this Type_ID type)
        {
            return type.IsGroupButtons() || type.IsGroupStates();
        }

        public static bool IsGroupButtons(this Type_ID type)
        {
            return type == Type_ID.GroupButtons;
        }




        //from class
        public static bool IsState(this SUElementData data)
        {
            return data.Type == Type_ID.State
                    || data.Type.IsTooltip()
                    || data.Type.IsDrag();
        }

        public static bool IsTooltip(this SUElementData data)
        {
            return data.Type == Type_ID.Tooltip_State;
        }

        public static bool IsDrag(this SUElementData data)
        {
            return IsVerticalDrag(data) || IsHorizontalDrag(data);
        }

        public static bool IsVerticalDrag(this SUElementData data)
        {
            return data.Type == Type_ID.DragUp_State
                    || data.Type == Type_ID.DragDown_State;
        }

        public static bool IsHorizontalDrag(this SUElementData data)
        {
            return data.Type == Type_ID.DragRight_State
                    || data.Type == Type_ID.DragLeft_State;
        }

        public static bool IsLoading(this SUElementData data)
        {
            return data.Type == Type_ID.Loading_Image
                    || data.Type == Type_ID.Loading_Text;
        }


        public static bool IsGroupStates(this SUElementData data)
        {
            return data.Type == Type_ID.GroupStates_OnOff
                    || data.Type == Type_ID.GroupStates_InOut
                    || data.Type == Type_ID.GroupStates_OnOffAndInOut;
        }


        public static bool IsGroup(this SUElementData data)
        {
            return data.Type.IsGroupButtons() || data.Type.IsGroupStates();
        }

        public static bool IsGroupButtons(this SUElementData data)
        {
            return data.Type == Type_ID.GroupButtons;
        }

#if UNITY_2021_2_OR_NEWER

        public static bool IsAvailableNow(this SUElementData data)
        {
            return data.Query.Availability == SUTKQueryData.Availability_ID.Now;
        }

        public static bool IsAvailableLater(this SUElementData data)
        {
            return data.Query.Availability == SUTKQueryData.Availability_ID.Later;
        }

        public static bool IsTkRoot(this SUElementData data)
        {
            return data.Query.TKName.Equals(SUTKQueryData.kDocRootName);
        }

#endif

    }

}
