using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Surfer
{
    public enum SUPlatform_ID
    {
        Mobile,
        Xbox,
        PlayStation,
        Desktop,
        Switch,
        Stadia,
    }

    public static class SUPlatformIDExtensions
    {
        public static SUPlatform_ID ToPlatformID(this int index)
        {
            return (SUPlatform_ID)index;
        }

        public static SUPlatform_ID? ToPlatformID(this string name)
        {
            //avoided a TryParse overload, in order to have compatibility with older Unity and C# versions
            //(to deprecate in the future)
            foreach (SUPlatform_ID value in System.Enum.GetValues(typeof(SUPlatform_ID)))
            {
                if (value.ToString().Equals(name))
                {
                    return value;
                }
            }

            return null;
        }

        public static SUPlatform_ID? ToPlatformID(this RuntimePlatform runPlatf)
        {

#if UNITY_EDITOR
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android ||
                EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
            {
                return SUPlatform_ID.Mobile;
            }
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.XboxOne
#if UNITY_2020_1_OR_NEWER
            ||
                EditorUserBuildSettings.activeBuildTarget == BuildTarget.GameCoreXboxSeries ||
                EditorUserBuildSettings.activeBuildTarget == BuildTarget.GameCoreXboxOne
#endif
                )
            {
                return SUPlatform_ID.Xbox;
            }
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.PS4
#if UNITY_2020_1_OR_NEWER
            ||
                EditorUserBuildSettings.activeBuildTarget == BuildTarget.PS5
#endif
                )
            {
                return SUPlatform_ID.PlayStation;
            }
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Switch)
            {
                return SUPlatform_ID.Switch;
            }
#if UNITY_2019_3_OR_NEWER
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Stadia)
            {
                return SUPlatform_ID.Stadia;
            }
#endif
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneLinux64 ||
                EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneOSX ||
                EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows ||
                EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64)
            {
                return SUPlatform_ID.Desktop;
            }
#if !UNITY_2019_2_OR_NEWER
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneLinuxUniversal ||
                EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneLinux)
            {
                return SUPlatform_ID.Desktop;
            }
#endif
#endif



            switch (runPlatf)
            {
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.LinuxEditor:
                case RuntimePlatform.LinuxPlayer:

                    return SUPlatform_ID.Desktop;

                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:

                    return SUPlatform_ID.Mobile;

                case RuntimePlatform.XboxOne:
#if UNITY_2020_1_OR_NEWER
                case RuntimePlatform.GameCoreXboxOne:
                case RuntimePlatform.GameCoreXboxSeries:
#endif

                    return SUPlatform_ID.Xbox;

                case RuntimePlatform.PS4:
#if UNITY_2020_1_OR_NEWER
                case RuntimePlatform.PS5:

                    return SUPlatform_ID.PlayStation;
#endif


                case RuntimePlatform.Switch:

                    return SUPlatform_ID.Switch;

#if UNITY_2019_3_OR_NEWER
                case RuntimePlatform.Stadia:

                    return SUPlatform_ID.Stadia;
#endif

            }

            return null;
        }
    }
}

