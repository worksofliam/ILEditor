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

            foreach (string Line in Code.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                line++;
                CurrentLine = Line.Trim();
                if (CurrentLine == "") continue;

                lexer = new CLLexer();
                lexer.Lex(CurrentLine);
                Tokens = lexer.GetTokens();

                token = Tokens[0];
                switch (token.Type)
                {
                    case CLLexer.Type.OPERATION:
                        switch (token.Value)
                        {
                            case "DCL":
                                if (Tokens.Count < 3) break;
                                if (Tokens[1].Block == null) break;
                                if (Tokens[2].Block == null) break;

                                CurrentProcedure.AddVariable(new Variable(Tokens[1].Block[0].Value, Tokens[2].Block[0].Value, StorageType.Normal, line));
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
