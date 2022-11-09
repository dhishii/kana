namespace gamespace
{
    using System.Collections.Generic;

    [System.Serializable]
    public class Webs3
    {
        public List<web_item3> web;

    }

    [System.Serializable]
    public class web_item3
    {
        public int hh;
        public int vv;
        public string[] chars;
        public string[] btns;
        public List<string> adds;
        public List<Qus3> qus;

    }

    [System.Serializable]
    public class Word3
    {
        public List<string> add_chars;

    }
    [System.Serializable]
    public class Qus3
    {
        //public String url;
        public int[] ans;

    }
}