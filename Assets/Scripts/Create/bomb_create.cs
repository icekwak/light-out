using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_create : MonoBehaviour
{
    public GameObject bomb_prefab;      // 폭탄 프리팹
    private float t;
    private int n = scene_ctrl.index;   // 씬 인덱스

    // 생성할 폭탄 위치
    private void BombCreate(int n)
    {
        int x, z;
        bool[,] bomb = new bool[n, n];

        // (n - 1) * (n - 1) 개수만큼 폭탄 생성
        for (int i = 0; i < (n - 1) * (n - 1); i++)
        {
            x = Random.Range(0, n);
            z = Random.Range(0, n);

            // 이미 생성된 위치인지 확인
            if (bomb[x, z] == true)
                i--;
            else
                bomb[x, z] = true;
        }

        // 폭탄 생성
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                if (bomb[i, j] == true)
                    Instantiate(bomb_prefab, new Vector3(i + (0.1f * i), bomb_prefab.transform.position.y, j + (0.1f * j)), bomb_prefab.transform.rotation);
            }
        }
    }

    void Update()
    {
        // 플레이어가 생성되면 폭탄 생성
        if (player_create.player_check == true)
        {
            t += Time.deltaTime;
            
            // 10초일 때 폭탄 생성
            if ((int)t == 10)
            {
                block_ctrl.block_check = false; // 폭탄이 생성될 때 블록 못 밟음
                BombCreate(n + 1);
                t = 0;
            }
        }
    }
}