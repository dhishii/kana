using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surfer
{
    [ExecuteAlways]
    public class SUSafeAreaManager : MonoBehaviour,
    ISUOrientationHandler
    {


        public static SUSafeAreaManager I { get; private set; } = default;

        DeviceOrientation _lastOrientation = default;
        static Rect _lastSafeArea = default;
        static Vector2 _lastResolution = default;

        void Awake() 
        {

            if(!Application.isPlaying)
                return;
            
            if(I==null)
                I = this;
            else
                Destroy(this);

            SUOrientationManager.I.RegisterOrientationEvent(this);

        }

#if UNITY_EDITOR
        private void Update() {
            MainLoop();
        }
#endif

        public void MainLoop()
        {
            if (Screen.safeArea != _lastSafeArea)
            {
                _lastSafeArea = Screen.safeArea;
                UpdateResizers();
            }

            if(Screen.width != _lastResolution.x || Screen.height != _lastResolution.y)
            {
                _lastResolution = new Vector2(Screen.width,Screen.height);
                UpdateResizers();
            }
        }

        void UpdateResizers()
        {
            if (Application.isPlaying)
            {
                foreach(var item in SUSafeAreaResizer.AllResizers)
                {
                    item.Resize();
                }
            }
            else
            {
                var resizers = GameObject.FindObjectsOfType<SUSafeAreaResizer>();
            
                foreach(var item in resizers)
                {
                    item.Resize();
                }
            }
        }

        public void OnOrientationChanged(SUOrientationInfo info)
        {
            UpdateResizers();
        }
    }
}