using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel_ctrl : MonoBehaviour
{
    private Transform panel;            // 패널 변수
    private bool panel_check = false;   // 패널 존재 여부
    public Transform stage_panel;       // 스테이지 패널

    void Start()
    {
        // 스테이지 단계 텍스트
        stage_panel.GetChild(0).GetComponent<Text>().text = "Stage" + (scene_ctrl.index - 1).ToString() + "\n 난이도 : " + DifficultyText();

        // 스테이지 패널 생성
        panel = Instantiate(stage_panel, stage_panel.position, stage_panel.rotation) as Transform;
        panel.transform.SetParent(this.transform);

        // 패널 사운드
        panel.GetComponent<AudioSource>().Play();

        // 패널 생성
        panel_check = true;
    }

    // 난이도 텍스트
    private string DifficultyText()
    {
        switch (scene_ctrl.index)
        {
            case 2:
                return "하";
            case 3:
                return "중";
            case 4:
                return "상";
            case 5:
                return "최상";
            default:
                return "";
        }
    }

    void Update()
    {
        // 패널이 존재하고 투명도가 0일 때 삭제
        if (panel_check == true && panel.GetChild(0).GetComponent<Text>().color.a == 0)
        {
            Destroy(panel.gameObject);
            panel_check = false;
        }
    }
}
