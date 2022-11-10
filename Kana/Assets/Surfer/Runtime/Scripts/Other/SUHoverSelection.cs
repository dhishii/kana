using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Surfer
{
    public class SUHoverSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            var pID = gameObject.GetObjectStatePlayerID();

            if((SUEventSystemManager.I.GetEventSystem(pID)?.alreadySelecting) == false && GetComponent<Selectable>()!=null)
            {
                SUEventSystemManager.I.GetEventSystem(pID)?.SetSelectedGameObject(gameObject);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            var pID = gameObject.GetObjectStatePlayerID();

            if (GetComponent<Selectable>()!=null && SUEventSystemManager.I.GetEventSystem(pID)?.currentSelectedGameObject == gameObject)
            {
                SUEventSystemManager.I.GetEventSystem(pID)?.SetSelectedGameObject(null);
            }
        }
    }
}

