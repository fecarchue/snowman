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
        // JSON 파일 경로 설정
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        Snowball snowball = new Snowball
        {
            id = idcount,
            value1 = Random.Range(1, 100),
            value2 = Random.Range(1, 10)
        };
        idcount++;

        // Snowball 객체를 리스트에 추가
        snowballData.snowballs.Add(snowball);

        // Snowball 리스트를 JSON 문자열로 변환
        string jsonText = JsonUtility.ToJson(snowballData,true);

        // JSON 문자열을 파일에 쓰기
        File.WriteAllText(jsonPath, jsonText);

        // 파일 쓰기 확인
        Debug.Log("Snowball 데이터가 JSON 파일에 저장되었습니다.");
    }

    public void LoadData()
    {
        // JSON 파일 경로 설정
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        if (File.Exists(jsonPath))
        {
            // JSON 파일에서 데이터 읽기
            string jsonText = File.ReadAllText(jsonPath);

            // JSON 문자열을 객체로 역직렬화
            snowballData = JsonUtility.FromJson<SnowballData>(jsonText);
        }
        else
        {
            // 파일이 없을 경우 초기화
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