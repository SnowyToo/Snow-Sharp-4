using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

using SnowSharp.UI;

namespace SnowSharp.Themes
{
    public class ThemeConstructor
    {
        readonly DialogueManager dialogue;
        UIConstructor constructor = new UIConstructor();

        Text dText = null;
        CanvasGroup dBox = null;
        List<Button> b = null;
        CanvasGroup cBox = null;
        GridLayoutGroup g = null;

        public ThemeConstructor(DialogueManager d)
        {
            dialogue = d;
        }

        void ReadTheme(string fileName)
        {
            string[] lines = File.ReadAllLines(DialogueManager.appPath + "/Themes/" + fileName + ".sstheme");
            for (int i  = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace(" ", "").Replace("\t", "").ToLower();
                string[] arg = lines[i].Substring(lines[i].IndexOf(":")+1).Split(',');
                if (lines[i].Contains("dialoguetext"))
                {
                    dText = constructor.ConstructText(new Color(float.Parse(arg[5]), float.Parse(arg[6]), float.Parse(arg[7]), float.Parse(arg[8])), float.Parse(arg[0]), float.Parse(arg[1]), float.Parse(arg[2]), float.Parse(arg[3]), arg[4], GetAlignment(arg[9]));
                }
                if(lines[i].Contains("dialoguebox"))
                {
                    dBox = constructor.ConstructGroup(1, false, new Color(float.Parse(arg[4]), float.Parse(arg[5]), float.Parse(arg[6]), float.Parse(arg[7])), float.Parse(arg[0]), float.Parse(arg[1]), float.Parse(arg[2]), float.Parse(arg[3]));
                }
                if(lines[i].Contains("choicebox"))
                {
                    cBox = constructor.ConstructGroup(0, false, new Color(float.Parse(arg[4]), float.Parse(arg[5]), float.Parse(arg[6]), float.Parse(arg[7])), float.Parse(arg[0]), float.Parse(arg[1]), float.Parse(arg[2]), float.Parse(arg[3]));
                }
                if(lines[i].Contains("choicecells"))
                {
                    g = constructor.ConstructLayoutGroup(float.Parse(arg[0]), float.Parse(arg[1]), float.Parse(arg[2]), float.Parse(arg[3]), cBox.GetComponent<RectTransform>().sizeDelta.x, cBox.GetComponent<RectTransform>().sizeDelta.y);
                }
                if (lines[i].Contains("choices"))
                {
                    b = constructor.ConstructButton(new Color(float.Parse(arg[0]), float.Parse(arg[1]), float.Parse(arg[2]), float.Parse(arg[3])), new Color(float.Parse(arg[6]), float.Parse(arg[7]), float.Parse(arg[8]), float.Parse(arg[9])), int.Parse(arg[4]), g.cellSize.x, g.cellSize.y, arg[5]);
                }
            }
        }

        public UIManager ConstructTheme(string name)
        {
            ReadTheme(name);
            DialogueManager.uiManager.ResetUI();
            return new UIManager(dialogue, dText, dBox, constructor.ConstructCanvas(), b, cBox, g);
        }

        public TextAnchor GetAlignment(string arg)
        {
            switch(arg.ToLower())
            {
                case "center":
                case "c":
                    return TextAnchor.UpperCenter;
                case "right":
                case "r":
                    return TextAnchor.UpperRight;
                default:
                    return TextAnchor.UpperLeft;
            }
        }
    }

    public class ThemeSaver
    {
        
        public void CreateTheme(string name)
        {
            string[] lines = new string[5];
            if (GameObject.Find("SnowSharpCanvas") != null)
            {
                lines = new string[5] { GetDText(), GetDBox(), GetCBox(), GetCells(), GetChoices() };
            }
            else
            {
                throw new Exception("[Snow# 4] : The layout you want to save has to have the name 'SnowSharpCanvas'. None is found.");
            }
            File.WriteAllLines(DialogueManager.appPath + "/Themes/" + name + ".sstheme", lines);
        }

        string GetDText()
        {
            Text dText = GameObject.Find("SnowSharpCanvas").transform.GetChild(0).GetChild(0).GetComponent<Text>();
            RectTransform rect = dText.GetComponent<RectTransform>();
            return "DialogueText:" + Math.Round(rect.anchoredPosition.x, 2) + "," + Math.Round(rect.anchoredPosition.y, 2) + "," + Math.Round(rect.sizeDelta.x, 2) + "," + Math.Round(rect.sizeDelta.y, 2) + ",arial";
        }

        string GetDBox()
        {
            CanvasGroup dBox = GameObject.Find("SnowSharpCanvas").transform.GetChild(0).GetComponent<CanvasGroup>();
            RectTransform rect = dBox.GetComponent<RectTransform>();
            return "DialogueBox:" + Math.Round(rect.anchoredPosition.x, 2) + "," + Math.Round(rect.anchoredPosition.y, 2) + "," + Math.Round(rect.sizeDelta.x, 2) + "," + Math.Round(rect.sizeDelta.y, 2);
        }

        string GetCBox()
        {
            CanvasGroup cBox = GameObject.Find("SnowSharpCanvas").transform.GetChild(1).GetComponent<CanvasGroup>();
            RectTransform rect = cBox.GetComponent<RectTransform>();
            return "ChoiceBox:" + Math.Round(rect.anchoredPosition.x, 2) + "," + Math.Round(rect.anchoredPosition.y, 2) + "," + Math.Round(rect.sizeDelta.x, 2) + "," + Math.Round(rect.sizeDelta.y, 2);
        }

        string GetCells()
        {
            GridLayoutGroup cells = GameObject.Find("SnowSharpCanvas").transform.GetChild(1).GetChild(0).GetComponent<GridLayoutGroup>();
            return "ChoiceCells:" + Math.Round(cells.cellSize.x, 2) + "," + Math.Round(cells.cellSize.y, 2) + "," + Math.Round(cells.spacing.x, 2) + "," + Math.Round(cells.spacing.y, 2);
        }

        string GetChoices()
        {
            int button = GameObject.Find("SnowSharpCanvas").transform.GetChild(1).GetChild(0).childCount;
            return "Choices:"+button+",arial";
        }
    }
}
