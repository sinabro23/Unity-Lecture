using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{ 
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null; // Action<Define.MouseEvent> : <>������ ���� Invoke�� ���ڷ� ������ ����, �޽����� �޴��ʿ��� ����ϴ� �Լ��� ���ڷ� <>������ ���� �����

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
