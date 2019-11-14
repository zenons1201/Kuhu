using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    Ray ray;
    RaycastHit rayhit = new RaycastHit();
    void Interact_Object()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 카메라 중앙에서 레이를 쏜다.
        Debug.DrawRay(ray.origin, ray.direction, Color.red); // 광선을 쐈을 때 시작점부터 방향대로 빨간 색 광선을 보여준다.
        if (Physics.Raycast(ray.origin, ray.direction, out rayhit, 1 << LayerMask.NameToLayer("Object"))) // Object 레이어까지 충돌을 판별한다.
        {
            if(rayhit.transform != null) // 만약 충돌했으면 아래 코드를 실행한다.
            {
                Debug.Log("Clicked");
            }
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // 오른쪽 마우스를 클릭했을 때 함수를 실행한다.
        {
            Interact_Object();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
    