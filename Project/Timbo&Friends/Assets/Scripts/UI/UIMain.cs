using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMain : UIBase
{
    private void Start()
    {
        Buttons[0].onClick.AddListener(OnClickStart);
        Buttons[1].onClick.AddListener(OnClickTutorial);
        Buttons[2].onClick.AddListener(OnClickLevelSelect);
        Buttons[3].onClick.AddListener(OnClickQuite);
    }

    private void OnClickStart()
    {
    }

    private void OnClickQuite()
    {
        GameData.Singleton.QuitGame();
    }

    private void OnClickTutorial()
    {
        GameData.Singleton.CurLevel = -1;
        ((PlayScene.Singletone) as PlayScene).IniteGame();
    }

    public void OnClickLevelSelect()
    {
        UIManager.HideCurUI();
        UIManager.Show("UILevelSelect").GetComponent<UILevelSelect>();

    }
}
