using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    private SnowballData snowballData;
    private string jsonPath;
    private int idcount = 1;

    private void Awake()
    {
        LoadData();
    }

    public void Save()
    {
        // JSON ���� ��� ����
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        Snowball snowball = new Snowball
        {
            id = idcount,
            value1 = Random.Range(1, 100),
            value2 = Random.Range(1, 10)
        };
        idcount++;

        // Snowball ��ü�� ����Ʈ�� �߰�
        snowballData.snowballs.Add(snowball);

        // Snowball ����Ʈ�� JSON ���ڿ��� ��ȯ
        string jsonText = JsonUtility.ToJson(snowballData,true);

        // JSON ���ڿ��� ���Ͽ� ����
        File.WriteAllText(jsonPath, jsonText);

        // ���� ���� Ȯ��
        Debug.Log("Snowball �����Ͱ� JSON ���Ͽ� ����Ǿ����ϴ�.");
    }

    public void LoadData()
    {
        // JSON ���� ��� ����
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        if (File.Exists(jsonPath))
        {
            // JSON ���Ͽ��� ������ �б�
            string jsonText = File.ReadAllText(jsonPath);

            // JSON ���ڿ��� ��ü�� ������ȭ
            snowballData = JsonUtility.FromJson<SnowballData>(jsonText);
        }
        else
        {
            // ������ ���� ��� �ʱ�ȭ
            snowballData = new SnowballData();
        }
    }
}

[System.Serializable]
public class Snowball
{
    public int id;
    public int value1;
    public int value2;
}

[System.Serializable]
public class SnowballData
{
    public List<Snowball> snowballs = new List<Snowball>();
}