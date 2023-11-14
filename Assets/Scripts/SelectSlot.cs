using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class SelectSlot : MonoBehaviour //isSelect �ּ� ���� �ٶ�
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

        //�����̰� ���� ���� Ŭ���Ҷ� ����
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

    private GameObject clickObject;
    private Image currentSlotImage, previousSlotImage;
    private Snowball TopSnowball, BotSnowball;
    private int power;

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
    public void SaveSnow() //�׽�Ʈ�� �Լ� ���߿� �����!
    {
        SaveData.Instance.Save(5, 30);
    }
}