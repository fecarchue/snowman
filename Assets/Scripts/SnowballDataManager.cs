using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class SnowballDataManager : MonoBehaviour
{
    public SnowballData snowballdata = new SnowballData();
    
    //�ӽ÷� �����ϴ� ��ư
    [ContextMenu("To Json")]
    public void SaveSnowballData()
    {
        string data = JsonUtility.ToJson(snowballdata, true);
        string path = Path.Combine(Application.dataPath +"/Data/" + "snowballData.json");
        File.WriteAllText(path, data);
    }
}

//����� ���� ����
public class SnowballData
{
    public int ID = 99;
    public float Weight,Mass;
}