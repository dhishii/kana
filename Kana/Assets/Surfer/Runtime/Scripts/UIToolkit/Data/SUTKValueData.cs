using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2021_2_OR_NEWER
using UnityEngine.UIElements;

namespace Surfer
{
    [System.Serializable]
    public class SUTKValueData 
    {

        public enum Mode
        {
            Unchanged,
            Pixel,
            Percentage
        }

        public bool HasValue
        {
            get
            {
                return _mode != Mode.Unchanged;
            }
        }

        public LengthUnit Unit
        {
            get
            {
                return _mode == Mode.Pixel ? LengthUnit.Pixel : LengthUnit.Percent;
            }
        }

        [SerializeField]
        float _value = default;
        public float Value { get => _value; }

        [SerializeField]
        Mode _mode = default;

        public StyleLength GetStyleLength()
        {
            if(!HasValue)
                return new StyleLength();

            return new StyleLength(new Length(_value,Unit));
        }

    }
}

#endif

