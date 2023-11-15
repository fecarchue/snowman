using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    //저장 방법 그냥 실험한 스크립트
    private SnowballData snowballData;
    private string jsonPath;
    private int idcount = 1;

    private static SaveData instance;

    // 다른 스크립트에서 인스턴스에 접근할 수 있는 프로퍼티
    public static SaveData Instance
    {
        get
        {
            // 인스턴스가 없는 경우 새로 생성
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public void Awake()
    {
        // 인스턴스가 이미 있는 경우 중복 생성을 방지
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
        // JSON 파일 경로 설정
        jsonPath = Application.dataPath + "/Data/SnowballData.json";

        idcount = snowballData.snowballs.Count + 1;

        Snowball snowball = new Snowball
        {
            id = idcount,
            weight = snowweight,
            volume = snowvolume
        };

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
    public int weight;
    public int volume;
}

[System.Serializable]
public class SnowballData
{
    public List<Snowball> snowballs = new List<Snowball>();
}