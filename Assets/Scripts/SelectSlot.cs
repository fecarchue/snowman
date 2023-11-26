using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class SelectSlot : MonoBehaviour
{
    public Snowball SelectSnowball;
    public GameObject WeightObj, VolumeObj, PowerObj;
    private string jsonPath;
    private SnowballData snowballData;
    private int slotID;
    private GameObject clickObject;
    private SlotController currentSlot, previousSlot, TopSlotController, BotSlotController;
    private Snowball TopSnowball, BotSnowball;
    private int power;

    public void Awake()
    {
        SelectSnowball = null;
        
        // JSON 파일 경로 설정
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        // JSON 파일에서 데이터 읽기
        string jsonText = File.ReadAllText(jsonPath);

        // JSON 문자열을 객체로 역직렬화
        snowballData = JsonUtility.FromJson<SnowballData>(jsonText);

        LoadImage();
    }

    public void LoadImage()
    {
        GameObject inventory = GameObject.Find("content");
        int childCount = inventory.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform slotTransform = inventory.transform.GetChild(i);
            SlotController controller = slotTransform.GetComponent<SlotController>();

            if (controller != null)
            {
                // 해당 슬롯의 존재 여부 확인 및 처리
                if (i < snowballData.snowballs.Count)
                {
                    controller.exist();
                    controller.NonClicked();
                }
                else
                {
                    break; // 눈덩이가 들어있는 슬롯을 찾았으면 반복문 종료
                }
            }
        }
    }

    public void ClickSlot() //슬롯 클릭 할때 호출
    {
        clickObject = EventSystem.current.currentSelectedGameObject;
        
        //슬롯 클릭시 버튼 번호 가져오기
        string numberPart = "";

        //슬롯 번호 추출
        for (int i = 6; i <=7 ; i++)
        {
            if (char.IsDigit(clickObject.name[i]))
            {
                numberPart += clickObject.name[i];
            }
            else if (numberPart.Length > 0)
            {
                break;
            }
        }
        // 추출된 숫자를 int로 변환 후 리턴
        slotID = int.Parse(numberPart);


        //눈덩이가 없는 슬롯 클릭할때 오류
        if (slotID + 1  <= snowballData.snowballs.Count)
        {
            //SelectSnowball 은 현재 선택된 Snowball
            SelectSnowball = snowballData.snowballs[slotID];
            ShowStatus(); //눈덩이 스탯 보여주기
            ClickNow(); //클릭할때 슬롯 이미지 바꾸기
        }
        else
        {
            Debug.Log("눈덩이 없음");
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
        string objectName = clickObject.name; //오브젝트 이름 저장
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

    private void ClickNow()  //클릭한 슬롯의 색깔 바꾸는 함수
    {
        currentSlot = clickObject.GetComponent<SlotController>();

        // 이전 버튼의 색상을 원래대로 되돌리기 (초기 상태로 복원)
        
        if(previousSlot == null || previousSlot == BotSlotController || previousSlot == TopSlotController)
        {
            previousSlot = null;
        }

        if (previousSlot != null)
        {
            previousSlot.NonClicked();
        }

        if(currentSlot != TopSlotController && currentSlot != BotSlotController)
        {
            currentSlot.Clicked();
        }
        
        previousSlot = currentSlot;
    }


    //선택된 눈덩이를 눈사람 윗부분으로 사용
    public void GoTopsnowman()
    {
        if (SelectSnowball != null) //선택된 눈덩이가 있을때
        {
            // 이전에 있던 눈덩이 원상복구
            if (TopSlotController != null)
            {
                TopSlotController.condition = "None";
                TopSlotController.NonClicked();
                TopSlotController = null;
            }

            TopSlotController = currentSlot;
            currentSlot.isUsed = true;

            TopSnowball = SelectSnowball;
            TopSlotController.Used();

            PowerCheck();
        }
    }

    //선택된 눈덩이를 눈사람 밑부분으로 사용
    public void GoBotsnowman()
    {
        if (SelectSnowball != null)
        {
            //전에 선택된 슬롯 프레임 되돌리기
            if(BotSlotController != null)
            {
                BotSlotController.isUsed = false;
                BotSlotController.NonClicked();
                BotSlotController = null;
            }

            BotSlotController = currentSlot;
            currentSlot.isUsed = true;
            BotSnowball = SelectSnowball;
            BotSlotController.Used();
            PowerCheck();
        }
    }

    private void PowerCheck() // TopSnowball과 BotSnowball이 비어있지 않으면 전투력 측정
    {
        if (TopSnowball != null && BotSnowball != null)
        {
            power = (TopSnowball.weight + BotSnowball.weight) * (TopSnowball.volume + BotSnowball.volume);
            TextMeshProUGUI Powertext = PowerObj.GetComponent<TextMeshProUGUI>();
            Powertext.text = "Power: " + power;
        }
    }

    //전투버튼 누르면 호출
    public void Fight()
    {
        if (TopSnowball != null && BotSnowball != null)
        {
            DataManager.Instance.DeleteData(TopSnowball.id, BotSnowball.id);
            Awake();
            if (power > 10000)
            {
                SceneController.Instance.Win();
            }
            else
            {
                SceneController.Instance.Lose();
            }
        }
        else
        {
            Debug.Log("오류");
        }
    }
}