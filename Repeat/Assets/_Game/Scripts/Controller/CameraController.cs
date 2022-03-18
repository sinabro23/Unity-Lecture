using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuaterView;
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f); // 플레이어 위치로부터 얼마나 떨어져있을지
    [SerializeField]
    GameObject _player = null; // 에디터에서 드래그앤 드롭으로 넣어줬음
    

    void Start()
    {

    }

    void LateUpdate() // 플레이어가 움직이고나서 카메라가 뒤따라가게 하기 위해. Update()안쓰고 LateUpdate()썼음. 안그러면 카메라가 부들부들 떨림
    {
        if(_mode == Define.CameraMode.QuaterView)
        {
            // 캐릭터가 벽에 가려지면 카메라 당겨지게 하기위해
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Wall");
            // 캐릭터로부터 카메라 위치로 레이캐스트 함
            if (Physics.Raycast(_player.transform.position, _delta, out hit, 100.0f, mask)) // 지금은 _delta가 방향벡터임 플레이어 포지션에서 델타만큼만 더하면 목표위치로 가니깐
            {
                // 캐릭터에서 벽까지의 거리
                float distance = (hit.point - _player.transform.position).magnitude;
                transform.position = _player.transform.position + _delta.normalized * distance;
                transform.LookAt(_player.transform);
            }
            else
            {
                transform.position = _player.transform.position + _delta; // 카메라를 플레이어 위치에서 델타값만큼 뒤에 위치하게 만듦
                transform.LookAt(_player.transform); // LookAt함수는 로테이션을 그 대상을 바라보게 바꿔줌
            }
          
        }
    }

    void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}
