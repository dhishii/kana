using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if SUVis
using Unity.VisualScripting;
#endif


namespace Surfer
{

    public class SUOrientationManager : MonoBehaviour
    {

        public static SUOrientationManager I { get; private set; } = default;
        DeviceOrientation _orientation = DeviceOrientation.Unknown;
        HashSet<ISUOrientationHandler> _eventsReg = new HashSet<ISUOrientationHandler>();
        public const string kOrientationChanged = "_SUOrientationChanged";

        private void Awake() {
            
            if(I==null)
                I = this;
            else
                Destroy(this);

        }

        public void MainLoop()
        {

            if(_eventsReg.Count <= 0)
            return;

            if(Input.deviceOrientation != _orientation)
            {

                var evtData = new SUOrientationInfo(_orientation, Input.deviceOrientation);

                foreach(ISUOrientationHandler item in _eventsReg)
                {
                    if(item.Equals(null))
                        continue;

                    item.OnOrientationChanged(evtData);
                }
                
                _orientation = Input.deviceOrientation;

#if SUVis
                EventBus.Trigger(kOrientationChanged,evtData);
#endif

            }

        }


        public void RegisterOrientationEvent(ISUOrientationHandler interf)
        {
            _eventsReg.Add(interf);
        }


        public void UnregisterOrientationEvent(ISUOrientationHandler interf)
        {
            _eventsReg.Remove(interf);
        }

    }

}
