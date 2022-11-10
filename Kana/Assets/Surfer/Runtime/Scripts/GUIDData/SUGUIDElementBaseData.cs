using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    public abstract class SUGUIDElementBaseData 
    {
        [SerializeField]
        protected string _name = default;
        public string Name { get => _name; }

        [SerializeField]
        protected string _guid = default;
        public string GUID { get => _guid; }

        public SUGUIDElementBaseData(SUGUIDElementBaseData data)
        {
            _guid = data.GUID;
            _name = data.Name;
        }

        public SUGUIDElementBaseData()
        {
            
        }
    }
}

