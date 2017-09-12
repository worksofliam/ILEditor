using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    public class Member
    {
        public string _Local;
        private string _Lib;
        private string _Obj;
        private string _Mbr;
        private string _Ext;
        public string _Text;
        private int _RecordLength;
        private Boolean _isEditable;
        public Boolean _IsBeingSaved;

        public Member(string Local, string Lib, string Obj, string Mbr, string Ext, Boolean isEditable = true, int RecordLength = 0)
        {
            this._Local = Local;
            this._Lib = Lib;
            this._Obj = Obj;
            this._Mbr = Mbr;
            this._Ext = Ext;
            this._RecordLength = RecordLength;
            this._isEditable = isEditable;
            this._IsBeingSaved = false;
        }

        public string GetText()
        {
            return this._Text;
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
