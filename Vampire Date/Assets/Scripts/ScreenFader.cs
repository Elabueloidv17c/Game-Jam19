using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {

    public static ScreenFader instance;

    public Texture2D faderTexture;
    public float fadeSpeed = 0.8f;
    private int drawDepth = -1000;
    private bool isFading = true;
    private int fadeDir = FADE_TO_CLEAR;
    private float alpha = 1f;

    public const int FADE_TO_BLACK = 1;
    public const int FADE_TO_CLEAR = -1;

    public bool IsFading
    {
        get{ return isFading; }
    }

    public float Alpha
    {
        get
        { return alpha; }
    }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += sceneLoaded;
        }
        else
        {
            Destroy(this);
        }
    }

    private void sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        alpha = 1f;
        isFading = true;
        fadeDir = FADE_TO_CLEAR;
    }

    public void FadeStart(int fadeDir)
    {
        isFading = true;
        this.fadeDir = fadeDir;
    }

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        if (Mathf.Clamp01(fadeDir) == alpha) isFading = false;
        GUI.color = new Color(1f, 1f, 1f, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), faderTexture);
    }
}
