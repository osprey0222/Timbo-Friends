using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainLogo : UIBase
{
    private void Update()
    {
        if (Input.anyKey)
        {
            gameObject.SetActive(false);
        }
    }
}
