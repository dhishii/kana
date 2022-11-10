using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    [System.Serializable]
    public class SUInputIconsData
    {

        [SerializeField]
        List<SUGUIDSpriteData> _sprites = new List<SUGUIDSpriteData>();
        public List<SUGUIDSpriteData> Sprites { get => _sprites; }


        [SerializeField]
        List<SUGUIDResourcesSpriteData> _resSprites = new List<SUGUIDResourcesSpriteData>();
        public List<SUGUIDResourcesSpriteData> ResSprites { get => _resSprites; }

        public SUInputIconsData(SUInputIconsData dataToCopy)
        {
            if (dataToCopy == null)
                return;

            foreach (var data in dataToCopy.Sprites)
            {
                _sprites.Add(new SUGUIDSpriteData(data));
            }

            foreach (var data in dataToCopy.ResSprites)
            {
                _resSprites.Add(new SUGUIDResourcesSpriteData(data));
            }

        }

        public bool GetSpriteFromGUID(string guid, out Sprite sprite)
        {
            sprite = default;

            if (string.IsNullOrEmpty(guid))
                return false;
            if (guid == SurferHelper.Unset)
                return false;

            foreach (var data in Sprites)
            {
                if (data.GUID == guid)
                {
                    sprite = data.Sprite;
                    return true;
                }
            }

            return false;
        }

        public bool GetSpriteFromName(string name, out Sprite sprite)
        {
            sprite = default;

            if (string.IsNullOrEmpty(name))
                return false;
            if (name == SurferHelper.Unset)
                return false;

            foreach (var data in Sprites)
            {
                if (data.Name == name)
                {
                    sprite = data.Sprite;
                    return true;
                }
            }

            return false;
        }


        public bool GetResSpriteFromGUID(string guid, out Sprite sprite)
        {
            sprite = default;

            if (string.IsNullOrEmpty(guid))
                return false;
            if (guid == SurferHelper.Unset)
                return false;

            foreach (var data in ResSprites)
            {
                if (data.GUID == guid)
                {
                    sprite = Resources.Load<Sprite>(data.Path);
                    return true;
                }
            }

            return false;
        }

        public bool GetResSpriteFromName(string name, out Sprite sprite)
        {
            sprite = default;

            if (string.IsNullOrEmpty(name))
                return false;
            if (name == SurferHelper.Unset)
                return false;

            foreach (var data in ResSprites)
            {
                if (data.Name == name)
                {
                    sprite = Resources.Load<Sprite>(data.Path);
                    return true;
                }
            }

            return false;
        }
    }

}
