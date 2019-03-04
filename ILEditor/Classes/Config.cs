using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace ILEditor.Classes
{
	internal class Config
	{
		private readonly string                     _configLocation;
		private readonly Dictionary<string, string> _dataDictionary;

		public Config(string Location)
		{
			_configLocation = Location;
			_dataDictionary = new Dictionary<string, string>();
			LoadConfig();
		}

		public void LoadConfig()
		{
			if (File.Exists(_configLocation))
				foreach (var line in File.ReadAllLines(_configLocation))
				{
					var data = line.Split(new[] {'='}, 2);
					for (var i = 0; i < data.Length; i++)
						data[i] = data[i].Trim();

					if (_dataDictionary.ContainsKey(data[0]))
						_dataDictionary[data[0]] = data[1];
					else
						_dataDictionary.Add(data[0], data[1]);
				}

			SaveConfig();
		}

		public void CheckExist(string key, string value)
		{
			if (!_dataDictionary.ContainsKey(key))
				SetValue(key, value);
		}

		public void DoEditorDefaults()
		{
			CheckExist("acspath", "false");
			CheckExist("darkmode", "false");
			CheckExist("srcdat_agreement", "false");
		}

		public void DoSystemDefaults()
		{
			CheckExist("system", "system");
			CheckExist("username", "myuser");
			CheckExist("password", "mypass");
			CheckExist("alias", _dataDictionary["system"]);
			CheckExist("useFTPES", "false");
			CheckExist("transferMode", "AutoPassive");

			CheckExist("datalibl", "SYSTOOLS");
			CheckExist("curlib", "SYSTOOLS");

			CheckExist("homeDir", "/home/" + _dataDictionary["username"] + "/");
			CheckExist("tempSpf", "QSOURCE");

			CheckExist("printerLib", "*LIBL");
			CheckExist("printerObj", "QPRINT");
			CheckExist("fetchJobLog", "false");

			CheckExist("TREE_LIST", "");
			CheckExist("FONT", "Consolas");
			CheckExist("ZOOM", 12.75f.ToString());
			CheckExist("INDENT_SIZE", "4");
			CheckExist("SHOW_SPACES", "false");
			CheckExist("HIGHLIGHT_CURRENT_LINE", "true");
			CheckExist("CONV_TABS", "true");
			CheckExist("CL_FORMAT_ON_SAVE", "false");
			CheckExist("CHARACTER_ASSIST", "false");

			CheckExist("CMPTYPES", "RPGLE|SQLRPGLE|CLLE|C|CMD");
			CheckExist("DFT_RPGLE", "CRTBNDRPG");
			CheckExist("DFT_SQLRPGLE", "CRTSQLRPGI");
			CheckExist("DFT_CLLE", "CRTBNDCL");
			CheckExist("DFT_C", "CRTBNDC");
			CheckExist("DFT_CMD", "CRTCMD");

			CheckExist("TYPE_RPGLE", "CRTBNDRPG|CRTRPGMOD");
			CheckExist("CRTBNDRPG",
				"CRTBNDRPG PGM(&OPENLIB/&OPENMBR) SRCFILE(&OPENLIB/&OPENSPF) OPTION(*EVENTF) DBGVIEW(*SOURCE)");

			CheckExist("CRTBNDRPG_IFS",
				"CRTBNDRPG PGM(&BUILDLIB/&FILENAME) SRCSTMF('&FILEPATH') OPTION(*EVENTF) DBGVIEW(*SOURCE)");

			CheckExist("CRTRPGMOD", "CRTRPGMOD MODULE(&OPENLIB/&OPENMBR) SRCFILE(&OPENLIB/&OPENSPF) OPTION(*EVENTF)");

			CheckExist("TYPE_SQLRPGLE", "CRTSQLRPGI|CRTSQLRPGI_MOD");
			CheckExist("CRTSQLRPGI",
				"CRTSQLRPGI OBJ(&OPENLIB/&OPENMBR) SRCFILE(&OPENLIB/&OPENSPF) COMMIT(*NONE) OPTION(*EVENTF *XREF)");

			CheckExist("CRTSQLRPGI_IFS",
				"CRTSQLRPGI OBJ(&BUILDLIB/&FILENAME) SRCSTMF('&FILEPATH') COMMIT(*NONE) OPTION(*EVENTF *XREF)");

			CheckExist("CRTSQLRPGI_MOD",
				"CRTSQLRPGI OBJ(&OPENLIB/&OPENMBR) SRCFILE(&OPENLIB/&OPENSPF) COMMIT(*NONE) OBJTYPE(*MODULE) OPTION(*EVENTF *XREF)");

			CheckExist("TYPE_CLLE", "CRTBNDCL");
			CheckExist("CRTBNDCL", "CRTBNDCL PGM(&OPENLIB/&OPENMBR) SRCFILE(&OPENLIB/&OPENSPF) OPTION(*EVENTF)");

			CheckExist("TYPE_C", "CRTBNDC|CRTCMOD");
			CheckExist("CRTBNDC",
				"CRTBNDC PGM(&OPENLIB/&OPENMBR) SRCFILE(&OPENLIB/&OPENSPF) DBGVIEW(*SOURCE) OPTION(*EVENTF)");

			CheckExist("CRTCMOD",
				"CRTCMOD MODULE(&OPENLIB/&OPENMBR) SRCFILE(&OPENLIB/&OPENSPF) DBGVIEW(*SOURCE) OPTION(*EVENTF)");

			CheckExist("TYPE_CMD", "CRTCMD");
			CheckExist("CRTCMD", "CRTCMD CMD(&OPENLIB/&OPENMBR) PGM(&OPENLIB/&OPENMBR) SRCFILE(&OPENLIB/&OPENSPF)");

			CheckExist("IFS_LINKS", "");
		}

		public void SaveConfig()
		{
			var lines = new List<string>();
			foreach (var key in _dataDictionary.Keys)
				lines.Add(key + '=' + _dataDictionary[key]);

			File.WriteAllLines(_configLocation, lines);
		}

		public string GetValue(string Key)
		{
			if (_dataDictionary.ContainsKey(Key))
				return _dataDictionary[Key];

			return "";
		}

		public void SetValue(string Key, string Value)
		{
			if (_dataDictionary.ContainsKey(Key))
				_dataDictionary[Key] = Value;
			else
				_dataDictionary.Add(Key, Value);

			SaveConfig();
		}
	}

	internal class Password
	{
		public static string Encode(string ValuePlain)
		{
			var softwareKey = Registry.CurrentUser.OpenSubKey("ILEditor", true);

			if (softwareKey == null)
				softwareKey = Registry.CurrentUser.CreateSubKey("ILEditor");

			var valBytes = Encoding.ASCII.GetBytes(ValuePlain);

			// Generate additional entropy (will be used as the Initialization vector)
			byte[] entropy;

			entropy = softwareKey.GetValue("passkey") as byte[];
			if (entropy == null)
			{
				entropy = new byte[20];
				using (var rng = new RNGCryptoServiceProvider())
				{
					rng.GetBytes(entropy);
				}

				softwareKey.SetValue("passkey", entropy);
			}

			byte[] ciphertext;
			ciphertext = ProtectedData.Protect(valBytes, entropy, DataProtectionScope.CurrentUser);

			return Convert.ToBase64String(ciphertext);
		}

		public static string Decode(string ValueBase64)
		{
			var softwareKey = Registry.CurrentUser.OpenSubKey("ILEditor", true);

			if (softwareKey == null)
				softwareKey = Registry.CurrentUser.CreateSubKey("ILEditor");

			var entropy = softwareKey.GetValue("passkey") as byte[];

			if (entropy != null)
				try
				{
					//Usually crashed due to invalid base64 (the old password (-:)
					var ciphertext = Convert.FromBase64String(ValueBase64);
					var plaintext  = ProtectedData.Unprotect(ciphertext, entropy, DataProtectionScope.CurrentUser);

					return Encoding.ASCII.GetString(plaintext);
				}
				catch
				{
					return "";
				}

			return "";
		}
	}
}