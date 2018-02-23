using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes
{
    public enum FileSystem
    {
        QSYS, IFS
    }

    public class RemoteSource
    {
        public string _Local;
        private FileSystem RemoteFS;

        private string _IFSPath;

        private string _Lib;
        private string _Obj;
        private string _Mbr;
        private string _Ext;
        private int _RecordLength;

        public string _Text;
        private Boolean _isEditable;

        private Boolean _isLocked;

        public RemoteSource(string Local, string Lib, string Obj, string Mbr, string Ext, Boolean isEditable = true, int RecordLength = 0)
        {
            this.RemoteFS = FileSystem.QSYS;

            this._Local = Local;
            this._Lib = Lib;
            this._Obj = Obj;
            this._Mbr = Mbr;
            this._Ext = Ext;
            this._RecordLength = RecordLength;
            this._isEditable = isEditable;

            this._isLocked = false;
            this._IFSPath = "";
        }

        public RemoteSource(string Local, string Remote, Boolean isEditable = true)
        {
            string[] data;

            this.RemoteFS = FileSystem.IFS;

            this._Local = Local;
            this._IFSPath = Remote;
            this._isEditable = isEditable;
            
            this._Mbr = Remote.Split('/').Last();
            if (this._Mbr.Contains("."))
            {
                data = this._Mbr.Split('.');
                this._Mbr = data.First();
                this._Ext = data.Last();
            }
            else
            {
                this._Ext = "";
            }

            this._RecordLength = 0;
            this._isLocked = false;

            this._Lib = "";
            this._Obj = "";
        }

        public FileSystem GetFS()
        {
            return this.RemoteFS;
        }

        public int GetRecordLength()
        {
            return this._RecordLength;
        }

        public string GetText()
        {
            return this._Text;
        }

        public string GetLocalFile()
        {
            return this._Local;
        }

        public string GetRemoteFile()
        {
            return this._IFSPath;
        }

        public string GetLibrary()
        {
            return this._Lib;
        }

        public string GetObject()
        {
            return this._Obj;
        }

        public string GetName()
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

        public void Lock()
        {
            bool result;
            if (this._isEditable)
            {
                switch (this.RemoteFS)
                {
                    case FileSystem.QSYS:
                        result = IBMi.RemoteCommand("ALCOBJ OBJ((" + this._Lib + "/" + this._Obj + " *FILE *EXCLRD " + this._Mbr + ")) WAIT(1)", false);

                        this._isLocked = result;
                        if (result == false)
                        {
                            Editor.TheEditor.SetStatus("Failed to allocate a lock for " + this._Mbr + "! Member has been placed in read-only mode.");
                            this._isEditable = false;
                        }
                        break;
                }
                
            }
        }

        public void Unlock()
        {
            if (this._isLocked)
                switch (this.RemoteFS)
                {
                    case FileSystem.QSYS:
                        IBMi.RemoteCommand("DLCOBJ OBJ((" + this._Lib + "/" + this._Obj + " *FILE *EXCLRD " + this._Mbr + "))", false);
                        break;
                }
        }
    }
}
