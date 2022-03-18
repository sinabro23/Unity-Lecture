using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // ������ RigidBody �־�� �Ѵ� (isKinematic : off)
    // ������ Collider�� �־�� �Ѵ� (isTrigger : off)
    // �������� Collider�� �־�� �Ѵ� (isTrigger : off)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision Name : { collision.gameObject.name }");
    }

    // �Ѵ� Collider�� �־�� �Ѵ�
    // ���� �ϳ��� istrigger on
    // ���� �ϳ��� rigidbody�� �־�� �Ѵ�.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger Name : { other.gameObject.name }");
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"Hit result : { hit.collider.gameObject.tag}");
            }
        }

        //if(Input.GetMouseButtonDown(0))
        // {
        //     Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //     Vector3 dir = mouseWorldPosition - Camera.main.transform.position;
        //     dir = dir.normalized;

        //     RaycastHit hit;
        //     Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);
        //     if(Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //     {
        //         Debug.Log($"raycast hit : {hit.collider.name}");
        //     }
        // }
    }

}
