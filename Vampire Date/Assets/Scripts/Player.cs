using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform m_sceneManager;
    public Transform m_sceneLoader;

    BoxCollider2D m_collider;
    SpriteRenderer m_sprite;
    Rigidbody2D m_rigidBody;
    Animator m_animator;

    public Item[] m_inventory; 

    float m_elizabethEmpathy;
    float m_miriamEmpathy;

    Vector2 m_direction;
    float m_speed;

    public bool m_isTriggering;
    public int m_currentItem;

    void Start()
    {
        m_inventory = new Item[]
        {
            new Item ("Cafe ticket", false),
            new Item ("Poster Disco", false),
            new Item ("Ring", false),
            new Item ("Sombrero", false),
            new Item ("Pipe", false),
            new Item ("scarf", false),
            new Item ("Bun", false),
            new Item ("Collar", false),
            new Item ("Book", false)
        };

        m_collider = GetComponent<BoxCollider2D>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();

        m_elizabethEmpathy = 0.0f;
        m_miriamEmpathy = 0.0f;

        m_currentItem = 0;
        m_isTriggering = false;

        m_speed = 5;
    }

    void Movement()
    {
        m_direction.x = Input.GetAxis("Horizontal");
        m_direction.y = Input.GetAxis("Vertical");
        m_rigidBody.velocity = new Vector2(m_direction.x * m_speed, m_direction.y * m_speed);

        if (m_direction.x < 0)
        {
            m_sprite.flipX = false;
        }

        if (m_direction.x > 0)
        {
            m_sprite.flipX = true;
        }
    }

    void Update()
    {
        Movement();

        if (m_isTriggering)
        {
            if (Input.anyKeyDown)
            {
                m_inventory[m_currentItem].m_isActive = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "left" && other.gameObject.tag != "right")
        {
            m_currentItem = other.GetComponent<ItemIndex>().m_index;
            m_isTriggering = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_isTriggering = false;

        if(other.gameObject.tag == "left")
        {
            GameManager.instance.WarptoLoadScene(2);
        }

        if (other.gameObject.tag == "right")
        {
            GameManager.instance.WarptoLoadScene(3);
        }
    }

    public bool GetItemState(int index)
    {
        return m_inventory[index].m_isActive;
    }
}
