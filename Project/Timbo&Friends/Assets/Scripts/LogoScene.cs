using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LogoScene : SceneBase
{
    private void Start()
    {
        StartCoroutine(WaitShowUIs());
    }

    private IEnumerator WaitShowUIs()
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
        UIManager.Show(uiPrefabName).GetComponent<UIMainLogo>().OnPressedKey += MainLogoKeyPressed;
        yield return new WaitForSeconds(Config.MAIN_LOGO_TIME);

        yield return null;
    }

    private void MainLogoKeyPressed()
    {
        SceneManager.LoadScene("Play");
    }
}
