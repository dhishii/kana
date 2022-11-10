using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    [System.Serializable]
    public class SUGUIDSpriteData : SUGUIDElementBaseData
    {
        [SerializeField]
        Sprite _sprite = default;
        public Sprite Sprite { get => _sprite; }

        public SUGUIDSpriteData(string guid, string name, Sprite sprite)
        {
            _guid = guid;
            _name = name;
            _sprite = sprite;
        }

        public SUGUIDSpriteData(SUGUIDSpriteData data) : base(data)
        {
            _sprite = data.Sprite;
        }

        public void UpdateSprite(SUGUIDSpriteData dataUpdated)
        {
            if (dataUpdated.GUID != _guid)
                return;

            _sprite = dataUpdated.Sprite;
        }

        public void UpdateName(SUGUIDSpriteData dataUpdated)
        {
            if (dataUpdated.GUID != _guid)
                return;

            _name = dataUpdated.Name;
        }
    }
}

