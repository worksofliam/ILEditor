using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ILEditor.Classes.LanguageTools
{
    class RPGFree
    {
        private static string LastKey = "";
        private static Dictionary<string, List<string>> KeyLists = new Dictionary<string, List<string>>();
        private static void AddKeyList(string Name)
        {
            Name = Name.ToUpper();
            if (KeyLists.ContainsKey(Name))
                KeyLists[Name].Clear();
            else
                KeyLists.Add(Name, new List<string>());
        }
        private static void AddKeyItem(string ListName, string Key)
        {
            ListName = ListName.ToUpper();
            if (KeyLists.ContainsKey(ListName))
                KeyLists[ListName].Add(Key);
        }

        private static string prevName = "";
        private static Boolean isSubf = false;
        public static string getFree(string input)
        {
            input = ' ' + input.PadRight(80);
            char[] chars = input.ToCharArray();

            string line = input.Substring(8).Trim();
            string name = input.Substring(7, 16).Trim();
            string len = "";
            string type = "";
            string decimals = "";
            string keywords = input.Substring(44).Trim();
            string output = "";
            string field = "";

            string factor1 = "", factor2 = "", result = "", opcode = "", plainOp = "", extended = "";
            string ind1, ind2, ind3;

            switch (chars[7])
            {
                case '/':
                    if (line.ToUpper() == "FREE" || line.ToUpper() == "END-FREE") return "*BLANK";
                    return "";
                case '*':
                    if (line == "")
                    {
                        return "*BLANK;";
                    }
                    else
                    {
                        return "".PadLeft(6) + "*" + line;
                    }
            }

            switch (Char.ToUpper(chars[6]))
            {
                case 'H':
                    keywords = input.Substring(7);
                    output = "Ctl-Opt " + keywords.Trim() + ';';
                    break;
                case 'F':
                    
                    break;
                case 'D':
                    len = input.Substring(33, 7).Trim();
                    type = input.Substring(40, 1).Trim();
                    decimals = input.Substring(41, 3).Trim();
                    field = input.Substring(24, 2).Trim().ToUpper();

                    if (prevName != "")
                    {
                        name = prevName;
                        prevName = "";
                    }
                    if (line.EndsWith("..."))
                    {
                        prevName = line.Substring(0, line.Length - 3);
                        return "*BLANK;";
                    }

                    switch (type.ToUpper())
                    {
                        case "A":
                            if (keywords.ToUpper().Contains("VARYING"))
                            {
                                keywords = Regex.Replace(keywords, "varying", "", RegexOptions.IgnoreCase);
                                type = "Varchar";
                            }
                            else
                            {
                                type = "Char";
                            }
                            type += "(" + len + ")";
                            break;
                        case "B":
                            type = "Bindec" + "(" + len + ")";
                            break;
                        case "C":
                            type = "Ucs2" + "(" + len + ")";
                            break;
                        case "D":
                            type = "Date";
                            break;
                        case "F":
                            type = "Float" + "(" + len + ")";
                            break;
                        case "G":
                            if (keywords.ToUpper().Contains("VARYING"))
                            {
                                type = "Graph";
                            }
                            else
                            {
                                type = "Vargraph";
                            }
                            type += "(" + len + ")";
                            break;
                        case "I":
                            type = "Int" + "(" + len + ")";
                            break;
                        case "N":
                            type = "Ind";
                            break;
                        case "P":
                            type = "Packed" + "(" + len + ":" + decimals + ")";
                            break;
                        case "S":
                            type = "Zoned" + "(" + len + ":" + decimals + ")";
                            break;
                        case "T":
                            type = "Time";
                            break;
                        case "U":
                            type = "Uns" + "(" + len + ")";
                            break;
                        case "Z":
                            type = "Timestamp";
                            break;
                        case "*":
                            type = "Pointer";
                            break;
                        case "":
                            if (len != "")
                            {
                                if (decimals == "")
                                {
                                    if (keywords.ToUpper().Contains("VARYING"))
                                    {
                                        type = "Varchar";
                                    }
                                    else
                                    {
                                        type = "Char";
                                    }
                                    type += "(" + len + ")";
                                }
                                else
                                {
                                    if (isSubf)
                                    {
                                        type = "Zoned" + "(" + len + ":" + decimals + ")";
                                    } 
                                    else
                                    {
                                        type = "Packed" + "(" + len + ":" + decimals + ")";
                                    }
                                }
                            }
                            break;
                    }

                    switch (field)
                    {
                        case "C":
                            output = "Dcl-C " + name + " " + keywords;
                            break;
                        case "S":
                            output = "Dcl-S " + name + " " + type + " " + keywords;
                            break;
                        case "DS":
                        case "PR":
                        case "PI":
                            if (name == "") name = "*N";
                            isSubf = (field == "DS");
                            output = "Dcl-" + field + " " + name + " " + type + " " + keywords;
                            break;
                        case "":
                            if (name == "") name = "*N";
                            if (name == "*N" && type == "")
                            {
                                output = "//ABOVE VARIABLE KEYWORD ADITION: " + keywords;
                            }
                            else
                            {
                                output = "  " + (isSubf ? "Dcl-Subf" : "Dcl-Parm") + " " + name + " " + type + " " + keywords;
                            }
                            break;
                    }

                    output = output.TrimEnd() + ';';

                    break;

                case 'P':
                    if (prevName != "")
                    {
                        name = prevName;
                        prevName = "";
                    }
                    if (line.EndsWith("..."))
                    {
                        prevName = line.Substring(0, line.Length - 3);
                        return "*BLANK;";
                    }

                    switch (Char.ToUpper(chars[24]))
                    {
                        case 'B':
                            output = ("Dcl-Proc " + name + " " + keywords).TrimEnd() + ";";
                            break;
                        case 'E':
                            output = "End-Proc;";
                            break;
                    }
                    break;

                case 'C':
                    int spaces = 0;
                    string sep = "";
                    factor1 = input.Substring(12, 14).Trim();
                    opcode = input.Substring(26, 10).Trim().ToUpper();
                    plainOp = "";
                    factor2 = input.Substring(36, 14).Trim();
                    extended = input.Substring(36).Trim();
                    result = input.Substring(50, 14).Trim();

                    ind1 = input.Substring(71, 2);
                    ind2 = input.Substring(73, 2);
                    ind3 = input.Substring(75, 2);

                    plainOp = opcode;
                    if (plainOp.Contains('('))
                        plainOp = opcode.Substring(0, opcode.IndexOf('('));

                    switch (plainOp)
                    {
                        case "KLIST":
                            LastKey = factor1.ToUpper();
                            AddKeyList(LastKey);
                            output = "*SAME";
                            break;
                        case "KFLD":
                            //Handle var declaration
                            AddKeyItem(LastKey, result);
                            output = "*SAME";
                            break;
                        case "ADD":
                            output = result + " = " + result + " + " + factor2;
                            break;
                        case "BEGSR":
                            output = opcode + " " + factor1;
                            break;
                        case "CAT":
                            if (factor2.Contains(":"))
                            {
                                spaces = int.Parse(factor2.Split(':')[1]);
                                factor2 = factor2.Split(':')[0].Trim();
                            }
                            output = result + " = " + factor1 + "+ '" + "".PadLeft(spaces) + "' + " + factor2;
                            break;
                        case "CHAIN":
                            if (KeyLists.ContainsKey(factor1.ToUpper()))
                                output = opcode + " (" + String.Join(":", KeyLists[factor1.ToUpper()]) + ") " + factor2 + " " + result;
                            else
                                output = opcode + " " + factor1 + " " + factor2 + " " + result;
                            break;
                        case "CHECK":
                            output = result + " = %Check(" + factor1 + ":" + factor2 + ")";
                            break;
                        case "CHECKR":
                            output = result + " = %CheckR(" + factor1 + ":" + factor2 + ")";
                            break;
                        case "CLEAR":
                            output = opcode + " " + factor1 + " " + factor2 + " " + result;
                            break;
                        case "CLOSE":
                            output = opcode + " " + factor2;
                            break;
                        case "DELETE":
                            output = opcode + " " + factor2;
                            break;
                        case "DIV":
                            output = result + " = " + factor1 + " / " + factor2;
                            break;
                        case "DO":
                            output = "For " + result + " = " + factor1 + " to " + factor2;
                            break;
                        case "DOU":
                        case "DOW":
                            output = opcode + " " + extended;
                            break;
                        case "DOWEQ":
                            output = "Dow (" + factor1 + " = " + factor2 + ")";
                            break;
                        case "DOWNE":
                            output = "Dow (" + factor1 + " <> " + factor2 + ")";
                            break;
                        case "DOWGT":
                            output = "Dow (" + factor1 + " > " + factor2 + ")";
                            break;
                        case "DOWLT":
                            output = "Dow (" + factor1 + " < " + factor2 + ")";
                            break;
                        case "DOWGE":
                            output = "Dow (" + factor1 + " >= " + factor2 + ")";
                            break;
                        case "DOWLE":
                            output = "Dow (" + factor1 + " <= " + factor2 + ")";
                            break;
                        case "DSPLY":
                            output = opcode + " (" + factor1 + ") " + factor2 + " " + result;
                            break;
                        case "ELSE":
                            output = opcode + " " + factor2;
                            break;
                        case "ELSEIF":
                            output = opcode + " " + factor2;
                            break;
                        case "ENDDO":
                            output = "Enddo";
                            break;
                        case "ENDIF":
                            output = opcode;
                            break;
                        case "ENDMON":
                            output = opcode;
                            break;
                        case "ENDSL":
                            output = opcode;
                            break;
                        case "ENDSR":
                            output = opcode;
                            break;
                        case "EVAL":
                            output = extended;
                            break;
                        case "EVALR":
                            output = opcode + " " + extended;
                            break;
                        case "EXFMT":
                            output = opcode + " " + factor2;
                            break;
                        case "EXSR":
                            output = opcode + " " + factor2;
                            break;
                        case "FOR":
                            output = opcode + " " + extended;
                            break;
                        case "IF":
                            output = opcode + " " + extended;
                            break;
                        case "IFGT":
                            output = "If (" + factor1 + " > " + factor2 + ")";
                            break;
                        case "IFLT":
                            output = "If (" + factor1 + " < " + factor2 + ")";
                            break;
                        case "IFEQ":
                            output = "If (" + factor1 + " = " + factor2 + ")";
                            break;
                        case "IFNE":
                            output = "If (" + factor1 + " <> " + factor2 + ")";
                            break;
                        case "IFGE":
                            output = "If (" + factor1 + " >= " + factor2 + ")";
                            break;
                        case "IFLE":
                            output = "If (" + factor1 + " <= " + factor2 + ")";
                            break;
                        case "IN":
                            output = opcode + " " + factor1 + " " + factor2;
                            break;
                        case "ITER":
                            output = opcode;
                            break;
                        case "LEAVE":
                            output = opcode;
                            break;
                        case "LEAVESR":
                            output = opcode;
                            break;
                        case "LOOKUP":
                            output = "*In" + ind3 + " = (%Lookup(" + factor1 + ":" + factor2 + ") > 0)";
                            break;
                        case "MONITOR":
                            output = opcode;
                            break;
                        case "MULT":
                            output = result + " = " + factor1 + " / " + factor2;
                            break;
                        case "ON-ERROR":
                            output = opcode + " " + factor2;
                            break;
                        case "OPEN":
                            output = opcode + " " + factor2;
                            break;
                        case "OUT":
                            output = opcode + " " + factor1 + " " + factor2;
                            break;
                        case "READ":
                        case "READC":
                            output = opcode + " " + factor2 + " " + result;
                            break;
                        case "READE":
                            output = opcode + " " + factor1 + " " + factor2 + " " + result;
                            break;
                        case "READP":
                            output = opcode + " " + factor2 + " " + result;
                            break;
                        case "READPE":
                            output = opcode + " " + factor1 + " " + factor2 + " " + result;
                            break;
                        case "RETURN":
                            output = opcode + " " + factor2;
                            break;
                        case "SCAN":
                            output = result + " = %Scan(" + factor1 + ":" + factor2 + ")";
                            break;
                        case "SELECT":
                            output = opcode;
                            break;
                        case "SETGT":
                            output = opcode + " " + factor1 + " " + factor2;
                            break;
                        case "SETLL":
                            output = opcode + " " + factor1 + " " + factor2;
                            break;
                        case "SORTA":
                            output = opcode + " " + extended;
                            break;
                        case "SUB":
                            output = result + " = " + factor1 + " - " + factor2;
                            break;
                        case "SUBST":
                            if (factor2.Contains(":"))
                            {
                                sep = factor2.Split(':')[1];
                                factor2 = factor2.Split(':')[0].Trim();
                            }
                            output = result + " = %Subst(" + factor2 + ":" + sep + ":" + factor1 + ")";
                            break;
                        case "UNLOCK":
                            output = opcode + " " + factor2;
                            break;
                        case "UPDATE":
                            output = opcode + " " + factor2 + " " + result;
                            break;
                        case "WRITE":
                            output = opcode + " " + factor2 + " " + result;
                            break;
                        case "Z-ADD":
                            output = result + " = 0 + " + factor2;
                            break;
                        case "Z-SUB":
                            output = result + " = 0 - " + factor2;
                            break;
                        default:
                            return "";
                    }
                    output = output.TrimEnd() + ';';
                    break;

                default:
                    return "";
            }

            return "".PadLeft(7) + output;
        }
    }
    
}
