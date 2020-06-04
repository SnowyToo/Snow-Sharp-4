using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.Networking;

using SnowSharp.Variables;
using SnowSharp.Themes;
using SnowSharp.Dialogue;
using SnowSharp.Thesauruses;
using SnowSharp.UI;

namespace SnowSharp.Functions
{

    public class FunctionManager : MonoBehaviour
    {
        ThemeConstructor themeConstructor;
        ThemeSaver themeSaver;

        CharacterManager charManager;
        TextParser parser;
        VariableManager varManager;

        DialogueManager dialogue;

        Thesaurus thes;

        System.Random RG = new System.Random();

        public FunctionManager(DialogueManager d)
        {
            dialogue = d;

            themeConstructor = DialogueManager.themeConstructor;
            themeSaver = new ThemeSaver();

            charManager = DialogueManager.charManager;
            parser = DialogueManager.parser;
            varManager = DialogueManager.varManager;
        }

        public void InterpretFunction(string line)
        {
            string arg = line.Substring(line.IndexOf("(")).Replace("(", "").Replace(")", "");
            line = line.ToLower();
            string funcName = line.Substring(0, line.IndexOf("("));
            thes = DialogueManager.thes;
            if (thes.IsSynonym("opentheme", funcName))
            {
                DialogueManager.uiManager = themeConstructor.ConstructTheme(arg);
            }
            else if (thes.IsSynonym("savetheme", funcName))
            {
                themeSaver.CreateTheme(arg);
                DialogueManager.uiManager.ResetUI();
                DialogueManager.uiManager = themeConstructor.ConstructTheme(arg);
            }
            else if (thes.IsSynonym("setpersistentvar", funcName))
            {
                arg = arg.Replace(" ", "");
                varManager.SetVar(parser.ParseParameters(arg), true);
            }
            else if (thes.IsSynonym("setvar", funcName))
            {
                arg = arg.Replace(" ", "");
                varManager.SetVar(parser.ParseParameters(arg));
            }
            else if (thes.IsSynonym("resetvar", funcName))
            {
                arg = arg.Replace(" ", "");
                varManager.ResetVar();
            }
            else if (thes.IsSynonym("comparevar", funcName))
            {
                string[] statements = parser.ParseParameters(arg);
                bool hasElse = statements[statements.Length-1] == "else";
                DialogueManager.uiManager.choices = new string[statements.Length];
                int index = varManager.CompareVar(statements)-1;
                if(index >= 0)
                {
                    DialogueManager.uiManager.ChooseChoice(index);
                }
                else if(hasElse)
                {
                    DialogueManager.uiManager.ChooseChoice(DialogueManager.uiManager.choices.Length-1);
                }
                else
                {
                    dialogue.FallThrough();
                    DialogueManager.currentlyActiveChoice.choicesPassed += DialogueManager.uiManager.choices.Length;
                }
            }
            else if (thes.IsSynonym("setcast", funcName))
            {
                charManager.SetCharacterCast(parser.ParseParameters(arg));
            }
            else if (thes.IsSynonym("setchar", funcName))
            {
                charManager.SetCurrentChar(arg);
            }
            else if (thes.IsSynonym("random", funcName))
            {
                arg = arg.Replace(" ", "");
                Random(parser.ParseParameters(arg));
            }
            else if (thes.IsSynonym("goto", funcName))
            {
                arg = arg.Replace(" ", "");
                GotoKey(arg);
            }
            else if (thes.IsSynonym("setkey", funcName))
            {
                arg = arg.Replace(" ", "");
                varManager.SetKey(arg);
            }
            else if (thes.IsSynonym("loadthes", funcName))
            {
                arg = arg.Replace(" ", "");
                DialogueManager.thes = new Thesaurus(arg);
            }
            else if(thes.IsSynonym("requirechar", funcName))
            {
                string val = (arg.ToLower() == "false" ? "0" : "1");
                varManager.SetVar(new string[] { "charVisible="+ val });
            }
            else if(thes.IsSynonym("setspeed", funcName))
            {
                if(arg == "d" || arg == "default")
                {
                    arg = dialogue.startingTextSpeed + "";
                }
                varManager.SetVar(new string[] { "typeRate=" + arg });
            }
            else if(thes.IsSynonym("loadfile", funcName))
            {
                string[] args = parser.ParseParameters(arg);
                DialogueManager.parser.MakeTree(args[1].Replace(" ", ""), args[0]);
                DialogueManager.dialogueLine = new List<int>(1) { 0 };
                dialogue.StartReading(DialogueManager.parser.dialogue);
            }
            else if(thes.IsSynonym("wait", funcName))
            {
                if(float.TryParse(arg, out float t))
                {
                    dialogue.FallThrough(t);
                }
                else
                {
                    throw new Exception("Snow# 4: Argument of the '$Wait' function must be a numerical value. Value found was: [" + arg + "]");
                }
            }
            else if(thes.IsSynonym("quit", funcName))
            {
                Application.Quit();
            }
        }

        void GotoKey(string arg)
        {
            if (varManager.keys.ContainsKey(arg))
            {
                DialogueManager.dialogueLine = varManager.keys[arg].lines;

                Choice keyChoice = varManager.keys[arg].choice;

                Choice goChoice = new Choice(keyChoice)
                {
                    subChoices = keyChoice.subChoices,
                    choiceText = keyChoice.choiceText,
                    choicesPassed = keyChoice.choicesPassed
                };
                dialogue.StartReading(goChoice);
            }
            else
            {
                throw new Exception("[Snow# 4] : No valid key moment found.");
            }
        }

        public void Random(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                double randVal = RG.NextDouble()*100;
                if ((args[i] != "rest" && args[i] != "else" && randVal <= varManager.EvaluateFormula(args[i])) || (args[i] == "rest" || args[i] == "else"))
                {
                    DialogueManager.uiManager.choices = new string[args.Length];
                    DialogueManager.uiManager.ChooseChoice(i);
                    return;
                }
            }
            dialogue.FallThrough();
            DialogueManager.currentlyActiveChoice.choicesPassed += args.Length;
        }

        public bool RequiresStop(string arg)
        {
            thes = DialogueManager.thes;
            string funcName = arg.Substring(0, arg.IndexOf("("));
            return thes.IsSynonym("random", funcName) || thes.IsSynonym("comparevar", funcName) || thes.IsSynonym("goto", funcName) || thes.IsSynonym("loadfile", funcName) || thes.IsSynonym("wait", funcName);
        }
    }
}
