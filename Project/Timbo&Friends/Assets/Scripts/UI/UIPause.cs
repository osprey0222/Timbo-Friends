using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPause : UIBase
{
    private CustomButton m_ContiueBtn;
    private CustomButton m_LevelSelBtn;
    private CustomButton m_QuiteBtn;
    
    private void Start()
    {
        m_ContiueBtn = GetComponentsInChildren<CustomButton>()[0];
        m_LevelSelBtn = GetComponentsInChildren<CustomButton>()[1];
        m_QuiteBtn = GetComponentsInChildren<CustomButton>()[2];
        m_ContiueBtn.OnClick.AddListener(OnClickContiueBtn);
        m_LevelSelBtn.OnClick.AddListener(OnClickLevelSelBtn);
        m_QuiteBtn.OnClick.AddListener(OnClickQuiteBtn);

    }

    private void OnClickContiueBtn()
    {
        GameData.Singleton.IsPlay = true;
        UIManager.HideUI("UIPause");
    }

    private void OnClickLevelSelBtn()
    {
        UIManager.HideAllUI();
        UIManager.Show("UILevelSelect");
    }

    private void OnClickQuiteBtn()
    {
        ((PlayScene)PlayScene.Singletone).QuitGame();
    }

}

