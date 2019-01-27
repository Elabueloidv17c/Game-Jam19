using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StorySystem;
using System.Linq;
using System;

//Not really just DialogManager
public class MainControl : MonoBehaviour
{
  //References to UI
  public Animator ButtonOption1;
  public Animator ButtonOption2;
  public Animator ButtonOption3;
  public TextTyper MainTT;
  public Image Character1Image;
  public Image Character2Image;

  //script object
  public string protagonistName = "Petyr";
  private StoryScript storyScript = null;

  string[] m_changeScene;

  //current dialogue being displayed
  private int dialogueIndex = 0;
  private int sceneIndex = 0;

  //in case buttons are pressed jump to the corresponding dialogue
  private string option1Tag;
  private string option2Tag;
  private string option3Tag;

  //are the buttons displayed?
  private bool buttonsSet;

  private void Awake()
  {
    storyScript = StoryScript.Load();

    m_changeScene = new string[] { "regresar al pueblo", "ir a la disco" };
  }

  public int SceneToLoad(string tag)
  {
    if("regresar al pueblo" == tag)
    {
        return 1;
    }
    
    if ("ir a la disco" == tag)
    {
        return 3;
    }

    return -1;
  }

  public void PressOption1()
  {
      dialogueIndex = FindIndexOfTag(option1Tag);
      TextTyperNext();
  }

  public void PressOption2()
  {
    dialogueIndex = FindIndexOfTag(option2Tag);
    TextTyperNext();
  }

  public void PressOption3()
  {
    dialogueIndex = FindIndexOfTag(option3Tag);
    TextTyperNext();
  }

  public int FindIndexOfTag(string tag)
  {
    int index = -1;

    if(tag == "\n")
    {
        return storyScript.scenes[sceneIndex].dialogs.Length;
    }

    for (int i = 0; i < storyScript.scenes[sceneIndex].dialogs.Length; i++)
    {
      if (storyScript.scenes[sceneIndex].dialogs[i].tag == tag)
      {
        index = i;
        break;
      }
    }

    if (SceneToLoad(tag) != -1)
    {
      GameManager.instance.WarptoLoadScene(SceneToLoad(tag));
      return 0;
      //Necesito in indice valido para el inicio de la siguiente escena
    }

    if (index == -1)
    {
      Debug.LogError("Dialog with tag: '" + tag + "' not found.");
    }

    return index;
  }

  public void TextTyperNext()
  {
      StartCoroutine(TextTyperNextCoroutine());
  }

  IEnumerator TextTyperNextCoroutine()
  {
    if (dialogueIndex >= storyScript.scenes[sceneIndex].dialogs.Length 
      || dialogueIndex < -1)
    {
        GameManager.instance.WarptoLoadScene(sceneIndex + 1);
    }
    else
    {
      SetCharacterImages(storyScript.scenes[sceneIndex].dialogs[dialogueIndex]);

      //check if we must display buttons
      if (storyScript.scenes[sceneIndex].dialogs[dialogueIndex].dialogOption 
        == DialogTextOption.EndWithOptions)
      {
        option1Tag = storyScript.scenes[sceneIndex].dialogs[dialogueIndex].option1.next;
        option2Tag = storyScript.scenes[sceneIndex].dialogs[dialogueIndex].option2.next;
        option3Tag = storyScript.scenes[sceneIndex].dialogs[dialogueIndex].option3.next;
      }
      else if(buttonsSet)
      {
        yield return StartCoroutine(SetEnableButtons(false));
      }
      SetDisplay(storyScript.scenes[sceneIndex].dialogs[dialogueIndex]);

      string nextTag = storyScript.scenes[sceneIndex].dialogs[dialogueIndex].next;
      if (nextTag == "")
      {
        dialogueIndex++;
      }
      else
      {
        dialogueIndex = FindIndexOfTag(nextTag);
      }
    }
  }

  void SetCharacterImages(Dialog dg)
  {
    if (dg.character1 == "\n")
    {
      Character1Image.sprite = null;
      Character1Image.color = Color.clear;
    }
    else
    {
      if (dg.character1 != "")
      {
        Sprite spr = Resources.Load<Sprite>("Characters/" + dg.character1);
        if (spr == null)
        {
          Debug.LogError("Expected Image '" + dg.character1 + "' Not Found.");
        }
        else
        {
          Character1Image.sprite = spr;
        }
      }
      if (Character1Image.sprite != null)
      {
          Character1Image.color = (dg.shader1) ? Color.gray : Color.white;
      }
    }

    if (dg.character2 == "\n")
    {
      Character2Image.sprite = null;
      Character2Image.color = Color.clear;
    }
    else
    {
      if (dg.character2 != "")
      {
        Sprite spr = Resources.Load<Sprite>("Characters/" + dg.character2);
        if (spr == null)
        {
          Debug.LogError("Expected Image '" + dg.character2 + "' Not Found.");
        }
        else
        {
          Character2Image.sprite = spr;
        }
      }
      if(Character2Image.sprite != null)
      {
          Character2Image.color = (dg.shader2) ? Color.gray : Color.white;
      }
    }
  }

  internal void StartScene(int index)
    {
        dialogueIndex = 0;
        sceneIndex = index;
        Character1Image.sprite = null;
        Character2Image.sprite = null;
        Character1Image.color = Color.clear;
        Character2Image.color = Color.clear;
        TextTyperNext();
    }

  void SetDisplay(Dialog dg)
  {
    if (dg.dialogOption == DialogTextOption.EndWithOptions)
    {
      ButtonOption1.GetComponentInChildren<Text>().text = dg.option1.text;
      ButtonOption2.GetComponentInChildren<Text>().text = dg.option2.text;
      ButtonOption3.GetComponentInChildren<Text>().text = dg.option3.text;
    }

    if(!string.IsNullOrEmpty(dg.text))
    {
        MainTT.NextEnabled = true;
        MainTT.textOption = dg.dialogOption;

        string textProta = dg.text.Replace("<prota>", protagonistName);
        MainTT.ShowText(textProta);
    }
  }
    
  IEnumerator SetEnableButtons(bool value)
  {
    //Save current button state
    buttonsSet = value;

    //Disable buttons
    ButtonOption1.GetComponent<Button>().interactable = false;
    ButtonOption2.GetComponent<Button>().interactable = false;

    //wait for text typer to stop typing text
    while (MainTT.IsTypingText && value)
    {
      yield return false;
    }

    //order buttons to fade-in or fade-out (animation)
    ButtonOption1.SetBool("IsShown", value);
    ButtonOption2.SetBool("IsShown", value);
    ButtonOption3.SetBool("IsShown", value);

    //Wait for button animation to end
    while (ButtonAnimationIsInTransition())
    {
      yield return false;
    }

    //Enable/Disable buttons
    ButtonOption1.GetComponent<Button>().interactable = value;
    ButtonOption2.GetComponent<Button>().interactable = value;
    ButtonOption3.GetComponent<Button>().interactable = value;
  }

  bool ButtonAnimationIsInTransition()
  {
    return
      ButtonOption1.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 ||
      ButtonOption1.IsInTransition(0) ||
      ButtonOption2.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 ||
      ButtonOption2.IsInTransition(0) ||
      ButtonOption3.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 ||
      ButtonOption3.IsInTransition(0);
  }

  void ShowButtons()
  {
    StartCoroutine(SetEnableButtons(true));
  }
}
