﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayScene : SceneBase
{
    public Transform m_CharacterPos;
    public List<RunEnv> m_Envs;
    public RunEnv m_CurEnv;
    private CharacterCtrl m_Player;
    private bool m_IsSuccess;
    private void Start()
    {
        m_Envs = new List<RunEnv>();
        UIManager.Show("UIMain");
        //UIManager.Show("UIMain");
        //IniteGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameData.Singleton.IsPlay)
        {
            string prefabName = "UIPause";
            GameData.Singleton.IsPlay = false;
            UIManager.Show(prefabName);
        }
    }

    private void Reset()
    {
        foreach (var item in m_Envs)
        {
            Destroy(item.gameObject);
        }
        m_Envs.Clear();
        if (m_Player != null)
        {
            Destroy(m_Player.gameObject);
            m_Player = null;

        }
        m_IsSuccess = false;

        GameData.Singleton.ResetData();
    }
    public void IniteGame()
    {
        Reset();

        UIGame uiGame = UIManager.Show("UIGame").GetComponent<UIGame>();
        CloneEnv(Vector3.zero);
        m_Player = Instantiate(Resources.Load<CharacterCtrl>("Prefab/Player"));
        m_Player.transform.position = m_CharacterPos.position;
        m_Player.Logic.OnHitVWall += HitVWall;
        GameData.Singleton.IsPlay = true;
        GameData.Singleton.OnSuccessEnd += SuccessEnd;
        GameData.Singleton.OnDead += Dead;
        FollowCameraBounds2D.Singletone.player = m_Player.transform;
    }

    private void Dead()
    {
        GameData.Singleton.IsPlay = false;
        UIMsg.ShowMsg("UIGameOver", OnClickBack, OnClickRestart);
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
        m_Envs.Add(runEnv);
    }

    private void HitVWall()
    {
        if (m_IsSuccess)
        {
            UIMsg.ShowMsg("UISuccess", OnClickBack, OnClickRestart);
            m_Player.Success();
            GameData.Singleton.IsPlay = false;
        }
        else
        {
            CloneEnv(m_CurEnv.NextRunEnvPos);
            m_CurEnv.SpawnSprites();
        }
    }

    private void OnClickRestart()
    {
        IniteGame();
    }

    private void OnClickBack()
    {
        UIManager.HideAllUI();
        if (GameData.Singleton.CurLevel == -1)
        {
            UIManager.Show("UIMain");
        }
        else
        {
            UIManager.Show("UISelectLevel");
        }
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
