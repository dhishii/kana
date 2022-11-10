using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    [System.Serializable]
    public class SUGUIDFontSizeData : SUGUIDElementBaseData
    {
        [SerializeField]
        int _fontSize = default;
        public int FontSize { get => _fontSize; }

        public SUGUIDFontSizeData(string guid, string name, int size)
        {
            _guid = guid;
            _name = name;
            _fontSize = size;
        }

        public SUGUIDFontSizeData(SUGUIDFontSizeData data) : base(data)
        {
            _fontSize = data.FontSize;
        }

        public void UpdateFontSize(SUGUIDFontSizeData dataUpdated)
        {
            if (dataUpdated.GUID != _guid)
                return;

            _fontSize = dataUpdated.FontSize;
        }

        public void UpdateName(SUGUIDFontSizeData dataUpdated)
        {
            if (dataUpdated.GUID != _guid)
                return;

            _name = dataUpdated.Name;
        }
    }
}

