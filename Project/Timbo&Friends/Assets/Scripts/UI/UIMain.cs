using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMain : UIBase
{
    public UnityEvent OnTutorialLevel;
    private void Start()
    {
        //start
        GetComponentsInChildren<Button>()[0].onClick.AddListener(OnClickStart);
        //tutorial
        GetComponentsInChildren<Button>()[1].onClick.AddListener(OnClickTutorialLevel);
        //level selection
        GetComponentsInChildren<Button>()[2].onClick.AddListener(OnClickLevelSelection);
        //quite
        GetComponentsInChildren<Button>()[3].onClick.AddListener(OnClickQuit);
    }

    private void OnClickTutorialLevel()
    {
        gameObject.SetActive(false);
        OnTutorialLevel.Invoke();
    }

    private void OnClickLevelSelection()
    {
    }

    private void OnClickQuit()
    {
    }

    private void OnClickStart()
    {
    }
}
