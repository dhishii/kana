using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Surfer
{

    [System.Serializable]
    public class SUGUIDFontData : SUGUIDElementBaseData
    {
        [SerializeField]
        TMP_FontAsset _font = default;
        public TMP_FontAsset Font { get => _font; }

        public SUGUIDFontData(string guid, string name, TMP_FontAsset font)
        {
            _guid = guid;
            _name = name;
            _font = font;
        }

        public SUGUIDFontData(SUGUIDFontData data) : base(data)
        {
            _font = data.Font;
        }

        public void UpdateFont(SUGUIDFontData dataUpdated)
        {
            if (dataUpdated.GUID != _guid)
                return;

            _font = dataUpdated.Font;
        }

        public void UpdateName(SUGUIDFontData dataUpdated)
        {
            if (dataUpdated.GUID != _guid)
                return;

            _name = dataUpdated.Name;
        }
    }

}
