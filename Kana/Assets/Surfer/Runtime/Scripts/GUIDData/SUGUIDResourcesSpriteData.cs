using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    [System.Serializable]
    public class SUGUIDResourcesSpriteData : SUGUIDElementBaseData
    {
        [SerializeField]
        string _path = default;
        public string Path { get => _path; }

        public SUGUIDResourcesSpriteData(string guid, string name, string path)
        {
            _guid = guid;
            _name = name;
            _path = path;
        }

        public SUGUIDResourcesSpriteData(SUGUIDResourcesSpriteData data) : base(data)
        {
            _path = data.Path;
        }

        public void UpdatePath(SUGUIDResourcesSpriteData dataUpdated)
        {
            if (dataUpdated.GUID != _guid)
                return;

            _path = dataUpdated.Path;
        }

        public void UpdateName(SUGUIDResourcesSpriteData dataUpdated)
        {
            if (dataUpdated.GUID != _guid)
                return;

            _name = dataUpdated.Name;
        }
    }
}

