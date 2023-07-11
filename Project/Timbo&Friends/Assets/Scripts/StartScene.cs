using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartScene : SceneBase
{
    private void Start()
    {
        UIBase uiMain = UIManager.Show("UIMain").GetComponent<UIBase>();
        uiMain.Buttons[0].onClick.AddListener(OnClickStart);
        uiMain.Buttons[1].onClick.AddListener(OnClickTutorial);
        uiMain.Buttons[2].onClick.AddListener(OnClickLevelSelect);
        uiMain.Buttons[3].onClick.AddListener(OnClickQuite);
    }

    private void OnClickQuite()
    {
        GameData.Singleton.QuitGame();
    }

    private void OnClickTutorial()
    {
        GameData.Singleton.CurLevel = -1;
        SceneManager.LoadScene("Play");
    }

    private void OnClickLevelSelect()
    {
        UIManager.HideCurUI();
        UILevelSelect levelSel = UIManager.Show("UILevelSelect").GetComponent<UILevelSelect>();
        levelSel.m_SaveBtn.onClick.AddListener(BackGame);
        levelSel.m_BackBtn.onClick.AddListener(BackGame);
        levelSel.OnClickedLevel += SelectLevel;
    }

    private void SelectLevel(int obj)
    {
        GameData.Singleton.CurLevel = obj;
        SceneManager.LoadScene("Play");
    }

    private void BackGame()
    {
        UIManager.HideCurUI();
        UIManager.Show("UIMain");
    }

    private void OnClickStart()
    {

    }


}
