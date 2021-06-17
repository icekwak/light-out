using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_create : MonoBehaviour
{   
    private GameObject row_laser; // 가로 레이저
    private GameObject col_laser; // 세로 레이저
    private float timer;
    public GameObject laser;      // 레이저 프리팹

    private void LaserCreate(){

        // 레이저 생성
        row_laser = Instantiate(laser, new Vector3(0, 0.8f, LaserStartPosi(Random.Range(0, 2))), Quaternion.identity) as GameObject;
        col_laser = Instantiate(laser, new Vector3(LaserStartPosi(Random.Range(0, 2)), 0.8f, 0), Quaternion.identity) as GameObject;

        // 레이저 크기 설정
        row_laser.transform.localScale = new Vector3(20, row_laser.transform.localScale.y, row_laser.transform.localScale.z);
        col_laser.transform.localScale = new Vector3(col_laser.transform.localScale.x, col_laser.transform.localScale.y, 20);
    }

    // 레이저 생성 위치
    private int LaserStartPosi(int n)
    {
        if (n == 0)
            return -30;
        return 30;
    }

    void Update()
    {
        // 플레이가 생성 되었을 때
        if (player_create.player_check == true)
        {
            timer += Time.deltaTime;

            // 10초일 때 레이저 생성
            if ((int)timer == 10)
            {
                // 만약 이미 생성된 레이저가 있으면 삭제
                if(row_laser != null && col_laser != null)
                {
                    Destroy(row_laser.gameObject);
                    Destroy(col_laser.gameObject);
                }

                LaserCreate();
                timer = 0;
            }
        }
    }
}