using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Surfer
{
    [System.Serializable]
    public class SUThemeData
    {
        [SerializeField]
        string _name = default;
        public string Name { get => _name; set => _name = value; }

        [SerializeField]
        List<SUGUIDColorData> _colors = new List<SUGUIDColorData>();
        public List<SUGUIDColorData> Colors { get => _colors; private set => _colors = value; }

        [SerializeField]
        List<SUGUIDFontData> _fonts = new List<SUGUIDFontData>();
        public List<SUGUIDFontData> Fonts { get => _fonts; private set => _fonts = value; }

        [SerializeField]
        List<SUGUIDFontSizeData> _fontSizes = new List<SUGUIDFontSizeData>();
        public List<SUGUIDFontSizeData> FontSizes { get => _fontSizes; private set => _fontSizes = value; }

        [SerializeField]
        List<SUGUIDSpriteData> _sprites = new List<SUGUIDSpriteData>();
        public List<SUGUIDSpriteData> Sprites { get => _sprites; private set => _sprites = value; }

        [SerializeField]
        List<SUGUIDResourcesSpriteData> _resSprites = new List<SUGUIDResourcesSpriteData>();
        public List<SUGUIDResourcesSpriteData> ResSprites { get => _resSprites; private set => _resSprites = value; }

        public SUThemeData(string name, SUThemeData currentTheme)
        {
            _name = name;

            if (currentTheme == null)
                return;


            foreach(var data in currentTheme.Fonts)
            {
                _fonts.Add(new SUGUIDFontData(data));
            }

            foreach(var data in currentTheme.FontSizes)
            {
                _fontSizes.Add(new SUGUIDFontSizeData(data));
            }

            foreach(var data in currentTheme.Colors)
            {
                _colors.Add(new SUGUIDColorData(data));
            }

            foreach(var data in currentTheme.Sprites)
            {
                _sprites.Add(new SUGUIDSpriteData(data));
            }

            foreach(var data in currentTheme.ResSprites)
            {
                _resSprites.Add(new SUGUIDResourcesSpriteData(data));
            }            

        }


        public bool GetFont(string guid,out TMP_FontAsset font)
        {
            font = null;

            if(string.IsNullOrEmpty(guid))
                return false;
            if(guid == SurferHelper.Unset)
                return false;

            foreach(var data in Fonts)
            {
                if(data.GUID == guid)
                {
                    font = data.Font;
                    return true;
                }
            }

            return false;
        }

        public bool GetFontSize(string guid,out float fontSize)
        {
            fontSize = default;

            if(string.IsNullOrEmpty(guid))
                return false;
            if(guid == SurferHelper.Unset)
                return false;

            foreach(var data in FontSizes)
            {
                if(data.GUID == guid)
                {
                    fontSize = data.FontSize;
                    return true;
                }
            }

            return false;
        }

        public bool GetColor(string guid,out Color color)
        {
            color = default;

            if(string.IsNullOrEmpty(guid))
                return false;
            if(guid == SurferHelper.Unset)
                return false;

            foreach(var data in Colors)
            {
                if(data.GUID == guid)
                {
                    color = data.Color;
                    return true;
                }
            }

            return false;
        }

        public bool GetSprite(string guid,out Sprite sprite)
        {
            sprite = default;

            if(string.IsNullOrEmpty(guid))
                return false;
            if(guid == SurferHelper.Unset)
                return false;

            foreach(var data in Sprites)
            {
                if(data.GUID == guid)
                {
                    sprite = data.Sprite;
                    return true;
                }
            }

            return false;
        }


        public bool GetSpriteRes(string guid,out Sprite sprite)
        {
            sprite = default;

            if(string.IsNullOrEmpty(guid))
                return false;
            if(guid == SurferHelper.Unset)
                return false;

            foreach(var data in ResSprites)
            {
                if(data.GUID == guid)
                {
                    sprite = Resources.Load<Sprite>(data.Path);
                    return true;
                }
            }

            return false;
        }
    }
}

