using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : UIBase
{
    public Text m_CookieGoalCount;
    public Text m_CookieCount;
    public Text m_Time;
    private void Start()
    {
        GameData.Singleton.OnCookieCountChanged += SetCookieCount;
        GameData.Singleton.OnTimeChange += SetTime;

        m_CookieGoalCount.text = GameData.Singleton.CookieGoalCount + "";
    }

    public void SetCookieCount(int count)
    {
        m_CookieCount.text = count + "";
    }

    public void SetTime(int time)
    {
        if (m_Time != null)
        {
            m_Time.text = time + "";
        }
    }
}
