using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_ctrl : MonoBehaviour
{
    public static int index = 0; // 씬 인덱스

    // 다음 스테이지로 이동
    public void OnClickNext()
    {
        index++;
        SceneManager.LoadScene(index);
    }

    // 메인 화면으로
    public void OnClickHome()
    {
        index = 0;
        SceneManager.LoadScene(index);
    }
}