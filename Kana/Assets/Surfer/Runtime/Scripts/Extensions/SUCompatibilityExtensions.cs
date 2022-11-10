using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surfer
{
    public static class SUCompatibilityExtensions
    {
        
        public static bool IsCanvasCompatible(this SUCompatibility_ID compatibility)
        {
            return compatibility == SUCompatibility_ID.Canvas || compatibility == SUCompatibility_ID.Both;
        }
        
        public static bool IsUIToolkitCompatible(this SUCompatibility_ID compatibility)
        {
            return compatibility == SUCompatibility_ID.UIToolkit || compatibility == SUCompatibility_ID.Both;
        }

        public static bool IsCompatibleWith(this SUCompatibility_ID compatibility,SUCompatibility_ID toCompare)
        {
            if(toCompare == SUCompatibility_ID.Both)
                return true;
            if(compatibility == SUCompatibility_ID.Both)
                return true;

            return toCompare == compatibility;
        }
    }
}

