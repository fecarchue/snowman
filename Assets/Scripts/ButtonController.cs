using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private GameObject ThisObj;
    public void Scenechange()
    {
        SceneController.Instance.SwitchScene();
    }

    public void SortButton()
    {
        DataManager.Instance.SortData();
    }

    // Start 함수에서 버튼에 자동으로 클릭 이벤트 함수를 연결
    void Awake()
    {
        ThisObj = this.gameObject;
        Button button = GetComponent<Button>();

        // 버튼이 null이 아니라면
        if (button != null)
        {
            if (ThisObj.name == "Sort")
            {
                button.onClick.AddListener(SortButton);
            }
            else// 버튼에 클릭 이벤트 함수(Scenechange) 연결
            {
                button.onClick.AddListener(Scenechange);
            }
        }
    }
}
