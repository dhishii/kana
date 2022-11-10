using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Surfer
{

    public partial class SUElement
    {

        SUScrollHandlerData _scrollHandler = default;

        public void CheckScroll()
        {
            _scrollHandler = new SUScrollHandlerData();
            _scrollHandler.OnPositionUpdate += OnPositionUpdate;
            _scrollHandler.AddScroll(ElementData,Events);
        }

        void OnPositionUpdate(SUScrollHandlerData.Position_ID positionID)
        {
            switch(positionID)
            {
                case SUScrollHandlerData.Position_ID.Bottom:
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.ScrollRect_OnReachedBottom);
                    break;
                case SUScrollHandlerData.Position_ID.Left:
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.ScrollRect_OnReachedLeft);
                    break;
                case SUScrollHandlerData.Position_ID.Top:
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.ScrollRect_OnReachedTop);
                    break;
                case SUScrollHandlerData.Position_ID.Right:
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.ScrollRect_OnReachedRight);
                    break;
                case SUScrollHandlerData.Position_ID.None:
                    _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.ScrollRect_OnNotReachedAnySide);
                    break;
            }
        }


        public void ResetScroll()
        {
            if (_scrollHandler == null)
                return;

            _scrollHandler.ResetScroll();
            _scrollHandler.OnPositionUpdate -= OnPositionUpdate;
        }

    }

}


