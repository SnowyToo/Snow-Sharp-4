using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

using SnowSharp.Dialogue;
namespace SnowSharp.Variables
{
    public class VariableManager
    {
        Dictionary<string, double> variables;
        Dictionary<string, double> persVars;
        public Dictionary<string, KeyMoment> keys;
        readonly List<string> invalidSymbols;
        readonly List<string> logicOperators;

        public VariableManager()
        {
            persVars = LoadPersVars();
            if(persVars.Keys.Count == 0)
            {
                persVars.Add("textSound", 1);
                persVars.Add("textAnim", 1);
                persVars.Add("backgroundMusic", 1);
            }
            variables = new Dictionary<string, double>();
            foreach(string key in persVars.Keys)
            {
                variables.Add(key, persVars[key]);
            }
            invalidSymbols = new List<string>() { ":", "=", "!", "@", "<", ">"};
            logicOperators = new List<string>() { "<=", ">=", "!=", "=", "<", ">"};
            keys = new Dictionary<string, KeyMoment>();
        }

        public void ResetVar()
        {
            persVars = new Dictionary<string, double>();
            SavePersVars();
        }

        public void SetKey(string arg)
        {
            List<int> lineList = new List<int>();
            //Choice curChoice = DialogueManager.currentlyActiveChoice;
            Choice newChoice = DialogueManager.currentlyActiveChoice.Clone();
            //Choice curChoice = (Choice)DialogueManager.currentlyActiveChoice.Clone();
            for (int i = 0; i < DialogueManager.dialogueLine.Count; i++)
            {
                lineList.Add(DialogueManager.dialogueLine[i]);
            }
            KeyMoment key = new KeyMoment(newChoice, lineList);
            if (keys.ContainsKey(arg))
            {
                keys[arg] = key;
            }
            else
            {
                keys.Add(arg, key);
            }
        }

        public void SetVar(string[] args, bool persistent = false)
        {
            foreach (string arg in args)
            {
                string name = arg.Substring(0, arg.IndexOf('='));
                string value = arg.Substring(name.Length + 1);
                if (InvalidName(name))
                {
                    throw new Exception("[Snow# 4] : Invalid variable name. Do not include special operators in variable names");
                }
                if (double.TryParse(value, out double val))
                {
                    UpdateVar(variables, name, val);
                    if(persistent)
                    {
                        UpdateVar(persVars, name, val);
                        SavePersVars();
                    }
                }
                else if (value == "null")
                {
                    if (persistent && persVars.ContainsKey(name))
                    {
                        persVars.Remove(name);
                        SavePersVars();
                    }
                    if (variables.ContainsKey(name))
                    {
                        variables.Remove(name);
                    }
                }
                else if (value.Contains("*") || value.Contains("+") || value.Contains("-"))
                {
                    value = value.Replace("-", "+-");
                    UpdateVar(variables, name, EvaluateFormula(value));
                    if(persistent)
                    {
                        UpdateVar(persVars, name, EvaluateFormula(value));
                        SavePersVars();
                    }
                }
                else
                {
                    throw new Exception("[Snow# 4] Syntax Error : Non-number variable assignment is invalid in Snow# 4.");
                }
            }
        }

        public int CompareVar(string[] args)
        {
            int index = 0;
            foreach(string arg in args)
            {
                index++;
                string[] conditions = DialogueManager.parser.ParseParameters(arg, '&');
                int trues = 0;
                foreach(string cond in conditions)
                {
                    if (GetTrueExpressions(cond))
                    {
                        trues++;
                    }
                }
                if(trues == conditions.Length)
                {
                    return index;
                }
            }
            return 0;
        }

        public bool DoesCompare(string arg, string relation)
        {
            arg = arg.Replace(" ", "");
            string val1 = arg.Substring(0, arg.IndexOf(relation));
            string val2 = arg.Substring(val1.Length + relation.Length);
            switch(relation)
            {
                default:
                    throw new Exception("[Snow# 4] : Undefined relation between two values: '" + relation + "'.");
                case "<":
                    return (EvaluateFormula(val1) < EvaluateFormula(val2));
                case ">":
                    return (EvaluateFormula(val1) > EvaluateFormula(val2));
                case "=":
                    return (EvaluateFormula(val1) == EvaluateFormula(val2));
                case "!=":
                    return (EvaluateFormula(val1) != EvaluateFormula(val2));
                case "<=":
                    return (EvaluateFormula(val1) <= EvaluateFormula(val2));
                case ">=":
                    return (EvaluateFormula(val1) >= EvaluateFormula(val2));
            }
        }

