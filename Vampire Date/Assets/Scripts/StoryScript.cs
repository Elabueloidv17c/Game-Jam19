using System;
using System.IO;
using UnityEngine;

namespace SurvivalNightStory
{
  [Serializable]
  public class Dialog
  {
    public int next = -2;
    public bool buttons = false;
    public string image = "";
    public string message;
    public LoadTextOption option;
    public int yes = -1;
    public string textYes = "";
    public int no = -1;
    public string textNo = "";
    public bool loop = true;
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

      var res = JsonUtility.FromJson<StoryScript>(src);
      for (int i = 0; i < res.scenes.Length; i++)
      {
        for (int j = 0; j < res.scenes[i].dialogs.Length; j++)
        {
          if(res.scenes[i].dialogs[j].next == -2)
          {
            res.scenes[i].dialogs[j].next = j + 1;
          }
        }
      }

      return res;
    }
  }
}
