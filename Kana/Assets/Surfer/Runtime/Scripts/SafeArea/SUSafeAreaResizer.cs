using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    public class SUSafeAreaResizer : MonoBehaviour
    {

        public static HashSet<SUSafeAreaResizer> AllResizers = new HashSet<SUSafeAreaResizer>();//only for runtime

        RectTransform _rectT = default;
        Canvas _rootCanvas = default;

        void Awake() {

            GetComponents();

            AllResizers.Add(this);

        }

        public void Resize()
        {
            GetComponents();

            if(_rectT == null)
                return;
            if(_rootCanvas == null)
                return;

            var area = Screen.safeArea;
    
            var min = area.position;
            var max = area.position + area.size;

            min.x /= _rootCanvas.pixelRect.width;
            min.y /= _rootCanvas.pixelRect.height;
            max.x /= _rootCanvas.pixelRect.width;
            max.y /= _rootCanvas.pixelRect.height;
    
            _rectT.anchorMin = min;
            _rectT.anchorMax = max;
        }

        void GetComponents()
        {
            if(_rootCanvas == null)
                _rootCanvas = GetComponentInParent<Canvas>()?.rootCanvas;
            
            if(_rectT == null)
                _rectT = GetComponent<RectTransform>();
        }

        private void Reset() {
            Resize();
        }

        private void OnDestroy() {
            AllResizers.Remove(this);
        }

    }

}
