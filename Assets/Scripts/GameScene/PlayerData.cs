using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public UI ui;

    private List<int> objects; //흡수한 오브젝트들 ID 리스트

    //플레이어와 모든 플레이어의 수치를 변경하는 오브젝트들은 다음과 같은 데이터를 가진다:
    //[0]==MaxHealth, [1]==Health, [2]==Volume [3]==Power
    public float[] playerData = { 1, 1, 1, 1 };

    public float[] groundData = { 0, -4, 0, 0 };
    public float[] snowgroundData = { 2, 3, 1, 1 };

    public float[] stone0Data = { 0, -2, 2, 2 };
    public float[] stone1Data = { 0, -2, 2, 2 };
    public float[] stone2Data = { 0, -2, 2, 2 };
    public float[] stone3Data = { 0, -2, 2, 2 };
    public float[] stone4Data = { 0, -2, 2, 2 };
    public float[] stone5Data = { 0, -2, 2, 2 };
    public float[] stone6Data = { 0, -2, 2, 2 };
    public float[] stone7Data = { 0, -2, 2, 2 };
    public float[] tree0Data = { 0, -2, 3, 3 };
    public float[] tree1Data = { 0, -2, 3, 3 };
    public float[] tree2Data = { 0, -2, 3, 3 };
    public float[] tree3Data = { 0, -2, 3, 3 };
    public float[] tree4Data = { 0, -2, 3, 3 };
    public float[] tree5Data = { 0, -2, 3, 3 };
    public float[] tree6Data = { 0, -2, 3, 3 };
    public float[] tree7Data = { 0, -2, 3, 3 };
    public float[] tree8Data = { 0, -2, 3, 3 };
    public float[] tree9Data = { 0, -2, 3, 3 };
    public float[] tree10Data = { 0, -2, 3, 3 };
    public float[] tree11Data = { 0, -2, 3, 3 };
    public float[] tree12Data = { 0, -2, 3, 3 };

    public float scaleRate = 0.33333333333f;
    public float[] sizeCut = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 9999 };
    [HideInInspector] public int playerSize = 0;
    [HideInInspector] public float snowScale; // 그래픽 변환 고려한 실제 Scale
    
    private int ID;
    private int typeID;
    private int imageID;
    private int posID;

    private int starCount = 0;
    void Start()
    {
        objects = new List<int>();
    }

    void Update()
    {
        if (playerData[2] >= sizeCut[playerSize]) playerSize++;

        snowScale = Mathf.Pow(playerData[2], scaleRate); //부피의 3분의 1이 반지름
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
        //뒤 2자리: 같은 이미지 습득 저장이 필요할 때; 00부터 시작
        ID = other.gameObject.GetComponent<ObjectID>().ID;
        typeID = ID / 10000;
        imageID = ID % 10000 / 100;
        posID = ID % 100;

        switch (typeID)
        {
            case 10: //stone
                for (int i = 0; i < 4; i++)
                {
                    switch (imageID)
                    {
                        case 0: playerData[i] += stone0Data[i]; break;
                        case 1: playerData[i] += stone1Data[i]; break;
                        case 2: playerData[i] += stone2Data[i]; break;
                        case 3: playerData[i] += stone3Data[i]; break;
                        case 4: playerData[i] += stone4Data[i]; break;
                        case 5: playerData[i] += stone5Data[i]; break;
                        case 6: playerData[i] += stone6Data[i]; break;
                        case 7: playerData[i] += stone7Data[i]; break;
                    }
                }
                if (playerData[1] > 0) Destroy(other.gameObject);
                break;

            case 11: //tree
                for (int i = 0; i < 4; i++)
                    switch (imageID)
                    {
                        case 0: playerData[i] += tree0Data[i]; break;
                        case 1: playerData[i] += tree1Data[i]; break;
                        case 2: playerData[i] += tree2Data[i]; break;
                        case 3: playerData[i] += tree3Data[i]; break;
                        case 4: playerData[i] += tree4Data[i]; break;
                        case 5: playerData[i] += tree5Data[i]; break;
                        case 6: playerData[i] += tree6Data[i]; break;
                        case 7: playerData[i] += tree7Data[i]; break;
                        case 8: playerData[i] = tree8Data[i]; break;
                        case 9: playerData[i] += tree9Data[i]; break;
                        case 10: playerData[i] += tree10Data[i]; break;
                        case 11: playerData[i] += tree11Data[i]; break;
                        case 12: playerData[i] += tree12Data[i]; break;
                    }
                if (playerData[1] > 0) Destroy(other.gameObject);
                break;

            case 20: //orb
                Destroy(other.gameObject);
                break;
            case 21: //star
                starCount++;
                objects.Add(ID);
                Destroy(other.gameObject);
                break;
            case 40: //goal
                ui.finish();
                break;

        }
    }
}
