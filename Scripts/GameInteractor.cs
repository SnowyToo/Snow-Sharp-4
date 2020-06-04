using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInteractor : MonoBehaviour {

    DialogueManager dialogue;

    [SerializeField]
    CanvasGroup spaceWarn;
    float t;

	// Use this for initialization
	void Start ()
    {
        dialogue = GetComponent<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!DialogueManager.uiManager.isDisplayingText && dialogue.canPressButton)
        {
            t += 3f*Time.deltaTime;
            spaceWarn.alpha = Mathf.Sin(t+3.1415f)*0.5f;
        }
        else
        {
            t = 0;
            spaceWarn.alpha = 0;
        }

        if(DialogueManager.varManager.EvaluateFormula("quitGame") == 1)
        {
            Application.Quit();
        }

        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Resetting");
            DialogueManager.varManager.ResetVar();
        }

        Screen.SetResolution(Screen.width, Screen.height, DialogueManager.varManager.EvaluateFormula("fullScreen") == 1);

	}
}
