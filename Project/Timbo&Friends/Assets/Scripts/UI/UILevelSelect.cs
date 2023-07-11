using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILevelSelect : UIBase
{
    public Button m_BackBtn;
    public Button m_SaveBtn;
    public GameObject m_ButtonHolder;
    private List<Button> m_levelButtons;
    public Action<int> OnClickedLevel;

    public void Start()
    {
        m_levelButtons = new List<Button>(m_ButtonHolder.GetComponentsInChildren<Button>());

        for (int i = 0; i < m_levelButtons.Count; i++)
        {
            LevelBtn btn = m_levelButtons[i].gameObject.AddComponent<LevelBtn>();
            btn.Index = i;
            btn.OnClickLevelBtn += ClickLevel;

        }
    }

    private void ClickLevel(int obj)
    {
        if (OnClickedLevel != null)
        {
            OnClickedLevel(obj);
        }
    }
}

public class LevelBtn : MonoBehaviour
{
    public int Index { get; set; }
    public Action<int> OnClickLevelBtn;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (OnClickLevelBtn != null)
            {
                OnClickLevelBtn(Index);
            }
        });
    }
}


