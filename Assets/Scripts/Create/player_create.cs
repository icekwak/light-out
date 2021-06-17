using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_create : MonoBehaviour
{
    public GameObject player_prefab; // 플레이어
    public static bool player_check; // 플레이어 생성되었는지 체크하는 변수 (중복 생성 방지)
    RaycastHit hit;                  // 충돌 물체 정보

    void Start()
    {
        player_check = false;
    }

    void Update()
    {
        // 플레이어 생성
        if (!player_check)
        {
            // 마우스 왼쪽 버튼
            if (Input.GetMouseButtonDown(0))
            {
                // 마우스 좌표
                Ray screen_point = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 world_point = screen_point.direction;

                // 충돌한 블록 위에 플레이어 생성
                if(Physics.Raycast(this.transform.position, world_point, out hit, 15))
                {
                    Vector3 player_position = new Vector3(hit.transform.position.x, 3.0f, hit.transform.position.z);
                    Instantiate(player_prefab, player_position, player_prefab.transform.rotation);
                  
                    player_check = true;
                }
            }
        }
    }
}