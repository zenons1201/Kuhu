using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    private CharacterController theCharCon;

    public float speed; // 플레이어의 속도 변수
    public float mouseX; // 마우스의 X축 이동 값
    public float rotSpeed; // 플레이어의 회전 속도 변수

    public bool isSit = false;

    private float origin_speed;
    private float origin_height;

    void Move()
    {
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) // 위
        {
            direction = transform.forward;
        }

        if (Input.GetKey(KeyCode.S)) // 아래
        {
            direction = -transform.forward;
        }

        if (Input.GetKey(KeyCode.A)) // 왼쪽
        {
            direction = transform.right * -1;
        }

        if (Input.GetKey(KeyCode.D)) // 오른쪽
        {
            direction = transform.right;
        }

        else
        {
            direction += Physics.gravity; // 중력을 설정해준다.
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) // 앉는다.
            isSit = true;

        if (Input.GetKeyUp(KeyCode.LeftShift)) // 선다.
            isSit = false;
        theCharCon.Move(direction * speed * Time.deltaTime);
    }

    void Rot()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * rotSpeed;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, mouseX, 0), Time.deltaTime / 0.1f); // 현재 회전 값에서 마우스 X축 이동 값만큼 0.1초에 해당하게 회전
    }

    void Sit()
    {
        if (isSit) // 앉았을 때
        {
            speed = origin_speed / 2; // 앉았을 시 속도는 느려진다.
            theCharCon.center = new Vector3(0, -0.5f, 0); // 물론 콜라이더의 기준점도 바뀐다.
            theCharCon.height = origin_height / 2; // 충돌 범위도 줄어든다.
        }

        else // 모든 걸 원위치로
        {
            speed = origin_speed;
            theCharCon.center = Vector3.zero;
            theCharCon.height = origin_height;
        }
    }

    void Start()
    {
        theCharCon = GetComponent<CharacterController>();
        origin_speed = speed;
        origin_height = theCharCon.height;
    }

    void Update()
    {
        Rot();
        Move();
        Sit();
    }
}
