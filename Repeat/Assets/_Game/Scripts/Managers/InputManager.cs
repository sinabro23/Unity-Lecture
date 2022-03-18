using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{ 
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null; // Action<Define.MouseEvent> : <>사이의 값을 Invoke의 인자로 보낼수 있음, 메시지를 받는쪽에서 사용하는 함수의 인자로 <>사이의 값을 써야함

    bool isMousePressed = false;

    public void OnUpdate()
    {
        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if(MouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                isMousePressed = true;
            }
            else
            {
                if(isMousePressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.Click);
                }

                isMousePressed = false;
            }
        }
    }
}
