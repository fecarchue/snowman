using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    //���� ��� �׳� ������ ��ũ��Ʈ
    private SnowballData snowballData;
    private string jsonPath;
    private int idcount = 1;

    private static SaveData instance;

    // �ٸ� ��ũ��Ʈ���� �ν��Ͻ��� ������ �� �ִ� ������Ƽ
    public static SaveData Instance
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

    public void Save(int snowweight = 1, int snowvolume = 1)
    {
        // JSON ���� ��� ����
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        idcount = snowballData.snowballs.Count + 1;

        Snowball snowball = new Snowball
        {
            id = idcount,
            weight = snowweight,
            volume = snowvolume
        };

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
    public int weight;
    public int volume;
}

[System.Serializable]
public class SnowballData
{
    public List<Snowball> snowballs = new List<Snowball>();
}