using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILEditor.Classes.LanguageTools
{
    class CLLexer
    {
        public enum Type
        {
            BLOCK, UNKNOWN, OPERATOR, STRING_LITERAL, BLOCK_OPEN, BLOCK_CLOSE, OPERATION, INT_LITERAL, DOUBLE_LITERAL, WORD_LITERAL
        }
        private static string[] OPERATORS = new[] { "(", ")", " " };
        private static char[] STRING_LITERAL = new char[0];
        private static string[] BLOCK_OPEN = new[] { "(" };
        private static string[] BLOCK_CLOSE = new[] { ")" };
        private static Dictionary<Type, string[]> Pieces = new Dictionary<Type, string[]>
{
{ Type.BLOCK_OPEN, new[] {  "(" } },{ Type.BLOCK_CLOSE, new[] {  ")" } },{ Type.OPERATION, new[] {  "DCL", "SUBR" } },{ Type.INT_LITERAL, new[] {  "/[-0-9]+/" } },{ Type.DOUBLE_LITERAL, new[] {  @"/\d+\.?\d*/" } },{ Type.WORD_LITERAL, new[] {  "/.*?/" } }
};


        //***************************************************
        private CLToken TokenList = new CLToken(Type.BLOCK, "");
        public List<CLToken> GetTokens() => TokenList.Block;

        //***************************************************
        private int printIndex = -1;
        public void PrintBlock(List<CLToken> Block)
        {
            printIndex++;
            foreach (CLToken Token in Block)
            {
                Console.WriteLine("".PadRight(printIndex, '\t') + Token.Type.ToString() + " " + Token.Value);

                if (Token.Block != null)
                    PrintBlock(Token.Block);

            }
            printIndex--;
        }


        //***************************************************
        private Boolean InString = false;
        private string token = "";
        private int cIndex = 0;
        private bool IsOperator = false;
        public void Lex(string Text)
        {
            TokenList.Block = new List<CLToken>();
            while (cIndex < Text.Length)
            {
                IsOperator = false;
                if (InString == false)
                {
                    foreach (string Operator in OPERATORS)
                    {
                        if (cIndex + Operator.Length > Text.Length) continue;
                        if (Text.Substring(cIndex, Operator.Length) == Operator)
                        {
                            //Sort the old token before adding the operator
                            WorkToken();

                            //Insert new token (operator token)
                            token = Text.Substring(cIndex, Operator.Length);
                            WorkToken();

                            cIndex += Operator.Length;
                            IsOperator = true;
                            break;
                        }
                    }
                }

                if (IsOperator == false)
                {
                    char c = Text.Substring(cIndex, 1).ToCharArray()[0];

                    if (STRING_LITERAL.Contains(c))
                    {
                        if (Text.Substring(cIndex - 1, 1) == "\\")
                            token += c;
                        else
                        {
                            //This means it's end of STRING_LITERAL, and must be added to token list
                            WorkToken(InString);
                            InString = !InString;
                        }
                    }
                    else
                        token += c;


                    cIndex++;
                }
            }

            WorkToken();
        }

        private int BlockIndex = 0;
        private List<CLToken> GetLastToken(int Direction = 0)
        {
            List<CLToken> Result = TokenList.Block;

            BlockIndex += Direction;

            for (int levels = 0; levels < BlockIndex; levels++)
            {
                if (Result.Count() > 0)
                {
                    if (Result[Result.Count - 1].Block == null)
                        Result[Result.Count - 1].Block = new List<CLToken>();

                    Result = Result[Result.Count - 1].Block;
                }
            }

            return Result;
        }

        public void WorkToken(Boolean stringToken = false)
        {
            string piece = token;
            token = "";

            if (piece != "")
            {
                if (stringToken == false)
                {
                    foreach (var Piece in Pieces)
                    {
                        foreach (string Value in Piece.Value)
                        {
                            if (Value.Length > 1 && Value.StartsWith("/") && Value.EndsWith("/") && !OPERATORS.Contains(piece))
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(piece, Value.Trim('/')))
                                {
                                    GetLastToken().Add(new CLToken(Piece.Key, piece));
                                    return;
                                }
                            }
                            else
                            {
                                if (Value == piece)
                                {
                                    if (BLOCK_OPEN.Contains(piece))
                                    {
                                        GetLastToken(1);
                                    }
                                    else if (BLOCK_CLOSE.Contains(piece))
                                    {
                                        GetLastToken(-1);
                                    }
                                    else
                                    {
                                        GetLastToken().Add(new CLToken(Piece.Key, piece));
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    GetLastToken().Add(new CLToken(Type.STRING_LITERAL, piece));
                }
            }

        }
    }

    class CLToken
    {
        public List<CLToken> Block;
        public CLLexer.Type Type;
        public string Value;

        public CLToken(CLLexer.Type type, string value)
        {
            Type = type;
            Value = value;
            Block = null;
        }
    }
}
