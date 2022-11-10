using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    public class SUThemeManager : MonoBehaviour
    {
#if UNITY_2019_3_OR_NEWER

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            I = null;
        }

#endif

        string _currentThemeGUID = default;
        HashSet<ISUThemeChangeHandler> _listeners = new HashSet<ISUThemeChangeHandler>();

        public static SUThemeManager I {get;private set;}

        void Awake()
        {

            if(I==null)
            {
                I=this;
                _currentThemeGUID = SurferHelper.SO.SelectedThemeGUID;
            }
            else
                Destroy(this);

        }

        public void ChangeTheme(string themeName)
        {
            var guid = SurferHelper.SO.GetThemeKey(themeName);

            if(_currentThemeGUID == guid)
                return;

            var evtData = new SUThemeChangeEventData(SurferHelper.SO.GetTheme(_currentThemeGUID),SurferHelper.SO.GetTheme(guid));

            foreach(var listener in _listeners)
            {
                if(listener.Equals(null))
                    continue;
                    
                listener.OnThemeChanged(evtData);
            }

            _currentThemeGUID = guid;

        }

        public void RegisterThemeChange(ISUThemeChangeHandler interf)
        {
            _listeners.Add(interf);
        }

    }
}

