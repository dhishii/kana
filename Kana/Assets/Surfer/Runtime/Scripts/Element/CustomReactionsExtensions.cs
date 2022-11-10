using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Surfer
{


    public static class CustomReactionsExtensions 
    {
        
        public static List<PathField> GetFieldsList(string key )
        {
            
            if(CustomReactions.All.TryGetValue(key, out var value))
            {
                return value.Fields;
            }

            if(DefaultCustomReactions.All.TryGetValue(key, out var defaultValue))
            {
                return defaultValue.Fields;
            }

            return null;

        }

        
        /// <summary>
        /// Play a specific custom reaction 
        /// </summary>
        /// <param name="reactionKey">Reaction key</param>
        public static void PlayReaction(string key, SUElementData eleData, SUReactionData data,object evtData)
        {
            if (string.IsNullOrEmpty(key))
                return;
            if (key.Equals(SurferHelper.Unset))
                return;

            FuncInput input = new FuncInput(eleData, data, evtData);

            if (CustomReactions.All.TryGetValue(key, out PathAction value))
            {
                value.Action.Invoke(input);
            }
            else if (DefaultCustomReactions.All.TryGetValue(key, out PathAction defaultValue))
            {
                defaultValue.Action.Invoke(input);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("Reaction not found : " + key);
#endif
            }



        }

        /// <summary>
        /// Get the name/path of a specific reaction. Used for the inspector
        /// </summary>
        /// <param name="key">Reaction key to retrieve the name/path</param>
        /// <returns>Reaction name/path</returns>
        public static string GetNameFromUnion(string key)
        {
            string output = CustomReactions.GetName(key);

            if (string.IsNullOrEmpty(output))
            {
                output = DefaultCustomReactions.GetName(key);
            }

            return output;
        }

        /// <summary>
        /// Get the key of a specific reaction. Used for the inspector
        /// </summary>
        /// <param name="path">Reaction name/path to retrieve the key</param>
        /// <returns>Reaction key</returns>
        public static string GetKeyFromUnion(string path,SUCompatibility_ID compatibilityID = SUCompatibility_ID.Both)
        {

            string output = GetKey(path,compatibilityID);

            if(string.IsNullOrEmpty(output))
            {

                output = DefaultCustomReactions.GetKey(path,compatibilityID);

            }

            return output;
        }


        /// <summary>
        /// Get the key of a specific reaction. Used for the inspector
        /// </summary>
        /// <param name="path">Reaction name/path to retrieve the key</param>
        /// <returns>Reaction key</returns>
        public static string GetKey(string path, SUCompatibility_ID compatibilityID = SUCompatibility_ID.Both)
        {
            foreach(KeyValuePair<string, PathAction> pair in CustomReactions.All)
            {
                if(!pair.Value.Compatibility.IsCompatibleWith(compatibilityID))
                    continue;

                if(pair.Value.Path.Equals(path))
                return pair.Key;
            }
            return string.Empty;
        }


        /// <summary>
        /// Get all the canvas names/paths of the reactions. Used for the inspector
        /// </summary>
        /// <returns>Names/paths list</returns>
        public static string[] GetAllCanvasNames()
        {
            return CustomReactions.All.Where(x=>x.Value.Compatibility.IsCanvasCompatible()).Select(x=>x.Value.Path).OrderBy(x=>x).Prepend(SurferHelper.Unset).ToArray();
        }

        /// <summary>
        /// Get all the UIToolkit names/paths of the reactions. Used for the inspector
        /// </summary>
        /// <returns>Names/paths list</returns>
        public static string[] GetAllUIToolkitNames()
        {
            return CustomReactions.All.Where(x=>x.Value.Compatibility.IsUIToolkitCompatible()).Select(x=>x.Value.Path).OrderBy(x=>x).Prepend(SurferHelper.Unset).ToArray();
        }

    }


}
