using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    public List<Button> Buttons { get; internal set; }

    protected virtual void Awake()
    {
        Buttons = new List<Button>(GetComponentsInChildren<Button>());
    }
}
