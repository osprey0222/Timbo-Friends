using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : BaseCharacter
{
    [Header("Character Details")]
    [Space]
    [SerializeField]
    protected float m_MaxRunSpeed = 8f;
    [SerializeField]
    protected float m_RunSmoothTime = 5f;
    [SerializeField]
    protected float m_RunSpeed = 5f;
    [SerializeField]
    protected float m_JumpStrength = 5f;
    [SerializeField]
    private LayerMask jumpableGround;

    private float m_CurrentSmoothVelocity;
    private Vector2 m_Speed;
    private float m_CurrentRunSpeed;
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private BoxCollider2D m_Collider2D;
    private int m_JumpCounter = 0;
    private CharacterLogic m_Logic;
    public CharacterLogic Logic
    {
        get
        {
            return m_Logic;
        }
    }

    protected override void Awake()
    {
        m_Rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        m_Animator = GetComponentInChildren<Animator>();
        m_Collider2D = GetComponentInChildren<BoxCollider2D>();
        m_Logic = gameObject.AddComponent<CharacterLogic>();
    }

    void Update()
    {
        if (GameData.Singleton.IsPlay)
        {
            // Speed
            m_Speed = new Vector2(Mathf.Abs(m_Rigidbody2D.velocity.x), Mathf.Abs(m_Rigidbody2D.velocity.y));

            // Speed Calculations
            m_CurrentRunSpeed = m_RunSpeed;
            if (m_Speed.x >= m_RunSpeed)
            {
                m_CurrentRunSpeed = Mathf.SmoothDamp(m_Speed.x, m_MaxRunSpeed, ref m_CurrentSmoothVelocity, m_RunSmoothTime);
            }
            // Input Processing
            Move(Input.GetAxisRaw("Horizontal"));

            if (Input.GetButtonDown("Jump") && m_JumpCounter < 1 && !GameData.Singleton.IsSuccess)
            {
                Jump();
                ++m_JumpCounter;
            }
            if (IsGrounded())
            {
                m_JumpCounter = 0;
            }
        }
        else
        {
            m_Rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void Jump()
    {
        Vector2 velocity = m_Rigidbody2D.velocity;
        velocity.y = m_JumpStrength;
        m_Rigidbody2D.velocity = velocity;
    }

    private void Move(float horizontalAxis)
    {
        if (horizontalAxis < 0 && transform.position.x < (GameData.Singleton.PlayerSpawnPos.x-1f))
        {
            m_Rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(horizontalAxis * m_CurrentRunSpeed, m_Rigidbody2D.velocity.y);
            if (horizontalAxis > 0f)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(horizontalAxis);
                transform.localScale = scale;
            }
            else if (horizontalAxis < 0f)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(horizontalAxis);
                transform.localScale = scale;
            }
        }

        UpdateAnimationState(horizontalAxis);
    }

    void UpdateAnimationState(float dirX)
    {
        EMovementState state;

        if (dirX == 0f)
        {
            state = EMovementState.Idle;
        }
        else
        {
            state = EMovementState.Run;
        }

        if (m_Rigidbody2D.velocity.y > .1f)
        {
            state = EMovementState.Jump;
        }
        else if (m_Rigidbody2D.velocity.y < -0.1f)
        {
            state = EMovementState.Fall;
        }

        m_Animator.Play("" + state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(m_Collider2D.bounds.center, m_Collider2D.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    public void Dead()
    {
        m_Animator.Play("" + EMovementState.Death);
    }

    public void Success()
    {
        m_Animator.Play("" + EMovementState.Yeah);
    }
}