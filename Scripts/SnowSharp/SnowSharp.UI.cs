using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace SnowSharp.UI
{
    public class UIManager
    {
        DialogueManager dialogue;

        public CanvasGroup choiceBox;
        public List<Button> buttons = new List<Button>();
        public List<Text> choiceOptions = new List<Text>();
        public Text dialogueText;
        public CanvasGroup dialogueBox;
        public GridLayoutGroup bLayout;
        public Canvas canvas;

        public string[] choices;

        public bool isDisplayingText;

        bool wasWait;

        public UIManager(DialogueManager d, Text dText, CanvasGroup dBox, Canvas c, List<Button> b, CanvasGroup cBox, GridLayoutGroup g)
        {
            dialogue = d;
            canvas = c;
            dialogueBox = dBox;
            dialogueText = dText; 
            buttons = b;
            choiceBox = cBox;
            bLayout = g;
            RigUI();
        }

        public void ResetUI()
        {
            if (canvas != null)
            {
                MonoBehaviour.Destroy(canvas.gameObject);
            }
            else if(GameObject.Find("SnowSharpCanvas"))
            {
                MonoBehaviour.Destroy(GameObject.Find("SnowSharpCanvas"));
            }
        }

        public void RigUI()
        {
            dialogueBox.transform.SetParent(canvas.transform, false);
            dialogueText.transform.SetParent(dialogueBox.transform, false);

            choiceBox.transform.SetParent(canvas.transform, false);
            bLayout.transform.SetParent(choiceBox.transform, false);

            foreach(Button b in buttons)
            {
                b.transform.SetParent(bLayout.transform, false);
                choiceOptions.Add(b.transform.GetChild(0).gameObject.GetComponent<Text>());
                b.onClick.AddListener(delegate { ChooseChoice(buttons.IndexOf(b)); });
            }
            choiceBox.interactable = true;
            choiceBox.interactable = false;
        }

        public IEnumerator DisplayText(string text, bool wait=false)
        {
            isDisplayingText = true;
            if(!wasWait)
            {
                dialogueText.text = string.Empty;
            }
            wasWait = wait;
            if(DialogueManager.varManager.EvaluateFormula("textAnim")==1)
            {
                foreach (char c in text.ToCharArray())
                {
                    dialogueText.text += c;
                    dialogue.PlayBeep(c);
                    yield return new WaitForSeconds((float)(1/(5f*DialogueManager.varManager.EvaluateFormula("typeRate"))));
                    if(!isDisplayingText)
                    {
                        dialogueText.text = text;
                        break;
                    }
                }
            }
            else
            {
                dialogueText.text = text;
            }
            isDisplayingText = false;
        }

        public void DisplayChoices(string line)
        {
            List<string> validChoices = new List<string>();
            choices = DialogueManager.parser.ParseParameters(line, '/');
            if (choices.Length > choiceOptions.Count)
            {
                throw new Exception("[Snow# 4] Choice overload : More choices defined than can be displayed. Load a theme that can manage more choices.");
            }

            validChoices = DialogueManager.choiceManager.GetValidChoices(choices);
            if(validChoices.Count == 0)
            {
                dialogue.FallThrough();
                DialogueManager.currentlyActiveChoice.choicesPassed += choices.Length;
            }
            else if (validChoices.Count == 1)
            {
                dialogue.StartReading(DialogueManager.currentlyActiveChoice.subChoices[DialogueManager.currentlyActiveChoice.choicesPassed + Array.IndexOf(choices, validChoices[0])]);
            }
            else
            {
                for (int i = 0; i < choiceOptions.Count; i++)
                {
                    string cChoice = "";
                    if (i < choices.Length)
                    {
                        cChoice = DialogueManager.parser.ClearedChoice(choices[i]);
                        choiceOptions[i].text = cChoice;
                    }
                    buttons[i].gameObject.SetActive(i < choices.Length && validChoices.Contains(choices[i]));
                }
                dialogueBox.blocksRaycasts = false;
                choiceBox.interactable = true;
                choiceBox.alpha = 1;
            }
        }

        //Called by the UI buttons
        public void ChooseChoice(int i)
        {
            dialogue.StartReading(DialogueManager.currentlyActiveChoice.subChoices[DialogueManager.currentlyActiveChoice.choicesPassed + i]);
            DialogueManager.currentlyActiveChoice.choicesPassed += choices.Length;
            dialogueBox.alpha = 1;
            dialogueBox.blocksRaycasts = true;
            choiceBox.interactable = false;
            choiceBox.blocksRaycasts = true;
            choiceBox.alpha = 0;
        }
    }

    public class UIConstructor
    {
        public void SetPosition(GameObject g, float posX, float posY, float w, float h)
        {
            RectTransform rectTransform = g.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(posX, posY);
            rectTransform.sizeDelta = new Vector2(w, h);
        }

        public Text ConstructText(Color color, float posX = 0, float posY = 0, float w = 1150, float h = 155, string font = "arial", TextAnchor align = TextAnchor.UpperLeft)
        {
            GameObject textObject = new GameObject("Text");
            textObject.AddComponent<RectTransform>();
            Text t = textObject.AddComponent<Text>();
            t.font = Resources.Load<Font>(font);
            t.resizeTextForBestFit = true;
            t.resizeTextMinSize = 10;
            t.resizeTextMaxSize = 40;
            t.alignment = align;
            t.color = color;
            SetPosition(textObject, posX, posY, w, h);
            return textObject.GetComponent<Text>();
        }

        public CanvasGroup ConstructGroup(float a, bool interactable, Color color, float posX = 0, float posY = -240, float w = 1190, float h = 185)
        {
            GameObject group = new GameObject("Box");
            CanvasGroup g = group.AddComponent<CanvasGroup>();
            Image im = group.AddComponent<Image>();
            im.color = color;
            im.raycastTarget = false;
            SetPosition(group, posX, posY, w, h);
            g.alpha = a;
            g.interactable = interactable;
            g.blocksRaycasts = true;
            return g;
        }

        public GridLayoutGroup ConstructLayoutGroup(float cellX = 350f, float cellY = 75f, float spaceX = 145f, float spaceY = 12f, float w = 1190, float h = 185, GridLayoutGroup.Axis startAxis = GridLayoutGroup.Axis.Horizontal, TextAnchor anchor = TextAnchor.MiddleCenter)
        {
            GameObject layout = new GameObject("LayoutGroup");
            layout.AddComponent<RectTransform>();
            GridLayoutGroup group = layout.AddComponent<GridLayoutGroup>();
            group.cellSize = new Vector2(cellX, cellY);
            group.spacing = new Vector2(spaceX, spaceY);
            group.startAxis = startAxis;
            group.childAlignment = anchor;
            SetPosition(layout, 0, 0, w, h);
            return layout.GetComponent<GridLayoutGroup>();
        }

        public List<Button> ConstructButton(Color textColor, Color buttonColor, int number = 4, float cellX = 350f, float cellY = 75f, string font = "arial", TextAnchor align = TextAnchor.MiddleCenter)
        {
            List<Button> buttons = new List<Button>();
            for(int i = 0; i < number; i++)
            {
                GameObject button = new GameObject("Button");
                button.AddComponent<Button>();
                Image im = button.AddComponent<Image>();
                im.color = buttonColor;
                button.GetComponent<Button>().targetGraphic = button.GetComponent<Image>();
                ConstructText(textColor, 0, 0, cellX, cellY, font, align).transform.SetParent(button.transform);
                buttons.Add(button.GetComponent<Button>());
            }
            return buttons;
        }

        public void Choose(int i)
        {
            UIManager uiManager = DialogueManager.uiManager;
            uiManager.ChooseChoice(i);
        }

        public Canvas ConstructCanvas()
        {
            GameObject canvas = new GameObject("SnowSharpCanvas");
            CanvasScaler cScale = canvas.AddComponent<CanvasScaler>();
            cScale.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cScale.referenceResolution = new Vector2(1280, 720);
            canvas.AddComponent<GraphicRaycaster>();
            Canvas c = canvas.GetComponent<Canvas>();
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            return c;
        }
    }
}
