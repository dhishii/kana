using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surfer
{

    [Serializable]
    public class SUInputIconsDict : SerializableDictionary<SUPlatform_ID, SUInputIconsData>
    {

        public string[] GetSpritesNames()
        {
            List<string> names = new List<string>();
            names.Add(SurferHelper.Unset);

            foreach (var pair in this)
            {
                foreach (var data in pair.Value.Sprites)
                {
                    names.Add(data.Name);
                }
                break;
            }

            return names.ToArray();
        }

        public string[] GetSpritesResNames()
        {
            List<string> names = new List<string>();
            names.Add(SurferHelper.Unset);

            foreach (var pair in this)
            {
                foreach (var data in pair.Value.ResSprites)
                {
                    names.Add(data.Name);
                }
                break;
            }

            return names.ToArray();
        }

        public void RemoveSprite(string GUID)
        {
            foreach (KeyValuePair<SUPlatform_ID, SUInputIconsData> pair in this)
            {
                pair.Value.Sprites.RemoveAll(x => x.GUID == GUID);
            }
        }

        public void RemoveResSprite(string GUID)
        {
            foreach (KeyValuePair<SUPlatform_ID, SUInputIconsData> pair in this)
            {
                pair.Value.ResSprites.RemoveAll(x => x.GUID == GUID);
            }
        }

        public int GetSpriteGUIDIndex(string GUID)
        {
            var names = this.GetSpritesNames();

            foreach (var pair in this)
            {
                foreach (var data in pair.Value.Sprites)
                {
                    if (data.GUID == GUID)
                    {
                        var idx = Array.IndexOf(names, data.Name);
                        return Mathf.Clamp(idx, 0, names.Length);
                    }
                }
                break;
            }

            return default;
        }

        public string GetSpriteGUIDFromIndex(int idx)
        {
            var names = this.GetSpritesNames();
            var name = names[idx];

            foreach (var pair in this)
            {
                foreach (var data in pair.Value.Sprites)
                {
                    if (data.Name == name)
                    {
                        return data.GUID;
                    }
                }
                break;
            }

            return string.Empty;
        }


        public int GetSpriteResGUIDIndex(string GUID)
        {
            var names = this.GetSpritesResNames();

            foreach (var pair in this)
            {
                foreach (var data in pair.Value.ResSprites)
                {
                    if (data.GUID == GUID)
                    {
                        var idx = Array.IndexOf(names, data.Name);
                        return Mathf.Clamp(idx, 0, names.Length);
                    }
                }
                break;
            }

            return default;
        }

        public string GetSpriteResGUIDFromIndex(int idx)
        {
            var names = this.GetSpritesResNames();
            var name = names[idx];

            foreach (var pair in this)
            {
                foreach (var data in pair.Value.ResSprites)
                {
                    if (data.Name == name)
                    {
                        return data.GUID;
                    }
                }
                break;
            }

            return string.Empty;
        }
    }
}





