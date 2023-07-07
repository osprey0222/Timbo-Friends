using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
    protected virtual void Awake()
    {
        UIManager.Init();
    }
}
