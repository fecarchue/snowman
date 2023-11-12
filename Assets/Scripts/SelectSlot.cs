using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class SelectSlot : MonoBehaviour
{
    Snowball snowball;
    public GameObject WeightObj, VolumeObj, TopSnowmanObj, BotSnowmanObj;
    private string jsonPath;
    private SnowballData snowballData;
    private int slotID, BotSnowWeight, BotSnowVolume, TopSnowWeight, TopSnowVolume;
    private Image TopSnowman, BotSnowman;
    private bool isSelect;

    public void Awake()
    {
        isSelect = false;
        // JSON 파일 경로 설정
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        // JSON 파일에서 데이터 읽기
        string jsonText = File.ReadAllText(jsonPath);

        // JSON 문자열을 객체로 역직렬화
        snowballData = JsonUtility.FromJson<SnowballData>(jsonText);

        TopSnowman = TopSnowmanObj.GetComponent<Image>();
        BotSnowman = BotSnowmanObj.GetComponent<Image>();
    }

    public void ClickSlot() //슬롯 클릭 할때
    {
        //슬롯 클릭시 버튼 번호 가져오기
        slotID = getClickID();

        //눈덩이가 없는 슬롯 클릭할때 오류
        if (slotID <= snowballData.snowballs.Count)
        {
            snowball = snowballData.snowballs[slotID - 1];
            isSelect = true;
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
        WeightText.text = "Weight: " + snowball.weight;
        VolumeText.text = "Volume: " + snowball.volume;
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
    private bool Topisnull = true, Botisnull = true;

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

    public void ClickUseBtn()
    {

    }
    public void GoBottomSnowman()
    {
        if (isSelect)
        {
            //비어있으면 값 집어넣기
            if (Botisnull)
            {
                BotSnowWeight = snowball.weight;
                BotSnowVolume = snowball.volume;
                BotSnowman.color = Color.red;
            }
            else
            {
                BotSnowVolume = 0;
                BotSnowWeight = 0;
                BotSnowman.color = Color.white;
            }

            Botisnull = !Botisnull;   //현재 상태 업데이트
        }
    }

    public void GoTopSnowman()
    {
        if (isSelect)
        {
            //비어있으면 값 집어넣기
            if (Topisnull)
            {
                TopSnowWeight = snowball.weight;
                TopSnowVolume = snowball.volume;
                TopSnowman.color = Color.red;
            }
            else
            {
                TopSnowVolume = 0;
                TopSnowWeight = 0;
                TopSnowman.color = Color.white;
            }

            Topisnull = !Topisnull;   //현재 상태 업데이트
        }   
    }
}