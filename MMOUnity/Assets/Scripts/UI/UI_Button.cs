using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    // UnityEngine.Object : 모든것의 최상위 부모, 모든 값을 받을수 있음(텍스트든 버튼이든)
    // Dictionary : 맵
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    enum Buttons
    {
        PointButton, // 버튼이름
    }

    enum Texts
    {
        PointText, // 버튼위에 쓰이는 텍스트
        ScoreText, // 점수 표시할 텍스트
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

    void Bind<T>(Type type) where T : UnityEngine.Object // reflection으로 이넘값 받아오기위해 Type(reflection)
    {
        string[] names = Enum.GetNames(type); // 이넘타입의 이름들 가져올수 있음 c#
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // 받아온 enum안의 값 개수
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
