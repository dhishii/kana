using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.PointerEventData;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Surfer
{

    public partial class SUElement :
        IPointerClickHandler ,IPointerDownHandler, IPointerUpHandler,
        IPointerEnterHandler, IPointerExitHandler,
        ISelectHandler, IDeselectHandler,
        ISubmitHandler,
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        
        public void OnPointerClick(PointerEventData eventData)
        {

            if (eventData.clickCount == 1)
                _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnClick,eventData);
            if (eventData.clickCount == 2)
                _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnDoubleClick,eventData);
            if (eventData.clickCount == 1 && eventData.button == InputButton.Right)
                _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnMouseRightClick,eventData);
        }

        public void OnDeselect(BaseEventData eventData)
        {

            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnDeselect,eventData);

            if(SUEventSystemManager.I.IsHistoryReceiver(gameObject)
                && gameObject.IsMyStateOpen())
            {
                _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnBecomeLastStateSelection,eventData);
            }


        }


        //public void OnBecomeLastStateSelection(SULastSelectionEventData eventInfo)
        //{
        //    Debug.Log(" i became "+name);
        //    RunEventBehaviour(SUEvent.Type_ID.UIGeneric_OnBecomeLastStateSelection);
        //}


        public void OnSelect(BaseEventData eventData)
        {

            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnSelect,eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if((SUEventSystemManager.I.GetEventSystem(ElementData.PlayerID)?.alreadySelecting) == false && GetComponent<Selectable>()!=null)
            {
                SUEventSystemManager.I.GetEventSystem(ElementData.PlayerID)?.SetSelectedGameObject(gameObject);
            }

            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnEnter,eventData);
        }  

        public void OnPointerDown(PointerEventData eventData)
        {

            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnPointerDown,eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {

            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnPointerUp,eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {


            if (GetComponent<Selectable>()!=null && SUEventSystemManager.I.GetEventSystem(ElementData.PlayerID)?.currentSelectedGameObject == gameObject)
            {
                SUEventSystemManager.I.GetEventSystem(ElementData.PlayerID)?.SetSelectedGameObject(null);
                //GetComponent<Selectable>()?.OnDeselect(eventData);
            }


            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnExit,eventData);


        }




        public void OnSubmit(BaseEventData eventData)
        {
            _events.RunEventBehaviour(ElementData,SUEvent.Type_ID.UIGeneric_OnSubmit,eventData);
        }


        public void OnResetLastStateSelection()
        {
            OnDeselect(null);
        }


        public void OnBeginDrag(PointerEventData eventData)
        {

            CheckDragTypeForBeginDrag(eventData);

            /// Check used by drag event handlers (even onDrag and onEndDrag). It is used because when a child is dragged it should propagate the event 
            /// to the parent otherwise the latter cannot be dragged
            if(!_elementData.IsState())
            {
                //propagate events to parent state
                ParentState?.OnBeginDrag(eventData);

            }


        }

        public void OnDrag(PointerEventData eventData)
        {

            CheckDragTypeForDrag(eventData);

            if (!_elementData.IsState())
            {
                //propagate events to parent state
                ParentState?.OnDrag(eventData);
            }

        }

        public void OnEndDrag(PointerEventData eventData)
        {

            CheckDragTypeForEndDrag(eventData);

            if (!_elementData.IsState())
            {
                //propagate events to parent state
                ParentState?.OnEndDrag(eventData);

            }


        }

        
    }

    
}
