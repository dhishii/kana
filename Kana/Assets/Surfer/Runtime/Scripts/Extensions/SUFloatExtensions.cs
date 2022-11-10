using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surfer
{
    public static class SUFloatExtensions 
    {
        public static bool IsEqualTo(this float caller,float otherValue)
        {
            return Mathf.Abs(caller - otherValue) < Mathf.Epsilon;
        }
    }
}

