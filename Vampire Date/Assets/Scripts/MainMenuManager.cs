using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    private GameObject mainPanel;
    private GameObject creditsPanel;
    private AudioSource audioSource;

    public AudioClip mainMenuMusic;
    public AudioClip creditsMusic;

    private void Awake()
    {
        mainPanel = GameObject.Find("MainPanel");
        creditsPanel = GameObject.Find("CreditsPanel");
        audioSource = GameObject.Find("MainMenuMusic").GetComponent<AudioSource>();
        //FindObjectOfType<PlayVideo>().Play();
        audioSource.clip = mainMenuMusic;
        audioSource.Play();

        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

	public void PlayButton()
    {
        if (ScreenFader.instance == null || ScreenFader.instance.IsFading) return;
        StopAllCoroutines();
        StartCoroutine(PlayButtonCoroutine());
    }

    private IEnumerator PlayButtonCoroutine()
    {
        while(ScreenFader.instance == null || ScreenFader.instance.IsFading)
            yield return null;
        ScreenFader.instance.FadeStart(ScreenFader.FADE_TO_BLACK);
        while (ScreenFader.instance.IsFading)
            yield return null;
        SceneManager.LoadScene(1);
    }


    public void CreditsButton()
    {
        if (ScreenFader.instance == null || ScreenFader.instance.IsFading) return;
        StopAllCoroutines();
        StartCoroutine(CreditsButtonCoroutine(0));
    }

    private IEnumerator CreditsButtonCoroutine(int dir)
    {
        while (ScreenFader.instance == null || ScreenFader.instance.IsFading)
            yield return null;
        ScreenFader.instance.FadeStart(ScreenFader.FADE_TO_BLACK);
        while (ScreenFader.instance.IsFading)
            yield return null;
        if (dir == 0)
        {
            mainPanel.SetActive(false);
            creditsPanel.SetActive(true);
            audioSource.clip = creditsMusic;
            audioSource.Play();
        }
        else
        {
            mainPanel.SetActive(true);
            creditsPanel.SetActive(false);
            audioSource.clip = mainMenuMusic;
            audioSource.Play();
        }
        ScreenFader.instance.FadeStart(ScreenFader.FADE_TO_CLEAR);
    }

    public void ExitButton()
    {
        if (ScreenFader.instance == null || ScreenFader.instance.IsFading) return;
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void BackButton()
    {
        if (ScreenFader.instance == null || ScreenFader.instance.IsFading) return;
        StopAllCoroutines();
        StartCoroutine(CreditsButtonCoroutine(1));
    }
}
