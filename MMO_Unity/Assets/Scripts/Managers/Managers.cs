using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }    // �ܺο��� �Ŵ��� ������������ �Լ� 

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
            if(go == null) // �����Ϳ��� ���°�� ���� �����
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>(); // �� ��ũ��Ʈ�� ������Ʈ�� ���ΰ�
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }     
    }
}
