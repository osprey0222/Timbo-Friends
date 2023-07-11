using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIMainLogo : UIBase
{
    public Action OnPressedKey;
    private void Update()
    {
        if (Input.anyKey)
        {
            if (OnPressedKey != null)
            {
                OnPressedKey.Invoke();
            }
            gameObject.SetActive(false);
        }
    }
}
