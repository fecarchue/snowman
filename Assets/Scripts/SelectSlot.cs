using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class SelectSlot : MonoBehaviour
{
    private ObjectInfomation objectinfo;
    public Snowball SelectSnowball;
    public GameObject WeightObj, VolumeObj, PowerObj, TopsnowmanObj,BotsnowmanObj, ObjectInfoObj;
    private Image Topsnowman, Botsnowman;
    private string jsonPath;
    private SnowballData snowballData;
    private int slotID;
    private GameObject clickObject;
    private SlotController currentSlot, previousSlot, TopSlotController, BotSlotController;
    private Snowball TopSnowball, BotSnowball;
    private int power;

    public void Awake()
    {
        objectinfo = ObjectInfoObj.GetComponent<ObjectInfomation>();
        Topsnowman = TopsnowmanObj.GetComponent<Image>();
        Botsnowman = BotsnowmanObj.GetComponent<Image>();

        SelectSnowball = null;
        
        // JSON 파일 경로 설정
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        // JSON 파일에서 데이터 읽기
        string jsonText = File.ReadAllText(jsonPath);

        // JSON 문자열을 객체로 역직렬화
        snowballData = JsonUtility.FromJson<SnowballData>(jsonText);
        LoadImage();
    }

    //처음에만 작동(이미지 불러오기)
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
                    controller.BringImage();
                    controller.exist();
                    controller.NonClicked();
                }
                else
                {
                    break; //눈덩이 없으면 종료
                }
            }
        }
    }

    public void ClickSlot() //슬롯 클릭 할때 호출
    {
        string[] objects = new string[10];
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
            objects = SelectSnowball.objects;
            objectinfo.MakeObject(objects);
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

        if (previousSlot != null)
        {
            previousSlot.NonClicked();
        }
        
        if(currentSlot.condition == "None")
        {
            currentSlot.Clicked(); 
            previousSlot = currentSlot;
        }

    }

    public void use()
    {
       if(currentSlot == TopSlotController) //선택한 슬롯이 윗 눈사람
        {//윗 눈사람 돌려놓기
            TopSlotController.condition = "None";
            TopSlotController.Clicked();
            TopSlotController = null;
            TopSnowball = null;
            previousSlot = currentSlot;

            Topsnowman.color = Color.white;

            PowerCheck();
        }

       else if(currentSlot == BotSlotController && TopSlotController != null) //선택한 슬롯이 아랫 눈사람이면서 윗 눈사람이 존재
        {//위에 있는 눈덩이를 치워야 아래눈덩이가 제거됨
            Debug.Log("위에 있는 눈덩이를 먼저 제거해주세요");
            PowerCheck();
        }

       else if (currentSlot == BotSlotController) //선택한 슬롯이 아랫 눈사람인데 윗 눈사람이 없음
        {//아래 눈사람 돌려놓기
            BotSlotController.condition = "None";
            BotSlotController.Clicked();
            BotSlotController = null;
            BotSnowball = null;
            previousSlot = currentSlot;

            Botsnowman.color = Color.white;

            PowerCheck();
        }

       else if(BotSlotController == null && TopSlotController ==null) //위 아래 눈사람 다 비어있을때
        {
            BotSlotController = currentSlot;
            BotSlotController.Used();
            BotSlotController.condition = "Bot";
            BotSnowball = SelectSnowball;
            previousSlot = null;

            Botsnowman.color = Color.gray;

            PowerCheck();
        }

       else if(BotSlotController != null && TopSlotController == null) //아래 눈사람만 있을때
        {
            TopSlotController = currentSlot;
            TopSlotController.Used();
            TopSlotController.condition = "Top";
            TopSnowball = SelectSnowball;
            previousSlot = null;

            Topsnowman.color = Color.gray;

            PowerCheck();
        }

       else // 꽉 차있을때
        {
            Debug.Log("이미 모두 선택되었습니다");
            PowerCheck();
        }
    }

    private void PowerCheck() // TopSnowball과 BotSnowball이 비어있지 않으면 전투력 측정
    {
        TextMeshProUGUI Powertext = PowerObj.GetComponent<TextMeshProUGUI>();
        if (TopSnowball != null && BotSnowball != null)
        {
            power = (TopSnowball.weight + BotSnowball.weight) * (TopSnowball.volume + BotSnowball.volume);
            Powertext.text = "Power: " + power;
        }
        else
        {
            Powertext.text = "Power: " + 0;
        }
    }

    //전투버튼 누르면 호출
    public void Fight()
    {
        if (TopSnowball != null && BotSnowball != null)
        {
            DataManager.Instance.DeleteData(TopSnowball.id, BotSnowball.id);
            Awake();
            DataManager.Instance.CurrentPower = power;
            SceneManager.LoadScene("FightScene");
        }
        else
        {
            Debug.Log("오류");
        }
    }
}