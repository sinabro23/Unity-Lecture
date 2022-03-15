using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollsion : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collsion @ { collision.gameObject.name} !");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collsion @ { other.gameObject.name} !");
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Local <-> World <-> Viewport <-> Screen
        // Debug.Log(Input.mousePosition); // Screen
        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); // Viewport 0~1 / 0~1 스크린과 비슷
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");
            //int mask = (1 << 8) | (1 << 9);


            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
        }


        //if (Input.GetMouseButtonDown(0))
        //{
        //    // 메인카메라 태그 붙인애가 메인
        //    // 마우스의 월드 포지션
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    Vector3 dir = mousePos - Camera.main.transform.position;
        //    dir = dir.normalized;

        //    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        //    }
        //}
    }
}
