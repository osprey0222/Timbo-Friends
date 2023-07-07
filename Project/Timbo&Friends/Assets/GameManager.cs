using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Action<string, uint> OnScoreChanged;
    private bool m_GameRunning = false;
    //private bool m_GameStarted=false;
    public bool GameRunning
    {
        get
        {
            return m_GameRunning;
        }
        set
        {
            if (value)
            {
                m_GameRunning = value;
                foreach (var item in m_Characters)
                {
                    StartCoroutine(item.UpdateCoroutine());
                }
            }
        }
    }

    public UnityEngine.GameObject m_EndUI;
    private float m_Time;
    private CharacterCtrl[] m_Characters = new CharacterCtrl[2];

    public static GameManager Singleton { get; internal set; }

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    private void Start()
    {
        m_Characters = gameObject.GetComponentsInChildren<CharacterCtrl>();
        m_EndUI.GetComponentInChildren<Button>().onClick.AddListener(Retry);
    }

    public void StartGame()
    {
        int idx = 0;
        foreach (var item in m_Characters)
        {
            item.Reset();
            ++idx;
        }
        firstObjName = "";
        StartCoroutine(DelayStart());
        return;
        //test
        m_GameRunning = true;
        //
        StartCoroutine(ChangeTime());
    }

    private void Retry()
    {
        GameUI.Singleton.SetTime(true);
        m_EndUI.SetActive(false);
        StartGame();
    }

    private IEnumerator DelayStart()
    {
        int seconds = 0;
        while (seconds < 5f)
        {
            GameUI.Singleton.ShowReadyPanel(seconds);
            yield return new WaitForSecondsRealtime(1.0f);
            ++seconds;
            yield return null;
        }
        yield return null;
    }

    public void StartRunning()
    {
        StartCoroutine(ChangeTime());
        GameRunning = true;

    }
    private IEnumerator ChangeTime()
    {
        m_Time = 0f;
        while (true)
        {
            yield return new WaitForSecondsRealtime(Config.TIMEER_INTERVAL);
            m_Time += 0.01f;
            GameUI.Singleton.TimeChanged(m_Time);
            if (!m_GameRunning)
            {
                yield break;
            }
        }
    }

    string firstObjName;
    internal void FinishRunnig(bool m_IsBot, string objName)
    {
        if (firstObjName == "")
        {
            firstObjName = objName;
            GameUI.Singleton.SetTime(false, m_Time, objName);
            if (m_Time < Config.QUALIFY_TIME)
            {
                m_EndUI.GetComponentInChildren<Text>().text = objName + " Win!!";
                if (OnScoreChanged != null)
                {
                    OnScoreChanged(objName, Config.HIGH_SCORE);
                }
            }
            else
            {
                m_EndUI.GetComponentInChildren<Text>().text = " Game Over !!";
            }
        }
        else
        {
            m_GameRunning = false;
            GameUI.Singleton.SetTime(false, m_Time, objName);
            m_EndUI.SetActive(true);
        }
    }
}

