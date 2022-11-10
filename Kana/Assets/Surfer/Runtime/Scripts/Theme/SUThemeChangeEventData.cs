using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    public struct SUThemeChangeEventData
    {
        public SUThemeData OldTheme { get; private set; }
        public SUThemeData NewTheme { get; private set; }

        public SUThemeChangeEventData(SUThemeData oldTheme,SUThemeData newTheme)
        {
            OldTheme = oldTheme;
            NewTheme = newTheme;
        }
    }
}

