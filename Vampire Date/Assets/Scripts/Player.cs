using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    BoxCollider2D m_collider;
    SpriteRenderer m_sprite;
    Rigidbody2D m_rigidBody;
    Animator m_animator;

    float m_elizabethEmpathy;
    float m_miriamEmpathy;

    Vector2 m_direction;
    float m_speed;

    void Start()
    {
        m_collider = GetComponent<BoxCollider2D>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();

        m_elizabethEmpathy = 0.0f;
        m_miriamEmpathy = 0.0f;

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
    }
}
