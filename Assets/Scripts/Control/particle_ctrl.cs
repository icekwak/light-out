using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_ctrl : MonoBehaviour
{
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // 3초가 넘어가면 파티클 삭제
        if (timer > 3)
            Destroy(this.gameObject);
    }
}
