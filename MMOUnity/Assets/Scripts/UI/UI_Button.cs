using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    // UnityEngine.Object : ������ �ֻ��� �θ�, ��� ���� ������ ����(�ؽ�Ʈ�� ��ư�̵�)
    // Dictionary : ��
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    enum Buttons
    {
        PointButton, // ��ư�̸�
    }

    enum Texts
    {
        PointText, // ��ư���� ���̴� �ؽ�Ʈ
        ScoreText, // ���� ǥ���� �ؽ�Ʈ
    }

    enum GameObjects
    {
        TestObject,
    }

    public GameObject obj;

    private void Start()
    {
        Bind<Button>(typeof(Buttons)); 
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Get<Text>((int)Texts.ScoreText).text = "Bind Text";
    }

    void Bind<T>(Type type) where T : UnityEngine.Object // reflection���� �̳Ѱ� �޾ƿ������� Type(reflection)
    {
        string[] names = Enum.GetNames(type); // �̳�Ÿ���� �̸��� �����ü� ���� c#
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // �޾ƿ� enum���� �� ����
        _objects.Add(typeof(T), objects);
     
        for(int i = 0; i<names.Length; i++)
        {
            if(typeof(T) == typeof(GameObject))
                 objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
        }
    }

    T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        if(_objects.TryGetValue(typeof(T), out objects)==false)
            return null;

        return objects[idx] as T;
    }

    int _score = 0;
    public void OnButtonClicked()
    {
        _score++;
    }
}
