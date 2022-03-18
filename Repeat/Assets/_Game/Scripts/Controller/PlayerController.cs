using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 플레이어를 이동하거나 하는 등의 행동을 하게하는 스크립트
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f; // 이동하는 스피드

    Vector3 _destPos;

    void Start()
    {
       // Managers.Input.KeyAction -= OnKeyboard; // 실수라도 다른곳에서 넣으면 두번 호출 될 수 있으니 미리 하나 빼는것, 그러면 이벤트가 두번씩 호출됨
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
        // 아무것도 못함
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position; // 캐릭터 위치에서 목적지로 향하는 방향벡터
        if (dir.magnitude < 0.0001f) // 거의 목적지에 다다랐으면 멈춰야함 (float이라)
        {
            _state = PlayerState.IDLE;
        }
        else
        {
            float moveDistance = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDistance;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime); // 자연스럽게 회전하게 하기 위해
            // Quaternion.LookRotation(dir) : 방향벡터 넣어서 그방향으로 바라보는 회전값 가져오기
        }

        // 애니메이션
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        // 애니메이션
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
        if (Physics.Raycast(ray, out hit, 100.0f, mask)) // 마스크까지 땅으로 설정했으니 이동할 곳이 있다는 뜻
        {
            _destPos = hit.point;
            _state = PlayerState.MOVING;
        }
    }
}
