using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SnowSharp.Variables;

namespace SnowSharp.Dialogue
{
    public class TextParser
    {
        readonly string path;
        const string extension = "ss";

        string[] lines;

        DialogueManager d;

        public Choice dialogue = new Choice(null);
        public Choice currentActive;

        public TextParser(DialogueManager dia)
        {
            d = dia;
            MakeTree(d.file, d.directory);
        }

        public TextParser(string startFile, string startDirectory)
        {
            MakeTree(startFile, startDirectory);
        }

        // Use this for initialization
        public void MakeTree(string file, string directory)
        {
            dialogue = new Choice(null);
            string path = Path.Combine(DialogueManager.appPath, "SnowSharp/" + (directory == "root" ? "" : directory + "/") + file + "." + extension);
            lines = File.ReadAllLines(path);
            currentActive = dialogue;
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("\t", "");
                if (lines[i] == "}")
                {
                    currentActive.parentChoice.subChoices.Add(currentActive);
                    currentActive = currentActive.parentChoice;
                }
                else if (lines[i] == "{")
                {
                    currentActive = new Choice(currentActive);
                }
                else if(lines[i] != "" && lines[i].First() != '/')
                {
                    currentActive.choiceText.Add(lines[i]);
                }
            }
            if (currentActive != dialogue)
            {
                throw new Exception("[Snow# 4] Syntax Error : Choice block missing end point.");
            }
        }

        public string[] ParseParameters(string paramLine, char splitter = ',')
        {
            return paramLine.Split(splitter);
        }

        public string ClearedChoice(string choice)
        {
            choice = choice.Remove(0, (choice[0] == ' ' ? 1 : 0));
            choice = choice.Remove(choice.Length - 1, (choice[choice.Length - 1] == ' ' ? 1 : 0));
            choice = choice.Replace("\\n ", "\n");
            if (choice.Contains("("))
            {
                choice = choice.Remove(choice.IndexOf("("));
            }
            if(choice.Contains("|"))
            {
                choice = choice.Remove(choice.IndexOf("|"));
            }
            return choice;
        }
    }

    public class CharacterManager
    {
        List<string> cast = new List<string>();
        public string currentChar;

        public CharacterManager(string[] cast)
        {
            SetCharacterCast(cast);
        }

        public void SetCharacterCast(string[] args)
        {
            cast = new List<string>();
            foreach (string arg in args)
            {
                cast.Add(arg.Remove(0, (arg[0] == ' ' ? 1 : 0)));
            }
            if(args.Length > 0)
            {
                currentChar = args[0];
            }
        }

        public void SetCurrentChar(string arg)
        {
            int val = -1;
            if(int.TryParse(arg, out val))
            {
                if(val >= cast.Count)
                {
                    throw new Exception("[Snow# 4] : Index invalid!");
                }
                else
                {
                    currentChar = cast[val];
                }
            }
            else if(cast.Contains(arg.Replace(" ","")))
            {
                currentChar = arg;
            }
            else
            {
                throw new Exception("[Snow# 4] : Character or index (" + arg + ") not defined in cast!");
            }
        }
    }

    public class ChoiceManager
    {
        public List<string> GetValidChoices(string[] choices)
        {
            List<string> validChoices = new List<string>();
            for (int i = 0; i < choices.Length; i++)
            {
                string[] cond = choices[i].Split(',');
                if (choices[i].Contains("("))
                {
                    cond = choices[i].Substring(choices[i].IndexOf("(")).Split(',');
                }

                int validParam = 0;
                for (int t = 0; t < cond.Length; t++)
                {
                    if (cond[t].Contains("|"))
                    {
                        cond[t] = cond[t].Replace("|", "").Replace(" ", "").Replace("(","").Replace(")","");
                        if(DialogueManager.varManager.CompareVar(DialogueManager.parser.ParseParameters(cond[t])) == 1)
                        {
                            validParam++;
                        }
                    }
                    else
                    {
                        validParam++;
                    }
                }
                if(validParam == cond.Length)
                {
                    validChoices.Add(choices[i]);
                }
            }
            return validChoices;
        }
    }

    [Serializable]
    public class Choice
    {
        public List<string> choiceText = new List<string>();
        public List<Choice> subChoices = new List<Choice>();
        public int choicesPassed = 0;
        public Choice parentChoice;

        public Choice(Choice parent)
        {
            parentChoice = parent ?? this;
        }

        public Choice Clone()
        {
            Choice cChoice = (Choice)this.MemberwiseClone();
            cChoice.parentChoice = this.parentChoice;
            cChoice.subChoices = new List<Choice>();
            foreach(Choice s in subChoices)
            {
                cChoice.subChoices.Add(s.Clone());
            }
            return cChoice;
        }
    }
}