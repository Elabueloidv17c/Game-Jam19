using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SurvivalNightStory;
using UnityEngine.Video;
using System.IO;
using UnityEditor;

public class MainControl : MonoBehaviour {
    public Animator ButtonYes;
    public Animator ButtonNo;
    public TextTyper MainTT;
    public Image BackgroundImage;
    public Image Character1Image;
    public Image Character2Image;
    private int counter = 0;
    private int yesOption;
    private int noOption;
    private bool buttonsSet;
    private StoryScript storyScript = null;

    private void Awake()
    {
      storyScript = StoryScript.Load();
      TextTyperNext();
    }

    public void PressYes()
    {
        counter = yesOption;
        TextTyperNext();
    }

    public void PressNo()
    {
        counter = noOption;
        TextTyperNext();
    }

    public void TextTyperNext()
    {
        StartCoroutine(TextTyperNextCoroutine());
    }

    IEnumerator TextTyperNextCoroutine()
    {
        if (counter >= storyScript.scenes[0].dialogs.Length || counter < 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            if (storyScript.scenes[0].dialogs[counter].buttons)
            {
                yesOption = storyScript.scenes[0].dialogs[counter].yes;
                noOption = storyScript.scenes[0].dialogs[counter].no;
            }
            else if(buttonsSet)
            {
                yield return StartCoroutine(SetEnableButtons(false));
            }
            SetDisplay(storyScript.scenes[0].dialogs[counter]);
            counter = storyScript.scenes[0].dialogs[counter].next;
        }
    }

    void SetDisplay(Dialog dg)
    {
        if (dg.image != "")
        {
            /*Sprite spr = Resources.Load<Sprite>("Backgrounds/" + dg.image);
            if (spr == null)
                Debug.LogError("Expected Clip '" + dg.image + "' Not Found.");
            else
            {
                BackgroundImage.sprite = spr;
            }*/
        }
        if(dg.textYes!="")
            ButtonYes.GetComponentInChildren<Text>().text = dg.textYes;
        if (dg.textNo != "")
            ButtonNo.GetComponentInChildren<Text>().text = dg.textNo;
        MainTT.NextEnabled = true;
        MainTT.loadTextOption = dg.option;
        MainTT.ShowText(dg.message);
    }

    IEnumerator SetEnableButtons(bool value)
    {
        buttonsSet = value;
        ButtonYes.GetComponent<Button>().interactable = false;
        ButtonNo.GetComponent<Button>().interactable = false;
        while (MainTT.IsTypingText && value)
            yield return false;
        ButtonYes.SetBool("IsShown", value);
        ButtonNo.SetBool("IsShown", value);
        while (ButtonYes.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || ButtonYes.IsInTransition(0) ||
            ButtonNo.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || ButtonNo.IsInTransition(0))
            yield return false;
        ButtonYes.GetComponent<Button>().interactable = value;
        ButtonNo.GetComponent<Button>().interactable = value;
    }

    void ShowButtons()
    {
        StartCoroutine(SetEnableButtons(true));
    }
}
