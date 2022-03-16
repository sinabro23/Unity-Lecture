using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;

    Vector3 _destPos; // ���콺 ���� ����ĳ������ ������

    void Start()
    { 
        Managers.Input.MouseAction -= OnMouseDownClicked;
        Managers.Input.MouseAction += OnMouseDownClicked;
    }


    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;

    void OnRunEvent()
    {
        Debug.Log("�ѹ��ѹ�");
    }

    void UpdateDie()
    {

    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f) // ������ �ߴٸ�
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDistance = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDistance;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);// �÷��̾ �̵��ϸ鼭 ������ �������� �Ĵٺ��� �ϱ� ����
        }

        //Animation
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        //Animation
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }

    void Update()
    {
        switch(_state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Die:
                UpdateDie();
                break;
        }
    }


    void OnMouseDownClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        }
    }
}
