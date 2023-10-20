using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class SnowballDataManager : MonoBehaviour
{
    public SnowballData snowballdata = new SnowballData();
    
    //임시로 저장하는 버튼
    [ContextMenu("To Json")]
    public void SaveSnowballData()
    {
        string data = JsonUtility.ToJson(snowballdata, true);
        string path = Path.Combine(Application.dataPath +"/Data/" + "snowballData.json");
        File.WriteAllText(path, data);
    }
}

//눈사람 정보 저장
public class SnowballData
{
    public int ID = 99;
    public float Weight,Mass;
}