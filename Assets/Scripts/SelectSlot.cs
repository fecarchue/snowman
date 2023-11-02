using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class SelectSlot: MonoBehaviour
{
    private string jsonPath;
    private SnowballData snowballData;
    Snowball snowball;
    private int slotID;
    public GameObject WeightObj, VolumeObj;
    private Image previousButtonImage;
    public void ClickSlot()
    {
        // JSON 파일 경로 설정
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        //슬롯 클릭시 버튼 번호 가져오기
        slotID = ClickID();

        // JSON 파일에서 데이터 읽기
        string jsonText = File.ReadAllText(jsonPath);

        // JSON 문자열을 객체로 역직렬화
        snowballData = JsonUtility.FromJson<SnowballData>(jsonText);
        
        //out of range 오류 걸러내기
        if (slotID <= snowballData.snowballs.Count)
        {
            snowball = snowballData.snowballs[slotID - 1];
            ShowStatus();
            ClickCheck();
        }
        else
        {
            Debug.Log("Error");
        }

    }

    public void ShowStatus()
    {
        TextMeshProUGUI WeightText = WeightObj.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI VolumeText = VolumeObj.GetComponent<TextMeshProUGUI>();
        WeightText.text = "Weight: " + snowball.value1;
        VolumeText.text = "Volume: " + snowball.value2;
    }

    public int ClickID()
    {
        // 클릭된 GameObject 가져오기
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        // GameObject 이름 가져오기
        string objectName = clickObject.name;

        // 숫자 부분 추출
        string numberPart = "";

        for (int i = 5; i < objectName.Length; i++)
        {
            if (char.IsDigit(objectName[i]))
            {
                numberPart += objectName[i];
            }
            else if (numberPart.Length > 0)
            {
                // 숫자 부분이 이미 추출 중인데 다른 문자를 만나면 중단
                break;
            }
        }

        // 추출된 숫자를 int로 변환
        return int.Parse(numberPart);
    }

    public void ClickCheck()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        Image currentButtonImage = clickObject.GetComponentInChildren<Image>();

        // 이전 버튼의 색상을 원래대로 되돌리기 (초기 상태로 복원)
        if (previousButtonImage != null)
        {
            previousButtonImage.color = Color.white; // 여기서는 기본 색상을 흰색으로 가정
        }

        // 현재 버튼의 색상 변경
        currentButtonImage.color = Color.red;

        // 이전 버튼을 현재 버튼으로 업데이트
        previousButtonImage = currentButtonImage;
    }
}