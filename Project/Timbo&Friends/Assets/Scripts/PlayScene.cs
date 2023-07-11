using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayScene : SceneBase
{
    public Transform m_CharacterPos;
    public RunEnv m_CurEnv;
    private CharacterCtrl m_Player;
    private bool m_IsSuccess;
    private void Start()
    {
        IniteGame();
    }

    private void Reset()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string prefabName = "UIPause";
            if (GameData.Singleton.IsPlay)
            {
                GameData.Singleton.IsPlay = false;
                UIManager.Show(prefabName);

            }
            else
            {
                GameData.Singleton.IsPlay = true;
                UIManager.HideUI(prefabName);
            }
        }
    }


    internal void Resume()
    {
        GameData.Singleton.IsPlay = true;
        UIManager.HideUI("UIPause");
    }

    internal void LevelSelect()
    {

    }


    private void EnterTutorialLevel()
    {
        IniteGame();
    }

    private void IniteGame()
    {
        UIGame uiGame = UIManager.Show("UIGame").GetComponent<UIGame>();
        CloneEnv(Vector3.zero);
        m_Player = Instantiate<CharacterCtrl>(Resources.Load<CharacterCtrl>("Prefab/Player"));
        m_Player.transform.position = m_CharacterPos.position;
        m_Player.Logic.OnHitVWall += HitVWall;
        GameData.Singleton.IsPlay = true;
        GameData.Singleton.OnSuccessEnd += SuccessEnd;
        GameData.Singleton.OnDead += Dead;
        FollowCameraBounds2D.Singletone.player = m_Player.transform;
    }

    private void Dead()
    {
        UIManager.Show("UIGameOver");
        m_Player.Dead();
    }
    
    private void SuccessEnd()
    {
        m_CurEnv.HideToys();
        CloneEnv(m_CurEnv.NextRunEnvPos);
        m_CurEnv.ShowSuccess();
        CloneEnv(m_CurEnv.NextRunEnvPos);
        m_IsSuccess = true;
        //UIManager.Show("UISuccess");
    }

    private void CloneEnv(Vector3 position)
    {
        RunEnv runEnv = Instantiate<RunEnv>(Resources.Load<RunEnv>("Prefab/RunEnv"));
        runEnv.transform.position = position;
        runEnv.SetParams(3);
        m_CurEnv = runEnv;
    }

    private void HitVWall()
    {
        if (m_IsSuccess)
        {
            UIBase uiSuccess =  UIManager.Show("UISuccess").GetComponent<UIBase>();
            uiSuccess.Buttons[0].onClick.AddListener(OnClickSucUIBackBtn);
            uiSuccess.Buttons[1].onClick.AddListener(OnClickSucUIRestartBtn);
            m_Player.Success();
            GameData.Singleton.IsPlay = false;
        }
        else
        {
            CloneEnv(m_CurEnv.NextRunEnvPos);
            m_CurEnv.SpawnSprites();
        }
    }

    private void OnClickSucUIRestartBtn()
    {
    }

    private void OnClickSucUIBackBtn()
    {

    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
