using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

using SnowSharp.Variables;
using SnowSharp.Functions;
using SnowSharp.Dialogue;
using SnowSharp.Thesauruses;
using SnowSharp.UI;
using SnowSharp.Themes;

public class DialogueManager : MonoBehaviour {

    [Header("Practical")]
    [Tooltip("The name of the subfolder in StreamingAssets that houses the SnowSharp files.")]
    public string directory = "root";
        
    [Tooltip("The name of the starting SnowSharp file.")]
    public string file = "StartUp";

    [Tooltip("If this is ticked on, there will be no character name before the lines printed on start.")]
    public bool startWithExposition;

    [Tooltip("Starting cast of characters.")]
    public string[] startingCast = new string[1] { "Kate" };

    [Tooltip("Starting speed at which text appears. Default value is 10. Higher value means faster typing.")]
    public int startingTextSpeed;

    [Tooltip("Current game version number")]
    public double version = 1.00;

    [Header("Debug")]
    public Choice tree;
     public List<int> line;
     public Choice savedChoice;
     public List<int> savedLine;

    //Static Snow Sharp Values
    public static TextParser parser;
    public static ChoiceManager choiceManager;
    public static FunctionManager functionManager;
    public static VariableManager varManager;
    public static UIManager uiManager;
    public static UIConstructor uiConstructor;
    public static CharacterManager charManager;
    public static Thesaurus thes;
    public static ThemeConstructor themeConstructor;
    string[] versionValue;

    [HideInInspector]
    public bool canPressButton;

    public static List<int> dialogueLine = new List<int>(1);

    public static Choice currentlyActiveChoice;
    public static string appPath;

    string text;

    void Awake()
    {
        System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.NeutralCultures)[0];

        appPath = Application.streamingAssetsPath;

        dialogueLine = new List<int>(1){ 0 };

        parser = new TextParser(this);
        choiceManager = new ChoiceManager();
        charManager = new CharacterManager(startingCast);

        themeConstructor = new ThemeConstructor(this);

        uiConstructor = new UIConstructor();
        uiManager = new UIManager(this, uiConstructor.ConstructText(Color.black), uiConstructor.ConstructGroup(1, false, Color.gray), uiConstructor.ConstructCanvas(), uiConstructor.ConstructButton(Color.black, Color.black), uiConstructor.ConstructGroup(0, false, Color.gray), uiConstructor.ConstructLayoutGroup());

        varManager = new VariableManager();
        functionManager = new FunctionManager(this);

        //Base starting values
        if(startWithExposition)
        {
            varManager.SetVar(new string[] { "charVisible=0" });
        }
        else
        {
            varManager.SetVar(new string[] { "charVisible=1" });
        }
        varManager.SetVar(new string[] { "typeRate=" + startingTextSpeed });
        varManager.SetVar(new string[] { "buildV=" + version });
        varManager.SetVar(new string[] { "pitch=1" });

