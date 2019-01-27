using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager m_instance = null;

    public AudioSource m_intro;
    public AudioSource m_town;
    public AudioSource m_cafe;
    public AudioSource m_club;
    public AudioSource m_woods;
    public AudioSource m_elizabethHouse;
    public AudioSource m_miriamHouse;

    void Start()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }

        else if (m_instance != this)
        {
            Destroy(gameObject);
        }
    }
}

