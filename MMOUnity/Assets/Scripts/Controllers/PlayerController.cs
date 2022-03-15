using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;

    bool _moveToDest = false; // 마우스로 클릭시 이동할지 불값
    Vector3 _destPos; // 마우스 찍은 레이캐스팅한 목적지

    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard; // 이전에 구독이 돼있으면 구독 취소먼저 하고
        Managers.Input.KeyAction += OnKeyboard; // 구독 신청

        Managers.Input.MouseAction -= OnMouseDownClicked;
        Managers.Input.MouseAction += OnMouseDownClicked;
    }

    void Update()
    {
        if(_moveToDest) // 마우스 클릭해서 목적지가 생긴 상황
        {
            Vector3 dir = _destPos - transform.position;
            if(dir.magnitude < 0.0001f) // 도착을 했다면
            {
                _moveToDest = false;
            }
            else
            {
                float moveDistance = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
                transform.position +=  dir.normalized * moveDistance;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);// 플레이어가 이동하면서 목적지 방향으로 쳐다보게 하기 위해
            }
        }
    }

    void OnKeyboard()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * _speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * _speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * _speed * Time.deltaTime;
        }

        _moveToDest = false;
    }

    void OnMouseDownClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _moveToDest = true;
            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        }
    }
}
