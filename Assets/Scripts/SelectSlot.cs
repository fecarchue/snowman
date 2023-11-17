using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class SelectSlot : MonoBehaviour
{
    Snowball SelectSnowball;
    public GameObject WeightObj, VolumeObj, PowerObj;
    private string jsonPath, SlotName;
    private SnowballData snowballData;
    private int slotID;
    private GameObject clickObject;
    private GameObject TopSlot, BotSlot;
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
    }

    public void ClickSlot() //���� Ŭ�� �Ҷ� ȣ��
    {
        //���� Ŭ���� ��ư ��ȣ ��������
        slotID = getClickID();
        SlotName = "Slot (" + slotID + ")";

        //�����̰� ���� ���� Ŭ���Ҷ� ����
        if (slotID <= snowballData.snowballs.Count)
        {
            //SelectSnowball �� ���� ���õ� Snowball
            SelectSnowball = snowballData.snowballs[slotID - 1];
            ShowStatus(); //������ ���� �����ֱ�
            ClickNow(); //Ŭ���Ҷ� ���� �̹��� �ٲٱ�
        }
        else
        {
            Debug.Log("Error");
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
        GameObject clickObject = EventSystem.current.currentSelectedGameObject; //���� ���� ���� ������Ʈ ��������
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
        clickObject = EventSystem.current.currentSelectedGameObject;
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


    //����� Ŭ���� ��ư �� �ٲ�� TopSnowball�� �߰�
    public void GoTopsnowman()
    {
        if (SelectSnowball != null)
        {
            //���� ���õ� ���� ������ �ǵ�����
            if(TopSlotController != null && currentSlot.isUsed != true)
            {
                TopSlotController.isUsed = false;
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

    public void GoBotsnowman()
    {
        if (SelectSnowball != null && currentSlot.isUsed != true)
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


    public void SaveSnow() //�׽�Ʈ�� �Լ� ���߿� �����!
    {
        SaveData.Instance.Save(5, 30);
    }
}