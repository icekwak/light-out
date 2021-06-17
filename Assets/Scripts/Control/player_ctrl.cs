using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_ctrl : MonoBehaviour
{
    private Animator player_animator;   // 플레이어 애니메이터
    private float player_speed = 2.0f;  // 플레이어 속도
    private float jump_power = 5.0f;    // 점프 힘
    private int jump_count = 0;         // 더블 점프 카운트
    private GameObject block;           // block_ctrl 접근

    void Start()
    {
        block = GameObject.Find("block");
        player_animator = GetComponent<Animator>();
    }

    // 대각선 이동
    private void DiagonalMove()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (vertical == 1.0f && horizontal == 1.0f)
            this.transform.rotation = Quaternion.Euler(0, 45, 0);
        if (vertical == 1.0f && horizontal == -1.0f)
            this.transform.rotation = Quaternion.Euler(0, -45, 0);
        if (vertical == -1.0f && horizontal == 1.0f)
            this.transform.rotation = Quaternion.Euler(0, 135, 0);
        if (vertical == -1.0f && horizontal == -1.0f)
            this.transform.rotation = Quaternion.Euler(0, -135, 0);
    }

    // 이동
    private void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.transform.Translate(Vector3.forward * player_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            this.transform.Translate(Vector3.forward * player_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
            this.transform.Translate(Vector3.forward * player_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            this.transform.Translate(Vector3.forward * player_speed * Time.deltaTime);
        }
    }

    // 점프
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ++jump_count;
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, jump_power, 0);
            player_animator.Play("Jump_start", -1, 0);
        }
    }

    void Update()
    {
        // 방향키 이동
        Move();

        // 대각선 이동
        DiagonalMove();

        // 점프
        if(jump_count < 2)
            Jump();

        // player가 떨어지면 재시작
        if (this.transform.position.y < -5.0f)
            SceneManager.LoadScene(scene_ctrl.index);
    }

    void OnCollisionEnter(Collision other)
    {
        // 블록 충돌
        if (other.gameObject.tag == "Block")
        {
            // 블록을 밟을 수 있을 때
            if(block_ctrl.block_check == true)
            {
                // 블록 값 변경
                block.GetComponent<block_ctrl>().setBlock(other.gameObject.transform.position);
                block.GetComponent<block_ctrl>().BlockCheck(other.gameObject);

                this.GetComponent<AudioSource>().Play(); // 블록 사운드
            }
            
            player_animator.Play("Jump_end", -1, 0); // 점프 종료 애니메이션
            jump_count = 0; // 점프 횟수 초기화
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // 레이저 충돌 or 폭탄 충돌 시 재시작
        if (other.gameObject.tag == "Laser" || other.gameObject.tag == "Bomb")
            SceneManager.LoadScene(scene_ctrl.index);
    }
}