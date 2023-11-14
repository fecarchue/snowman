using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class SelectSlot : MonoBehaviour //isSelect 주석 해제 바람
{
    Snowball SelectSnowball;
    public GameObject WeightObj, VolumeObj, TopSnowmanObj, BotSnowmanObj, PowerObj;
    private string jsonPath;
    private SnowballData snowballData;
    private int slotID;
    //private bool isSelect;

    public void Awake()
    {
        //isSelect = false;
        // JSON 파일 경로 설정
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        // JSON 파일에서 데이터 읽기
        string jsonText = File.ReadAllText(jsonPath);

        // JSON 문자열을 객체로 역직렬화
        snowballData = JsonUtility.FromJson<SnowballData>(jsonText);
    }

    public void ClickSlot() //슬롯 클릭 할때
    {
        //슬롯 클릭시 버튼 번호 가져오기
        slotID = getClickID();

        //눈덩이가 없는 슬롯 클릭할때 오류
        if (slotID <= snowballData.snowballs.Count)
        {
            SelectSnowball = snowballData.snowballs[slotID - 1];
            //isSelect = true;
            ShowStatus();
            ClickNow();
        }
        else
        {
            Debug.Log("Error");
        }
    }

    public void ShowStatus() // 상태창에 Text 띄워주는 함수
    {

        TextMeshProUGUI WeightText = WeightObj.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI VolumeText = VolumeObj.GetComponent<TextMeshProUGUI>();
        WeightText.text = "Weight: " + SelectSnowball.weight;
        VolumeText.text = "Volume: " + SelectSnowball.volume;
    }

    public int getClickID() // 클릭된 버튼 숫자 가져오는 함수
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        string objectName = clickObject.name;
        // 숫자 부분 추출
        string numberPart = "";

        //10의 자리까지 추출
        for (int i = 5; i < objectName.Length; i++)
        {
            if (char.IsDigit(objectName[i]))
            {
                numberPart += objectName[i];
            }
            else if (numberPart.Length > 0)
            {
                break;
            }
        }
        // 추출된 숫자를 int로 변환 후 리턴
        return int.Parse(numberPart);
    }

    private GameObject clickObject;
    private Image currentSlotImage, previousSlotImage;
    private Snowball TopSnowball, BotSnowball;
    private int power;

    private void ClickNow()  //클릭한 슬롯의 색깔 바꾸는 함수
    {
        clickObject = EventSystem.current.currentSelectedGameObject;

        currentSlotImage = clickObject.GetComponentInChildren<Image>();

        // 이전 버튼의 색상을 원래대로 되돌리기 (초기 상태로 복원)
        if (previousSlotImage != null)
        {
            previousSlotImage.color = Color.white; // 여기서는 기본 색상을 흰색으로 가정
        }

        // 현재 버튼의 색상 변경
        currentSlotImage.color = Color.red;

        // 이전 버튼을 현재 버튼으로 업데이트
        previousSlotImage = currentSlotImage;
    }

    public void GoTopsnowman()
    {
        SelectSnowball.condition = 1;
        TopSnowball = SelectSnowball;
        PowerCheck();
    }

    public void GoBotsnowman()
    {
        SelectSnowball.condition = 1;
        BotSnowball = SelectSnowball;
        PowerCheck();
    }

    private void PowerCheck()
    {
        if (TopSnowball != null && BotSnowball != null)
        {
            power = (TopSnowball.weight + BotSnowball.weight) * (TopSnowball.volume + BotSnowball.volume);
            TextMeshProUGUI Powertext = PowerObj.GetComponent<TextMeshProUGUI>();
            Powertext.text = "Power: " + power;
        }
    }    

    public void Fight()
    {
        if (power > 10000)
        {
            SceneController.Instance.Win();
        }
        else
        {
            SceneController.Instance.Lose();
        }
    }    
    public void SaveSnow() //테스트용 함수 나중에 지울것!
    {
        SaveData.Instance.Save(5, 30);
    }
}