using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Surfer
{
    [Serializable]
    public class ThemesDictionary : SerializableDictionary<string, SUThemeData> 
    {


        /// <summary>
        /// Get a list of all the theme names. 
        /// </summary>
        /// <returns>names list</returns>
        public string[] GetNames()
        {
            string[] names = new string[this.Count + 1];
            names[0] = SurferHelper.Unset;

            int index = 1;
            foreach (KeyValuePair<string, SUThemeData> pair in this)
            {
                names[index] = pair.Value.Name;
                index++;
            }

            return names;
        }

        #region Names List

        public string[] GetFontsNames()
        {
            List<string> names = new List<string>();
            names.Add(SurferHelper.Unset);

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.Fonts)
                {
                    names.Add(data.Name);
                }
                break;
            }

            return names.ToArray();
        }

        public string[] GetFontSizesNames()
        {
            List<string> names = new List<string>();
            names.Add(SurferHelper.Unset);

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.FontSizes)
                {
                    names.Add(data.Name);
                }
                break;
            }

            return names.ToArray();
        }

        public string[] GetColorsNames()
        {
            List<string> names = new List<string>();
            names.Add(SurferHelper.Unset);

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.Colors)
                {
                    names.Add(data.Name);
                }
                break;
            }

            return names.ToArray();
        }

        public string[] GetSpritesNames()
        {
            List<string> names = new List<string>();
            names.Add(SurferHelper.Unset);

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.Sprites)
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

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.ResSprites)
                {
                    names.Add(data.Name);
                }
                break;
            }

            return names.ToArray();
        }
#endregion


#region Names Availability


        public bool IsSpriteNameAvailable(string name)
        {
            
            foreach(var pair in this)
            {
                if (pair.Value.Sprites.Count > 0)
                {
                    foreach(var item in pair.Value.Sprites)
                    {
                        if(item.Name == name)
                            return false;
                    }
                    break;
                }
                
            }

            return true;
        }


        public bool IsColorNameAvailable(string name)
        {
            
            foreach(var pair in this)
            {
                if (pair.Value.Colors.Count > 0)
                {
                    foreach(var item in pair.Value.Colors)
                    {
                        if(item.Name == name)
                            return false;
                    }
                    break;
                }
                
            }

            return true;
        }


        public bool IsFontNameAvailable(string name)
        {
            
            foreach(var pair in this)
            {
                if (pair.Value.Fonts.Count > 0)
                {
                    foreach(var item in pair.Value.Fonts)
                    {
                        if(item.Name == name)
                            return false;
                    }
                    break;
                }
                
            }

            return true;
        }


        public bool IsFontSizeNameAvailable(string name)
        {
            
            foreach(var pair in this)
            {
                if (pair.Value.FontSizes.Count > 0)
                {
                    foreach(var item in pair.Value.FontSizes)
                    {
                        if(item.Name == name)
                            return false;
                    }
                    break;
                }
                
            }

            return true;
        }



        public bool IsResSpriteNameAvailable(string name)
        {
            
            foreach(var pair in this)
            {
                if (pair.Value.ResSprites.Count > 0)
                {
                    foreach(var item in pair.Value.ResSprites)
                    {
                        if(item.Name == name)
                            return false;
                    }
                    break;
                }
                
            }

            return true;
        }
#endregion

        #region Indexes Logic



        public int GetFontGUIDIndex(string GUID)
        {
            var names = this.GetFontsNames();

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.Fonts)
                {
                    if(data.GUID == GUID)
                    {
                        var idx = Array.IndexOf(names,data.Name);
                        return Mathf.Clamp(idx,0,names.Length);
                    }
                }
                break;
            }

            return default;
        }

        public string GetFontGUIDFromIndex(int idx)
        {
            var names = this.GetFontsNames();
            var name = names[idx];

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.Fonts)
                {
                    if(data.Name == name)
                    {
                        return data.GUID;
                    }
                }
                break;
            }

            return string.Empty;
        }


        public int GetFontSizeGUIDIndex(string GUID)
        {
            var names = this.GetFontSizesNames();

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.FontSizes)
                {
                    if(data.GUID == GUID)
                    {
                        var idx = Array.IndexOf(names,data.Name);
                        return Mathf.Clamp(idx,0,names.Length);
                    }
                }
                break;
            }

            return default;
        }

        public string GetFontSizeGUIDFromIndex(int idx)
        {
            var names = this.GetFontSizesNames();
            var name = names[idx];

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.FontSizes)
                {
                    if(data.Name == name)
                    {
                        return data.GUID;
                    }
                }
                break;
            }

            return string.Empty;
        }


        public int GetColorGUIDIndex(string GUID)
        {
            var names = this.GetColorsNames();

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.Colors)
                {
                    if(data.GUID == GUID)
                    {
                        var idx = Array.IndexOf(names,data.Name);
                        return Mathf.Clamp(idx,0,names.Length);
                    }
                }
                break;
            }

            return default;
        }

        public string GetColorGUIDFromIndex(int idx)
        {
            var names = this.GetColorsNames();
            var name = names[idx];

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.Colors)
                {
                    if(data.Name == name)
                    {
                        return data.GUID;
                    }
                }
                break;
            }

            return string.Empty;
        }


        public int GetSpriteGUIDIndex(string GUID)
        {
            var names = this.GetSpritesNames();

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.Sprites)
                {
                    if(data.GUID == GUID)
                    {
                        var idx = Array.IndexOf(names,data.Name);
                        return Mathf.Clamp(idx,0,names.Length);
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

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.Sprites)
                {
                    if(data.Name == name)
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

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.ResSprites)
                {
                    if(data.GUID == GUID)
                    {
                        var idx = Array.IndexOf(names,data.Name);
                        return Mathf.Clamp(idx,0,names.Length);
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

            foreach(var pair in this)
            {
                foreach(var data in pair.Value.ResSprites)
                {
                    if(data.Name == name)
                    {
                        return data.GUID;
                    }
                }
                break;
            }

            return string.Empty;
        }

        #endregion

        #region Deletion Logic

        public void RemoveFont(string GUID)
        {
            foreach (KeyValuePair<string, SUThemeData> pair in this)
            {
                pair.Value.Fonts.RemoveAll(x=>x.GUID == GUID);
            }
        }

        public void RemoveColor(string GUID)
        {
            foreach (KeyValuePair<string, SUThemeData> pair in this)
            {
                pair.Value.Colors.RemoveAll(x=>x.GUID == GUID);
            }
        }

        public void RemoveFontSize(string GUID)
        {
            foreach (KeyValuePair<string, SUThemeData> pair in this)
            {
                pair.Value.FontSizes.RemoveAll(x=>x.GUID == GUID);
            }
        }

        public void RemoveSprite(string GUID)
        {
            foreach (KeyValuePair<string, SUThemeData> pair in this)
            {
                pair.Value.Sprites.RemoveAll(x=>x.GUID == GUID);
            }
        }

        public void RemoveResSprite(string GUID)
        {
            foreach (KeyValuePair<string, SUThemeData> pair in this)
            {
                pair.Value.ResSprites.RemoveAll(x=>x.GUID == GUID);
            }
        }

        #endregion
    }
}

