using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private GameObject ThisObj;
    // 버튼에 할당할 함수
    public void ButtonClick()
    {
        SceneController.Instance.SwitchScene();
    }

    // Start 함수에서 버튼에 자동으로 클릭 이벤트 함수를 연결
    void Awake()
    {
        ThisObj = this.gameObject;
        // 이 스크립트가 부착된 게임 오브젝트의 Button 컴포넌트 가져오기
        Button button = GetComponent<Button>();

        // 버튼이 null이 아니라면
        if (button != null && ThisObj.tag != "Slot")
        {
            // 버튼에 클릭 이벤트 함수(ButtonClick) 연결
            button.onClick.AddListener(ButtonClick);
        }
        else
        {
            Debug.LogError("이 스크립트를 부착한 게임 오브젝트에 Button 컴포넌트가 없습니다.");
        }
    }
}