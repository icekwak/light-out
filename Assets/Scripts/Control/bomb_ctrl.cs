using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_ctrl : MonoBehaviour
{
    private Color block_color;    // 블록 색상
    public AudioClip bomb_sound;  // 폭탄 사운드
    public Transform bomb_effect; // 폭탄 파티클
    RaycastHit hit;               // 충돌 물체 정보

    void Start()
    {
        // 폭탄이 생성되는 위치에 있는 블록 확인
        if(Physics.Raycast(new Vector3(this.transform.position.x, -2, this.transform.position.z), new Vector3(0,1,0), out hit, 5))
        {
            // 원래 색상 저장
            block_color = hit.transform.GetComponent<Renderer>().material.GetColor("_EmissionColor");
            
            // 블록이 있으면 색상 빨간색으로
            hit.transform.GetComponent<Renderer>().material.color = Color.red;
            hit.transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        // 블록과 충돌 시 폭탄 사라짐
        if (other.gameObject.tag == "Block")
        {
            // 폭탄 사운드
            AudioSource.PlayClipAtPoint(bomb_sound, other.gameObject.transform.position);

            Instantiate(bomb_effect, other.gameObject.transform.position, bomb_effect.transform.rotation);
         
            // 블록 원래 색상으로 바꿈
            other.GetComponent<Renderer>().material.SetColor("_EmissionColor", block_color);
            other.GetComponent<Renderer>().material.color = Color.white;

            // 다시 블록 밟을 수 있음
            block_ctrl.block_check = true;

            // 충돌 후 폭탄 삭제
            Destroy(this.gameObject);
        }
    }
}
