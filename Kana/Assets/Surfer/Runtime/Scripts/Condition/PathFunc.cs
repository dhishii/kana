using System;
using System.Collections.Generic;
using UnityEngine;

namespace Surfer
{

    /// <summary>
    /// Data to store condition name/path and runtime logic
    /// </summary>
    public struct PathFunc
    {
        public string Path {get;private set;}
        public Func<FuncInput,bool> Function {get;private set;}
        public List<PathField> Fields { get; private set; }
        public SUCompatibility_ID Compatibility { get; private set; }

        public PathFunc(string path,Func<FuncInput, bool> func,SUCompatibility_ID compatibility = SUCompatibility_ID.Both)
        {
            Path = path;
            Function = func;
            Fields = new List<PathField>();
            Compatibility = compatibility;
        }

        public PathFunc(string path, List<PathField> fields , Func<FuncInput, bool> func,SUCompatibility_ID compatibility = SUCompatibility_ID.Both)
        {
            Path = path;
            Function = func;
            Fields = fields;
            Compatibility = compatibility;
        }
    }
}