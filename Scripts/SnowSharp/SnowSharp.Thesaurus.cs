using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace SnowSharp.Thesauruses
{
    public class Thesaurus
    {
        Dictionary<string, List<string>> thesaurus;

        public Thesaurus(string f)
        {
            thesaurus = LoadThesaurus(f);
        }

        Dictionary<string, List<string>> LoadThesaurus(string fileName)
        {
            Dictionary<string, List<string>> thes = new Dictionary<string, List<string>>();
            string[] lines = File.ReadAllLines(DialogueManager.appPath + "/Thesaurus/" + fileName + ".ssthes");
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "" && lines[i].First() != '/')
                {
                    lines[i] = lines[i].Replace(" ", "").Replace("\t", "").ToLower();
                    string key = lines[i].Substring(0, lines[i].IndexOf(":"));
                    List<string> arg = lines[i].Substring(lines[i].IndexOf(":") + 1).Split(',').ToList();
                    arg.Add(key);
                    if (arg.Contains("none"))
                    {
                        arg.Remove("none");
                    }
                    thes.Add(key, arg);
                }
            }
            return thes;
        }

        public bool IsSynonym(string compare, string arg)
        {
            int matches = 0;
            if (thesaurus.ContainsKey(compare))
            {
                foreach(string val in thesaurus[compare])
                {
                    if (val == arg)
                    {
                        matches++;
                    }
                }
            }
            else
            {
                throw new Exception("[Snow# 4] : Faulty hardcoded function value: " + compare);
            }

            return matches == 1;
        }
    }
}
