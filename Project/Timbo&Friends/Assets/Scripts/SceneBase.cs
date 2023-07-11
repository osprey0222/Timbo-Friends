using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
    public static SceneBase Singletone;
    protected virtual void Awake()
    {
        UIManager.Init();
        if (Singletone==null)
        {
            Singletone = this;
        }
    }

    protected virtual void OnDestroy()
    {
        UIManager.ClearUIs();
    }
}
