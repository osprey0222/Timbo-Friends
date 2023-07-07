using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Text m_TimerText;
    [SerializeField]
    private Text m_P1Speed;
    [SerializeField]
    private Text m_P2Speed;
    [SerializeField]
    private Text m_P2MoveLenght;
    [SerializeField]
    private Text m_P1MoveLenght;
    [SerializeField]
    private Text m_P1Time;
    [SerializeField]
    private Text m_P2Time;
    [SerializeField]
    private Text m_HScore;
    [SerializeField]
    private Text m_P1Score;
    [SerializeField]
    private Text m_P2Score;
    [SerializeField]
    private Button m_StartBtn;
    private Dictionary<int, string> m_ReadyAlerts = new Dictionary<int, string>() { { 0, "One" }, { 1, "Two" }, { 2, "Three" }, { 3, "Go" } };
    [SerializeField]
    public UnityEngine.GameObject m_ReadyPanel = null;
    public static GameUI Singleton;


    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }

        UIManager.Init();
    }

    void Start()
    {
        m_StartBtn.onClick.AddListener(OnClickStartBtn);
        m_StartBtn.transform.parent.gameObject.SetActive(true);
        m_HScore.text = "H:" + Config.HIGH_SCORE;
        GameManager.Singleton.OnScoreChanged += ScoreChange;
        //UIManager.Show("StartUI");
    }

    private void ScoreChange(string arg1, uint arg2)
    {
        if (arg1.Contains("Player"))
        {
            m_P1Score.text = arg2+"";
        }
    }

    private void OnClickStartBtn()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        GameManager.Singleton.StartGame();
    }

    public void TimeChanged(float secondsTime)
    {
        m_TimerText.text = GetTimeFormat(secondsTime);
    }

    public void SetTime(bool isReset ,float time = 0f, string objName = "")
    {
        if (isReset)
        {
            m_P1Time.text = "";
            m_P2Time.text = "";
            return;
        }
        if (m_P1Time.text=="")
        {
            m_P1Time.text = objName+"=>" + GetTimeFormat(time);
        }
        else
        {
            m_P2Time.text = objName + "=>" + GetTimeFormat(time);
        }
    }

    public void SetScore()
    {

    }

    public string GetTimeFormat(float time)
    {
        float seconds = Mathf.FloorToInt(time % 60);
        float milliSeconds = (time % 1) * 100;
        return string.Format("{0:00}:{1:00}", seconds, milliSeconds);
    }

    public void ShowReadyPanel(int num)
    {
        if (!m_ReadyAlerts.ContainsKey(num))
        {
            m_ReadyPanel.SetActive(false);
            GameManager.Singleton.StartRunning();
            return;
        }
        m_ReadyPanel.SetActive(true);
        m_ReadyPanel.GetComponentInChildren<Text>().text = m_ReadyAlerts[num];
    }

    public void SetRunnerInfo(float pLen, float speed, bool isBot)
    {
        int len = Mathf.FloorToInt(pLen);
        if (isBot)
        {
            m_P1MoveLenght.text = len + "m";
            m_P1Speed.text = string.Format("CPU {0:0000}cm/sec", (speed) * 100);

        }
        else
        {
            m_P2MoveLenght.text = len + "m";
            m_P2Speed.text = string.Format("PLAYER {0:0000}cm/sec", (speed) * 100);
        }
    }
}
