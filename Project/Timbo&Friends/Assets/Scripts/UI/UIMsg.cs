using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMsg : UIBase
{
    private static Action OnOkAc, OnNoAc;
    private void Start()
    {
        Buttons[0].onClick.AddListener(OnClickOk);
        Buttons[1].onClick.AddListener(OnClickNo);
    }

    private void OnClickOk()
    {
        if (OnOkAc != null)
        {
            OnOkAc();
        }
        gameObject.SetActive(false);
    }

    private void OnClickNo()
    {
        if (OnNoAc != null)
        {
            OnNoAc();
        }
        gameObject.SetActive(false);
    }

    public static void ShowMsg(string prefabName = "", Action OkAction = null, Action NoAction = null, string title = "", string content = "")
    {
        GameObject obj = UIManager.Show(prefabName == "" ? "UIMsg" : prefabName);
        Text[] uiTexts = obj.GetComponentsInChildren<Text>();
        if (uiTexts.Length == 2)
        {
            uiTexts[0].text = title;
            uiTexts[1].text = content;
        }
        OnOkAc = OkAction;
        OnNoAc = NoAction;
    }
}
