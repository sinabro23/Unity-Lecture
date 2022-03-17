using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기능성 함수들 넣어줄 클래스
public class Util 
{
    public static GameObject FindChild(GameObject go, string name = null, bool recurisve = false)
    {
        Transform transform = FindChild<Transform>(go, name, recurisve);
        if (transform == null)
            return null;

         return transform.gameObject;
    }

    //recurisve는 하위자식만 찾을지, 자식의 자식도 찾을지
    // T는 찾고싶은 컴포넌트 넣어줄 예정 ex) Button , Text
    public static T FindChild<T>(GameObject go, string name = null, bool recurisve = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(recurisve == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T Component = transform.GetComponent<T>();
                    if (Component != null)
                        return Component;
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
 
}
