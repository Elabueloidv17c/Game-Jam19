using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform m_canvas;
    public Transform m_bat;
    public Transform m_petyrHouse;
    public Transform m_miriamHouse;
    public Transform m_elizabethHouse;

    public Transform m_character1;
    public Transform m_character2;
    public Transform m_dialog;

    public Transform m_woods;
    public Transform m_town;
    public Transform m_cafe;
    public Transform m_disco;
    public Transform m_directions;

    int m_lastScene;
    int m_sceneBeingLoaded;
    
    private enum SceneLoaderState
    {
        IDLE,
        QUITING_TO_MAIN_MENU,
        LOADING_SCENE
    }

    ScreenFader screenFader;
    private SceneLoaderState m_activeState;
    public bool IsLoading
    {
        get
        {
            bool value = m_activeState.IsAnyOf(
                SceneLoaderState.LOADING_SCENE,
                SceneLoaderState.QUITING_TO_MAIN_MENU)
                || null == screenFader
                || !screenFader.IsClear;
            return value;
        }
    }

    public ScreenFader Screen_Fader
    {
        get
        {
            if (null == screenFader)
            {
                screenFader = FindObjectOfType<ScreenFader>();
            }
            return screenFader;
        }

        private set
        {
            screenFader = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        m_activeState = SceneLoaderState.IDLE;
        m_lastScene = -1;
        SetScene(0);

        Screen_Fader.FadeToClear();
    }

    private void Update()
    {
        if (m_activeState.IsAnyOf(
            SceneLoaderState.LOADING_SCENE,
            SceneLoaderState.QUITING_TO_MAIN_MENU))
        {
            if (!Screen_Fader.IsFading)
            {
                m_activeState = SceneLoaderState.IDLE;
                //-------------------------------------------------
                SetScene(m_sceneBeingLoaded);
                //-------------------------------------------------
                Screen_Fader.FadeToClear();
            }
        }
    }

    public void QuittoMainMenu()
    {
        this.m_sceneBeingLoaded = 0;
        Screen_Fader.FadeToBlack();
        m_activeState = SceneLoaderState.QUITING_TO_MAIN_MENU;
    }

    public void WarptoLoadScene(int sceneLoading)
    {
        this.m_sceneBeingLoaded = sceneLoading;
        Screen_Fader.FadeToBlack();
        m_activeState = SceneLoaderState.LOADING_SCENE;
    }

    private void SetScene(int index)
    {
        if (m_lastScene != index)
        {
            switch (index)
            {
                case 0:
                    //Intro
                    m_canvas.gameObject.SetActive(true);
                    m_dialog.GetComponent<MainControl>().StartScene(index);

                    m_bat.position = new Vector3(0, 0, 0);
                    m_bat.gameObject.SetActive(false);

                    m_petyrHouse.GetComponent<SpriteRenderer>().enabled = true;

                    m_town.GetComponent<SpriteRenderer>().enabled = false;
                    m_directions.GetComponent<SpriteRenderer>().enabled = false;

                    m_miriamHouse.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.GetComponent<SpriteRenderer>().enabled = false;

                    m_woods.GetComponent<SpriteRenderer>().enabled = false;
                    m_cafe.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.GetComponent<SpriteRenderer>().enabled = false;

                    SoundManager.m_instance.m_intro.Play();

                    SoundManager.m_instance.m_cafe.Stop();
                    SoundManager.m_instance.m_club.Stop();
                    SoundManager.m_instance.m_town.Stop();
                    SoundManager.m_instance.m_woods.Stop();

                    break;

                case 1:
                    //Town
                    m_bat.position = new Vector3(0, 0, 0);
                    m_bat.gameObject.SetActive(true);

                    m_town.GetComponent<SpriteRenderer>().enabled = true;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    m_canvas.gameObject.SetActive(false);
                    m_character1.gameObject.SetActive(false);
                    m_character2.gameObject.SetActive(false);
                    m_dialog.gameObject.SetActive(false);

                    m_miriamHouse.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.GetComponent<SpriteRenderer>().enabled = false;
                    m_petyrHouse.GetComponent<SpriteRenderer>().enabled = false;

                    m_woods.GetComponent<SpriteRenderer>().enabled = false;
                    m_cafe.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.GetComponent<SpriteRenderer>().enabled = false;


                    SoundManager.m_instance.m_intro.Stop();
                    SoundManager.m_instance.m_cafe.Stop();
                    SoundManager.m_instance.m_club.Stop();
                    SoundManager.m_instance.m_town.Play();
                    SoundManager.m_instance.m_woods.Stop();

                    break;

                case 2:
                    //Cafe
                    m_dialog.gameObject.SetActive(true);
                    m_dialog.GetComponent<MainControl>().StartScene(index);

                    m_canvas.gameObject.SetActive(true);
                    m_character1.gameObject.SetActive(true);
                    m_character2.gameObject.SetActive(true);

                    m_bat.transform.position = new Vector3(0, 0, 0);
                    m_bat.gameObject.SetActive(false);

                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    SoundManager.m_instance.m_intro.Stop();
                    SoundManager.m_instance.m_cafe.Play();
                    SoundManager.m_instance.m_club.Stop();
                    SoundManager.m_instance.m_town.Stop();
                    SoundManager.m_instance.m_woods.Stop();

                    break;

                case 3:
                    //Club

                    m_canvas.gameObject.SetActive(true);
                    m_dialog.GetComponent<MainControl>().StartScene(index);

                    m_bat.transform.position = new Vector3(0, 0, 0);
                    m_bat.gameObject.SetActive(false);

                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    SoundManager.m_instance.m_intro.Stop();
                    SoundManager.m_instance.m_cafe.Stop();
                    SoundManager.m_instance.m_club.Play();
                    SoundManager.m_instance.m_town.Stop();
                    SoundManager.m_instance.m_woods.Stop();

                    break;

                case 4:
                    //Woods

                    m_canvas.gameObject.SetActive(true);
                    m_dialog.GetComponent<MainControl>().StartScene(index);

                    m_bat.transform.position = new Vector3(0, 0, 0);
                    m_bat.gameObject.SetActive(false);

                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    SoundManager.m_instance.m_intro.Stop();
                    SoundManager.m_instance.m_cafe.Stop();
                    SoundManager.m_instance.m_club.Stop();
                    SoundManager.m_instance.m_town.Stop();
                    SoundManager.m_instance.m_woods.Play();

                    break;

                case 5:

                    m_canvas.gameObject.SetActive(true);
                    m_dialog.GetComponent<MainControl>().StartScene(index);

                    m_bat.transform.position = new Vector3(0, 0, 0);
                    m_bat.gameObject.SetActive(false);

                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    SoundManager.m_instance.m_intro.Stop();
                    SoundManager.m_instance.m_cafe.Stop();
                    SoundManager.m_instance.m_club.Stop();
                    SoundManager.m_instance.m_town.Stop();
                    SoundManager.m_instance.m_woods.Stop();

                    break;

                case 6:

                    m_canvas.gameObject.SetActive(true);
                    m_dialog.GetComponent<MainControl>().StartScene(index);

                    m_bat.transform.position = new Vector3(0, 0, 0);
                    m_bat.gameObject.SetActive(false);

                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    SoundManager.m_instance.m_intro.Stop();
                    SoundManager.m_instance.m_cafe.Stop();
                    SoundManager.m_instance.m_club.Stop();
                    SoundManager.m_instance.m_town.Stop();
                    SoundManager.m_instance.m_woods.Stop();

                    break;
            }

            m_lastScene = index;
        }
    }
}
