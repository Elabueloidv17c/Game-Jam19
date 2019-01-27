using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        SetScene(1);

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

                    m_bat.gameObject.SetActive(false);
                    m_bat.transform.position = new Vector3(0, 0, 0);
                    
                    m_dialog.gameObject.SetActive(false);

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    break;

                case 1:
        
                    m_bat.gameObject.SetActive(true);
                    m_bat.transform.position = new Vector3(0, 0, 0);
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    m_character1.gameObject.SetActive(false);
                    m_character2.gameObject.SetActive(false);
                    m_dialog.gameObject.SetActive(false);

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    break;

                case 2:

                    m_bat.gameObject.SetActive(false);
                    m_bat.transform.position = new Vector3(0, 0, 0);

                    m_character1.gameObject.SetActive(true);
                    m_character2.gameObject.SetActive(true);
                    m_dialog.gameObject.SetActive(true);

                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    break;

                case 3:

                    m_bat.gameObject.SetActive(false);
                    m_bat.transform.position = new Vector3(0, 0, 0);

                    m_character1.gameObject.SetActive(true);
                    m_character2.gameObject.SetActive(true);
                    m_dialog.gameObject.SetActive(true);

                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    break;

                case 4:

                    m_bat.gameObject.SetActive(false);
                    m_bat.transform.position = new Vector3(0, 0, 0);

                    m_character1.gameObject.SetActive(true);
                    m_character2.gameObject.SetActive(true);
                    m_dialog.gameObject.SetActive(true);

                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    break;

                case 5:

                    m_bat.gameObject.SetActive(false);
                    m_bat.transform.position = new Vector3(0, 0, 0);

                    m_character1.gameObject.SetActive(true);
                    m_character2.gameObject.SetActive(true);
                    m_dialog.gameObject.SetActive(true);

                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    break;

                case 6:

                    m_bat.gameObject.SetActive(false);
                    m_bat.transform.position = new Vector3(0, 0, 0);

                    m_character1.gameObject.SetActive(true);
                    m_character2.gameObject.SetActive(true);
                    m_dialog.gameObject.SetActive(true);

                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    m_directions.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    break;
            }

            m_lastScene = index;
        }
    }
}
