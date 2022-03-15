using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
    public Action KeyAction = null; // 일종의 델레게이트

    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;

        if (KeyAction != null)
            KeyAction.Invoke();
    }
}
