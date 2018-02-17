using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.LanguageTools
{
    class CLParser
    {
        public static Function[] Parse(string Code)
        {
            CLLexer lexer;
            List<CLToken> Tokens;
            List<Function> Procedures = new List<Function>();
            CLToken token;
            int line = -1;
            string CurrentLine;
            Function CurrentProcedure = new Function("Globals", 0);
            string name, type;

            foreach (string Line in Code.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                line++;
                CurrentLine = Line.Trim();
                if (CurrentLine == "") continue;

                lexer = new CLLexer();
                lexer.Lex(CurrentLine);
                Tokens = lexer.GetTokens();

                if (Tokens.Count == 0) continue;

                token = Tokens[0];
                switch (token.Type)
                {
                    case CLLexer.Type.OPERATION:
                        switch (token.Value)
                        {
                            case "DCL":
                                if (Tokens.Count < 3) break;
                                if (Tokens[1].Block == null)
                                    name = Tokens[1].Value;
                                else
                                    name = Tokens[1].Block[0].Value;

                                if (Tokens[2].Block == null)
                                    type = Tokens[2].Value;
                                else
                                    type = Tokens[2].Block[0].Value;

                                CurrentProcedure.AddVariable(new Variable(name, type, StorageType.Normal, line));
                                break;
                            case "SUBR":
                                if (Tokens.Count < 2) break;
                                if (Tokens[1].Block == null) break;

                                Procedures.Add(CurrentProcedure);
                                CurrentProcedure = new Function(Tokens[1].Block[0].Value, line);
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
