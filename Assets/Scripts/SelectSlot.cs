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
        
        // JSON ���� ��� ����
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        // JSON ���Ͽ��� ������ �б�
        string jsonText = File.ReadAllText(jsonPath);

        // JSON ���ڿ��� ��ü�� ������ȭ
        snowballData = JsonUtility.FromJson<SnowballData>(jsonText);
        LoadImage();
    }

    //ó������ �۵�(�̹��� �ҷ�����)
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
                // �ش� ������ ���� ���� Ȯ�� �� ó��
                if (i < snowballData.snowballs.Count)
                {
                    controller.BringImage();
                    controller.exist();
                    controller.NonClicked();
                }
                else
                {
                    break; //������ ������ ����
                }
            }
        }
    }

    public void ClickSlot() //���� Ŭ�� �Ҷ� ȣ��
    {
        string[] objects = new string[10];
        clickObject = EventSystem.current.currentSelectedGameObject;
        
        //���� Ŭ���� ��ư ��ȣ ��������
        string numberPart = "";

        //���� ��ȣ ����
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
        // ����� ���ڸ� int�� ��ȯ �� ����
        slotID = int.Parse(numberPart);


        //�����̰� ���� ���� Ŭ���Ҷ� ����
        if (slotID + 1  <= snowballData.snowballs.Count)
        {
            //SelectSnowball �� ���� ���õ� Snowball
            SelectSnowball = snowballData.snowballs[slotID];
            objects = SelectSnowball.objects;
            objectinfo.MakeObject(objects);
            ShowStatus(); //������ ���� �����ֱ�
            ClickNow(); //Ŭ���Ҷ� ���� �̹��� �ٲٱ�
        }
        else
        {
            Debug.Log("������ ����");
        }
    }

    public void ShowStatus() // ����â�� Text ����ִ� �Լ�
    {

        TextMeshProUGUI WeightText = WeightObj.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI VolumeText = VolumeObj.GetComponent<TextMeshProUGUI>();
        WeightText.text = "Weight: " + SelectSnowball.weight;
        VolumeText.text = "Volume: " + SelectSnowball.volume;
    }

    public int getClickID() // Ŭ���� ��ư ���� �������� �Լ�
    {
        string objectName = clickObject.name; //������Ʈ �̸� ����
        // ���� �κ� ����
        string numberPart = "";

        //10�� �ڸ����� ����
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
        // ����� ���ڸ� int�� ��ȯ �� ����
        return int.Parse(numberPart);
    }

    private void ClickNow()  //Ŭ���� ������ ���� �ٲٴ� �Լ�
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
       if(currentSlot == TopSlotController) //������ ������ �� �����
        {//�� ����� ��������
            TopSlotController.condition = "None";
            TopSlotController.Clicked();
            TopSlotController = null;
            TopSnowball = null;
            previousSlot = currentSlot;

            Topsnowman.color = Color.white;

            PowerCheck();
        }

       else if(currentSlot == BotSlotController && TopSlotController != null) //������ ������ �Ʒ� ������̸鼭 �� ������� ����
        {//���� �ִ� �����̸� ġ���� �Ʒ������̰� ���ŵ�
            Debug.Log("���� �ִ� �����̸� ���� �������ּ���");
            PowerCheck();
        }

       else if (currentSlot == BotSlotController) //������ ������ �Ʒ� ������ε� �� ������� ����
        {//�Ʒ� ����� ��������
            BotSlotController.condition = "None";
            BotSlotController.Clicked();
            BotSlotController = null;
            BotSnowball = null;
            previousSlot = currentSlot;

            Botsnowman.color = Color.white;

            PowerCheck();
        }

       else if(BotSlotController == null && TopSlotController ==null) //�� �Ʒ� ����� �� ���������
        {
            BotSlotController = currentSlot;
            BotSlotController.Used();
            BotSlotController.condition = "Bot";
            BotSnowball = SelectSnowball;
            previousSlot = null;

            Botsnowman.color = Color.gray;

            PowerCheck();
        }

       else if(BotSlotController != null && TopSlotController == null) //�Ʒ� ������� ������
        {
            TopSlotController = currentSlot;
            TopSlotController.Used();
            TopSlotController.condition = "Top";
            TopSnowball = SelectSnowball;
            previousSlot = null;

            Topsnowman.color = Color.gray;

            PowerCheck();
        }

       else // �� ��������
        {
            Debug.Log("�̹� ��� ���õǾ����ϴ�");
            PowerCheck();
        }
    }

    private void PowerCheck() // TopSnowball�� BotSnowball�� ������� ������ ������ ����
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

    //������ư ������ ȣ��
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
            Debug.Log("����");
        }
    }
}