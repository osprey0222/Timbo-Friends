using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayScene : SceneBase
{
    public Transform m_CharacterPos;
    public RunEnv m_CurEnv;

    private void Start()
    {
        StartGame();
    }

    private IEnumerator WaitShowUIS()
    {
        string uiPrefabName = "UIFLogo";
        UIManager.Show(uiPrefabName);
        yield return new WaitForSeconds(Config.FIRST_LOGO_TIME);
        UIManager.HideUI(uiPrefabName);

        uiPrefabName = "UISLogo";
        UIManager.Show(uiPrefabName);
        yield return new WaitForSeconds(Config.SECOND_LOGO_TIME);
        UIManager.HideUI(uiPrefabName);

        uiPrefabName = "UITLogo";
        UIManager.Show(uiPrefabName);
        yield return new WaitForSeconds(Config.SECOND_LOGO_TIME);
        UIManager.HideUI(uiPrefabName);

        uiPrefabName = "UIMainLogo";
        UIManager.Show(uiPrefabName).GetComponent<UIMainLogo>().OnPressedKey.AddListener(MainLogoKeyPressed);
        yield return new WaitForSeconds(Config.MAIN_LOGO_TIME);

        yield return null;
    }

    private void MainLogoKeyPressed()
    {
        UIManager.Show("UIMain").GetComponent<UIMain>().OnTutorialLevel.AddListener(EnterTutorialLevel);

    }

    private void EnterTutorialLevel()
    {
        StartGame();
        
    }

    private void StartGame()
    {
        UIGame uiGame = UIManager.Show("UIGame").GetComponent<UIGame>();
        CloneEnv(Vector3.zero);
        CharacterCtrl player = Instantiate<CharacterCtrl>(Resources.Load<CharacterCtrl>("Prefab/Player"));
        player.transform.position = m_CharacterPos.position;
        player.Logic.OnHitVWall += HitVWall;
        GameData.Singleton.IsPlay = true;
        GameData.Singleton.OnSuccessEnd += SuccessEnd;
        FollowCameraBounds2D.Singletone.player = player.transform;
    }

    private void SuccessEnd()
    {
        UIManager.Show("UISuccess");
    }

    private void CloneEnv(Vector3 position)
    {
        RunEnv runEnv = Instantiate<RunEnv>(Resources.Load<RunEnv>("Prefab/RunEnv"));
        runEnv.transform.position = position;

        m_CurEnv = runEnv;
    }
   
    private void HitVWall()
    {
        CloneEnv(m_CurEnv.NextRunEnvPos);
        m_CurEnv.SpawnSprites();
    }
}