        Dictionary<string, double> LoadPersVars()
        {
            Dictionary<string, double> vars = new Dictionary<string, double>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Entry>));
            FileStream stream = new FileStream(DialogueManager.appPath + "/PersistentVars.ssvar", FileMode.Open);
            List<Entry> saveData = serializer.Deserialize(stream) as List<Entry>;
            foreach(Entry entry in saveData)
            {
                vars.Add(entry.key, entry.value);
            }
            stream.Close();
            return vars;
        }

        void SavePersVars()
        {
            List<Entry> entries = new List<Entry>(persVars.Count);
            foreach (string key in persVars.Keys)
            {
                entries.Add(new Entry(key, persVars[key]));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Entry>));
            FileStream stream = new FileStream(DialogueManager.appPath +"/PersistentVars.ssvar", FileMode.Create);
            serializer.Serialize(stream, entries);
            stream.Close();
        }

        public double EvaluateFormula(string arg)
        {
            if(arg == "RAD" || arg == "rand")
            {
                return new Random().NextDouble()*100;
            }
            string valArg = VarToValue(arg);
            double.TryParse(VarToValue(valArg), out double totVal);

            if (VarToValue(valArg).Contains("*"))
            {
                double secVal = 0;
                string firstArg;
                string secArg;

                firstArg = valArg.Substring(0, valArg.IndexOf("*"));
                secArg = valArg.Remove(0, firstArg.Length + 1);
                if (firstArg.Contains("+"))
                {
                    firstArg = firstArg.Substring(firstArg.LastIndexOf("+") + 1);
                }
                if (secArg.Contains("+"))
                {
                    secArg = secArg.Substring(0, secArg.IndexOf("+"));
                }

                if (double.TryParse(firstArg, out double firstVal))
                {
                    if (!double.TryParse(secArg, out secVal))
                    {
                        secVal = EvaluateFormula(secArg);
                    }
                }
                else
                {
                    firstVal = EvaluateFormula(firstArg);
                }
                totVal = firstVal * secVal;
                valArg = valArg.Replace(firstArg + "*" + secArg, totVal.ToString());
            }
            if (VarToValue(valArg).Contains("+"))
            {
                totVal = Sum(valArg);
            }

            return totVal;
        }

        string VarToValue(string arg)
        {
            string orgArg = arg;
            double val = 0;
            int safetycounter = 0;
            while (!double.TryParse(arg.Replace("+", "").Replace("*", "").Replace("-", ""), out val))
            {
                List<string> matches = new List<string>();
                for (int i = 0; i < variables.Count; i++)
                {
                    if (arg.Contains(variables.ElementAt(i).Key))
                    {
                        matches.Add(variables.ElementAt(i).Key);
                    }
                }

                if (matches.Count == 0)
                {
                    //UnityEngine.Debug.LogWarning("[Snow# 4] Variable Error : One or more variables in the following formula are undefined: [" + orgArg + "]. Returning 0.");
                    return "0";
                }
                else
                {
                    arg = arg.Replace(FindBestFit(matches), variables[FindBestFit(matches)].ToString());
                    safetycounter++;
                    if (safetycounter == 20)
                    {
                        throw new Exception("[Snow# 4] Syntax Error : Cannot evaluate the following formula: [" + orgArg + "].");
                    }
                }
            }
            return arg;
        }

        string FindBestFit(List<string> lines)
        {
            string bestFit = lines[0];
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains(bestFit))
                {
                    bestFit = lines[i];
                }
            }
            return bestFit;
        }

        double Sum(string valArg)
        {
            string firstArg;
            string secArg;

            firstArg = valArg.Substring(0, valArg.IndexOf("+"));
            secArg = valArg.Remove(0, firstArg.Length + 1);
            if (double.TryParse(firstArg, out double firstVal) && double.TryParse(secArg, out double secVal))
            {
                return firstVal + secVal;
            }
            else
            {
                throw new Exception("[Snow# 4] Calculation Error : Imparsable sum found in: " + firstArg + " or " + secArg);
            }
        }

        void UpdateVar(Dictionary<string, double> dic, string name, double val)
        {
            if (dic.ContainsKey(name))
            {
                dic[name] = val;
            }
            else
            {
                dic.Add(name, val);
            }
        }

        bool InvalidName(string name)
        {
            foreach(string sym in invalidSymbols)
            {
                if(name.Contains(sym))
                {
                    return true;
                }
            }
            return false;
        }

        bool GetTrueExpressions(string arg)
        {
            foreach(string op in logicOperators)
            {
                if (arg.Contains(op))
                {
                    return DoesCompare(arg, op);
                }
            }
            return false;
        }
    }

    public class Entry
    {
        public string key;
        public double value;

        public Entry()
        { }

        public Entry(string key, double value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public class KeyMoment
    {
        public Choice choice;
        public List<int> lines;

        public KeyMoment(Choice c, List<int> l)
        {
            choice = new Choice (c.parentChoice);
            lines = l;
            choice.subChoices = c.subChoices;
            choice.choiceText = c.choiceText;
            choice.choicesPassed = c.choicesPassed;
        }
    }
}
