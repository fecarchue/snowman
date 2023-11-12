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
        // JSON ���� ��� ����
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        // JSON ���Ͽ��� ������ �б�
        string jsonText = File.ReadAllText(jsonPath);

        // JSON ���ڿ��� ��ü�� ������ȭ
        snowballData = JsonUtility.FromJson<SnowballData>(jsonText);

        TopSnowman = TopSnowmanObj.GetComponent<Image>();
        BotSnowman = BotSnowmanObj.GetComponent<Image>();
    }

    public void ClickSlot() //���� Ŭ�� �Ҷ�
    {
        //���� Ŭ���� ��ư ��ȣ ��������
        slotID = getClickID();

        //�����̰� ���� ���� Ŭ���Ҷ� ����
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

    public void ShowStatus() // ����â�� Text ����ִ� �Լ�
    {

        TextMeshProUGUI WeightText = WeightObj.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI VolumeText = VolumeObj.GetComponent<TextMeshProUGUI>();
        WeightText.text = "Weight: " + snowball.weight;
        VolumeText.text = "Volume: " + snowball.volume;
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
    private bool Topisnull = true, Botisnull = true;

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

    public void ClickUseBtn()
    {

    }
    public void GoBottomSnowman()
    {
        if (isSelect)
        {
            //��������� �� ����ֱ�
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

            Botisnull = !Botisnull;   //���� ���� ������Ʈ
        }
    }

    public void GoTopSnowman()
    {
        if (isSelect)
        {
            //��������� �� ����ֱ�
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

            Topisnull = !Topisnull;   //���� ���� ������Ʈ
        }   
    }
}