namespace ILEditor.Classes
{
	public class ILEObject
	{
		public string Extension;

		public string Library;
		public string Name;
		public string Owner;
		public uint   SizeKB;
		public string SrcLib;
		public string SrcMbr;
		public string SrcSpf;
		public string Text;
		public string Type;

		public ILEObject()
		{
		}

		public ILEObject(string Lib, string Obj, string Type = "*PGM")
		{
			Library   = Lib;
			Name      = Obj;
			this.Type = Type;
		}
	}
}