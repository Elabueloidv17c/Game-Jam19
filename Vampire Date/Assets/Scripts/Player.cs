using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
            new Item ("Cupones cafe", false),
            new Item ("Cartel Disco", false),
            new Item ("Anillo", false),
            new Item ("Sombrero", false),
            new Item ("Pipa", false),
            new Item ("Bufanda", false),
            new Item ("Moño", false),
            new Item ("Collar", false),
            new Item ("Libro", false)
        };

        m_collider = GetComponent<BoxCollider2D>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();

        m_elizabethEmpathy = 0.0f;
        m_miriamEmpathy = 0.0f;

        m_currentItem = 0;
        m_isTriggering = false;

        m_speed = 4;
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
        m_currentItem = other.GetComponent<ItemIndex>().m_index;
        m_isTriggering = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_isTriggering = false;
    }

    public bool GetItemState(int index)
    {
        return m_inventory[index].m_isActive;
    }
}
