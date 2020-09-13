using System;

namespace tlibrary
{
    public abstract class Api
    {
        internal string _file = "";
        internal string _path = "";
        internal string _pathFile => System.IO.Path.Combine(_path, _file);

        public bool ok {get;set;}= false;

        public abstract void Go();

        //@@@
    }




}
