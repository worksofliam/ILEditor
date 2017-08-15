using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    public class Member
    {
        private string _Local;
        private string _Lib;
        private string _Obj;
        private string _Mbr;
        private string _Ext;
        private Boolean _isEditable;

        public Member(string Local, string Lib, string Obj, string Mbr, string Ext, Boolean isEditable)
        {
            this._Local = Local;
            this._Lib = Lib;
            this._Obj = Obj;
            this._Mbr = Mbr;
            this._Ext = Ext;
            this._isEditable = isEditable;
        }

        public string GetLocalFile()
        {
            return this._Local;
        }

        public string GetLibrary()
        {
            return this._Lib;
        }

        public string GetObject()
        {
            return this._Obj;
        }

        public string GetMember()
        {
            return this._Mbr;
        }

        public string GetExtension()
        {
            return this._Ext;
        }

        public bool IsEditable()
        {
            return this._isEditable;
        }
    }
}
