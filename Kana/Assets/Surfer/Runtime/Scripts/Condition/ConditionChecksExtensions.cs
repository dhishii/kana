using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Surfer
{
    public static class ConditionChecksExtensions 
    {
        
        public static List<PathField> GetFieldsList(string key )
        {
            
            if(ConditionChecks.All.TryGetValue(key, out var value))
            {
                return value.Fields;
            }

            if(DefaultConditionChecks.All.TryGetValue(key, out var defaultValue))
            {
                return defaultValue.Fields;
            }


            return null;

        }


        /// <summary>
        /// Check if a specific condition is satisfied
        /// </summary>
        /// <param name="conditionKey">Condition key</param>
        /// <returns>true if satisfied, false otherwise</returns>
        public static bool IsSatisfied(string conditionKey,SUElementData eleData,SUConditionData data,object evtData)
        {
            if(string.IsNullOrEmpty(conditionKey))
            return true;
            if(conditionKey.Equals(SurferHelper.Unset))
            return true;

            FuncInput inputData = new FuncInput(eleData,data,evtData);

            if(ConditionChecks.All.TryGetValue(conditionKey,out PathFunc value))
            {
                return value.Function.Invoke(inputData) == true;
            }
            if(DefaultConditionChecks.All.TryGetValue(conditionKey,out PathFunc valueDefault))
            {
                return valueDefault.Function.Invoke(inputData) == true;
            }

#if UNITY_EDITOR
            Debug.LogWarning("Condition not found: "+conditionKey);
#endif
            return false;
        }


        /// <summary>
        /// Get the name/path of a specific condition. Used for the inspector
        /// </summary>
        /// <param name="key">Condition key to retrieve the name/path</param>
        /// <returns>Condition name/path</returns>
        public static string GetNameFromUnion(string key)
        {
            string output = ConditionChecks.GetName(key);

            if(string.IsNullOrEmpty(output))
            {
                output = DefaultConditionChecks.GetName(key);
            }

            return output;
        }

        /// <summary>
        /// Get the key of a specific condition. Used for the inspector
        /// </summary>
        /// <param name="path">Condition name/path to retrieve the key</param>
        /// <returns>Condition key</returns>
        public static string GetKeyFromUnion(string path,SUCompatibility_ID compatibilityID = SUCompatibility_ID.Both)
        {

            string output = GetKey(path,compatibilityID);

            if(string.IsNullOrEmpty(output))
            {
                output = DefaultConditionChecks.GetKey(path,compatibilityID);
            }

            return output;
        }

         /// <summary>
        /// Get the key of a specific condition. Used for the inspector
        /// </summary>
        /// <param name="path">Condition name/path to retrieve the key</param>
        /// <returns>Condition key</returns>
        public static string GetKey(string path, SUCompatibility_ID compatibilityID = SUCompatibility_ID.Both)
        {
            foreach(KeyValuePair<string,PathFunc> pair in ConditionChecks.All)
            {
                if(!pair.Value.Compatibility.IsCompatibleWith(compatibilityID))
                    continue;
                    
                if(pair.Value.Path.Equals(path))
                return pair.Key;
            }
            return "";
        }


        /// <summary>
        /// Get all the canvas names/paths of the reactions. Used for the inspector
        /// </summary>
        /// <returns>Names/paths list</returns>
        public static string[] GetAllCanvasNames()
        {
            return ConditionChecks.All.Where(x=>x.Value.Compatibility.IsCanvasCompatible()).Select(x=>x.Value.Path).OrderBy(x=>x).Prepend(SurferHelper.Unset).ToArray();
        }

        /// <summary>
        /// Get all the UIToolkit names/paths of the reactions. Used for the inspector
        /// </summary>
        /// <returns>Names/paths list</returns>
        public static string[] GetAllUIToolkitNames()
        {
            return ConditionChecks.All.Where(x=>x.Value.Compatibility.IsUIToolkitCompatible()).Select(x=>x.Value.Path).OrderBy(x=>x).Prepend(SurferHelper.Unset).ToArray();
        }

    }


}