        thes = new Thesaurus("Default");
        uiManager = themeConstructor.ConstructTheme("Default");
    }

    IEnumerator Start()
    {
        text = string.Empty;
        StartCoroutine(LoadVersion());
        yield return new WaitForSeconds(0);
        StartReading(parser.dialogue);
    }

    public void StartReading(Choice choice, float time = 0)
    {
        StartCoroutine(ReadDialogue(choice, time));
    }

    public IEnumerator ReadDialogue(Choice subChoice, float time=0)
    {
        //uiManager.dialogueText.text = string.Empty;
        while (uiManager.isDisplayingText)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(time);
        currentlyActiveChoice = subChoice;
        string func;
        for (int i = dialogueLine[dialogueLine.Count - 1]; i < subChoice.choiceText.Count; i++)
        {
            string line = subChoice.choiceText[i];
            dialogueLine[dialogueLine.Count - 1] = i;
            switch (subChoice.choiceText[i].First())
            {
                default:
                    text += (varManager.EvaluateFormula("charVisible") == 1 ? (charManager.currentChar + ": ") : "") + subChoice.choiceText[i];
                    if (text.Contains("\\n"))
                    {
                        text = text.Replace(" \\n", "\n").Replace("\\n", "\n");
                    }
                    else
                    {
                        StartCoroutine(uiManager.DisplayText(text));
                        yield return new WaitForSeconds(.6f);
                        canPressButton = true;
                        while (canPressButton)
                        {
                            yield return new WaitForEndOfFrame();
                        }
                        text = string.Empty;
                    }
                    break;
                case '-': //$SwitchCharacter() --- OR --- $SetCast() Quick notation
                    line = line.Replace("-", "").Replace("(", "").Replace(")", "");
                    string[] args = parser.ParseParameters(line);
                    if (args.Length == 1)
                    {
                        charManager.SetCurrentChar(args[0]);
                    }
                    else
                    {
                        charManager.SetCharacterCast(args);
                    }
                    break;
                case '*': //$DisplayChoice() Quick notation
                    SaveLine(i+1);
                    uiManager.DisplayChoices(line.Replace("*", ""));
                    yield break;
                case '[': //$SetVar() Quick notation
                    line = line.Replace("[", "").Replace("]", "").Replace(" ", "");
                    func = "setvar(" + line + ")";
                    functionManager.InterpretFunction(func);
                    break;
                case '<': //$SetPersistentVar() Quick notations
                    line = line.Replace("<", "").Replace(">", "").Replace(" ", "");

                    func = "setpersistentvar(" + line + ")";
                    functionManager.InterpretFunction(func);
                    break;
                case '|': //$GetVar() Quick notation --OR-- $CompareVar() Quick notation
                    line = line.Replace("|", "").Replace(" ", "");
                    if(!(line.Contains(">") || line.Contains("<") || line.Contains("=")))
                    {
                        Debug.Log(varManager.EvaluateFormula(line));
                        break;
                    }
                    else
                    {
                        SaveLine(i + 1);
                        func = "comparevar(" + line + ")";
                        functionManager.InterpretFunction(func);
                        yield break;
                    }
                case '%': //$ChanceDialogue() Quick Notation
                    line = line.Replace(" ", "").Replace("%", "").Replace("(", "").Replace(")", "");
                    SaveLine(i + 1);

                    func = "random(" + line + ")";
                    functionManager.InterpretFunction(func);
                    yield break;
                case '$': //Special function
                    func = line.Substring(1, line.IndexOf("(") - 1).ToLower();
                    if (thes.IsSynonym("wait", func))
                    {
                        StartCoroutine(uiManager.DisplayText(text, true));
                        text = string.Empty;
                    }
                    if (functionManager.RequiresStop(line.ToLower().Replace("$", "")))
                    {
                        SaveLine(i + 1);
                        functionManager.InterpretFunction(line.Replace("$", ""));
                        yield break;
                    }
                    else
                    {
                        functionManager.InterpretFunction(line.Replace("$", ""));
                        break;
                    }
            }
        }
        if(subChoice.parentChoice != subChoice)
        {
            FallThrough();
        }
        else
        {
            Debug.LogWarning("[Snow# 4] End of File.");
        }
    }

    void SaveLine(int i)
    {
        dialogueLine[dialogueLine.Count - 1] = i;
        dialogueLine.Add(0);
    }

    public void FallThrough(float time = 0)
    {
        dialogueLine.Remove(dialogueLine[dialogueLine.Count - 1]);
        StartReading(currentlyActiveChoice.parentChoice, time);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchButton(); //Switch the button.
        }

        //Enable for visible debug
        tree = currentlyActiveChoice;
        line = dialogueLine;
        if(varManager.keys.ContainsKey("b"))
        {
            savedChoice = varManager.keys["b"].choice;
            savedLine = varManager.keys["b"].lines;
        }
    }

    public void SwitchButton()
    {
        if(uiManager.isDisplayingText)
        {
            uiManager.isDisplayingText = false;
        }
        else
        {
            canPressButton = false;
        }
    }

    IEnumerator LoadVersion()
    {
        WWW www = new WWW("https://pastebin.com/raw/9zj3NYFk");
        yield return www;
        versionValue = new string[] { "version=" + www.text };
        DialogueManager.varManager.SetVar(versionValue);
    }

    public void PlayBeep(char c)
    {
        if(varManager.EvaluateFormula("textSound")==1)
        {
            AudioSource audio = GetComponent<AudioSource>();
            if (!audio.isPlaying && c != '\n')
            {
                audio.pitch = (float)varManager.EvaluateFormula("pitch");
                audio.Play();
            }
        }
    }
}
