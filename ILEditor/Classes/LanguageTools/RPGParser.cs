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
            Function CurrentProcedure = new Function("Mainline");

            foreach (string Line in Code.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                line++;
                CurrentLine = Line.Trim();
                if (CurrentLine == "") continue;

                lexer = new RPGLexer();
                lexer.Lex(CurrentLine);
                Tokens = lexer.GetTokens();

                for (int x = 0; x < Tokens.Count; x++)
                {
                    token = Tokens[x];

                    switch (token.Type)
                    {
                        case RPGLexer.Type.OPERATION:
                            switch (token.Value)
                            {
                                case "DCL-F":
                                    CurrentProcedure.AddVariable(new Variable(Tokens[x + 1].Value, "File", line));
                                    break;
                                case "DCL-S":
                                    CurrentProcedure.AddVariable(new Variable(Tokens[x + 1].Value, Tokens[x + 2].Value, line));
                                    break;
                                case "DCL-C":
                                    CurrentProcedure.AddVariable(new Variable(Tokens[x + 1].Value, Tokens[x + 2].Value, line));
                                    break;
                                case "DCL-DS":
                                    CurrentProcedure.AddVariable(new Variable(Tokens[x + 1].Value, "Data-Structure", line));
                                    break;
                                case "DCL-PROC":
                                    Procedures.Add(CurrentProcedure);
                                    CurrentProcedure = new Function(Tokens[x + 1].Value);
                                    break;
                            }
                            break;
                    }
                }
            }

            Procedures.Add(CurrentProcedure);

            return Procedures.ToArray();
        }
    }
}
