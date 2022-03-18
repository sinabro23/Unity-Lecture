using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuaterView;
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f); // �÷��̾� ��ġ�κ��� �󸶳� ������������
    [SerializeField]
    GameObject _player = null; // �����Ϳ��� �巡�׾� ������� �־�����
    

    void Start()
    {

    }

    void LateUpdate() // �÷��̾ �����̰��� ī�޶� �ڵ��󰡰� �ϱ� ����. Update()�Ⱦ��� LateUpdate()����. �ȱ׷��� ī�޶� �ε�ε� ����
    {
        if(_mode == Define.CameraMode.QuaterView)
        {
            // ĳ���Ͱ� ���� �������� ī�޶� ������� �ϱ�����
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Wall");
            // ĳ���ͷκ��� ī�޶� ��ġ�� ����ĳ��Ʈ ��
            if (Physics.Raycast(_player.transform.position, _delta, out hit, 100.0f, mask)) // ������ _delta�� ���⺤���� �÷��̾� �����ǿ��� ��Ÿ��ŭ�� ���ϸ� ��ǥ��ġ�� ���ϱ�
            {
                // ĳ���Ϳ��� �������� �Ÿ�
                float distance = (hit.point - _player.transform.position).magnitude;
                transform.position = _player.transform.position + _delta.normalized * distance;
                transform.LookAt(_player.transform);
            }
            else
            {
                transform.position = _player.transform.position + _delta; // ī�޶� �÷��̾� ��ġ���� ��Ÿ����ŭ �ڿ� ��ġ�ϰ� ����
                transform.LookAt(_player.transform); // LookAt�Լ��� �����̼��� �� ����� �ٶ󺸰� �ٲ���
            }
          
        }
    }

    void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}
