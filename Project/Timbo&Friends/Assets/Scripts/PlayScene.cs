using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayScene : SceneBase
{
    void Start()
    {
        //StartCoroutine(WaitShowUIS());
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
        
    }
}
