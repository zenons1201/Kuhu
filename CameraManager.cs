using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float rotSpeed;
    private float mouseY;

    public float minY; // 최소 회전 범위
    public float maxY; // 최대 회전 범위

    private Player player;
    private Vector3 ori_CameraPos;

    public float down_value;
    

    void Rot()
    {
        mouseY = Mathf.Clamp(mouseY, minY, maxY); // -80 ~ 80사이를 회전시킨다.
        mouseY += Input.GetAxisRaw("Mouse Y") * rotSpeed; // Y축의 이동량을 받는다.
        this.transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-mouseY, 0, 0), Time.deltaTime / 0.1f); // 보간 회전
    }

    void Camera_Sit(bool isSit)
    {
        if (isSit)
        {
            this.transform.localPosition = Vector3.Slerp(this.transform.localPosition, new Vector3(ori_CameraPos.x, ori_CameraPos.y - down_value, ori_CameraPos.z), Time.deltaTime / 0.1f);
        }

        else
        {
            this.transform.localPosition = Vector3.Slerp(this.transform.localPosition, ori_CameraPos, Time.deltaTime / 0.1f);
        }
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        ori_CameraPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Rot();
        Camera_Sit(player.isSit);
    }
}
