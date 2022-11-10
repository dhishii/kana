#if UNITY_2021_2_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Surfer
{
    public static class SUUIDocumentExtensions
    {

        public static List<string> GetElementsNames(this UIDocument doc)
        {
            var names = new List<string>();

            if (doc == null)
                return names;
            if(doc.rootVisualElement == null)
                return names;

            var allElements = doc.rootVisualElement.Query().ToList();

            foreach(var ele in allElements)
            {
                if (!ele.HasUserDefinedName())
                    continue;
                if (ele is TemplateContainer)
                    continue;

                names.Add(ele.name);
            }

            return names;
        }

    }
}

#endif

