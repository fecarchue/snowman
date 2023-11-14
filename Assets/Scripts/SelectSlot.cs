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
    private GameObject Top, Bot;
    private Image currentSlotImage, previousSlotImage, TopSnowballSlot, BotSnowballSlot;
    private Snowball TopSnowball, BotSnowball;
    private int power;

    public void Awake()
    {
        // JSON ���� ��� ����
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        // JSON ���Ͽ��� ������ �б�
        string jsonText = File.ReadAllText(jsonPath);

        // JSON ���ڿ��� ��ü�� ������ȭ
        snowballData = JsonUtility.FromJson<SnowballData>(jsonText);
    }

    public void ClickSlot() //���� Ŭ�� �Ҷ�
    {
        //���� Ŭ���� ��ư ��ȣ ��������
        slotID = getClickID();
        SlotName = "Slot (" + slotID + ")";

        //�����̰� ���� ���� Ŭ���Ҷ� ����
        if (slotID <= snowballData.snowballs.Count)
        {
            SelectSnowball = snowballData.snowballs[slotID - 1];
            ShowStatus();
            ClickNow();
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
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        string objectName = clickObject.name;
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

        currentSlotImage = clickObject.GetComponentInChildren<Image>();

        // ���� ��ư�� ������ ������� �ǵ����� (�ʱ� ���·� ����)
        if (previousSlotImage != null)
        {
            previousSlotImage.color = Color.white; // ���⼭�� �⺻ ������ ������� ����
        }

        // ���� ��ư�� ���� ����
        currentSlotImage.color = Color.red;

        // ���� ��ư�� ���� ��ư���� ������Ʈ
        previousSlotImage = currentSlotImage;
    }


    //����� Ŭ���� ��ư �� �ٲ�� TopSnowball�� �߰�
    public void GoTopsnowman()
    {
        SelectSnowball.condition = 1;
        TopSnowball = SelectSnowball;
        Top = GameObject.Find(SlotName);
        TopSnowballSlot = Top.GetComponent<Image>();
        TopSnowballSlot.color = Color.yellow;
        PowerCheck();
    }

    public void GoBotsnowman()
    {
        SelectSnowball.condition = 1;
        BotSnowball = SelectSnowball; ;
        Bot = GameObject.Find(SlotName);
        BotSnowballSlot = Bot.GetComponent<Image>();
        BotSnowballSlot.color = Color.yellow;
        PowerCheck();
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