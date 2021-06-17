using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class block_ctrl : MonoBehaviour
{
    private GameObject[,] block;    // 블록
    private int[,] block_value;     // 블록 조명 (ON / OFF) 값
    private int n, result = 0;      // n = n x n 행렬, result = 블록 조명 총 합
    public GameObject block_prefab; // 블록 프리팹
    public static bool block_check; // 블록 밟을 수 있는지 확인

    void Start()
    {
        block_check = true;
        n = scene_ctrl.index + 1;
        block = new GameObject[n, n];
        block_value = new int[n, n];

        int i = 0;
        while(i == 0)
        {
            BlockCreate(); // 블록 생성
            ++i;

            // 모두 off로 나올 경우
            if (result == 0)
            {
                BlockDestroy(); // 블록 초기화
                --i;
            }
        }
    }

    // 블록 초기화
    private void BlockDestroy()
    {
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < n; j++)
            {
                block_value[i, j] = 0;
                Destroy(block[i, j].gameObject);
            }
        }
    }

    // 블록 생성
    private void BlockCreate()
    {
        for (int x = 0; x < n; x++)
        {
            for (int z = 0; z < n; z++)
            {
                block[x, z] = Instantiate(block_prefab, new Vector3(x + (x * 0.1f), 0, z + (z * 0.1f)), block_prefab.transform.rotation) as GameObject;
                block_value[x, z] = Random.Range(0, 2); // ON == 1, OFF == 0

                BlockColorChage(x, z);
                BlockAnimation(x, z);

                result += block_value[x, z];
            }
        }
    }

    // 블록 색상 변경
    private void BlockColorChage(int x, int z)
    {
        if(block_value[x, z] == 1) // ON
            block[x, z].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
        else // OFF
            block[x, z].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
    }

    // 블록 애니메이션
    private void BlockAnimation(int x, int z)
    {
        if (block_value[x, z] == 1) // ON
            block[x, z].GetComponent<Animation>().Play("blockUp");
        else // OFF
            block[x, z].GetComponent<Animation>().Play("blockDown");
    }
    
    // 블록이 모두 OFF
    private int BlockOffCheck()
    {
        int block_value_sum = 0;

        for(int x = 0; x < n; x++)
        {
            for (int z = 0; z < n; z++)
                block_value_sum += block_value[x, z];
        }

        return block_value_sum;
    }

    // 블록 설정
    public void setBlock(Vector3 v)
    {
        int x = (int)(Mathf.Abs(v.x));
        int z = (int)(Mathf.Abs(v.z));

        if (block_value[x, z] == 0) // ON -> OFF
        {
            block_value[x, z] = 1;
            BlockColorChage(x, z);
            BlockAnimation(x, z);
        }
        else // OFF -> ON
        {
            block_value[x, z] = 0;
            BlockColorChage(x, z);
            BlockAnimation(x, z);
        }
    }

    // 인접한 블록 존재 여부 확인
    public void BlockCheck(GameObject block)
    {
        RaycastHit forward_block;
        RaycastHit back_block;
        RaycastHit right_block;
        RaycastHit left_block;
        
        if(Physics.Raycast(block.transform.position, Vector3.forward, out forward_block, 5))
            setBlock(forward_block.transform.position);
        if(Physics.Raycast(block.transform.position, Vector3.back, out back_block, 5))
            setBlock(back_block.transform.position);
        if(Physics.Raycast(block.transform.position, Vector3.right, out right_block, 5))
            setBlock(right_block.transform.position);
        if(Physics.Raycast(block.transform.position, Vector3.left, out left_block, 5))
            setBlock(left_block.transform.position);
    }

    void Update()
    {
        // 스테이지 클리어
        if(BlockOffCheck() == 0)
        {
            scene_ctrl.index++;
            SceneManager.LoadScene(scene_ctrl.index);
        }
    }
}
