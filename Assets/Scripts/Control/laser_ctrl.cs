using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_ctrl : MonoBehaviour
{
    private float laser_speed = 8.0f;   // 레이저 속도
    private Vector3 direction;          // 레이저 방향

    void Start()
    {
        this.GetComponent<AudioSource>().Play(); // 레이저 사운드

        // 레이저 방향 정하기
        if (this.transform.position.x == 0)
        {
            if (this.transform.position.z > 0)
                direction = Vector3.back;
            else
                direction = Vector3.forward;
        }
        else
        {
            if (this.transform.position.x > 0)
                direction = Vector3.left;
            else
                direction = Vector3.right;
        }
    }

    void Update()
    {
        this.transform.Translate(direction * laser_speed * Time.deltaTime); // 레이저 이동
    }
}
