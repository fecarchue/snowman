using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private List<int> objects; //흡수한 오브젝트들 ID 리스트

    //플레이어와 모든 플레이어의 수치를 변경하는 오브젝트들은 다음과 같은 데이터를 가진다:
    //[0]==MaxHealth, [1]==Health, [2]==Volume, [3]==Weight 
    public float[] playerData = { 1, 1, 1, 1 };

    public float[] groundData = { 0, -4, 0, 0 };
    public float[] snowgroundData = { 2, 3, 1, 1 };

    public float[,] stoneData = { { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 } };
    public float[,] treeData = { { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 } };

    public float growthRate = 1f; // 커지는 비율
    public float cameraScale; // degul12 기준 눈크기; 카메라, 트레일용
    public float snowScale; // 그래픽 변환 고려한 실제 Scale

    public int playerSize = 0;
    public float[] sizeCut = { 12, 16, 20, 26, 36, 48, 64, 86, 116, 202, 9999};

    public UI ui;

    private int ID;
    private int typeID;
    private int imageID;
    private int posID;

    void Start()
    {
        objects = new List<int>();
    }

    void Update()
    {

        if (snowScale > sizeCut[playerSize + 1] / sizeCut[playerSize]) playerSize++;

        cameraScale = growthRate * Mathf.Pow(playerData[2], 0.333333f); //부피의 3분의 1이 반지름
        snowScale = cameraScale * 12 / sizeCut[playerSize]; //12분의 pixel수

        transform.localScale = new Vector3(snowScale, snowScale, 1);  //스케일 적용

        if (playerData[1] <= 0) //게임오버
        {
            playerData[1] = 0;
            ui.fail();
        }
        if (playerData[0] <= playerData[1]) //체력 초과
        {
            playerData[1] = playerData[0];
        }
    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            for (int i = 0; i < 4; i++) playerData[i] += (groundData[i] - snowgroundData[i]) * Time.deltaTime;
        }
        //Snowground는 항상 밟고 있음
        if (other.gameObject.tag == "Snowground")
        {
            for(int i = 0; i < 4; i++) playerData[i] += snowgroundData[i] * Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //ID는 6글자, 구조는 다음과 같다:
        //앞 2자리: 오브젝트 타입(나무, 돌 등); 10부터 시작
        //중간 2자리: 그래픽 종류 (사이즈); 00부터 시작
        //뒤 2자리: 위치; 00부터 시작
        ID = other.gameObject.GetComponent<ObjectID>().ID;
        typeID = ID / 10000;
        imageID = ID % 10000 / 100;
        posID = ID % 100;

        if (typeID == 10) //Stone
        {
            for (int i = 0; i < 4; i++) playerData[i] += stoneData[imageID, i];
            objects.Add(ID);
            Destroy(other.gameObject);
        }
        if (typeID == 11) //Tree
        {
            for (int i = 0; i < 4; i++) playerData[i] += treeData[imageID, i];
            objects.Add(ID);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Goal")
        {
            ui.finish();
        }
    }
}
