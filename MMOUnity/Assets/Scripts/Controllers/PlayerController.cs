using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;

    bool _moveToDest = false; // ���콺�� Ŭ���� �̵����� �Ұ�
    Vector3 _destPos; // ���콺 ���� ����ĳ������ ������

    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard; // ������ ������ �������� ���� ��Ҹ��� �ϰ�
        Managers.Input.KeyAction += OnKeyboard; // ���� ��û

        Managers.Input.MouseAction -= OnMouseDownClicked;
        Managers.Input.MouseAction += OnMouseDownClicked;
    }

    void Update()
    {
        if(_moveToDest) // ���콺 Ŭ���ؼ� �������� ���� ��Ȳ
        {
            Vector3 dir = _destPos - transform.position;
            if(dir.magnitude < 0.0001f) // ������ �ߴٸ�
            {
                _moveToDest = false;
            }
            else
            {
                float moveDistance = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
                transform.position +=  dir.normalized * moveDistance;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);// �÷��̾ �̵��ϸ鼭 ������ �������� �Ĵٺ��� �ϱ� ����
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