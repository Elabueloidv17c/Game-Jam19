using System;
using System.IO;
using UnityEngine;

namespace StorySystem
{
  public enum DialogTextOption
  {
    EndWithArrow,
    EndWithoutArrow,
    EndWithOptions,
    Condition
  };

  [Serializable]
  public class ButtonOption
  {
    public string text;
    public string next = "";
  }

  [Serializable]
  public class Dialog
  {
    public string tag = "";
    public string text;
    public string next = "";
    public string character1 = "";
    public string character2 = "";
    public bool shader1 = false;
    public bool shader2 = false;
    public DialogTextOption dialogOption = 0;
    public ButtonOption option1 = null;
    public ButtonOption option2 = null;
    public ButtonOption option3 = null;
  }

  [Serializable]
  public struct Scene
  {
    public Dialog[] dialogs;
  }

  [Serializable]
  public class StoryScript
  {
    static string path = @"Assets/Resources/dialogs.json";

    public Scene[] scenes;

    public static StoryScript Load()
    {
      var reader = new StreamReader(path);
      string src = reader.ReadToEnd();
      StoryScript res = null;

      Debug.LogError("wtf");

      try
      {
        res = JsonUtility.FromJson<StoryScript>(src);
      }
      catch(ArgumentException e)
      {
        Debug.LogError(e.Message);
      }

      return res;
    }
  }
}
