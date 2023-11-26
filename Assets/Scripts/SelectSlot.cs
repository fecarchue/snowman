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
        
        // JSON ���� ��� ����
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        // JSON ���Ͽ��� ������ �б�
        string jsonText = File.ReadAllText(jsonPath);

        // JSON ���ڿ��� ��ü�� ������ȭ
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
                // �ش� ������ ���� ���� Ȯ�� �� ó��
                if (i < snowballData.snowballs.Count)
                {
                    controller.exist();
                    controller.NonClicked();
                }
                else
                {
                    break; // �����̰� ����ִ� ������ ã������ �ݺ��� ����
                }
            }
        }
    }

    public void ClickSlot() //���� Ŭ�� �Ҷ� ȣ��
    {
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

        // ���� ��ư�� ������ ������� �ǵ����� (�ʱ� ���·� ����)
        
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


    //���õ� �����̸� ����� ���κ����� ���
    public void GoTopsnowman()
    {
        if (SelectSnowball != null) //���õ� �����̰� ������
        {
            // ������ �ִ� ������ ���󺹱�
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

    //���õ� �����̸� ����� �غκ����� ���
    public void GoBotsnowman()
    {
        if (SelectSnowball != null)
        {
            //���� ���õ� ���� ������ �ǵ�����
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

    private void PowerCheck() // TopSnowball�� BotSnowball�� ������� ������ ������ ����
    {
        if (TopSnowball != null && BotSnowball != null)
        {
            power = (TopSnowball.weight + BotSnowball.weight) * (TopSnowball.volume + BotSnowball.volume);
            TextMeshProUGUI Powertext = PowerObj.GetComponent<TextMeshProUGUI>();
            Powertext.text = "Power: " + power;
        }
    }

    //������ư ������ ȣ��
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
            Debug.Log("����");
        }
    }
}