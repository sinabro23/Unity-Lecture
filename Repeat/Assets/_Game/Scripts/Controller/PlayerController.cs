using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �÷��̾ �̵��ϰų� �ϴ� ���� �ൿ�� �ϰ��ϴ� ��ũ��Ʈ
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f; // �̵��ϴ� ���ǵ�

    Vector3 _destPos;

    void Start()
    {
       // Managers.Input.KeyAction -= OnKeyboard; // �Ǽ��� �ٸ������� ������ �ι� ȣ�� �� �� ������ �̸� �ϳ� ���°�, �׷��� �̺�Ʈ�� �ι��� ȣ���
       // Managers.Input.KeyAction += OnKeyboard;

        Managers.Input.MouseAction -= OnMouseClicked; 
        Managers.Input.MouseAction += OnMouseClicked;
    }

    public enum PlayerState
    {
        DIE,
        MOVING,
        IDLE,
    }

    public PlayerState _state = PlayerState.IDLE;

    void UpdateDie()
    {
        // �ƹ��͵� ����
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position; // ĳ���� ��ġ���� �������� ���ϴ� ���⺤��
        if (dir.magnitude < 0.0001f) // ���� �������� �ٴٶ����� ������� (float�̶�)
        {
            _state = PlayerState.IDLE;
        }
        else
        {
            float moveDistance = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDistance;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime); // �ڿ������� ȸ���ϰ� �ϱ� ����
            // Quaternion.LookRotation(dir) : ���⺤�� �־ �׹������� �ٶ󺸴� ȸ���� ��������
        }

        // �ִϸ��̼�
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        // �ִϸ��̼�
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }


    void Update()
    {
        switch(_state)
        {
        case PlayerState.DIE:
            UpdateDie();
            break;
        case PlayerState.MOVING:
             UpdateMoving();
            break;
        case PlayerState.IDLE:
            UpdateIdle();
            break;
        }
    }


    //void OnKeyboard()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //        transform.position += Vector3.forward * _speed * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
    //        transform.position += Vector3.back * _speed * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
    //        transform.position += Vector3.left * _speed * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
    //        transform.position += Vector3.right * _speed * Time.deltaTime;
    //    }

    //    _moveToDest = false;
    //}


    private void OnMouseClicked(Define.MouseEvent mouseEvent)
    {
        if (_state == PlayerState.DIE)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        LayerMask mask = LayerMask.GetMask("Wall");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, mask)) // ����ũ���� ������ ���������� �̵��� ���� �ִٴ� ��
        {
            _destPos = hit.point;
            _state = PlayerState.MOVING;
        }
    }
}
