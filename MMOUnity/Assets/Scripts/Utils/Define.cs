using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum MouseEvent
    {
        Press, // 프레스는 누르고 있어도 인식
        Click, // 클릭은 뗐을때만 인식
    }

    public enum CameraMode
    {
        QuaterView,
    }
}
