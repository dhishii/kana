using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    [System.Serializable]
    public class SUGUIDColorData : SUGUIDElementBaseData
    {

        [SerializeField]
        Color _color = default;
        public Color Color { get => _color; }

        public SUGUIDColorData(string guid, string name, Color color)
        {
            _guid = guid;
            _name = name;
            _color = color;
        }

        public SUGUIDColorData(SUGUIDColorData data) : base(data)
        {
            _color = data.Color;
        }

        public void UpdateColor(SUGUIDColorData dataUpdated)
        {
            if (dataUpdated.GUID != _guid)
                return;

            _color = dataUpdated.Color;
        }

        public void UpdateName(SUGUIDColorData dataUpdated)
        {
            if (dataUpdated.GUID != _guid)
                return;

            _name = dataUpdated.Name;
        }
    }
}

