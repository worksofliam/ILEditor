using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.LanguageTools
{
    class RPGParser
    {
        public static Function[] Parse(string Code)
        {
            RPGLexer lexer;
            List<RPGToken> Tokens;
            List<Function> Procedures = new List<Function>();
            RPGToken token;
            int line = -1;
            string CurrentLine;
            Function CurrentProcedure = new Function("Globals", 0);

            foreach (string Line in Code.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                line++;
                CurrentLine = Line.Trim();
                if (CurrentLine == "") continue;

                lexer = new RPGLexer();
                lexer.Lex(CurrentLine);
                Tokens = lexer.GetTokens();

                if (Tokens.Count == 0) continue;

                token = Tokens[0];
                switch (token.Type)
                {
                    case RPGLexer.Type.OPERATION:
                        switch (token.Value)
                        {
                            case "DCL-F":
                                if (Tokens.Count < 2) break;
                                CurrentProcedure.AddVariable(new Variable(Tokens[1].Value, "File", StorageType.File, line));
                                break;
                            case "DCL-S":
                                if (Tokens.Count < 3) break;
                                if (Tokens[2].Value == "LIKE")
                                    if (Tokens[2].Block != null)
                                        if (Tokens[2].Block.Count > 0)
                                            Tokens[2].Value = Tokens[2].Block[0].Value;
                                CurrentProcedure.AddVariable(new Variable(Tokens[1].Value, Tokens[2].Value, StorageType.Normal, line));
                                break;
                            case "DCL-C":
                                if (Tokens.Count < 3) break;
                                CurrentProcedure.AddVariable(new Variable(Tokens[1].Value, Tokens[2].Value, StorageType.Const, line));
                                break;
                            case "DCL-DS":
                                if (Tokens.Count < 2) break;
                                CurrentProcedure.AddVariable(new Variable(Tokens[1].Value, "Data-Structure", StorageType.Struct, line));
                                break;
                            case "BEGSR":
                                if (Tokens.Count < 2) break;
                                CurrentProcedure.AddVariable(new Variable(Tokens[1].Value, "Subroutine", StorageType.Subroutine, line));
                                break;
                            case "DCL-PROC":
                                if (Tokens.Count < 2) break;
                                Procedures.Add(CurrentProcedure);
                                CurrentProcedure = new Function(Tokens[1].Value, line);
                                break;
                        }
                        break;
                }
            }

            Procedures.Add(CurrentProcedure);

            return Procedures.ToArray();
        }
    }
}
