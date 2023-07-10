using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Singleton { get; internal set; }

    public Action<int> OnCookieCountChanged;
    public Action OnSuccessEnd;
    public Action OnDead;
    public Action<int> OnTimeChange;

    private int m_CookieCount;
    public int CookieCount
    {
        get
        {
            return m_CookieCount;
        }
        set
        {
            m_CookieCount = value;
            if (OnCookieCountChanged != null)
            {
                OnCookieCountChanged(value);
            }
            if (m_CookieCount == CookieGoalCount)
            {
                if (OnSuccessEnd != null)
                {
                    OnSuccessEnd();
                }
                IsPlay = false;
            }
        }
    }

    private bool m_IsDead = false;
    public bool IsDead
    {
        get
        {
            return m_IsDead;
        }
        set
        {
            if (m_IsDead != value)
            {
                m_IsDead = value;
                if (m_IsDead && OnDead != null)
                {
                    OnDead();
                }
            }
        }
    }

    private bool m_GameRunning = true;
    private float m_Time;

    public bool IsPlay
    {
        get
        {
            return m_GameRunning;
        }
        set
        {
            m_GameRunning = value;
            StartCoroutine(StopWatch());
        }
    }

    private int m_CookieGoalCount = 10;
    public int CookieGoalCount { get; internal set; }

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator StopWatch()
    {
        m_Time = 0f;
        while (true)
        {
            yield return new WaitForSecondsRealtime(Config.TIMEER_INTERVAL);
            m_Time += 1f;
            if (OnTimeChange!=null)
            {
                OnTimeChange((int)m_Time);
            }
            if (!m_GameRunning)
            {
                yield break;
            }
        }
    }
}
