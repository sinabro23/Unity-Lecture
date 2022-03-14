using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }    // 외부에서 매니저 가져오기위한 함수 

    InputManagers _input = new InputManagers();
    public static InputManagers Input { get { return Instance._input; } }
 
    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null) // 에디터에서 없는경우 새로 만든다
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>(); // 이 스크립트를 컴포넌트로 붙인것
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }     
    }
}
