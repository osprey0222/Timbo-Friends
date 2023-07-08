using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIMainLogo : UIBase
{
    public UnityEvent OnPressedKey;
    private void Update()
    {
        if (Input.anyKey)
        {
            gameObject.SetActive(false);
            OnPressedKey.Invoke();
        }
    }
}
