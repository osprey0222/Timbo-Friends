using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{
    public bool m_IsBot = false;
    private float m_RunSpeed = 7f;
    private float m_CurSpeed;
    private float KEY_STROKE_TIME = 0.015f;
    private float COUNT_LIMIT = 1f;
    public KeyCode m_ACtrlKey = KeyCode.Z;
    public KeyCode m_BCtrlKey = KeyCode.X;
    private bool m_IsAKeyPressed = false;
    private float KeystrokeValue = 0.015f;
    private float m_Strokes;
    private float m_CountTime;
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody;
    private Vector3 m_InitPos;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_InitPos = transform.position;
    }
    private void GetKeyEvent()
    {
        if (Input.GetKeyUp(m_ACtrlKey) && Input.GetKeyUp(m_BCtrlKey))
        {
            //Debug.Log("same press");
            return;
        }
        if (Input.GetKeyUp(m_ACtrlKey) && !m_IsAKeyPressed)
        {
            m_Strokes += KeystrokeValue;
            //Debug.Log("a pressed");
            m_IsAKeyPressed = true;
            return;
        }
        if (Input.GetKeyUp(m_BCtrlKey) && m_IsAKeyPressed)
        {
            m_Strokes += KeystrokeValue;
            m_IsAKeyPressed = false;
            //Debug.Log("b pressed");
            return;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Singleton.GameRunning)
        {
            Time.fixedDeltaTime = KEY_STROKE_TIME;
            GetKeyEvent();
        }
    }

    float m_Movement = 0f;
    public IEnumerator UpdateCoroutine()
    {
        while(true)
        {
            if (GameManager.Singleton.GameRunning)
            {
                if (transform.position.x < Config.TRACK_LENGTH)
                {
                    if (m_IsBot)
                    {
                        m_Movement = Random.Range(0.8f, 1f);
                        m_Rigidbody.velocity = new Vector2(Random.Range(0.9f, 1f) * m_RunSpeed, m_Rigidbody.velocity.y);
                    }
                    else
                    {
                        //if (m_CountTime > Time.deltaTime)
                        //{
                        if (m_Strokes != 0f)
                        {
                            //movement = movement > 0f ? movement - 0.01f : movement;
                            m_Movement = m_Strokes / Time.deltaTime;
                            m_Rigidbody.velocity = new Vector2(m_Movement * m_RunSpeed, m_Rigidbody.velocity.y);
                        }
                        else
                        {
                            m_CountTime += Time.deltaTime;
                            if (m_CountTime > COUNT_LIMIT)
                            {
                                float dist = Mathf.Clamp(m_Rigidbody.velocity.x * 0.95f, 0f, m_RunSpeed);
                                Debug.Log("    " + m_Strokes + "--------------" + dist);
                                m_Rigidbody.velocity = new Vector2(dist, m_Rigidbody.velocity.y);
                                m_CountTime = 0f;
                            }
                        }
                        m_Strokes = 0f;
                    }
                    GameUI.Singleton.SetRunnerInfo(transform.position.x, m_Rigidbody.velocity.x, m_IsBot);
                }
                else
                {
                    GameManager.Singleton.FinishRunnig(m_IsBot, gameObject.name);
                    GameUI.Singleton.SetRunnerInfo(Config.TRACK_LENGTH, m_Rigidbody.velocity.x, m_IsBot);
                    m_Rigidbody.velocity = Vector2.zero;
                    m_Movement = 0f;
                    yield break;
                }
                m_Animator.SetFloat("Speed", m_Movement);
            }
            else
            {
                m_Rigidbody.velocity = Vector2.zero;
                m_Animator.SetFloat("Speed", 0f);
            }
            yield return null;
        }
    }
    private void Update()
    {
       
    }

    public void Reset()
    {
        GameUI.Singleton.SetRunnerInfo(0f, 0f, m_IsBot);
        transform.position = m_InitPos;
        m_Movement = 0f;
        m_Strokes = 0f;
    }


}