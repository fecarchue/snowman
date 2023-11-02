using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class SelectSlot: MonoBehaviour
{
    private string jsonPath;
    private SnowballData snowballData;
    Snowball snowball;
    private int slotID;
    public GameObject WeightObj, VolumeObj;
    private Image previousButtonImage;
    public void ClickSlot()
    {
        // JSON ���� ��� ����
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        //���� Ŭ���� ��ư ��ȣ ��������
        slotID = ClickID();

        // JSON ���Ͽ��� ������ �б�
        string jsonText = File.ReadAllText(jsonPath);

        // JSON ���ڿ��� ��ü�� ������ȭ
        snowballData = JsonUtility.FromJson<SnowballData>(jsonText);
        
        //out of range ���� �ɷ�����
        if (slotID <= snowballData.snowballs.Count)
        {
            snowball = snowballData.snowballs[slotID - 1];
            ShowStatus();
            ClickCheck();
        }
        else
        {
            Debug.Log("Error");
        }

    }

    public void ShowStatus()
    {
        TextMeshProUGUI WeightText = WeightObj.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI VolumeText = VolumeObj.GetComponent<TextMeshProUGUI>();
        WeightText.text = "Weight: " + snowball.value1;
        VolumeText.text = "Volume: " + snowball.value2;
    }

    public int ClickID()
    {
        // Ŭ���� GameObject ��������
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        // GameObject �̸� ��������
        string objectName = clickObject.name;

        // ���� �κ� ����
        string numberPart = "";

        for (int i = 5; i < objectName.Length; i++)
        {
            if (char.IsDigit(objectName[i]))
            {
                numberPart += objectName[i];
            }
            else if (numberPart.Length > 0)
            {
                // ���� �κ��� �̹� ���� ���ε� �ٸ� ���ڸ� ������ �ߴ�
                break;
            }
        }

        // ����� ���ڸ� int�� ��ȯ
        return int.Parse(numberPart);
    }

    public void ClickCheck()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        Image currentButtonImage = clickObject.GetComponentInChildren<Image>();

        // ���� ��ư�� ������ ������� �ǵ����� (�ʱ� ���·� ����)
        if (previousButtonImage != null)
        {
            previousButtonImage.color = Color.white; // ���⼭�� �⺻ ������ ������� ����
        }

        // ���� ��ư�� ���� ����
        currentButtonImage.color = Color.red;

        // ���� ��ư�� ���� ��ư���� ������Ʈ
        previousButtonImage = currentButtonImage;
    }
}