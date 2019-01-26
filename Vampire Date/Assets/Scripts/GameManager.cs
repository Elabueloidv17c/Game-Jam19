using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{         
    public Transform m_bat;
    public Transform m_petyrHouse;
    public Transform m_miriamHouse;
    public Transform m_elizabethHouse;

    public Transform m_woods;
    public Transform m_town;
    public Transform m_cafe;
    public Transform m_disco;

    int m_lastScene;

    void Start()
    {
        m_lastScene = -1;
        SetScene(1);
    }

    void Update()
    {

    }

    void SetScene(int index)
    {
        if (m_lastScene != index)
        {
            switch (index)
            {
                case 0:

                    m_bat.gameObject.SetActive(false);
                    m_bat.transform.position = new Vector3(0, 0, 0);

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = true;

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

                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = true;

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

                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = true;

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

                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = true;

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

                    m_woods.gameObject.GetComponent<SpriteRenderer>().enabled = true;

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

                    m_miriamHouse.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    m_petyrHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    m_cafe.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_town.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    m_disco.gameObject.GetComponent<SpriteRenderer>().enabled = false;

                    break;

                case 6:

                    m_bat.gameObject.SetActive(false);
                    m_bat.transform.position = new Vector3(0, 0, 0);

                    m_elizabethHouse.gameObject.GetComponent<SpriteRenderer>().enabled = true;

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
