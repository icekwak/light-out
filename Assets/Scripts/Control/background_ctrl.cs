using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_ctrl : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<AudioSource>().Play();    // 배경음악 재생
        DontDestroyOnLoad(this.gameObject);         // 사운드 유지
    }

    void Update()
    {
        this.transform.Rotate(0, 5 * Time.deltaTime, 0); // 배경 회전

        // 클리어 씬으로 넘어가면 삭제
        if (scene_ctrl.index == 6)
            Destroy(this.gameObject);
    }
}
