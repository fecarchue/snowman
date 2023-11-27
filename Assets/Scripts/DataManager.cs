using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class DataManager : MonoBehaviour
{
    //���� ��� �׳� ������ ��ũ��Ʈ
    private SnowballData snowballData;
    private string jsonPath;
    private int idcount = 0;

    private static DataManager instance;

    // �ٸ� ��ũ��Ʈ���� �ν��Ͻ��� ������ �� �ִ� ������Ƽ
    public static DataManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��� ���� ����
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public void Awake()
    {
        // �ν��Ͻ��� �̹� �ִ� ��� �ߺ� ������ ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        LoadData();
    }

    public void SaveData()
    {
        SortData();
        int snowweight = Random.Range(0, 100);
        int snowvolume = Random.Range(0, 100);
        string[] objects = null;
        // JSON ���� ��� ����
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        idcount = snowballData.snowballs.Count;

        Snowball snowball = new Snowball
        {
            id = idcount,
            weight = snowweight,
            volume = snowvolume,
            objects = objects
        };

        // Snowball ��ü�� ����Ʈ�� �߰�
        snowballData.snowballs.Add(snowball);

        // Snowball ����Ʈ�� JSON ���ڿ��� ��ȯ
        string jsonText = JsonUtility.ToJson(snowballData, true);

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

    public void DeleteData(int TopID ,int BotID = -1)
    {
        LoadData();
        if(BotID != -1)
        {
            snowballData.snowballs.RemoveAt(TopID);
            snowballData.snowballs.RemoveAt(BotID);
        }
        else
        {
            snowballData.snowballs.RemoveAt(TopID);
        }
        string jsonText = JsonUtility.ToJson(snowballData, true);
        File.WriteAllText(jsonPath, jsonText);
    }

    //�κ��丮 ����
    public void SortData()
    {
        for (int i = 0; i < snowballData.snowballs.Count ; i++)
        {
            Snowball temp = snowballData.snowballs[i];
            if(temp.id != i)
            {
                temp.id = i;
            }
        }

        string jsonText = JsonUtility.ToJson(snowballData, true);
        File.WriteAllText(jsonPath, jsonText);
    }

    //Delete��ư �۵��Լ�
    public void CurrentDelete()
    {
        GameObject selectObj = GameObject.Find("SelectManager");
        SelectSlot selectslot = selectObj.GetComponent<SelectSlot>();
        Snowball selectsnowball = selectslot.SelectSnowball;
        DeleteData(selectsnowball.id);
    }
}

[System.Serializable]
public class Snowball
{
    public int id;
    public int weight;
    public int volume;
    public string[] objects;
}

[System.Serializable]
public class SnowballData
{
    public List<Snowball> snowballs = new List<Snowball>();
}