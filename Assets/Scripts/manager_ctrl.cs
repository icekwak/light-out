using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager_ctrl : MonoBehaviour
{
    
    // 이 스크립트는 단순 씬을 넘기기 위한 스크립트 (관리자용)
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            scene_ctrl.index++;
            SceneManager.LoadScene(scene_ctrl.index);
        }

        if (scene_ctrl.index == 6)
            Destroy(this.gameObject);
    }
}
